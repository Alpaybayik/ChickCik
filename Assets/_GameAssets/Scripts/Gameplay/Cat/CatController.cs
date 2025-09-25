using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCaught;
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;

    [Header("Navigation Settings")]
    [SerializeField] private float _waitTime = 3f;
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private int _maxDestinationAttempts = 10;
    [SerializeField] private float _chaseDistanceThreshold = 2f;
    [SerializeField] private float _chaseDistance = 1.5f;

    private NavMeshAgent _catAgent;
    private CatStateController _catStateController;
    private Vector3 _initialPosition;
    private bool _isWaiting;
    private bool _isChasing;
    private float _timer;


    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
    }

    private void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume && GameManager.Instance.GetCurrentGameState() != GameState.CutScene)
        {
            _catAgent.speed = 0f;
            return;
        }
        if (_playerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
    }

    private void SetChaseMovement()
    {
        _isChasing = true;
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = _playerTransform.position + directionToPlayer * _chaseDistanceThreshold;

        _catAgent.SetDestination(offsetPosition);
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Running);

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance && _isChasing)
        {
            // CATCHED THE CIK
            OnCatCaught?.Invoke();
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;
        }
    }

    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;
        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }

        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }

    }

    private void SetRandomDestination()
    {
        int attemps = 0;
        bool destionSet = false;


        while (attemps < _maxDestinationAttempts && !destionSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += _initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;

                if (!IsPositionBLocked(finalPosition))
                {
                    _catAgent.SetDestination(finalPosition);
                    destionSet = true;
                }
                else
                {
                    attemps++;
                }
            }
            else
            {
                attemps++;
            }
        }

        if (!destionSet)
        {
            Debug.LogWarning("Couldn't find a valid destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }


    private bool IsPositionBLocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }
}
