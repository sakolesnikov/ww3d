public class LeftHandContainer : DefaultDrop<DropToLeftHandContainerSignal> {

    protected override DropToLeftHandContainerSignal GetSignal() => new() { Transform = transform };

}