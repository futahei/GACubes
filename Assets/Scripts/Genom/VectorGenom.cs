using UnityEngine;

public class VectorGenom : Genom<Vector2>
{
  public VectorGenom(int size) : base(size) { }
  public VectorGenom(VectorGenom baseGenom) : base(baseGenom) { }

  public override void MakeDna(int size)
  {
    Dna = new Vector2[size];

    for(int i = 0; i < size; i++)
    {
      Dna[i] = new Vector2(Random.Range(-10, 11), Random.Range(-10, 11));
    }
  }

  public Vector2 OutputDna()
  {
    var sum = Vector2.zero;
    for (int i = 0; i < Length; i++)
    {
      sum += Dna[i];
    }
    return sum;
  }
}