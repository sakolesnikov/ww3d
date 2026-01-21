using System;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class CursorInit : IEntityInitialization, IDisposable {

    [Inject]
    private InputSystem_Actions input;
    [Inject]
    private EntityStore world;

    public void Initialize(Entity entity) {
        input.Cursor.Enable();
        entity.AddComponent(new CursorComponent());
    }

    public void Dispose() {
        input.Cursor.Disable();
    }

    public Type getType() => typeof(CursorDef);

}