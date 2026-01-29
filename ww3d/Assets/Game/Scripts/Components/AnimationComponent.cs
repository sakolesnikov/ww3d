using Friflo.Engine.ECS;
using UnityEngine;

public struct AnimationComponent : IComponent {

    public Texture2D[] Frames;
    public int CurrentFrame;
    public float Timer;

}