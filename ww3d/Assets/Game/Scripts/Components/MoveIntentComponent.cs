using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct MoveIntentComponent : IComponent {

    public NNInfo Node;
    public MoveMode MoveMode;

}