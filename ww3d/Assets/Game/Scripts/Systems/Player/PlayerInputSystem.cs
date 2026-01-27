using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

[LevelScope]
public class PlayerInputSystem : IInitSystem, IDisposeSystem {

    [Inject]
    private readonly InputSystem_Actions input;
    [Inject]
    private readonly EntityStore world;
    private Camera camera;
    private Entity player;

    public void Init(EntityStore world) {
        player = world.GetPlayer();
        if (player == default) {
            return;
        }

        var cameraEntity = world.GetCamera();
        if (cameraEntity == default) {
            return;
        }

        camera = cameraEntity.GetComponent<CameraComponent>().Value;
        input.Player.Walk.performed += OnWalkPerformed;
        input.Player.Run.performed += OnRunPerformed;
    }

    public void Dispose(EntityStore world) {
        input.Player.Walk.performed -= OnWalkPerformed;
        input.Player.Run.performed -= OnRunPerformed;
    }

    private void OnRunPerformed(InputAction.CallbackContext context) {
        if (player == default || camera == null) {
            return;
        }

        if (context.control.device is not Pointer pointer) {
            return;
        }

        var screenPosition = pointer.position.ReadValue();
        Move(screenPosition, MoveMode.Run);
    }

    private void OnWalkPerformed(InputAction.CallbackContext context) {
        if (player == default || camera == null) {
            return;
        }

        if (context.control.device is not Pointer pointer) {
            return;
        }

        var screenPosition = pointer.position.ReadValue();
        Move(screenPosition, MoveMode.Walk);
    }

    private void Move(Vector2 screenPosition, MoveMode mode) {
        var ray = camera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, Masks.Ground | Masks.Interactable)) {
            var node = AstarPath.active.GetNearest(hit.point, NNConstraint.Walkable);
            Debug.Log($"hit.name {hit.collider.name} hit.point {hit.point} node.position {node.position} node center {(Vector3)node.node.position}");
            var entityMono = hit.collider.GetComponent<AbstractEntityMono>();
            player.AddComponent(new TapIntentComponent
            {
                Position = hit.point,
                Entity = entityMono != null ? entityMono.GetEntity() : default,
                MoveMode = mode
            });
        }
    }

}