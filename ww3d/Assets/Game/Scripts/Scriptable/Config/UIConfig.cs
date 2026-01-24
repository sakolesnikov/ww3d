using UnityEngine;

[CreateAssetMenu(menuName = "Configs/UIConfig", fileName = "UIConfig")]
public class UIConfig : ScriptableObject {

    [SerializeField]
    private AbstractEntityMono messagePrefab;
    public AbstractEntityMono MessagePrefab => messagePrefab;

}