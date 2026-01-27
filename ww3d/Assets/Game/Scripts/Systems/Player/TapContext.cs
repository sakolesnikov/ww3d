using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct TapContext {

    public Entity Actor;
    public NNInfo Node;
    public Vector3 TargetPosition;
    public Entity TargetEntity;
    public EntityDefinition EntityDef;
    public MoveMode MoveMode;
    public bool HasTarget => !TargetEntity.IsNull;

}