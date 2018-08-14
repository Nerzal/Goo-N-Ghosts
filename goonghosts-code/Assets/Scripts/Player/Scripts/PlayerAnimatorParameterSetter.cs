using UnityEngine;
using Variables;

namespace Player.Scripts {
  public class PlayerAnimatorParameterSetter : MonoBehaviour {

    [SerializeField] private Vector2Variable _playerCurrentMovementSpeed;
    [SerializeField] private Animator _animator;
    private bool _landedBefore = false;

    // Use this for initialization
    private void Start() {
      this._playerCurrentMovementSpeed.XChanged += XChanged;
      this._playerCurrentMovementSpeed.YChanged += YChanged;
    }

    private void YChanged(float obj) {
      if (Mathf.Abs(obj) <= 0.1f) {
        if (this._landedBefore) {
          return;
        }
        PlayerLanded();
      } else {
        this._landedBefore = false;
      }
    }

    private void XChanged(float obj) {
      MovementSpeedChanged(obj);
    }

    public void PlayerDied(bool obj) {
      if (this._animator == null) {
        return;
      }
      this._animator.SetTrigger("Died");
    }

    public void PlayerJumped(bool obj) {
      if (this._animator == null) {
        return;
      }
      this._animator.SetTrigger("Jump");
    }

    public void PlayerLanded() {
      if (this._animator == null) {
        return;
      }

      this._landedBefore = true;
      this._animator.SetTrigger("Landed");
    }

    private void MovementSpeedChanged(float obj) {
      if (this._animator == null) {
        return;
      }
      this._animator.SetFloat("MovementSpeed", Mathf.Abs(obj));
    }
  }
}