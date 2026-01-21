using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;
using VContainer.Unity;

[LevelScope]
public class TriggerEnterSignalImpl : GenericSignal<TriggerEnterSignal>, IInitializable {

    [Inject]
    private readonly IEnumerable<ITriggerEnter> triggerEnter;
    private readonly Dictionary<ContactKey2, ITriggerEnter> dicTriggerEnter = new();

    public void Initialize() {
        foreach (var trigger in triggerEnter) {
            var triggerWith = (IContactWith)trigger;
            dicTriggerEnter.Add(new ContactKey2(triggerWith.Iam2(), triggerWith.ContactedWith2()), trigger);
        }
    }

    protected override void Signal(Signal<TriggerEnterSignal> signal) {
        var iAm = signal.Entity;
        var triggeredWith = signal.Event.TriggeredWith;
        var triggerKey = new ContactKey2(
            iAm.GetComponent<DefinitionComponent>().Value.GetType(),
            triggeredWith.GetComponent<DefinitionComponent>().Value.GetType()
        );
        if (dicTriggerEnter.TryGetValue(triggerKey, out var foundTrigger)) {
            foundTrigger.Enter(iAm, triggeredWith);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => true;

}