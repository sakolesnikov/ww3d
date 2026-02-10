using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.UI;

public class ImageComponentProvider : MonoBehaviour, IEntityAware {

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new ImageComponent { Value = gameObject.GetComponent<Image>() });
    }

}