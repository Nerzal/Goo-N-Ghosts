using System;
using UnityEngine;

namespace Variables {
  [Serializable]
  public abstract class BaseVariable<TType> : ScriptableObject, IEquatable<TType> {
    public Action<TType> ValueChanged;

#if UNITY_EDITOR
    [NonSerialized]
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public TType Value;

    public virtual void SetValue(TType value) {
      if (Equals(value)) {
        return;
      }

      this.Value = value;
      InvokeChange(value);
    }

    protected void InvokeChange(TType value) {
      this.ValueChanged?.Invoke(value);
    }

    /// <inheritdoc />
    public abstract bool Equals(TType other);
  }
}