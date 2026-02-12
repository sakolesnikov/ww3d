using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class FlashlightChargedRemovedImpl : SignalBus<InventoryItemRemovedSignal<FlashlightChargedDef>> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<InventoryItemRemovedSignal<FlashlightChargedDef>> signal) {
        if (world.GetPlayer() is { IsNull: false } player) {
            var lights = player.GetComponent<FlashlightComponent>().Values;
            foreach (var light in lights) {
                light.gameObject.SetActive(false);
            }
        }
    }

}