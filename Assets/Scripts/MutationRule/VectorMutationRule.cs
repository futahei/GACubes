using UnityEngine;

public class VectorMutationRuls : MutationRule<Vector2>
{
  public override void Mutate(Genom<Vector2> genom)
  {
    int index = Random.Range(0, genom.Length);
    genom.Dna[index] = MutateDna(genom.Dna[index]);
  }

  public override Vector2 MutateDna(Vector2 dna)
  {
    return dna * -1;
  }
}