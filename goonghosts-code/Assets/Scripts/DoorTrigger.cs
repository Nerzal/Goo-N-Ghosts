using Player;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
  private Animator _animator;
  [SerializeField]
  private DoorTriggeredGameEvent _doorActivated;

  private BoxCollider2D[] _colliders;

  private void Start() {
    this._animator = GetComponentInChildren<Animator>();
    this._colliders = GetComponents<BoxCollider2D>();
  }

  public void OpenDoor() {
    this._animator.SetTrigger("Open");
    foreach (BoxCollider2D boxCollider2D in this._colliders) {
      boxCollider2D.enabled = false;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) {
      this._doorActivated.Raise(this);
    }
  }
}
