using UnityEngine;

public abstract class LootDef : EntityDefinition {

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;

}

public abstract class LootDef<T> : LootDef where T : LootDef<T> {

    private static readonly string Name = typeof(T).Name;
    public override string EntityName => Name;

}