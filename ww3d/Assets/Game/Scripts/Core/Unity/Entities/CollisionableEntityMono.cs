using UnityEngine;

public class CollisionableEntityMono : AbstractEntityMono {

    private void OnCollisionEnter2D(Collision2D other) {
        if (Entity.IsNull) {
            return;
        }

        var otherEntity = EntityProvider.GetEntity(other.gameObject.GetInstanceID());
        if (otherEntity.IsNull) {
            return;
        }

        Entity.EmitSignal(new CollisionEnterSignal { ContactedWith = otherEntity, Info = other });
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (Entity.IsNull) {
            return;
        }

        var otherEntity = EntityProvider.GetEntity(other.gameObject.GetInstanceID());
        if (otherEntity.IsNull) {
            return;
        }

        Entity.EmitSignal(new CollisionExitSignal { ContactedFrom = otherEntity, Info = other });
    }

}