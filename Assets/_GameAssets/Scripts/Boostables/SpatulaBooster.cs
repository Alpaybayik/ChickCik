using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float _jumpForce;

    private bool _isActive;

    public void Boost(PlayerController playerController)
    {
        if(_isActive) return;
        PlayBoostAnimation();
        Rigidbody playerRigidbody = playerController.GetRigidbody();

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActive = true;
        Invoke(nameof(ResetBoostAnimation), 0.2f);
    }

    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
    }

    private void ResetBoostAnimation()
    {
        _isActive = false;
    }
}
