using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultRecipe : EntityDefinition {

    [SerializeField]
    private List<LootDef> ingredients;
    [SerializeField]
    private LootDef result;
    public List<LootDef> Ingredients => ingredients;
    public LootDef Result => result;

}

public abstract class DefaultRecipe<T> : DefaultRecipe where T : DefaultRecipe<T> {

    private static readonly string Name = typeof(T).Name;
    public override string EntityName => Name;

}