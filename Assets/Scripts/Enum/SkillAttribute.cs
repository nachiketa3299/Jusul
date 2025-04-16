using System;

namespace Jusul
{
  /// <summary>
  /// 스킬의 속성을 나타낸다.
  /// </summary>
  [Serializable]
  public enum SkillAttribute
  {
    /// <summary>
    /// 바위 속성
    /// </summary>
    Rock=0, 

    /// <summary>
    /// 불 속성
    /// </summary>
    Fire,

    /// <summary>
    /// 물 속성
    /// </summary>
    Water
  }
}