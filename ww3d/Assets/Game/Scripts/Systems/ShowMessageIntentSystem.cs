using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class ShowMessageIntentSystem : QueryUpdateSystem<ShowMessageIntent> {

    [Inject]
    private readonly EntityStore world;

    protected override void OnUpdate() {
        var deltaTime = Time.deltaTime;
        Query.ForEachEntity((ref ShowMessageIntent messageReq, Entity entity) => {
            messageReq.Time += deltaTime;

            if (messageReq.Time >= 0.25f) {
                CommandBuffer.RemoveComponent<ShowMessageIntent>(entity.Id);
                CommandBuffer.AddComponent(entity.Id, new ShowMessageRequest());
            }
        });
    }

}