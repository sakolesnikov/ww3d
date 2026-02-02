using Friflo.Engine.ECS;
using VContainer;
using Transform = UnityEngine.Transform;

[LevelScope]
public class DropSignalImpl : GenericSignal<DropSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropSignal> signal) {
        var itemEntity = signal.Entity;
        var transform = itemEntity.GetTransform();
        itemEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        // transform.SetParent(parentTransform, false);

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