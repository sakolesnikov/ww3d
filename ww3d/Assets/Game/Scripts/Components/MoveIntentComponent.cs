using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct MoveIntentComponent : IComponent {

    public NNInfo Node;
    public Vector3 Target;
    public MoveMode MoveMode;

}