using UnityEngine;

public class TwoPointCrossOverRule<T> : CrossOverRule<T> where T : struct
{
  public override void CrossOverDna(Genom<T> a, Genom<T> b, Genom<T> child)
  {
    int crossPoint1 = Random.Range(0, a.Length - 1);
    int crossPoint2 = Random.Range(crossPoint1, a.Length);
    var childDna = new T[a.Length];
    for(var i = 0; i < childDna.Length; i++)
    {
      childDna[i] = i < crossPoint1 ? a.Dna[i] : i < crossPoint2 ? b.Dna[i] : a.Dna[i];
    }
    child.Dna = childDna;
  }
}