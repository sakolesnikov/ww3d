public class AnotherContainer : DefaultDrop<DropToContainerSignal> {

    protected override DropToContainerSignal GetSignal() => new() { Transform = transform };

}