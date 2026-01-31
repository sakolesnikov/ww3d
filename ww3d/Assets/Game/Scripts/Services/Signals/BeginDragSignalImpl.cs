using Friflo.Engine.ECS;
using UnityEngine;
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

    protected override void Signal(Signal<BeginDragSignal> signal) {
        var itemEntity = signal.Entity;
        var canvas = world.GetCanvas();
        var itemTransform = itemEntity.GetTransform();
        ref var imageComp = ref itemEntity.GetComponent<ImageComponent>();
        var color = imageComp.Value.color;
        imageComp.Value.raycastTarget = false;
        itemEntity.GetComponent<ParentTransformComponent>().Value = itemTransform.parent;
        itemTransform.SetParent(canvas.transform);
        
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}