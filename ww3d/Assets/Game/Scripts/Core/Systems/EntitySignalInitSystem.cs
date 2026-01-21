using Friflo.Engine.ECS;
using VContainer;

[MenuScope]
[LevelScope]
[Order(101)]
public class EntitySignalInitSystem : IInitSystem, IDisposeSystem {

    [Inject]
    private readonly SignalRegistrationService signalRegistrationService;

    public void Init(EntityStore world) {
        var buffer = world.GetCommandBuffer();
        world.Query<DefinitionComponent>().ForEachEntity((ref DefinitionComponent defComp, Entity entity) => {
            signalRegistrationService.Register(entity, defComp.Value);
        });
        buffer.Playback();
    }

    public void Dispose(EntityStore world) {
        world.Query<DefinitionComponent>().WithDisabled().ForEachEntity((ref DefinitionComponent defComp, Entity entity) => {
            signalRegistrationService.Unregister(entity, defComp.Value);
        });
    }

}