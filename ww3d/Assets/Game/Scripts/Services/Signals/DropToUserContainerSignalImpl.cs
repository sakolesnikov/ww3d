using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToUserContainerSignalImpl : GenericSignal<DropToUserContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropToUserContainerSignal> signal) {
        var player = world.GetPlayer();
        var containerTransform = signal.Event.Transform;
        var lootEntity = signal.Entity;
        lootEntity.GetComponent<ParentTransformComponent>().Value = containerTransform;

        player.RemoveRelation<CraftRelation>(lootEntity);

        if (!player.TryGetRelation<ContainsRelation, Entity>(lootEntity, out var rel)) {
            var entitiesRelationToContainer = lootEntity.GetIncomingLinks<ContainsRelation>();
            foreach (var entityRelation in entitiesRelationToContainer) {
                entityRelation.Entity.RemoveRelation<ContainsRelation>(lootEntity);
            }

            player.AddRelation(new ContainsRelation { Entity = lootEntity });
        }

        if (player.HasComponent<LeftHandComponent>()) {
            ref var leftHandComp = ref player.GetComponent<LeftHandComponent>();
            if (leftHandComp.Entity.Id == lootEntity.Id) {
                player.RemoveComponent<LeftHandComponent>();
                player.RemoveChild(leftHandComp.Entity);
            }
        }

        if (player.HasComponent<RightHandComponent>()) {
            ref var rightHandComp = ref player.GetComponent<RightHandComponent>();
            if (rightHandComp.Entity.Id == lootEntity.Id) {
                player.RemoveComponent<RightHandComponent>();
                player.RemoveChild(rightHandComp.Entity);
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}