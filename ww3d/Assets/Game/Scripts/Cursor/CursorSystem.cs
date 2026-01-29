using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

[LevelScope]
public class CursorSystem : EntityListSystem<CursorComponent>, IInitSystem {

    [Inject]
    private readonly InputSystem_Actions input;
    [Inject]
    private readonly EntityStore world;
    private int lastHoveredEntityId = -1;
    private readonly EntityList entityList = new();
    private Entity cameraEntity;

    public void Init(EntityStore world) {
        cameraEntity = world.GetCamera();
    }

    protected override bool CanProcess() => !cameraEntity.IsNull && !EventSystem.current.IsPointerOverGameObject();

    protected override void ProcessEntity(ref CursorComponent component, Entity cursorEntity) {
        ref var cameraComp = ref cameraEntity.GetComponent<CameraComponent>();
        var camera = cameraComp.Value;
        var position = input.Cursor.Position.ReadValue<Vector2>();

        var ray = camera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, Masks.Interactable)) {
            if (hit.collider.TryGetComponent<AbstractEntityMono>(out var entityMono)) {
                var hitEntity = entityMono.GetEntity();
                if (lastHoveredEntityId != hitEntity.Id) {
                    hitEntity.EmitSignal(new HoverEnterSignal());
                    //cursorEntity.EmitSignal(new HoverEnterSignal { Value = hitEntity });
                    // hitEntity.AddComponent(new HoverEnterComponent());
                }

                lastHoveredEntityId = hitEntity.Id;
            }
        } else {
            if (lastHoveredEntityId != -1) {
                world.GetEntityById(lastHoveredEntityId).EmitSignal(new HoverExitSignal());
                // cursorEntity.EmitSignal(new HoverExitSignal { EntityId = lastHoveredEntityId });
            }

            lastHoveredEntityId = -1;
        }
    }

    // protected override void OnUpdate() {
    //     if (cameraEntity.IsNull) {
    //         return;
    //     }
    //
    //     if (EventSystem.current.IsPointerOverGameObject()) {
    //         return;
    //     }
    //
    //     Query.Entities.ToEntityList(entityList);
    //     foreach (var entity in entityList) { }
    // }

}