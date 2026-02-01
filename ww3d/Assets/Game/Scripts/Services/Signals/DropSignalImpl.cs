using Friflo.Engine.ECS;
using VContainer;
using Transform = UnityEngine.Transform;

[LevelScope]
public class DropSignalImpl : GenericSignal<DropSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropSignal> signal) {
        var itemEntity = signal.Entity;
        // var player = world.GetPlayer();

        // itemEntity.GetComponent<ImageComponent>().Value.raycastTarget = true;
        var transform = itemEntity.GetTransform();
        itemEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        // transform.SetParent(parentTransform, false);

        // if (signal.Event.Area == DropAreaEnum.USER) {
        //     if (!player.TryGetRelation<ContainsRelation, Entity>(lootEntity, out var rel)) {
        //         var entitiesRelationToContainer = lootEntity.GetIncomingLinks<ContainsRelation>();
        //         foreach (var entityRelation in entitiesRelationToContainer) {
        //             entityRelation.Entity.RemoveRelation<ContainsRelation>(lootEntity);
        //         }
        //
        //         player.AddRelation(new ContainsRelation { Entity = lootEntity });
        //     }
        //
        //     if (player.HasComponent<LeftHandComponent>()) {
        //         ref var leftHandComp = ref player.GetComponent<LeftHandComponent>();
        //         if (leftHandComp.Entity.Id == lootEntity.Id) {
        //             player.RemoveComponent<LeftHandComponent>();
        //             player.RemoveChild(leftHandComp.Entity);
        //         }
        //     }
        //
        //     if (player.HasComponent<RightHandComponent>()) {
        //         ref var rightHandComp = ref player.GetComponent<RightHandComponent>();
        //         if (rightHandComp.Entity.Id == lootEntity.Id) {
        //             player.RemoveComponent<RightHandComponent>();
        //             player.RemoveChild(rightHandComp.Entity);
        //         }
        //     }
        // }
        //
        // if (signal.Event.Area == DropAreaEnum.CONTAINER) {
        //     if (player.TryGetRelation<ContainsRelation, Entity>(lootEntity, out var relation)) {
        //         player.RemoveRelation<ContainsRelation>(lootEntity);
        //         var links = lootEntity.GetIncomingLinks<LeftHandComponent>();
        //         if (links.Count > 0) {
        //             player.RemoveComponent<LeftHandComponent>();
        //             player.RemoveChild(lootEntity);
        //         }
        //
        //         ref var tappedComp = ref player.GetComponent<TappedEntityComponent>();
        //         tappedComp.Value.AddRelation(new ContainsRelation { Entity = lootEntity });
        //     }
        // }
        //
        // if (signal.Event.Area == DropAreaEnum.LEFT_HAND) {
        //     if (player.HasComponent<LeftHandComponent>()) {
        //         ref var leftHandComp = ref player.GetComponent<LeftHandComponent>();
        //         player.RemoveChild(leftHandComp.Entity);
        //         ReParent(ref leftHandComp.Entity, lootEntity.GetComponent<ParentTransformComponent>().Value);
        //         leftHandComp.Entity = lootEntity;
        //         player.AddChild(leftHandComp.Entity);
        //     } else {
        //         player.AddComponent(new LeftHandComponent { Entity = lootEntity });
        //         player.AddChild(lootEntity);
        //     }
        // }
        //
        // if (signal.Event.Area == DropAreaEnum.RIGHT_HAND) {
        //     if (player.HasComponent<RightHandComponent>()) {
        //         ref var rightHandComp = ref player.GetComponent<RightHandComponent>();
        //         player.RemoveChild(rightHandComp.Entity);
        //         ReParent(ref rightHandComp.Entity, lootEntity.GetComponent<ParentTransformComponent>().Value);
        //         rightHandComp.Entity = lootEntity;
        //         player.AddChild(lootEntity);
        //     } else {
        //         player.AddComponent(new RightHandComponent { Entity = lootEntity });
        //         player.AddChild(lootEntity);
        //     }
        // }
        //
        // lootEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
    }

    private void ReParent(ref Entity lootEntity, Transform newParent) {
        ref var imageComp = ref lootEntity.GetComponent<ImageComponent>();
        imageComp.Value.raycastTarget = true;
        var transform = lootEntity.GetTransform();
        lootEntity.GetComponent<ParentTransformComponent>().Value = newParent;
        transform.SetParent(newParent, false);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}