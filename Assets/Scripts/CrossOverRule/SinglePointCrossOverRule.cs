using UnityEngine;

public class SinglePointCrossOverRule<T> : CrossOverRule<T> where T : struct
{
  public override void CrossOverDna(Genom<T> a, Genom<T> b, Genom<T> child)
  {
    int crossPoint = Random.Range(0, a.Length);
    var childDna = new T[a.Length];
    for(var i = 0; i < childDna.Length; i++)
    {
      childDna[i] = i < crossPoint ? a.Dna[i] : b.Dna[i];
    }
    child.Dna = childDna;
  }
}