using Friflo.Engine.ECS;
using UnityEngine;

public class DoorDebug : MonoBehaviour, IEntityAware {

    [SerializeField]
    private bool open;
    private Entity entity;

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

    private void OnValidate() {
        if (entity.IsNull) {
            return;
        }

        if (open) {
            entity.AddComponent(new OpenDoorRequest());
        } else {
            entity.AddComponent(new CloseDoorRequest());
        }
    }

}