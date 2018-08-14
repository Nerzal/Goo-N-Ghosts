using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu]
  [Serializable]
  public class SpriteVariable : BaseVariable<Sprite> {
    /// <inheritdoc />
    public override bool Equals(Sprite other) {
      return this.Value.Equals(other);
    }
  }
}