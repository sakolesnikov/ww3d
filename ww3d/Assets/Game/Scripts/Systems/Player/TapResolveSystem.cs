using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class TapResolveSystem : EntityListSystem<TapIntentComponent> {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> pool;
    [Inject]
    private readonly CommandProvider commandProvider;
    [Inject]
    private readonly TapProviderResolver tapProviderResolver;

    protected override void ProcessEntity(ref TapIntentComponent tap, Entity player) {
        EntityDefinition def = null;
        if (!tap.Entity.IsNull) {
            def = tap.Entity.GetComponent<DefinitionComponent>().Value;
        }

        var ctx = new TapContext { Actor = player, TargetPosition = tap.Target, TargetEntity = tap.Entity, EntityDef = def };

        var provider = tapProviderResolver.Resolve(ctx);
        if (provider != null) {
            var q = pool.Get();
            provider.Build(ctx, q);
            player.EmitSignal(new NewCommandPlanSignal { Value = q });
        }

        player.RemoveComponent<TapIntentComponent>();


        // if (tapIntentComp.Entity.IsNull) {
        //     player.EmitSignal(new MoveToSignal { Target = tapIntentComp.Target });
        // } else {
        //     var def = tapIntentComp.Entity
        //         .GetComponent<DefinitionComponent>()
        //         .Value;
        //     player.EmitSignal(new NewCommandPlanSignal { Value = commandProvider.GetCommands(def) });
        // }
        //
        // player.RemoveComponent<TapIntentComponent>();
    }

    // protected override void OnUpdate() {
    //     Query.Entities.ToEntityList(entityList);
    //     foreach (var player in entityList) {
    //         ref var tapIntentComp = ref player.GetComponent<TapIntentComponent>();
    //         if (tapIntentComp.Entity.IsNull) {
    //             player.EmitSignal(new MoveToSignal { Target = tapIntentComp.Target });
    //         } else {
    //             var def = tapIntentComp.Entity
    //                 .GetComponent<DefinitionComponent>()
    //                 .Value;
    //             player.EmitSignal(new NewCommandPlanSignal { Value = commandProvider.GetCommands(def) });
    //         }
    //
    //         player.RemoveComponent<TapIntentComponent>();
    //     }
    // }
/*
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
*/

}