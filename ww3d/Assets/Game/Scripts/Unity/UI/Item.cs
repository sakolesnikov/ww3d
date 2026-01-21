using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEntityAware {

    private Entity entity;

    public void OnBeginDrag(PointerEventData eventData) {
        entity.EmitSignal(new BeginDragSignal());
    }

    public void OnDrag(PointerEventData eventData) {
        entity.EmitSignal(new DragSignal { Delta = eventData.delta });
    }

    public void OnEndDrag(PointerEventData eventData) {
        entity.EmitSignal(new EndDragSignal());
    }

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

}