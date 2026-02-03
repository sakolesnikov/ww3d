using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class EndDragSignalImpl : GenericSignal<EndDragSignal> {

    [Inject]
    private readonly ItemProvider itemProvider;
    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<EndDragSignal> signal) {
        var itemEntity = signal.Entity;
        ref var imageComp = ref itemEntity.GetComponent<ImageComponent>();
        var itemTransform = itemEntity.GetTransform();
        ref var parentTransformComp = ref itemEntity.GetComponent<ParentTransformComponent>();
        var parentTransform = parentTransformComp.Value;
        parentTransformComp.Value = null;
        imageComp.Value.raycastTarget = true;
        itemTransform.SetParent(parentTransform);

        // if the player did not press take all button
        if (itemEntity.HasComponent<ShadowComponent>()) {
            ref var shadowComp = ref itemEntity.GetComponent<ShadowComponent>();
            var shadowTransform = shadowComp.Value.GetTransform();

            // return back an item
            if (parentTransform.GetInstanceID() == shadowTransform.parent.GetInstanceID()) {
                itemTransform.SetSiblingIndex(GetSiblingIndex(ref itemEntity.GetComponent<ShadowComponent>().Value));
            }

            shadowTransform.SetParent(world.GetCanvas().transform);
            itemProvider.ReleaseShadow(ref shadowComp.Value);
            itemEntity.RemoveComponent<ShadowComponent>();
        }
    }

    private int GetSiblingIndex(ref Entity entity) => entity.GetTransform().GetSiblingIndex();

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}