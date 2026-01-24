using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverExitSignalImpl : GenericSignal<HoverExitSignal> {

    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly CursorService cursorService;

    protected override void Signal(Signal<HoverExitSignal> signal) {
        signal.Entity.RemoveComponent<HoverComponent>();
        var entity = world.GetEntityById(signal.Event.EntityId);
        entity.RemoveComponent<ShowMessageIntent>();
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(CursorDef);

}