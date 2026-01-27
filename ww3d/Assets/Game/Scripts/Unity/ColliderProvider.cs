using Friflo.Engine.ECS;
using UnityEngine;

public class ColliderProvider : MonoBehaviour, IEntityAware {

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new ColliderComponent { Value = GetComponent<Collider>() });
    }

}