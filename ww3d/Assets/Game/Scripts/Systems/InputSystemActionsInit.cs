using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
[Order(10)]
public class InputSystemActionsInit : IInitSystem, IDisposeSystem {

    [Inject]
    private readonly InputSystem_Actions inputSystem;

    public void Init(EntityStore world) {
        inputSystem.Player.Enable();
        inputSystem.UI.Enable();
        inputSystem.Mouse.Enable();
    }

    public void Dispose(EntityStore world) {
        inputSystem.Player.Disable();
        inputSystem.UI.Disable();
        inputSystem.Mouse.Disable();
    }

}