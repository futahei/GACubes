public abstract class SelectRule<T> : ISelectRule<T> where T : struct
{
  public abstract Genom<T>[] Select(Genom<T>[] target, int size);
}