using System.Linq;
using UnityEngine;

public class RouletteSelectRule<T> : SelectRule<T> where T : struct
{
  public override Genom<T>[] Select(Genom<T>[] target, int size)
  {
    var res = target.Take(size).ToArray();
    float sum = 0;
    for (int i = 0; i < target.Length; i++)
    {
      sum += target[i].Score;
    }
    for (int i = 0; i < size; i++)
    {
      float p = Random.Range(0, sum);
      float s = 0.0f;
      for (int j = 0; j < target.Length; j++)
      {
        s += sum - target[j].Score;
        if (s < p)
        {
          res[i] = target[j];
          continue;
        }
      }
    }
    return res;
  }
}