using UnityEngine;
using Transform = UnityEngine.Transform;

[CreateAssetMenu(menuName = "Definitions/Player")]
public class PlayerDef : GenericEntityDefinition<PlayerDef> {

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject cellMarker;
    [SerializeField]
    private Transform container;
    public float Speed => speed;
    public GameObject CellMarker => cellMarker;
    public Transform Container => container;

}