using System.Linq;

public class EliteSelectRule<T> : SelectRule<T> where T : struct
{
  public override Genom<T>[] Select(Genom<T>[] target, int size)
  {
    return target.Take(size).ToArray();
  }
}