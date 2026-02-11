using Friflo.Engine.ECS;

public class CraftContainer : DefaultDragDropHandler<DropToCraftContainerSignal>, IItemBeginDragHandler, IItemContainer {

    protected override DropToCraftContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<CraftRelation>(ref entity);
    }

    public int MaxAllowed => 1;
    public int CurrentAmount => transform.childCount;

}