using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Inventory", fileName = "InventoryConfig")]
public class InventoryConfig : ScriptableObject {

    [SerializeField]
    private AbstractEntityMono itemPrefab;
    [SerializeField]
    private AbstractEntityMono itemShadowPrefab;
    public AbstractEntityMono ItemPrefab => itemPrefab;
    public AbstractEntityMono ItemShadowPrefab => itemShadowPrefab;

}