using UnityEngine;

public struct DropToCraftContainerSignal : ITransform {

    public Transform Transform { get; set; }

}

public interface ITransform {

    Transform Transform { get; }

}