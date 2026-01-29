using UnityEngine;

public abstract class InteractableDef2<T> : GenericEntityDefinition<T>, IInteractable where T : InteractableDef2<T> {

    [SerializeField]
    private Texture2D[] cursor;
    public Texture2D[] Cursor => cursor.Length > 0 ? cursor : null;

}

public interface IInteractable {

    Texture2D[] Cursor { get; }

}