using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DefaultDragDropHandler<T> : MonoBehaviour, IDropHandler where T : struct {

    protected abstract T GetSignal();

    public void OnDrop(PointerEventData eventData) {
        var go = eventData.pointerDrag;
        var entityMono = go.GetComponent<AbstractEntityMono>();
        if (entityMono) {
            var lootEntity = entityMono.GetEntity();
            lootEntity.EmitSignal(GetSignal());
        }
    }

    protected void RemoveLinks<T>(ref Entity entity) where T : struct, ILinkRelation {
        var relations = entity.GetIncomingLinks<T>();
        foreach (var relation in relations) {
            relation.Entity.RemoveRelation<T>(entity);
        }
    }

}