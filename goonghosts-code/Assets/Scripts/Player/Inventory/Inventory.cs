using System;
using Sets;
using UnityEngine;

namespace Player.Inventory {
  [CreateAssetMenu]
  public class Inventory : RuntimeSet<Item> {
    /// <inheritdoc />
    public override void Add(Item thing) {
      this.Items.Add(thing);
      this.Index++;
      this.ItemsChanged?.Invoke();
    }

    public int Count(Item thing) {
      int result = 0;
      foreach (Item item in this.Items) {
        if (String.CompareOrdinal(item.Description, thing.Description) == 0) {
          result++;
        }
      }

      return result;
    }
  }
}