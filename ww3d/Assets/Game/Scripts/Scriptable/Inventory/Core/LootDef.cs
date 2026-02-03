using UnityEngine;

public abstract class LootDef : EntityDefinition {

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int amount = 1;
    public Sprite Sprite => sprite;
    public int Amount => amount;

}

public abstract class LootDef<T> : LootDef where T : LootDef<T> {

    private static readonly string Name = typeof(T).Name;
    public override string EntityName => Name;

}