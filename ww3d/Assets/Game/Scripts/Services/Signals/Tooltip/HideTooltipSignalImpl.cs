using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class HideTooltipSignalImpl : GenericSignal<HideTooltipSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<HideTooltipSignal> signal) {
        if (world.GetTooltip() is { IsNull: false } tooltipEntity) {
            tooltipEntity.GetTransform().GetChild(0).gameObject.SetActive(false);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(TooltipDef);

}