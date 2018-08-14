using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu]
  [Serializable]
  public class Vector2Variable : BaseVariable<Vector2> {
    public Action<float> XChanged;
    public Action<float> YChanged;

    public override void SetValue(Vector2 value) {
      this.Value = value;

      this.XChanged?.Invoke(value.x);
      this.YChanged?.Invoke(value.y);
    }

    /// <inheritdoc />
    public override bool Equals(Vector2 other) {
      return this.Value.Equals(other);
    }
  }
}