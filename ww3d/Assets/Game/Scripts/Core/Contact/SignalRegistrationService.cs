using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;

[MenuScope]
[LevelScope]
public class SignalRegistrationService : ISelfRegisterable {

    [Inject]
    private readonly IEnumerable<ISignal> signals;

    public void Register(Entity entity, EntityDefinition entityDef) {
        foreach (var signal in signals) {
            if (signal.IsSupported(entity, entityDef)) {
                signal.AddSignal(entity);
            }
        }
    }

    public void Register(Entity entity) {
        if (!entity.HasComponent<DefinitionComponent>()) {
            return;
        }

        var entityDef = entity.GetComponent<DefinitionComponent>().Value;
        Register(entity, entityDef);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Unregister(Entity entity, EntityDefinition entityDef) {
        foreach (var signal in signals) {
            if (signal.IsSupported(entity, entityDef)) {
                signal.RemoveSignal(entity);
            }
        }
    }

    public void Unregister(Entity entity) {
        if (!entity.HasComponent<DefinitionComponent>()) {
            return;
        }

        var entityDef = entity.GetComponent<DefinitionComponent>().Value;
        Unregister(entity, entityDef);
    }

}