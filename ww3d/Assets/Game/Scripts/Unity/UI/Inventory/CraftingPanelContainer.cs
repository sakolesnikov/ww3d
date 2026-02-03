public class CraftingPanelContainer : DefaultDrop<DropInCraftContainerSignal> {

    protected override DropInCraftContainerSignal GetSignal() => new() { Transform = transform };

}