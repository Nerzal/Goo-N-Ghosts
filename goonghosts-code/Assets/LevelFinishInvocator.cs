using Events;
using UnityEngine;

public class LevelFinishInvocator : MonoBehaviour {

  [SerializeField] private GameEvent _levelCompleted;
  private bool _eventRaised = false;


  private void OnTriggerEnter2D(Collider2D collision) {
    if (!collision.transform.CompareTag("Player")) {
      return;
    }

    if (this._eventRaised) {
      return;
    }

    this._eventRaised = true;
    this._levelCompleted.Raise();
  }
}
