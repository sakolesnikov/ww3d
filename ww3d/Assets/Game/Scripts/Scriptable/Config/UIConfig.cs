using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UI", fileName = "UIConfig")]
public class UIConfig : ScriptableObject {

    [SerializeField]
    private AbstractEntityMono messagePrefab;
    public AbstractEntityMono MessagePrefab => messagePrefab;

}