using UnityEngine;

namespace Player.Scripts {
  [RequireComponent(typeof(PlayerMovementController))]
  public class PlayerMovementControllerManager : MonoBehaviour {
    private PlayerMovementController _playerMovementController;

    private void Start() {
      this._playerMovementController = GetComponent<PlayerMovementController>();
    }

    public void OnRiddleStarted() {
      this._playerMovementController.enabled = false;
    }

    public void OnRiddleFinished() {
      this._playerMovementController.enabled = true;
    }
  }
}
