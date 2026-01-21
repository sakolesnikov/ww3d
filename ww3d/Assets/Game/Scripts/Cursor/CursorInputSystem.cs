using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

[LevelScope]
public class CursorInputSystem : QueryUpdateSystem<CursorComponent>, IInitSystem {

    [Inject]
    private readonly InputSystem_Actions input;
    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly EntityProvider entityProvider;
    private readonly Collider2D[] collider2D = new Collider2D[1];
    private int layerMask;
    private ContactFilter2D contactFilter;
    private int lastHoveredEntityId = -1;
    private readonly EntityList entityList = new();
    private Entity cameraEntity;

    public void Init(EntityStore world) {
        layerMask = LayerMask.GetMask("Interactable");
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(layerMask);
        cameraEntity = world.GetCamera();
    }

    protected override void OnUpdate() {
        if (cameraEntity.IsNull) {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ref var cursorComp = ref entity.GetComponent<CursorComponent>();
            ref var cameraComp = ref cameraEntity.GetComponent<CameraComponent>();
            var camera = cameraComp.Value;
            var position = input.Cursor.Position.ReadValue<Vector2>();
            var worldPoint = camera.ScreenToWorldPoint(position);
            worldPoint.z = 0f;
            cursorComp.Position = worldPoint;
            Physics2D.OverlapPoint(worldPoint, contactFilter, collider2D);
            if (collider2D[0]) {
                var hoveredEntity = entityProvider.GetEntity(collider2D[0].gameObject.GetInstanceID());
                if (lastHoveredEntityId != hoveredEntity.Id) {
                    entity.EmitSignal(new HoverEnterSignal { Value = hoveredEntity });
                }

                lastHoveredEntityId = hoveredEntity.Id;
            } else {
                if (lastHoveredEntityId != -1) {
                    entity.EmitSignal(new HoverExitSignal());
                }

                lastHoveredEntityId = -1;
            }

            collider2D[0] = null;
        }
    }

}