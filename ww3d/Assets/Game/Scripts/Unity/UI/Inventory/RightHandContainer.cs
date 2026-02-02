public class RightHandContainer : DefaultItemContainer<DropToRightHandContainerSignal> {

    protected override DropToRightHandContainerSignal GetSignal() => new() { Transform = transform };

}