interface ICrossOverRule<T> where T : struct
{
  void CrossOverDna(Genom<T> a, Genom<T> b, Genom<T> child);
}