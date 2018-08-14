using Events;
using Player.Inventory;
using UnityEngine;

public class AtticDoorController : MonoBehaviour {
  private Animator _animator;

  [SerializeField] private BoxCollider2D _openTrigger;
  [SerializeField] private BoxCollider2D _closeTrigger;
  [SerializeField] private BoxCollider2D _doorCollider;
  [SerializeField] private Inventory _playerInventory;
  [SerializeField] private Item _itemToOpen;

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

    if (this._itemToOpen == null) {
      return;
    }

    if (this._playerInventory.Contains(this._itemToOpen)) {
      this._playerInventory.Remove(this._itemToOpen);
      this._atticDoorTriggered.Raise();
      ChangeDoorState();
    } else {
      this._missingItemEvent.Raise(this._itemToOpen.Description);
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

      this._itemToOpen = null;
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