using Friflo.Engine.ECS;

public class AnotherContainer : DefaultDragDropHandler<DropToContainerSignal>, IItemBeginDragHandler {

    protected override DropToContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<ContainsRelation>(ref entity);
        RemoveLinks<ShowsRelation>(ref entity);
    }

}