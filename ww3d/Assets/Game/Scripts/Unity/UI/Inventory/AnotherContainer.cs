using Friflo.Engine.ECS;

public class AnotherContainer : DefaultDragDropHandler<DropToContainerSignal>, IItemBeginDragHandler, IItemContainer {

    protected override DropToContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<ContainsRelation>(ref entity);
        RemoveLinks<ShowsRelation>(ref entity);
    }

    public int MaxAllowed => int.MaxValue;
    public int CurrentAmount => transform.childCount;

}