using System.Collections.Generic;
using Friflo.Engine.ECS;
using VContainer;
using VContainer.Unity;

[LevelScope]
public class CollisionEnterSignalImpl : GenericSignal<CollisionEnterSignal>, IInitializable {

    [Inject]
    private readonly IEnumerable<ICollisionEnter> enters;
    private readonly Dictionary<ContactKey, ICollisionEnter> dicEnter = new();

    public void Initialize() {
        foreach (var enter in enters) {
            var with = (IContactWith)enter;
            dicEnter.Add(new ContactKey(with.Iam(), with.ContactedWith()), enter);
        }
    }

    protected override void Signal(Signal<CollisionEnterSignal> signal) {
        var iAm = signal.Entity;
        var contactedWith = signal.Event.ContactedWith;
        var contactKey = new ContactKey(
            iAm.GetComponent<TypeComponent>().Value,
            contactedWith.GetComponent<TypeComponent>().Value
        );
        if (dicEnter.TryGetValue(contactKey, out var foundContactImpl)) {
            foundContactImpl.Enter(iAm, contactedWith, signal.Event.Info);
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}