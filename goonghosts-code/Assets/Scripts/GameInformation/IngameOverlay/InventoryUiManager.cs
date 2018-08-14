using System;
using Player.Inventory;
using TMPro;
using UnityEngine;

namespace GameInformation.IngameOverlay {
  public class InventoryUiManager : MonoBehaviour {
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Inventory _allThings;
    [SerializeField] private TextMeshProUGUI _redKeysText;
    [SerializeField] private TextMeshProUGUI _yellowKeysText;

    private void Start() {
      this._inventory.ItemsChanged += ItemsChanged;
    }

    private void ItemsChanged() {
      foreach (Item thing in this._inventory) {
        int thingCount = this._inventory.Count(thing);
      
        if (String.Compare(thing.Description, this._allThings[0].Description, StringComparison.Ordinal) == 0) {
          this._redKeysText.text = thingCount.ToString();
        }

        if (String.Compare(thing.Description, this._allThings[1].Description, StringComparison.Ordinal) == 0) {
          this._yellowKeysText.text = thingCount.ToString();
        }
      }
    }
  }
}
