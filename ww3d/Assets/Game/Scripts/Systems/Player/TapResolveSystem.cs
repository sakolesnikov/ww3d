using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class TapResolveSystem : QueryUpdateSystem<TapIntentComponent> {

    private Entity cursor;
    [Inject]
    private readonly CommandProvider commandProvider;
    private readonly EntityList entityList = new();

    protected override void OnAddStore(EntityStore store) {
        cursor = store.GetCursor();
    }

    protected override void OnUpdate() {
        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            var queue = ResolveCommandQueue(entity);
            entity.EmitSignal(new NewCommandPlanSignal { Value = queue });
            entity.RemoveComponent<TapIntentComponent>();
        }
    }

    private PooledCommandQueue ResolveCommandQueue(Entity player) {
        if (cursor.TryGetComponent<HoverComponent>(out var hover)) {
            var def = hover.Entity
                .GetComponent<DefinitionComponent>()
                .Value;
            player.AddComponent(new TappedEntityComponent { Value = hover.Entity });
            return commandProvider.GetCommands(def);
        }

        if (player.HasComponent<TappedEntityComponent>()) {
            player.RemoveComponent<TappedEntityComponent>();
        }

        return commandProvider.GetCommands();
    }

}