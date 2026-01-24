using System;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class CursorInit : IEntityInitialization {

    [Inject]
    private EntityStore world;

    public void Initialize(Entity entity) {
        entity.AddComponent(new CursorComponent());
    }

    public Type getType() => typeof(CursorDef);

}