using System;
using System.Collections.Generic;

public readonly struct RecipeKey : IEquatable<RecipeKey> {

    private readonly string[] items;
    private readonly int hash;
    public int Count => items?.Length ?? 0;

    private RecipeKey(string[] normalizedSortedItems) {
        items = normalizedSortedItems;
        hash = ComputeHash(items);
    }

    public static RecipeKey FromNames(IEnumerable<string> ingredientNames) {
        if (ingredientNames == null) {
            throw new ArgumentNullException(nameof(ingredientNames));
        }

        var list = new List<string>();
        foreach (var n in ingredientNames) {
            if (string.IsNullOrEmpty(n)) {
                continue;
            }

            list.Add(n);
        }

        if (list.Count == 0) {
            throw new ArgumentException("RecipeKey can't be null.", nameof(ingredientNames));
        }

        list.Sort(StringComparer.Ordinal);

        return new RecipeKey(list.ToArray());
    }

    public static RecipeKey FromLoot(IEnumerable<LootDef> ingredients) {
        if (ingredients == null) {
            throw new ArgumentNullException(nameof(ingredients));
        }

        var list = new List<string>();
        foreach (var ing in ingredients) {
            if (ing == null) {
                continue;
            }

            list.Add(ing.EntityName);
        }

        if (list.Count == 0) {
            throw new ArgumentException("RecipeKey can't be empty", nameof(ingredients));
        }

        list.Sort(StringComparer.Ordinal);
        return new RecipeKey(list.ToArray());
    }

    public bool Equals(RecipeKey other) {
        if (Count != other.Count) {
            return false;
        }

        if (hash != other.hash) {
            return false;
        }

        for (var i = 0; i < items.Length; i++) {
            if (!string.Equals(items[i], other.items[i], StringComparison.Ordinal)) {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object obj) => obj is RecipeKey other && Equals(other);

    public override int GetHashCode() => hash;

    public static bool operator ==(RecipeKey left, RecipeKey right) => left.Equals(right);

    public static bool operator !=(RecipeKey left, RecipeKey right) => !left.Equals(right);

    public override string ToString()
        => items == null ? "[]" : $"[{string.Join(", ", items)}]";

    private static int ComputeHash(string[] items) {
        var hc = new HashCode();
        hc.Add(items.Length);
        for (var i = 0; i < items.Length; i++) {
            hc.Add(items[i], StringComparer.Ordinal);
        }

        return hc.ToHashCode();
    }

}