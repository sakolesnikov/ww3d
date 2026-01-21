using UnityEngine;

public class TriggerableEntityMono : AbstractEntityMono {

    private void OnTriggerEnter2D(Collider2D other) {
        if (Entity.IsNull) {
            return;
        }

        var otherEntity = EntityProvider.GetEntity(other.gameObject.GetInstanceID());
        if (otherEntity.IsNull) {
            return;
        }

        Entity.EmitSignal(new TriggerEnterSignal { TriggeredWith = otherEntity });
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (Entity.IsNull) {
            return;
        }

        var otherEntity = EntityProvider.GetEntity(other.gameObject.GetInstanceID());
        if (otherEntity.IsNull) {
            return;
        }

        Entity.EmitSignal(new TriggerExitSignal { TriggeredWith = otherEntity });
    }

}