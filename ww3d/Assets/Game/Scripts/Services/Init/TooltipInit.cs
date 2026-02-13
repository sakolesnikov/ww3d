using System;
using Friflo.Engine.ECS;
using UnityEngine.Localization.Components;

[LevelScope]
public class TooltipInit : IEntityInitialization {

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        entity.AddComponent(new LocalizeStringComponent { Value = go.GetComponentInChildren<LocalizeStringEvent>(true) });
    }

    public Type getType() => typeof(TooltipDef);

}