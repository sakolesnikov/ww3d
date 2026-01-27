using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HoverEnterSignalImpl : GenericSignal<HoverEnterSignal> {

    [Inject]
    private readonly CursorService cursorService;
    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<HoverEnterSignal> signal) {
        signal.Entity.AddComponent(new HoverComponent { Entity = signal.Event.Value });
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(CursorDef);

}