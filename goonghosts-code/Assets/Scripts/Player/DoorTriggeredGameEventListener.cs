using Events;
using UnityEngine;

namespace Player {
  public class DoorTriggeredGameEventListener : GenericGameEventListener<DoorTrigger> {
    [SerializeField]
    private DoorTriggeredGameEvent _event;

    [SerializeField]
    private DoorTriggeredUnityGameEvent _unityEvent;


    private void OnEnable() {
      this._event.RegisterListener(this);
    }

    private void OnDisable() {
      this._event.UnregisterListener(this);
    }

    public override void OnEventRaised(DoorTrigger arg) {
      this._unityEvent?.Invoke(arg);
    }
  }
}