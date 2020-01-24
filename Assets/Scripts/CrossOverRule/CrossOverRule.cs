using UnityEngine;

public abstract class CrossOverRule<T> : ICrossOverRule<T> where T : struct
{
  public abstract void CrossOverDna(Genom<T> a, Genom<T> b, Genom<T> child);
}