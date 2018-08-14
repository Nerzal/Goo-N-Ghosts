using System;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified for LDJAM42
// Author: Nerzal
// ----------------------------------------------------------------------------

namespace Variables {
  [CreateAssetMenu]
  [Serializable]
  public class FloatVariable : BaseVariable<float> {

    public void SetValue(FloatVariable value) {
      if (Equals(value.Value)) {
        return;
      }

      this.Value = value.Value;
      InvokeChange(this.Value);
    }

    public void ApplyChange(float amount) {
      if (amount.Equals(0)) {
        return;
      }

      this.Value += amount;
      InvokeChange(this.Value);
    }

    public void ApplyChange(FloatVariable amount) {
      ApplyChange(amount.Value);
    }

    /// <inheritdoc />
    public override bool Equals(float other) {
      return other.Equals(this.Value);
    }
  }
}