using Friflo.Engine.ECS;
using UnityEngine;

public struct MoveToCmd : ICommand {

    public Vector3 Target;

    public void Init(Entity actor) {
        actor.AddComponent(new MoveIntentComponent { Target = Target });
    }

    public bool IsFinished(Entity actor) => !actor.HasComponent<PathFollowerComponent>() && !actor.HasComponent<MoveIntentComponent>();

    public void Break(Entity actor) {
        actor.RemoveComponent<MoveIntentComponent>();
        actor.RemoveComponent<PathFollowerComponent>();
    }

}