public class RightHandContainer : DefaultDrop<DropToRightHandContainerSignal> {

    protected override DropToRightHandContainerSignal GetSignal() => new() { Transform = transform };

}