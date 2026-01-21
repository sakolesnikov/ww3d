using Friflo.Engine.ECS;
using UnityEngine;

public class Order : MonoBehaviour, IEntityAware {

    [SerializeField]
    private int order;

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new OrderComponent { Value = order });
    }

}