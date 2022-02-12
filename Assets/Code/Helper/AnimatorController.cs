using UnityEngine;

public sealed class AnimatorController
{
    private const string START = "Start";
    private const string END = "End";
    private Animator _animator;

    public AnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void PlayStartTransition()
    {
        _animator.SetTrigger(START);
    }

    public void PlayEndTransition()
    {
        _animator.SetTrigger(END);
    }
}