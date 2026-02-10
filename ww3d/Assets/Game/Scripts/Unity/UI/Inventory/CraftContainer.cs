using Friflo.Engine.ECS;

public class CraftContainer : DefaultDragDropHandler<DropToCraftContainerSignal>, IItemBeginDragHandler {

    protected override DropToCraftContainerSignal GetSignal() => new() { Transform = transform };

    public void OnItemBeginDrag(ref Entity entity) {
        RemoveLinks<CraftRelation>(ref entity);
    }

}