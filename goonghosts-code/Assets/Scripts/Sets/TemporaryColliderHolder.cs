using UnityEngine;

namespace Sets {
  [CreateAssetMenu]
  public class TemporaryColliderHolder : RuntimeSet<GameObject> {
    public override void Clear() {
      foreach (GameObject gameObject in this.Items) {
        Destroy(gameObject);
      }
      base.Clear();
    }
  }
}