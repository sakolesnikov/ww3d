using Friflo.Engine.ECS;
using UnityEngine;

public struct TapContext {

    public Entity Actor;
    public Vector3 TargetPosition;
    public Entity TargetEntity;
    public EntityDefinition EntityDef;
    public MoveMode MoveMode;
    public bool HasTarget => !TargetEntity.IsNull;

}