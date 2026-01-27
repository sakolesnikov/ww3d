using Friflo.Engine.ECS;
using UnityEngine;

public struct AnimatorComponent : IComponent {

    public Animator Value;

    public void CrossFade(string animationName) {
        Value.CrossFade(animationName, 0.1f);
    }

    public void CrossFade(string animationName, float duration) {
        Value.CrossFade(animationName, duration);
    }

}