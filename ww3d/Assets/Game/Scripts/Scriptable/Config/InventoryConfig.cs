using UnityEngine;

[CreateAssetMenu(menuName = "Configs/InventoryConfig", fileName = "InventoryConfig")]
public class InventoryConfig : ScriptableObject {

    [SerializeField]
    private AbstractEntityMono itemPrefab;
    public AbstractEntityMono ItemPrefab => itemPrefab;

}