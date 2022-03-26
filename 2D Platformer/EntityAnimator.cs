using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(IMover))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;
    private IMover _mover;

    private int _crouch = Animator.StringToHash("IsCrouching");
    private int _jump = Animator.StringToHash("IsJumping");
    private int _run = Animator.StringToHash("IsRunning");

    private bool IsJumping
    {
        get => _animator.GetBool(_jump);
        set => _animator.SetBool(_jump, value);
    }

    private bool IsCrouching
    {
        get => _animator.GetBool(_crouch);
        set => _animator.SetBool(_crouch, value);
    }

    private bool IsRunning
    {
        get => _animator.GetBool(_run);
        set => _animator.SetBool(_run, value);
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<IMover>();
        _mover.Running += (state) => { IsRunning = state; };
        _mover.Jumping += (state) => { IsJumping = state; };
        _mover.Crouching += (state) => { IsCrouching = state; };
    }

    private void OnDisable()
    {
        _mover.Running -= (state) => { IsRunning = state; };
        _mover.Jumping -= (state) => { IsJumping = state; };
        _mover.Crouching -= (state) => { IsCrouching = state; };
    }
}
