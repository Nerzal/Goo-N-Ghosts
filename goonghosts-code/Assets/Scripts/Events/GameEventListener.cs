using UnityEngine;
using UnityEngine.Events;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

namespace Events {
  public class GameEventListener : MonoBehaviour {
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void OnEnable() {
      this.Event.RegisterListener(this);
    }

    private void OnDisable() {
      this.Event.UnregisterListener(this);
    }

    public void OnEventRaised() {
      this.Response.Invoke();
    }
  }
}