using Events;
using UnityEngine;

namespace Riddles.Memory.Scripts {
  class SpriteChangerGameEventListener : GenericGameEventListener<SpriteChanger> {

    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField]
    private SpriteChangerUnityEvent _unityEvent;

    [Tooltip("Event to register with.")]
    [SerializeField]
    private SpriteChangerGameEvent _event;

    private void OnEnable() {
      this._event?.RegisterListener(this);
    }

    private void OnDisable() {
      this._event?.UnregisterListener(this);
    }

    public override void OnEventRaised(SpriteChanger arg) {
      this._unityEvent?.Invoke(arg);
    }
  }
}