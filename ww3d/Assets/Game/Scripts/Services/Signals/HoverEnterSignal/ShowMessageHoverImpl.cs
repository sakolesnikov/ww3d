using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class ShowMessageHoverImpl : GenericSignal<HoverEnterSignal> {

    [Inject]
    private readonly CursorService cursorService;
    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<HoverEnterSignal> signal) {
        var entity = signal.Entity;
        if (entity.HasComponent<TooltipComponent>()) {
            entity.AddComponent(new ShowMessageIntent());
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is IInteractable;

}