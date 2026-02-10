using System;
using Friflo.Engine.ECS;

public class PrefabItemInit : IEntityInitialization {

    public Type getType() => typeof(PrefabItemDef);

    public void Initialize(Entity entity) {
        // var go = entity.GetGameObject();
        // entity.AddComponent(new ImageComponent { Value = go.GetComponent<Image>() });
        // entity.AddComponent(new RectTransformComponent { Value = go.GetComponent<RectTransform>() });
        // entity.AddComponent(new ParentTransformComponent());
    }

}