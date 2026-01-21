using UnityEngine;

public class Lootable : MonoBehaviour {

    [SerializeField]
    private LootDef[] loots;
    public LootDef[] Loots => loots;

}