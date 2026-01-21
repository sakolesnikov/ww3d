using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

public abstract class LootDef : EntityDefinition {

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int amount = 1;
    public Sprite Sprite => sprite;
    public int Amount => amount;

    public void Initialize(Entity container, GameObject go, IObjectResolver resolver) {
        var world = resolver.Resolve<EntityStore>();
        var lootEntity = world.CreateEntity();
        lootEntity.AddComponent(new LootComponent { Value = this });
        container.AddRelation(new ContainsRelation { Entity = lootEntity });
    }

}

public abstract class LootDef<T> : LootDef where T : LootDef<T> {

    private static readonly string Name = typeof(T).Name;
    public override string EntityType => Name;

}