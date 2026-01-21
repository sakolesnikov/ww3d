using Friflo.Engine.ECS;

[LevelScope]
public class DragSystem : QueryUpdateSystem<DragComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref DragComponent dragComp, Entity entity) => {
            ref var rectComp = ref entity.GetComponent<RectTransformComponent>();
            rectComp.Value.anchoredPosition += dragComp.Delta;
        });
    }

}