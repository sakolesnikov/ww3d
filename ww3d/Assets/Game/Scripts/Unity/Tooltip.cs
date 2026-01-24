using Friflo.Engine.ECS;
using UnityEngine;

public class Tooltip : MonoBehaviour, IEntityAware {

    [SerializeField]
    private string tooltipKey;

    public void OnEntityReady(ref Entity entity) {
        if (tooltipKey.Length == 0) {
            return;
        }

        entity.AddComponent(new TooltipComponent { Key = tooltipKey });
    }

}