using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

public interface IState {

    /// <summary>
    ///     Event triggered when the state successfully begins (after Enter is completed).
    ///     The argument is typically the Entity associated with the state change.
    /// </summary>
    event Action<Entity> StateStarted;

    /// <summary>
    ///     Event triggered when the state successfully ends (after Exit is completed).
    ///     The argument is typically the Entity associated with the state change.
    /// </summary>
    event Action<Entity> StateEnded;

    UniTask Enter(Entity entity);

    void Update(Entity entity);

    UniTask Exit(Entity entity);

    bool IsAllowedNewState(Type nextState);

    UniTask<Type> GetNextState(Entity entity);

    // void SetCommandBuffer(CommandBuffer commandBuffer);

}