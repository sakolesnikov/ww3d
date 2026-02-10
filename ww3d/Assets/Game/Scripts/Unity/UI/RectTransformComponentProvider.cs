using Friflo.Engine.ECS;
using UnityEngine;

public class RectTransformComponentProvider : MonoBehaviour, IEntityAware {

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new RectTransformComponent { Value = gameObject.GetComponent<RectTransform>() });
    }

}