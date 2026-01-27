using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct MoveToCmd : ICommand {

    public NNInfo Node;
    public Vector3 Target;
    public MoveMode MoveMode;

    public void Init(Entity actor) {
        actor.AddComponent(new MoveIntentComponent { Target = Target, MoveMode = MoveMode, Node = Node });
    }

    public bool IsFinished(Entity actor) => !actor.HasComponent<PathFollowerComponent>() && !actor.HasComponent<MoveIntentComponent>();

    public void Break(Entity actor) {
        actor.RemoveComponent<MoveIntentComponent>();
        actor.RemoveComponent<PathFollowerComponent>();
    }

}