using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using UnityEngine.UI;

public class ScrollDownSignalImpl : GenericSignal<ScrollDownSignal> {

    protected override void Signal(Signal<ScrollDownSignal> signal) {
        var toolPanel = signal.Entity;
        ref var scrollRectComp = ref toolPanel.GetComponent<ScrollRectComponent>();
        ScrollDown(scrollRectComp.Value).Forget();
    }

    private async UniTask ScrollDown(ScrollRect scrollRect) {
        await UniTask.Yield();
        scrollRect.verticalNormalizedPosition = 0;
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(ToolPanelDef);

}