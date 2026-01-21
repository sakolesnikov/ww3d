using System;
using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
public class CameraDefInit : IEntityInitialization {

    public Type getType() => typeof(CameraDef);

    public void Initialize(Entity entity) {
        entity.AddComponent(new CameraComponent { Value = entity.GetGameObject().GetComponent<Camera>() });
    }

}