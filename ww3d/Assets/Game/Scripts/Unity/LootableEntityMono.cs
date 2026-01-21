using UnityEngine;

public class LootableEntityMono : AbstractEntityMono {

    [SerializeField]
    private LootDef[] loots;

    protected override void PostAwake() {
        // foreach (var loot in loots) {
        // loot.Initialize(Entity, gameObject, Resolver);
        // }
    }

}