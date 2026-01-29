using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverExitSignalImpl : GenericSignal<HoverExitSignal> {

    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly CursorService cursorService;

    protected override void Signal(Signal<HoverExitSignal> signal) {
        if (world.GetCursor() is { IsNull: false } c) {
            c.RemoveComponent<AnimationComponent>();
            cursorService.Default();
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is IInteractable;

}