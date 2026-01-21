using Friflo.Engine.ECS.Systems;

public abstract class BaseUpdateSystem : BaseSystem, IUpdateSystem {

    protected abstract void OnUpdate();

    protected override void OnUpdateGroup() {
        OnUpdate();
    }

}