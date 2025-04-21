namespace Jusul
{
  /// <summary>
  /// void 인 경우 아래를 사용
  /// </summary>
  public class Unit {}

  public interface IInitializeAfterInstantiation<T>
  {
    public void InitializeAfterInstantiation(T initData);
  }
}