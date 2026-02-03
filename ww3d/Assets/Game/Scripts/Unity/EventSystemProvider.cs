using UnityEngine.EventSystems;

[LevelScope]
public class DefaultEventSystem : ISelfRegisterable, IEventSystem {

    private EventSystem Get() => EventSystem.current;

    public bool IsPointerOverGameObject() => Get().IsPointerOverGameObject();

}