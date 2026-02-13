using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class ShowTooltipIntentSystem : QueryUpdateSystem<ShowTooltipIntent> {

    [Inject]
    private readonly EntityStore world;

    protected override void OnUpdate() {
        var deltaTime = Time.deltaTime;
        Query.ForEachEntity((ref ShowTooltipIntent tooltipIntent, Entity entity) => {
            tooltipIntent.Time += deltaTime;

            if (tooltipIntent.Time >= 0.3f) {
                CommandBuffer.RemoveComponent<ShowTooltipIntent>(entity.Id);
                world.GetTooltip().EmitSignal(new ShowTooltipSignal { Key = entity.GetComponent<TooltipComponent>().Key });
            }
        });
    }

}