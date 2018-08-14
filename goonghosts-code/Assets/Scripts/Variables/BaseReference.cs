using System;

namespace Variables {
  [Serializable]
  public abstract class BaseReference<TType, TVariable> where TVariable : BaseVariable<TType> {
    public bool UseConstant = true;
    public TType ConstantValue;
    public TVariable Variable;

    protected BaseReference() { }

    protected BaseReference(TType value) {
      this.UseConstant = true;
      this.ConstantValue = value;
    }

    public TType Value {
      get { return this.UseConstant ? this.ConstantValue : this.Variable.Value; }
    }

    public static implicit operator TType(BaseReference<TType, TVariable> reference) {
      return reference.Value;
    }
  }
}