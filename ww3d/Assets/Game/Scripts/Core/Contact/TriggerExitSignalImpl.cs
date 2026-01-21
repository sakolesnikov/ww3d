using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;
using VContainer.Unity;

[LevelScope]
public class TriggerExitSignalImpl : GenericSignal<TriggerExitSignal>, IInitializable {

    [Inject]
    private readonly IEnumerable<ITriggerExit> triggerExit;
    private readonly Dictionary<ContactKey, ITriggerExit> dicTriggerExit = new();

    public void Initialize() {
        foreach (var trigger in triggerExit) {
            var triggerWith = (ITriggerWith)trigger;
            dicTriggerExit.Add(new ContactKey(triggerWith.Iam(), triggerWith.TriggeredWith()), trigger);
        }
    }

    protected override void Signal(Signal<TriggerExitSignal> signal) {
        var iAm = signal.Entity;
        var triggeredWith = signal.Event.TriggeredWith;
        var triggerKey = new ContactKey(
            iAm.GetComponent<TypeComponent>().Value,
            triggeredWith.GetComponent<TypeComponent>().Value
        );
        if (dicTriggerExit.TryGetValue(triggerKey, out var foundTrigger)) {
            foundTrigger.Exit(iAm, triggeredWith);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}