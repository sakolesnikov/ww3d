using Friflo.Engine.ECS;
using VContainer;

[MenuScope]
[LevelScope]
[Order(50)]
public class EntityInitSystem : IInitSystem {

    [Inject]
    private readonly EntityInitService entityInitService;
    [Inject]
    private readonly EntityStore world;

    public void Init(EntityStore world) {
        var entities = world.Query<DefinitionComponent>().ToEntityList();
        var ids = entities.SortByComponentField<OrderComponent, int>("Value", SortOrder.Ascending);
        foreach (var entity in entities) {
            entityInitService.Init(entity);
        }
    }

}