using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Door", fileName = "DoorConfig")]
public class DoorConfig : ScriptableObject {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    public float Speed => speed;
    public float Distance => distance;

}