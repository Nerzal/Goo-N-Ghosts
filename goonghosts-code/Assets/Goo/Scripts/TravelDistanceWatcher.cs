using Events;
using UnityEngine;
using Variables;

namespace Goo.Scripts {
  /// <summary>
  /// Fires the given event, when a certain threshold was traveled on the X axis
  /// </summary>
  public class TravelDistanceWatcher : MonoBehaviour {

    [SerializeField] private GameEvent _event;
    [SerializeField] private Vector2Variable _currentGoSpeed;
    [SerializeField] private float _threshold = 1;

    private float _lastX;

    /// <summary>
    /// Traveled since last event invocation
    /// </summary>
    private float _deltaX;

    private void Start() {
      this._lastX = this.transform.position.x;
    }

    // Update is called once per frame
    private void Update() {
      UpdateDeltaX();
      CheckDeltaXForThreshold();
      this._lastX = this.transform.position.x;
    }

    private void CheckDeltaXForThreshold() {
      if (!(this._deltaX >= this._threshold)) {
        return;
      }

      this._event.Raise();
      this._deltaX = 0;
    }

    private void UpdateDeltaX() {
      if (this._currentGoSpeed.Value.x > 0) {
        this._deltaX += this.transform.position.x - this._lastX;
      } else {
        this._deltaX += this._lastX - this.transform.position.x;
      }
    }
  }
}
