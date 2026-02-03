using Friflo.Engine.ECS;
using VContainer;
using Transform = UnityEngine.Transform;

public abstract class DropToHandContainerSignalImpl<T, BODY_PART> : GenericSignal<T>
    where T : struct where BODY_PART : struct, IBodyPart, ILinkComponent {

    protected abstract Transform GetTransform(Signal<T> signal);

    protected abstract BODY_PART GetBody(Signal<T> signal);

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<T> signal) {
        var player = world.GetPlayer();
        var lootEntity = signal.Entity;
        var lootParent = lootEntity.GetComponent<ParentTransformComponent>().Value;
        var containerTransform = GetTransform(signal);
        var newBody = GetBody(signal);

        if (player.HasComponent<BODY_PART>()) {
            var prevHandEntity = player.GetComponent<BODY_PART>().Target;
            player.RemoveChild(prevHandEntity);
            ReParent(prevHandEntity, lootParent);
        }

        player.AddComponent(newBody);
        player.AddChild(lootEntity);
        lootEntity.GetComponent<ParentTransformComponent>().Value = containerTransform;
    }

    private void ReParent(Entity lootEntity, Transform newParent) {
        ref var imageComp = ref lootEntity.GetComponent<ImageComponent>();
        imageComp.Value.raycastTarget = true;
        var transform = lootEntity.GetTransform();
        transform.SetParent(newParent, false);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}