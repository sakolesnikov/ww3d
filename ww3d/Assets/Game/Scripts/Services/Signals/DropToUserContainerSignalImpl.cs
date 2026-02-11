using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToUserContainerSignalImpl : DefaultDropSignal<DropToUserContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Drop(ref Entity lootEntity) {
        if (world.GetPlayer() is { IsNull: false } player) {
            player.AddRelation(new InventoryRelation { Entity = lootEntity });
        }
    }

}