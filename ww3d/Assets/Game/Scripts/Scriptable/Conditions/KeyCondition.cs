using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Key", fileName = "Key")]
public class KeyCondition : Condition {

    public override bool Check(ConditionContext context) => true;

    public override string Message => "";

}