using Friflo.Engine.ECS;

public class CraftContainer : DefaultDragDropHandler<DropToCraftContainerSignal>, IItemBeginDragHandler, IItemContainer {

    protected override DropToCraftContainerSignal GetSignal() => new() { Transform = transform };

    protected override void Drop(ref Entity lootEntity) {
        base.Drop(ref lootEntity);
        if (lootEntity.TryGetComponent<LifecycleSignalsComponent>(out var lifecycleSignals)) {
            lifecycleSignals.InventoryAddedSignal(world);
        }
    }

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<CraftRelation>(ref entity);
        if (entity.TryGetComponent<LifecycleSignalsComponent>(out var lifecycleSignals)) {
            lifecycleSignals.InventoryRemovedSignal(world);
        }
    }

    public int MaxAllowed => 1;
    public int CurrentAmount => transform.childCount;

}