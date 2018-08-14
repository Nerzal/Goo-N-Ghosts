using Events;
using UnityEngine;

namespace Player {
  [CreateAssetMenu]
  public class DoorTriggeredGameEvent : GenericGameEvent<DoorTrigger> {
  }
}
