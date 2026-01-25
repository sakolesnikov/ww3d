using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class FSMUpdateSystem : EntityListSystem<FSMComponent> {

    [Inject]
    private readonly EntityStore world;

    protected override void ProcessEntity(ref FSMComponent fsm, Entity entity) {
        if (fsm.CurrentTask.Status != UniTaskStatus.Pending) {
            fsm.CurrentTask = fsm.Value.Update();
        }
    }

}