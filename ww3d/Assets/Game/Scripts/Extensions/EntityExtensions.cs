using Friflo.Engine.ECS;
using UnityEngine;
using Transform = UnityEngine.Transform;

public static class EntityExtensions {

    public static Transform GetTransform(this Entity entity) => entity.GetComponent<TransformComponent>().Value;

    public static GameObject GetGameObject(this Entity entity) => entity.GetComponent<GameObjectComponent>().Value;
}