using Friflo.Engine.ECS;
using UnityEngine;

public class ConditionProvider : MonoBehaviour, IEntityAware {

    [SerializeField]
    private Condition[] conditions;

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new ConditionComponent { Values = conditions });
    }

}