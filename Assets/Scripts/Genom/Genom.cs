using System.Collections.Generic;

public abstract class Genom<T> : IGenom where T : struct
{
  public float Score;
  public int Length;
  public T[] Dna;

  public Genom(int dnaLength)
  {
    Length = dnaLength;
    MakeDna(Length);
  }

  public Genom(Genom<T> baseGenom)
  {
    Length = baseGenom.Length;
    Dna = baseGenom.Dna;
  }

  public abstract void MakeDna(int size);
}