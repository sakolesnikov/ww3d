using Friflo.Engine.ECS;
using UnityEngine.InputSystem;
using VContainer;

[LevelScope]
public class CancelHandlerInit : IInitSystem, IDisposeSystem {

    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly InputSystem_Actions inputSystem;

    public void Init(EntityStore world) {
        inputSystem.UI.Cancel.performed += OnCancel;
    }

    private void OnCancel(InputAction.CallbackContext context) {
        var entity = world.CreateEntity();
        entity.AddTag<CancelTag>();
    }

    public void Dispose(EntityStore world) {
        inputSystem.UI.Cancel.performed -= OnCancel;
    }

}