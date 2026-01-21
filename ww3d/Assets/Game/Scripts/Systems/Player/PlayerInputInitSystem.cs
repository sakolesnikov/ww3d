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
        input.Player.Enable();
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
        // input.Player.Tap.performed += OnTapPerformed;
    }

    public void Dispose(EntityStore world) {
        input.Player.Disable();
        // input.Player.Tap.performed -= OnTapPerformed;
    }

    private void OnTapPerformed(InputAction.CallbackContext context) {
        if (player == default || camera == null) {
            return;
        }
        if (context.control.device is not Pointer pointer) {
            return;
        }

        var mousePos = pointer.position.ReadValue();
        var worldPoint = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        worldPoint.z = 0f;
        player.AddComponent(new TapIntentComponent { Target = worldPoint });
    }

}