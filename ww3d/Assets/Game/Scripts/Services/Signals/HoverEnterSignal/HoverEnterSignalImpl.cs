using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverEnterSignalImpl : GenericSignal<HoverEnterSignal> {

    [Inject]
    private readonly CursorService cursorService;
    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<HoverEnterSignal> signal) {
        var entity = signal.Entity;
        var entityInteractable = entity.GetComponent<DefinitionComponent>().GetValue<IInteractable>();
        if (world.GetCursor() is { IsNull: false } c) {
            c.AddComponent(new AnimationComponent { Frames = entityInteractable.Cursor });
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is IInteractable;

}