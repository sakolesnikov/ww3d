using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class FlashlightChargedCraftedImpl : SignalBus<InventoryItemCraftedSignal<FlashlightChargedDef>> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<InventoryItemCraftedSignal<FlashlightChargedDef>> signal) {
        if (world.GetPlayer() is { IsNull: false } player) {
            var lights = player.GetComponent<FlashlightComponent>().Values;
            foreach (var light in lights) {
                light.gameObject.SetActive(true);
            }
        }
    }

}