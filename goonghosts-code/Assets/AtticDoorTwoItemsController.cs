using Events;
using Player.Inventory;
using UnityEngine;

public class AtticDoorTwoItemsController : MonoBehaviour {
  private Animator _animator;

  [SerializeField] private BoxCollider2D _openTrigger;
  [SerializeField] private BoxCollider2D _closeTrigger;
  [SerializeField] private BoxCollider2D _doorCollider;
  [SerializeField] private Inventory _playerInventory;
  [SerializeField] private Item _item1ToOpen;
  [SerializeField] private Item _item2ToOpen;

  [SerializeField] private GameEvent _atticDoorTriggered;
  [SerializeField] private StringGameEvent _missingItemEvent;

  private bool _doorState;

  // Use this for initialization
  void Start() {
    this._animator = GetComponent<Animator>();
    this._doorState = false;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Player")) {
      return;
    }

    if (this._playerInventory == null) {
      return;
    }

    if (this._item1ToOpen == null || this._item2ToOpen == null) {
      return;
    }

    if (this._doorState) {
      ChangeDoorState();
    } else {
      if (!this._playerInventory.Contains(this._item1ToOpen)) {
        this._missingItemEvent.Raise(this._item1ToOpen.Description);
        return;
      }

      if (!this._playerInventory.Contains(this._item2ToOpen)) {
        this._missingItemEvent.Raise(this._item2ToOpen.Description);
        return;
      }

      this._playerInventory.Remove(this._item2ToOpen);
      this._playerInventory.Remove(this._item1ToOpen);

      this._atticDoorTriggered.Raise();
      ChangeDoorState();
    }
  }

  private void ChangeDoorState() {
    if (!this._doorState) {
      this._animator.SetTrigger("Open");
      this._doorCollider.enabled = false;
      this._openTrigger.enabled = false;

      if (this._closeTrigger != null) {
        this._closeTrigger.enabled = true;
      }

      this._item2ToOpen = null;
      this._item1ToOpen = null;
    } else {
      this._animator.SetTrigger("Close");
      this._doorCollider.enabled = true;

      if (this._closeTrigger != null) {
        this._closeTrigger.enabled = false;
      }
    }

    this._doorState = !this._doorState;
  }
}