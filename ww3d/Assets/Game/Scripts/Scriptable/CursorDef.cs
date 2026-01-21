using UnityEngine;

[CreateAssetMenu(menuName = "Definitions/Cursor", fileName = "Cursor")]
public class CursorDef : EntityDefinition2<CursorDef> {

    [SerializeField]
    private Texture2D lookUp;
    public Texture2D LookUp => lookUp;

}