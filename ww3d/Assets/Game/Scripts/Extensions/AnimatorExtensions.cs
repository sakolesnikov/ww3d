using UnityEngine;

public static class AnimatorExtensions {

    public static bool IsPlaying(this Animator animator, string animation) {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var exactAnimation = stateInfo.IsName(animation);
        if (exactAnimation) {
            return stateInfo.normalizedTime < 1.0f;
        }

        return false;
    }

}