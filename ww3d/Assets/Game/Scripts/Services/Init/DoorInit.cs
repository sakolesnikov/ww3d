using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class DoorInit : IEntityInitialization {

    [Inject]
    private readonly DoorOpenState doorOpenState;
    [Inject]
    private readonly DoorCloseState doorCloseState;
    [Inject]
    private readonly DoorIdleState doorIdleState;

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var doorMono = go.GetComponent<Door>();
        entity.AddComponent(new AnimatorComponent { Value = go.GetComponent<Animator>() });
        entity.AddComponent(new DoorComponent { Value = doorMono.DoorTransform });
        entity.AddComponent(new ColliderComponent { Value = doorMono.GetComponentInChildren<Collider>() });
        entity.AddTag<ClosedTag>();

        var fsm = new StateMachine(entity);
        fsm.AddState(doorIdleState);
        fsm.AddState(doorOpenState);
        fsm.AddState(doorCloseState);
        fsm.SetCurrentState(typeof(DoorIdleState));
        entity.AddComponent(new FSMComponent { Value = fsm, CurrentTask = UniTask.CompletedTask });
    }

    public Type getType() => typeof(DoorDef);

}