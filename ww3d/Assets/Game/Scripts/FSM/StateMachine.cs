using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

public class StateMachine {

    private readonly Entity entity;
    private Type currentStateType;
    private IState previousState;
    private IState currentState;
    private readonly Dictionary<Type, IState> states = new();
    private bool waiting;

    public void AddState(IState state) {
        states.Add(state.GetType(), state);
    }

    public StateMachine(Entity entity) => this.entity = entity;

    public async UniTask Update() {
        if (currentStateType != null) {
            await SwitchState(currentStateType);
            currentStateType = null;
        }

        currentState?.Update(entity);

        var nextStateType = await currentState.GetNextState(entity);
        if (nextStateType != null) {
            await SwitchState(nextStateType);
        }
    }

    public IState GetState(Type type) {
        if (states.TryGetValue(type, out var state)) {
            return state;
        }

        throw new KeyNotFoundException($"State of type {type.Name} is not registered in the StateMachine.");
    }

    private async UniTask SwitchState(Type newState) {
        if (currentState != null) {
            await currentState.Exit(entity);
            // Debug.Log($"{currentState} Exit");
            // currentState.SetCommandBuffer(null);
        }

        if (states.TryGetValue(newState, out var stateImpl)) {
            previousState = currentState;
            currentState = stateImpl;
            // currentState.SetCommandBuffer(commandBuffer);
            await currentState.Enter(entity);
            // Debug.Log($"{currentState} Enter");
        }
    }

    public void SetCurrentState(Type type) {
        currentStateType = type;
    }

    public bool RequestNewState(Type type) {
        if (currentState.IsAllowedNewState(type)) {
            SetCurrentState(type);
            return true;
        }

        return false;
    }

    public IState GetPreviousState() => previousState;

    public IState GetCurrentState() => currentState;

}