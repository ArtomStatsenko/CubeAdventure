using UnityEngine;

public sealed class AnimatorController
{
    private Animator _animator;

    public AnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void PlayStartTransition()
    {
        _animator.SetTrigger("Start");
    }

    public void PlayEndTransition()
    {
        _animator.SetTrigger("End");
    }
}