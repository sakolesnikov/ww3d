using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class ShowTooltipSignalImpl : GenericSignal<ShowTooltipSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<ShowTooltipSignal> signal) {
        if (world.GetTooltip() is { IsNull: false } tooltipEntity) {
            tooltipEntity.GetTransform().GetChild(0).gameObject.SetActive(true);
            tooltipEntity.GetComponent<LocalizeStringComponent>().Value.SetEntry(signal.Event.Key);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(TooltipDef);

}