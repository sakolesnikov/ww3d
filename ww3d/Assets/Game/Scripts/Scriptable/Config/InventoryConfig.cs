using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Inventory", fileName = "InventoryConfig")]
public class InventoryConfig : ScriptableObject {

    [SerializeField]
    private AbstractEntityMono itemPrefab;
    public AbstractEntityMono ItemPrefab => itemPrefab;

}