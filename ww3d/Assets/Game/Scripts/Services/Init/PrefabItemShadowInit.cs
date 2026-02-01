using System;
using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.UI;

[LevelScope]
public class PrefabItemShadowInit : IEntityInitialization {

    public Type getType() => typeof(PrefabItemShadowDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        entity.AddComponent(new ImageComponent { Value = go.GetComponent<Image>() });
        entity.AddComponent(new RectTransformComponent { Value = go.GetComponent<RectTransform>() });
        entity.AddComponent(new ParentTransformComponent());
    }

}