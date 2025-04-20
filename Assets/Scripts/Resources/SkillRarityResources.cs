using System;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "SkillRarityResources", menuName = "Jusul/Skill/SkillRarityResources")]
  public class SkillRarityResources : ScriptableObject
  {
    [Serializable]
    public class RarityResourceEntry
    {
      public Color Color;
      public string DisplayName;
    }

    [Header("희귀도 순으로 0번 부터 5번")][Space]
    public List<RarityResourceEntry> _resources = new();

    public Color GetColorBySkillRarity(SkillRarity rarity)
    {
      return _resources[(int)rarity].Color;
    }
    public string GetDisplayNameBySkillRarity(SkillRarity rarity)
    {
      return _resources[(int)rarity].DisplayName;
    }
  }
}