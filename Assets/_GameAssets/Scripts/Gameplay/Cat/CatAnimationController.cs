using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;
    private CatStateController _catStateController;

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();
    }

    private void Update()
    {
        SetCatAnimations();
    }
    
    private void SetCatAnimations()
    {
        var currentState = _catStateController.GetCurrentState();
        switch (currentState)
        {
            case CatState.Idle:
                _catAnimator.SetBool(Consts.CatsAnimations.IS_IDLEING, true);
                _catAnimator.SetBool(Consts.CatsAnimations.IS_WALKING, false);
                _catAnimator.SetBool(Consts.CatsAnimations.IS_RUNNING, false);
                break;
            case CatState.Walking:
                _catAnimator.SetBool(Consts.CatsAnimations.IS_IDLEING, false);
                _catAnimator.SetBool(Consts.CatsAnimations.IS_WALKING, true);
                _catAnimator.SetBool(Consts.CatsAnimations.IS_RUNNING, false);
                break;
            case CatState.Running:
                _catAnimator.SetBool(Consts.CatsAnimations.IS_RUNNING, true);
                break;
            case CatState.Attacking:
                _catAnimator.SetBool(Consts.CatsAnimations.IS_ATTACKING, true);
                break;
        }
    }
}
