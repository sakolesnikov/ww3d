public class UserContainer : DefaultItemContainer<DropToUserContainerSignal> {

    protected override DropToUserContainerSignal GetSignal() => new() { Transform = transform };

}
//     public void OnDrop(PointerEventData eventData) {
//         var go = eventData.pointerDrag;
//         var entityMono = go.GetComponent<AbstractEntityMono>();
//         if (entityMono) {
//             var entity = entityMono.GetEntity();
//             entity.EmitSignal(new DropToUserSignal { Transform = transform });
//         }
//     }
//
// }