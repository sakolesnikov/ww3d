using System.Collections.Generic;
using Friflo.Engine.ECS;
using UnityEngine;

public class RecipeRegistry : MonoBehaviour, IEntityAware {

    [SerializeField]
    private List<DefaultRecipe> recipes;

    public void OnEntityReady(ref Entity entity) {
        var registry = new Dictionary<RecipeKey, LootDef>();
        foreach (var recipe in recipes) {
            var key = RecipeKey.FromLoot(recipe.Ingredients);
            registry.Add(key, recipe.Result);
        }

        entity.AddComponent(new RecipeRegistryComponent { Registry = registry });
    }

}