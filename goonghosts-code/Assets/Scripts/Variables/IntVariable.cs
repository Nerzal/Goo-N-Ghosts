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
  public class IntVariable : BaseVariable<int> {
    public void SetValue(IntVariable value) {
      if (value.Equals(this.Value)) {
        return;
      }

      this.Value = value.Value;
      InvokeChange(this.Value);
    }

    public void ApplyChange(int amount) {
      if (amount == 0) {
        return;
      }

      this.Value += amount;
      InvokeChange(this.Value);
    }

    public void ApplyChange(IntVariable amount) {
      ApplyChange(amount.Value);
    }

    /// <inheritdoc />
    public override bool Equals(int other) {
      return this.Value == other;
    }
  }
}