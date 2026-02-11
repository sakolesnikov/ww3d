using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToContainerSignalImpl : DefaultDropSignal<DropToContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Drop(ref Entity lootEntity) {
        if (world.GetExchange() is { IsNull: false } exchange) {
            exchange.GetComponent<OpenedComponent>().Value.AddRelation(new ContainsRelation { Entity = lootEntity });
            exchange.AddRelation(new ShowsRelation { Entity = lootEntity });
        }
    }

}