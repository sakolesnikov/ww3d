using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;

[MenuScope]
[LevelScope]
public class EntityProvider : ISelfRegisterable {

    private readonly Dictionary<int, Entity> monoEntities = new();
    [Inject]
    private EntityStore world;

    public Entity GetEntity(int gameObjectId) {
        if (HasNoEntity(gameObjectId)) {
            AddEntity(gameObjectId, world.CreateEntity());
        }

        return monoEntities[gameObjectId];
    }

    public void Release(int gameObjectId) {
        if (HasEntity(gameObjectId)) {
            monoEntities.Remove(gameObjectId);
        }
    }

    private bool HasEntity(int gameObjectId) => monoEntities.ContainsKey(gameObjectId);

    private void AddEntity(int gameObjectId, Entity entity) {
        monoEntities[gameObjectId] = entity;
    }

    private bool HasNoEntity(int gameObjectId) => !monoEntities.ContainsKey(gameObjectId);

}