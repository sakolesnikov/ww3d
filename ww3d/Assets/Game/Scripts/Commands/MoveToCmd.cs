using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct MoveToCmd : ICommand {

    public NNInfo Node;
    public MoveMode MoveMode;
    public Entity TargetEntity;

    public void Init(Entity actor) {
        actor.AddComponent(new MoveIntentComponent { MoveMode = MoveMode, Node = Node });
    }

    public bool IsFinished(Entity actor) {
        if (!TargetEntity.IsNull) {
            var playerTransform = actor.GetTransform();
            var targetPosition = TargetEntity.GetComponent<ColliderComponent>().Value.ClosestPoint(playerTransform.position);
            if (Vector3.Distance(playerTransform.position, targetPosition) < 1f) {
                Break(actor);
                return true;
            }
        }

        return !actor.HasComponent<PathFollowerComponent>() && !actor.HasComponent<MoveIntentComponent>();
    }

    public void Break(Entity actor) {
        actor.RemoveComponent<MoveIntentComponent>();
        actor.RemoveComponent<PathFollowerComponent>();
    }

}