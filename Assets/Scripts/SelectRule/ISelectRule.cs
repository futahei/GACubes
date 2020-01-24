public interface ISelectRule<T> where T : struct
{
  Genom<T>[] Select(Genom<T>[] target, int size);
}