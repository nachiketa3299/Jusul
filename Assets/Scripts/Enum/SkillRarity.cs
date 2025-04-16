using System;

namespace Jusul
{
  /// <summary>
  /// 스킬의 희귀도를 나타낸다.
  /// </summary>
  [Serializable]
  public enum SkillRarity
  {
    /// <summary>
    /// 일반
    /// </summary>
    Normal = 0, 

    /// <summary>
    /// 희귀
    /// </summary>
    Rare, 

    /// <summary>
    /// 영웅
    /// </summary>
    Hero, 

    /// <summary>
    /// 전설
    /// </summary>
    Legend, 

    /// <summary>
    /// 선조
    /// </summary>
    Ancestor, 

    /// <summary>
    /// 천벌
    /// </summary>
    Scourge
  }
}