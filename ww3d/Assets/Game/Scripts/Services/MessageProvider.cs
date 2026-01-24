using UnityEngine;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class MessageProvider : ISelfRegisterable {

    [Inject]
    [Key(nameof(MessageDef))]
    private readonly ObjectPool<AbstractEntityMono> messagePool;

    public AbstractEntityMono Create(Transform parent, string key) {
        var messageMono = messagePool.Get();
        messageMono.transform.SetParent(parent, false);
        var messageEntity = messageMono.GetEntity();
        var localizeString = messageEntity.GetComponent<LocalizeStringComponent>().Value;
        localizeString.SetTable(Constants.LOCALIZATION_TABLE_NAME);
        localizeString.SetEntry(key);
        return messageMono;
    }

}