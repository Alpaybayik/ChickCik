using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerVisionTransform;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 10f;

    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume) return;
        Vector3 viewDirection =
        _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);

        _orientationTransform.forward = viewDirection.normalized;

        float horizantalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizantalInput;
        
        if (inputDirection != Vector3.zero)
        {
            _playerVisionTransform.forward = Vector3.Slerp(_playerVisionTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
    }
}
