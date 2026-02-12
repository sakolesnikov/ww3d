using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public abstract class DefaultDragDropHandler<T> : MonoBehaviour, IDropHandler where T : struct {

    [Inject]
    protected readonly EntityStore world;

    protected abstract T GetSignal();

    protected virtual void Drop(ref Entity lootEntity) { }

    public void OnDrop(PointerEventData eventData) {
        var go = eventData.pointerDrag;
        var entityMono = go.GetComponent<AbstractEntityMono>();
        if (entityMono) {
            var lootEntity = entityMono.GetEntity();
            lootEntity.EmitSignal(GetSignal());
            Drop(ref lootEntity);
        }
    }

    protected void RemoveLinks<TRelation>(ref Entity entity) where TRelation : struct, ILinkRelation {
        var relations = entity.GetIncomingLinks<TRelation>();
        foreach (var relation in relations) {
            relation.Entity.RemoveRelation<TRelation>(entity);
        }
    }

}