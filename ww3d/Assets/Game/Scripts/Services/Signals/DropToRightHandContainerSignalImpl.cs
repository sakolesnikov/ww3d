using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

[LevelScope]
public class DropToRightHandContainerSignalImpl : DropToHandContainerSignalImpl<DropToRightHandContainerSignal, RightHandComponent> {

    protected override Transform GetTransform(Signal<DropToRightHandContainerSignal> signal) => signal.Event.Transform;

    protected override RightHandComponent GetBody(Signal<DropToRightHandContainerSignal> signal) => new() { Entity = signal.Entity };

}