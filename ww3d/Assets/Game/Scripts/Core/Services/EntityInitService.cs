using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[MenuScope]
[LevelScope]
public class EntityInitService : ISelfRegisterable, IInitializable {

    [Inject]
    private readonly IEnumerable<IEntityInitialization> entityInitializations;
    private readonly Dictionary<Type, IEntityInitialization> dictByType = new();

    public void Initialize() {
        foreach (var service in entityInitializations) {
            dictByType.Add(service.getType(), service);
        }
    }

    public void Init(Entity entity) {
        if (!entity.HasComponent<DefinitionComponent>()) {
            return;
        }

        ref var defComp = ref entity.GetComponent<DefinitionComponent>();
        if (defComp.Value == null) {
            Debug.LogWarning($"No definition component attached to entity {entity.Name}");
            return;
        }

        if (dictByType.TryGetValue(defComp.Value.GetType(), out var value)) {
            value.Initialize(entity);
        }
    }

}