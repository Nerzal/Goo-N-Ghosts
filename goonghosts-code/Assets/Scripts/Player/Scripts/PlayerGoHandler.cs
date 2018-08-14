using Events;
using UnityEngine;

namespace Player.Scripts {
  public class PlayerGoHandler : MonoBehaviour {

    [SerializeField] private GameEvent _playerDiedEvent;

    private void OnCollisionEnter2D(Collision2D collision) {
      if (!collision.transform.CompareTag("Goo")) {
        return;
      }

      this._playerDiedEvent.Raise();
    }
  }
}