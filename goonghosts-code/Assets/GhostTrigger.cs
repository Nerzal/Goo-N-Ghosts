using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class GhostTrigger : MonoBehaviour {
  [SerializeField]
  private GameEvent _triggerGhost;

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Player")) {
      return;
    }

    this._triggerGhost.Raise();
  }
}
