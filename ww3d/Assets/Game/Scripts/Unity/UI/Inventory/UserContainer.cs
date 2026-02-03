public class UserContainer : DefaultDrop<DropToUserContainerSignal> {

    protected override DropToUserContainerSignal GetSignal() => new() { Transform = transform };

}