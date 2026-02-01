using UnityEngine;
using UnityEngine.EventSystems;

public class UserContainer : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        var go = eventData.pointerDrag;
        var entityMono = go.GetComponent<AbstractEntityMono>();
        if (entityMono) {
            var entity = entityMono.GetEntity();
            entity.EmitSignal(new DropToUserSignal { Transform = transform });
        }
    }

}