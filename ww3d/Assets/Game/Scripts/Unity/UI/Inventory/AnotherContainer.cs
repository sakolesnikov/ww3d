public class AnotherContainer : DefaultItemContainer<DropToContainerSignal> {

    protected override DropToContainerSignal GetSignal() => new() { Transform = transform };

}
//     public void OnDrop(PointerEventData eventData) {
//         var go = eventData.pointerDrag;
//         var entityMono = go.GetComponent<AbstractEntityMono>();
//         if (entityMono) {
//             var lootEntity = entityMono.GetEntity();
//             lootEntity.EmitSignal(new DropToContainerSignal { Transform = transform });
//         }
//     }
//
// }