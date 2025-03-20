using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "SkillPurchaseProbability", menuName = "Jusul/Skill/SkillPurchaseProbabillity")]
  public class SkillRarityProbability : ScriptableObject
  {
    [Serializable]
    public class Entry
    {
      public SkillRarity Rarity;
      public float Weight;
    }

    [Serializable]
    public class Probability
    {
      public List<Entry> Entries;
    }

    public List<Probability> Probabilities;

    public SkillRarity PickWithLevel(int level)
    {

      Probability probability = Probabilities[level];

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