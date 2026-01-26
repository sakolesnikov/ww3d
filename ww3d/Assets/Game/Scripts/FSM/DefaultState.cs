using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

public abstract class DefaultState : IState, ISelfRegisterable {

    public virtual UniTask Enter(Entity entity) {
        // Debug.Log($"Enter {GetType().Name}");
        StateStarted?.Invoke(entity);
        return UniTask.CompletedTask;
    }

    public virtual void Update(Entity entity) { }

    public virtual UniTask Exit(Entity entity) {
        // Debug.Log($"Exit {GetType().Name}");
        StateEnded?.Invoke(entity);
        return UniTask.CompletedTask;
    }

    public virtual UniTask<Type> GetNextState(Entity entity) => UniTask.FromResult<Type>(null);

    public virtual bool IsAllowedNewState(Type nextState) => true;

    public event Action<Entity> StateStarted;
    public event Action<Entity> StateEnded;

}