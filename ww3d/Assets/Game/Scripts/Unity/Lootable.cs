using System;
using Friflo.Engine.ECS;
using UnityEngine;

public class Lootable : MonoBehaviour, IEntityAware {

    [SerializeField]
    private LootDef[] loots;
    public LootDef[] Loots => loots;

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new LootComponent { Values = loots });
    }

}

[Serializable]
public class LootInfo {

    [SerializeField]
    private LootDef loot;
    [SerializeField]
    private GameObject gameObject;

}