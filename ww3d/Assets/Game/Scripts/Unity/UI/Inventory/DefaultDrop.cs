using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DefaultDrop<T> : MonoBehaviour, IDropHandler where T : struct {

    protected abstract T GetSignal();

    public void OnDrop(PointerEventData eventData) {
        var go = eventData.pointerDrag;
        var entityMono = go.GetComponent<AbstractEntityMono>();
        if (entityMono) {
            var lootEntity = entityMono.GetEntity();
            lootEntity.EmitSignal(GetSignal());
        }
    }

}