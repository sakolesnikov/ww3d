using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class MessageSystem : EntityListSystem<ShowMessageRequest> {

    [Inject]
    private readonly MessageProvider msgProvider;
    private Entity toolPanel;

    protected override void OnAddStore(EntityStore store) {
        toolPanel = store.GetToolPanel();
    }

    protected override void ProcessEntity(ref ShowMessageRequest component, Entity entity) {
        ref var msgComp = ref toolPanel.GetComponent<MessageComponent>();
        msgProvider.Create(msgComp.Value, entity.GetComponent<TooltipComponent>().Key);
        CommandBuffer.RemoveComponent<ShowMessageRequest>(entity.Id);
        toolPanel.EmitSignal(new ScrollDownSignal());
    }

}