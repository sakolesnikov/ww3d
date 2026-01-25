using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

public struct FSMComponent : IComponent {

    public UniTask CurrentTask;
    public StateMachine Value;

    public FSMComponent(StateMachine value) {
        Value = value;
        CurrentTask = UniTask.CompletedTask;
    }

}