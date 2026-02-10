using Friflo.Engine.ECS;

public class UserContainer : DefaultDragDropHandler<DropToUserContainerSignal> {

    protected override DropToUserContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<InventoryRelation>(ref entity);
    }

}