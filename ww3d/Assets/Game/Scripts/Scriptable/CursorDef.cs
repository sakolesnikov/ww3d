using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Cursor", fileName = "Cursor")]
public class CursorDef : GenericEntityDefinition<CursorDef> {

    [SerializeField]
    private Texture2D lookUp;
    public Texture2D LookUp => lookUp;

}