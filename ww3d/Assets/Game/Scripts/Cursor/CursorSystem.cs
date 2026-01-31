using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class CursorSystem : EntityListSystem<CursorComponent>, IInitSystem {

    [Inject]
    private readonly InputSystem_Actions input;
    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly IEventSystem eventSystem;
    private int lastHoveredEntityId = -1;
    private Entity cameraEntity;

    public void Init(EntityStore world) {
        cameraEntity = world.GetCamera();
    }

    protected override bool CanProcess() =>
        !cameraEntity.IsNull && !eventSystem.IsPointerOverGameObject();

    protected override void ProcessEntity(ref CursorComponent component, Entity cursorEntity) {
        // Debug.Log("CursorSystem");
        ref var cameraComp = ref cameraEntity.GetComponent<CameraComponent>();
        var camera = cameraComp.Value;
        var position = input.Mouse.Position.ReadValue<Vector2>();

        var ray = camera.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, Masks.Interactable)) {
            if (input.Mouse.RightButton.IsPressed()) {
                return;
            }

            if (hit.collider.TryGetComponent<AbstractEntityMono>(out var entityMono)) {
                var hitEntity = entityMono.GetEntity();
                if (lastHoveredEntityId != hitEntity.Id) {
                    hitEntity.EmitSignal(new HoverEnterSignal());
                }

                lastHoveredEntityId = hitEntity.Id;
            }
        } else {
            if (lastHoveredEntityId != -1) {
                world.GetEntityById(lastHoveredEntityId).EmitSignal(new HoverExitSignal());
            }

            lastHoveredEntityId = -1;
        }
    }

}