using Friflo.Engine.ECS;

public class UserContainer : DefaultDragDropHandler<DropToUserContainerSignal>, IItemBeginDragHandler, IItemContainer {

    protected override DropToUserContainerSignal GetSignal() => new() { Transform = transform };

    protected override void Drop(ref Entity lootEntity) {
        if (lootEntity.TryGetComponent<LifecycleSignalsComponent>(out var lifecycleSignals)) {
            lifecycleSignals.InventoryAddedSignal(world);
        }
    }

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<InventoryRelation>(ref entity);
        if (entity.TryGetComponent<LifecycleSignalsComponent>(out var lifecycleSignals)) {
            lifecycleSignals.InventoryRemovedSignal(world);
        }
    }

    public int MaxAllowed => 1;
    public int CurrentAmount => transform.childCount;

}