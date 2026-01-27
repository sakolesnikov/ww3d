using Friflo.Engine.ECS;
using UnityEngine;

public struct TapIntentComponent : IComponent {

    public Vector3 Position;
    public Entity Entity;
    public MoveMode MoveMode;

}