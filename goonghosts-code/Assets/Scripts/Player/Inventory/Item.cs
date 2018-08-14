using System;
using UnityEngine;

namespace Player.Inventory {
  [CreateAssetMenu]
  public class Item : ScriptableObject, IEquatable<Item>, IComparable<Item> {
    public Sprite Sprite;
    public string Description;

    /// <inheritdoc />
    public override bool Equals(object other) {
      return Equals((Item)other);
    }

    /// <inheritdoc />
    public bool Equals(Item other) {
      return this.CompareTo(other) == 0;
    }

    /// <inheritdoc />
    public int CompareTo(Item other) {
      int result = this.Sprite.GetHashCode().CompareTo(other.Sprite.GetHashCode());
      if (result != 0) {
        return result;
      }

      result = string.Compare(this.Description, other.Description, StringComparison.OrdinalIgnoreCase);
      return result;
    }
  }
}