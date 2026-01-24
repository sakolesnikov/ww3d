using System;
using Friflo.Engine.ECS;
using UnityEngine.Localization.Components;

[LevelScope]
public class PrefabMessageInit : IEntityInitialization {

    public Type getType() => typeof(MessageDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        entity.AddComponent(new LocalizeStringComponent { Value = go.GetComponent<LocalizeStringEvent>() });
    }

}