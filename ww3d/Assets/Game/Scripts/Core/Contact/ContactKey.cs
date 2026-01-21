using System;

public readonly struct ContactKey : IEquatable<ContactKey> {

    private readonly EntityType iAm;
    private readonly EntityType triggeredWith;

    public ContactKey(EntityType iAm, EntityType triggeredWith) {
        this.iAm = iAm;
        this.triggeredWith = triggeredWith;
    }

    public bool Equals(ContactKey other) => iAm == other.iAm && triggeredWith == other.triggeredWith;
    public override bool Equals(object obj) => obj is ContactKey other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(iAm, triggeredWith);

}