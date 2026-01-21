using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DragSignalImpl : GenericSignal<DragSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DragSignal> signal) {
        ref var rectTransformComp = ref signal.Entity.GetComponent<RectTransformComponent>();
        rectTransformComp.Value.anchoredPosition += signal.Event.Delta / world.GetCanvas().scaleFactor;
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}