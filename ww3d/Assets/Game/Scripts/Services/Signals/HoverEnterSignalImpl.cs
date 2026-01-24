using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverEnterSignalImpl : GenericSignal<HoverEnterSignal> {

    [Inject]
    private readonly CursorService cursorService;
    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<HoverEnterSignal> signal) {
        var entity = signal.Event.Value;
        signal.Entity.AddComponent(new HoverComponent { Entity = entity });
        if (entity.HasComponent<TooltipComponent>()) {
            entity.Add(new ShowMessageIntent());
        }
        // cursorService.LookUp();
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(CursorDef);

}