public abstract class MutationRule<T> : IMutationRule<T> where T : struct
{
  public abstract void Mutate(Genom<T> genom);
  public abstract T MutateDna(T dna);
}