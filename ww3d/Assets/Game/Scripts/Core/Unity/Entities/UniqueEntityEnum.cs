using System.Collections.Generic;

public enum UniqueEntityEnum {

    Camera = 200,
    Inventory = 400

}

public static class UniqueEntityEnumExtensions {

    private static readonly Dictionary<UniqueEntityEnum, string> map = new()
    {
        { UniqueEntityEnum.Camera, "CAMERA" },
        { UniqueEntityEnum.Inventory, "INVENTORY" }
    };

    public static string GetValue(this UniqueEntityEnum type) => map.GetValueOrDefault(type, "Unknown");

}