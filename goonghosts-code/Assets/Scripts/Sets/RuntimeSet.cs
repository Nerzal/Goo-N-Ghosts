using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified for LD42 Jam
// Author: Nerzal
// ----------------------------------------------------------------------------

namespace Sets {
  public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T> {
    public List<T> Items = new List<T>();
    public int Index = -1;

    public Action ItemsChanged;

    public T this[int i] {
      get { return this.Items[i]; }
      set { this.Items[i] = value; }
    }

    public void Initialize() {
      this.Items = new List<T>();
      this.Index = -1;
      this.ItemsChanged?.Invoke();
    }

    public virtual void Add(T thing) {
      if (this.Items.Contains(thing)) {
        return;
      }

      this.Items.Add(thing);
      this.Index++;
      this.ItemsChanged?.Invoke();
    }

    public virtual void Remove(T thing) {
      if (!this.Items.Contains(thing)) {
        return;
      }

      this.Items.Remove(thing);
      this.Index--;
      this.ItemsChanged?.Invoke();
    }

    public virtual void Clear() {
      this.Items.Clear();
      this.ItemsChanged?.Invoke();
    }

    public bool Contains(T item) {
      foreach (T item1 in this.Items) {
        if (item1.Equals(item)) {
          return true;
        }
      }

      return false;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() {
      return this.Items.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }
}