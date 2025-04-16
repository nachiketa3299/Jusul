using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{

  [CreateAssetMenu(fileName = "SckillEnhanceRateTable", menuName = "Jusul/SkillEnhanceRateTable")]
  public class SkillEnhanceRateTable : ScriptableObject
  {
    [Serializable]
    public class PerLevelEnhanceRateEntry
    {
      public float RockEnhanceRate;
      public float WaterEnhanceRate;
      public float FireEnhanceRate;
    }

    public bool TryGetCurrentAttributeEnhanceRate(in int currentLevel, in SkillAttribute attribute, out float rate)
    {
      rate = 0f;

      int listIndex = currentLevel + 1;

      if (listIndex < RateAtLevel.Count)
      {
        PerLevelEnhanceRateEntry entry = RateAtLevel[listIndex];
        rate = attribute switch
        {
          SkillAttribute.Rock => entry.RockEnhanceRate,
          SkillAttribute.Fire => entry.FireEnhanceRate,
          SkillAttribute.Water => entry.WaterEnhanceRate,
          _ => 0f
        };
        return true;
      }
      else
      {
        return false;
      }
    }

    [Header("Index + 1 레벨에서 데미지 비율에 대한 리스트")][Space]
    public List<PerLevelEnhanceRateEntry> RateAtLevel = new();
  }
}