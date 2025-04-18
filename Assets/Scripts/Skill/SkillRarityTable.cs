using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// 스킬의 구매시 랜덤 뽑기 확률을 관리,
  /// 레벨이 존재하며, 뽑기 확률이 달라짐
  /// </summary>
  [CreateAssetMenu(fileName = "SkillRarityTable", menuName = "Jusul/Skill/SkillRarityTable")]
  public class SkillRarityTable : ScriptableObject
  {
    [Serializable]
    public class RarityEntry
    {
      public SkillRarity Rarity;
      public float Weight;
    }

    [Serializable]
    public class ProbabilityEntry
    {
      // NOTE: 모든 Entry의 값 합은 1.0이 되어야 한다.
      public List<RarityEntry> Entries;
    }

    [Header("뽑기 레벨 n - 1 에서의 확률")][Space]
    public List<ProbabilityEntry> Probabilities;

    /// <summary>
    /// level에 맞는 스킬을 뽑는다.
    /// </summary>
    public SkillRarity PickWithLevel(int level)
    {
      ProbabilityEntry probability = Probabilities[level];

      float totalWeight = probability.Entries.Sum(entry => entry.Weight);
      float randomValue = UnityEngine.Random.Range(0.0f, totalWeight);
      float cummulativeWeight = 0.0f;

      foreach (var entry in probability.Entries)
      {
        cummulativeWeight += entry.Weight;

        if (randomValue <= cummulativeWeight)
        {
          return entry.Rarity;
        }
      }

      Debug.Log("확률을 발견하지 못했습니다.");
      return probability.Entries.Last().Rarity;
    }
  }
}