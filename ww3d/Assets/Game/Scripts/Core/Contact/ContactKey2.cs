using System;

public readonly struct ContactKey2 : IEquatable<ContactKey2> {

    private readonly Type iAm;
    private readonly Type triggeredWith;

    public ContactKey2(Type iAm, Type triggeredWith) {
        this.iAm = iAm;
        this.triggeredWith = triggeredWith;
    }

    public bool Equals(ContactKey2 other) => iAm == other.iAm && triggeredWith == other.triggeredWith;

    public override bool Equals(object obj) => obj is ContactKey other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(iAm, triggeredWith);

}