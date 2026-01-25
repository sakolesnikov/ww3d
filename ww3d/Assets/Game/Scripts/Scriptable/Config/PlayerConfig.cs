using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Player", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject {

    [Header("Speed")]
    [SerializeField]
    private float walk;
    [SerializeField]
    private float run;
    [SerializeField]
    private float rotationWalk;
    [SerializeField]
    private float rotationRun;
    private float angleRotationThreshold;
    public float WalkSpeed => walk;
    public float RunSpeed => run;
    public float RotationWalkSpeed => rotationWalk;
    public float RotationRunSpeed => rotationRun;

}