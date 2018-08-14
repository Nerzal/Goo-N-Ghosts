using Events;
using UnityEngine;

namespace Player.Inventory {
  public class PlayerPickupHandler : MonoBehaviour {
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameEvent _itemPickupEvent;

    private void OnTriggerEnter2D(Collider2D collision) {
      if (!collision.transform.CompareTag("Pickupable")) {
        return;
      }
      this._itemPickupEvent.Raise();
      Item thing = collision.gameObject.GetComponent<Pickupable>().Item;
      Debug.Log($"Player just picked up: {thing.name}");
      this._inventory.Add(thing);
      Destroy(collision.gameObject);
    }
  }
}