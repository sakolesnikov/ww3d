using Friflo.Engine.ECS;

public class UserContainer : DefaultDragDropHandler<DropToUserContainerSignal>, IItemBeginDragHandler, IItemContainer {

    protected override DropToUserContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<InventoryRelation>(ref entity);
    }

    public int MaxAllowed => 1;
    public int CurrentAmount => transform.childCount;

}