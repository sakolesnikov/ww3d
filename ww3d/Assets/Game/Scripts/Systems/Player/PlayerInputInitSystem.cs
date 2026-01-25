using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using Transform = UnityEngine.Transform;

[LevelScope]
public class PlayerInputInitSystem : IInitSystem, IDisposeSystem {

    [Inject]
    private readonly InputSystem_Actions input;
    [Inject]
    private readonly EntityStore world;
    private Transform playerTransform;
    private Camera camera;
    private Entity player;

    public void Init(EntityStore world) {
        player = world.GetPlayer();
        if (player == default) {
            return;
        }

        playerTransform = player.GetComponent<TransformComponent>().Value;
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
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, Masks.Ground)) {
            var entityMono = hit.collider.GetComponent<AbstractEntityMono>();
            player.AddComponent(new TapIntentComponent
            {
                Target = hit.point,
                Entity = entityMono != null ? entityMono.GetEntity() : default,
                MoveMode = mode
            });
        }
    }

}