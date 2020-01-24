public interface IMutationRule<T> where T : struct
{
  void Mutate(Genom<T> genom);
  T MutateDna(T dna);
}