using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu]
  [Serializable]
  public class TextureVariable : BaseVariable<Texture> {
    /// <inheritdoc />
    public override bool Equals(Texture other) {
      return this.Value.Equals(other);
    }
  }
}