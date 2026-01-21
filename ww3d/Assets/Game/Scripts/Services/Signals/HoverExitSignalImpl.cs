using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverExitSignalImpl : GenericSignal<HoverExitSignal> {

    [Inject]
    private readonly CursorService cursorService;

    protected override void Signal(Signal<HoverExitSignal> signal) {
        signal.Entity.RemoveComponent<HoverComponent>();
        cursorService.Default();
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(CursorDef);

}