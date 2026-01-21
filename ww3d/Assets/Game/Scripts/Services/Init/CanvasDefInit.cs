using System;
using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
public class CanvasDefInit : IEntityInitialization {

    public Type getType() => typeof(CanvasDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        entity.AddComponent(new CanvasComponent { Value = go.GetComponent<Canvas>() });
    }

}