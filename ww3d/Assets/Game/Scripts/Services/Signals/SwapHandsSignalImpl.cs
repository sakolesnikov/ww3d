using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class SwapHandsSignalImpl : GenericSignal<SwapHandsSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<SwapHandsSignal> signal) {
        var player = world.GetPlayer();
        if (player.ChildCount >= 2) {
            ref var activeItemIndexComp = ref player.GetComponent<ActiveItemComponent>();
            activeItemIndexComp.Index++;
            if (activeItemIndexComp.Index >= player.ChildCount) {
                activeItemIndexComp.Index = 0;
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(ToolPanelDef);

}