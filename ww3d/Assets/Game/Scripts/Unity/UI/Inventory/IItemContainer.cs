public interface IItemContainer {

    int MaxAllowed { get; }
    int CurrentAmount { get; }

    bool IsAvailableSlot() => CurrentAmount < MaxAllowed;

}