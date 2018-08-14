using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu]
  [Serializable]
  public class BoolVariable : BaseVariable<bool> { 
    public void SetValue(BoolVariable value) {
      if (Equals(value.Value)) {
        return;
      }

      this.Value = value.Value;
      InvokeChange(value);
    }

    /// <inheritdoc />
    public override bool Equals(bool other) {
      return this.Value == other;
    }
  }
}