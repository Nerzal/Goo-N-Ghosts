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
  public class StringVariable : BaseVariable<string> {
    /// <inheritdoc />
    public override bool Equals(string other) {
      return String.Compare(this.Value, other, StringComparison.Ordinal) == 0;
    }
  }
}