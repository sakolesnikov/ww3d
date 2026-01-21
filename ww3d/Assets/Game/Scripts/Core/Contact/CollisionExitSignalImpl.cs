using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;
using VContainer.Unity;

[LevelScope]
public class CollisionExitSignalImpl : GenericSignal<CollisionExitSignal>, IInitializable {

    [Inject]
    private readonly IEnumerable<ICollisionExit> enters;
    private readonly Dictionary<ContactKey, ICollisionExit> dicEnter = new();

    public void Initialize() {
        foreach (var enter in enters) {
            var with = (IContactWith)enter;
            dicEnter.Add(new ContactKey(with.Iam(), with.ContactedWith()), enter);
        }
    }

    protected override void Signal(Signal<CollisionExitSignal> signal) {
        var iAm = signal.Entity;
        var contactedFrom = signal.Event.ContactedFrom;
        var contactKey = new ContactKey(
            iAm.GetComponent<TypeComponent>().Value,
            contactedFrom.GetComponent<TypeComponent>().Value
        );
        if (dicEnter.TryGetValue(contactKey, out var foundContactImpl)) {
            foundContactImpl.Exit(iAm, contactedFrom, signal.Event.Info);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}