public class LeftHandContainer : DefaultItemContainer<DropToLeftHandContainerSignal> {

    protected override DropToLeftHandContainerSignal GetSignal() => new() { Transform = transform };

}