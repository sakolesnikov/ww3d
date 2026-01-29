using Friflo.Engine.ECS;

public class HoverEnterSystem : QueryUpdateSystem<HoverEnterComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref HoverEnterComponent comp1, Entity entity) => {
            CommandBuffer.RemoveComponent<HoverEnterComponent>(entity.Id);

            // entity.GetComponent<DefinitionComponent>().Value
        });
    }

}