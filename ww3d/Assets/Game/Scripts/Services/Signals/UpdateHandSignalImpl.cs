using Friflo.Engine.ECS;

[LevelScope]
public class UpdateHandSignalImpl : GenericSignal<UpdateHandSignal> {

    protected override void Signal(Signal<UpdateHandSignal> signal) {
        var player = signal.Entity;
        if (player.ChildCount == 0) {
            player.RemoveComponent<LeftHandComponent>();
            player.RemoveComponent<RightHandComponent>();
        }


        if (player.TryGetComponent<LeftHandComponent>(out var leftHandComp)) {
            foreach (var lootEntity in player.ChildEntities) {
                if (leftHandComp.Entity.Id == lootEntity.Id) { }
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}