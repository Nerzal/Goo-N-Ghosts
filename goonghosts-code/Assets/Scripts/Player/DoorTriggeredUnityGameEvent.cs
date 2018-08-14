using System;
using UnityEngine.Events;

namespace Player {
  [Serializable]
  public class DoorTriggeredUnityGameEvent : UnityEvent<DoorTrigger> {

  }
}