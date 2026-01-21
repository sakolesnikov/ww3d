using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class BeginDragSignalImpl : GenericSignal<BeginDragSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<BeginDragSignal> signal) {
        var canvas = world.GetCanvas();
        var transform = signal.Entity.GetTransform();
        ref var imageComp = ref signal.Entity.GetComponent<ImageComponent>();
        imageComp.Value.raycastTarget = false;
        transform.SetParent(canvas.transform);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}