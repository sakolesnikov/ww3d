using UnityEngine;

public abstract class Condition : ScriptableObject {

    public abstract bool Check(ConditionContext context);

    public abstract string Message { get; }

}