public class CollectableEntityMono : AbstractEntityMono {

    protected override void PostAwake() {
        Entity.AddTag<CollectableTag>();
    }

}