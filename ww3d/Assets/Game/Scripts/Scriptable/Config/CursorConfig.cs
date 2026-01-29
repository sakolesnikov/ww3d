using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Cursor", fileName = "CursorConfig")]
public class CursorConfig : ScriptableObject {

    [SerializeField]
    private float speedAnimation;
    public float FrameRate => speedAnimation;

}