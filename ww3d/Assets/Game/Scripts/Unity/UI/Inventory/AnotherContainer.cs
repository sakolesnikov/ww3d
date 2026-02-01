using UnityEngine;
using UnityEngine.EventSystems;

public class AnotherContainer : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        var go = eventData.pointerDrag;
        var entityMono = go.GetComponent<AbstractEntityMono>();
        if (entityMono) {
            var lootEntity = entityMono.GetEntity();
            lootEntity.EmitSignal(new DropToContainerSignal { Transform = transform });
        }
    }

}