using Friflo.Engine.ECS;
using VContainer;

/// <summary>
///     Handles the logic for initiating a drag operation on an entity.
/// </summary>
/// <remarks>
///     When dragging starts, the entity is temporarily re-parented to the UI Canvas to ensure it renders on top.
///     The <see cref="ParentTransformComponent" /> stores the original parent reference, allowing the item
///     to be returned to its previous position if dropped outside a valid target area.
/// </remarks>
[LevelScope]
public class BeginDragSignalImpl : GenericSignal<BeginDragSignal> {

    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly ItemProvider itemProvider;

    protected override void Signal(Signal<BeginDragSignal> signal) {
        var itemEntity = signal.Entity;
        var lootDef = itemEntity.GetComponent<DefinitionComponent>().GetValue<LootDef>();
        var canvas = world.GetCanvas();
        var itemTransform = itemEntity.GetTransform();
        var itemImage = itemEntity.GetComponent<ImageComponent>().Value;
        itemImage.raycastTarget = false;

        if (itemTransform.parent.TryGetComponent<IItemBeginDragHandler>(out var handler)) {
            handler.OnItemBeginDrag(ref itemEntity);
        }

        itemEntity.AddComponent(new ParentTransformComponent { Value = itemTransform.parent });
        var itemIndex = itemTransform.GetSiblingIndex();
        itemTransform.SetParent(canvas.transform);

        var shadowEntity = itemProvider.GetShadowEntity(lootDef);
        itemEntity.AddComponent(new ShadowComponent { Value = shadowEntity });
        var shadowTransform = shadowEntity.GetTransform();
        shadowTransform.SetParent(itemEntity.GetComponent<ParentTransformComponent>().Value, false);
        shadowTransform.SetSiblingIndex(itemIndex);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}