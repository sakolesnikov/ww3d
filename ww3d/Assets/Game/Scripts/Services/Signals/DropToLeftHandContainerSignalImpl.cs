using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

[LevelScope]
public class DropToLeftHandContainerSignalImpl : DropToHandContainerSignalImpl<DropToLeftHandContainerSignal, LeftHandComponent> {

    protected override Transform GetTransform(Signal<DropToLeftHandContainerSignal> signal) => signal.Event.Transform;

    protected override LeftHandComponent GetBody(Signal<DropToLeftHandContainerSignal> signal) => new() { Entity = signal.Entity };

}