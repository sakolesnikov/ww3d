using System.Collections.Generic;
using Friflo.Engine.ECS;

public struct RecipeRegistryComponent : IComponent {

    public Dictionary<RecipeKey, LootDef> Registry;

}