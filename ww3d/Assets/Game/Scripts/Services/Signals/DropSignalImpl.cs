using Friflo.Engine.ECS;
using VContainer;
using Transform = UnityEngine.Transform;

// [LevelScope]
public class DropSignalImpl : GenericSignal<DropSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropSignal> signal) {
        var itemEntity = signal.Entity;
        itemEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
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