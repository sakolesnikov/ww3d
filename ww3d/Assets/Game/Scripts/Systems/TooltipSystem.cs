using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

// [LevelScope]
public class TooltipSystem : QueryUpdateSystem<ShowMessageIntent> {

    [Inject]
    [Key(typeof(MessageDef))]
    private readonly ObjectPool<AbstractEntityMono> messagePool;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref ShowMessageIntent messageComp, Entity entity) => {
            Debug.Log("Show Tooltip");
            // messagePool
            CommandBuffer.RemoveComponent<ShowMessageIntent>(entity.Id);
        });
    }

    /*
    private EntityStore world;
    private ArchetypeQuery query;

    protected override void OnAddStore(EntityStore store) {
        world = store;
        world.EventRecorder.Enabled = true;
        query = store.Query();
        query.EventFilter.ComponentAdded<ShowTooltipRequest>();
    }

    protected override void OnUpdate() {
        // query.EventFilter.ComponentAdded<ShowTooltipRequest>();
        foreach (var entity in query.Entities) {
            if (query.HasEvent(entity.Id)) {
                Debug.Log($"{entity}");
            }
        }

        world.EventRecorder.ClearEvents();
    }
*/

}