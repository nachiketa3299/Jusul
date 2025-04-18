using System;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "EnhanceCostTable", menuName = "Jusul/EnhanceCostTable")]
  public class EnhanceCostTable : ScriptableObject
  {
    [Serializable]
    public class PerLevelCostEntry
    {
      public int PurchaseEnhanceCost;

      public int RockEnhanceCost;
      public int WaterEnhanceCost;
      public int FireEnhanceCost;

      public int CryptidEnhanceCost;
    }

    public bool TryGetNextPurchaseEnhanceCost(in int nextLevel, out CostType costType, out int cost)
    {
      costType = CostType.Soul;
      cost = 0;

      if (TryGetNextLevelEntry(nextLevel, out PerLevelCostEntry entry))
      {
        costType = _costMap[EnhanceType.PurchaseLevel];
        cost = entry.PurchaseEnhanceCost;
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool TryGetNextAttributeEnhanceCost(in int nextLevel, in SkillAttribute attribute, out CostType costType, out int cost)
    {
      costType = CostType.Soul;
      cost = 0;

      if (TryGetNextLevelEntry(nextLevel, out PerLevelCostEntry entry))
      {
        switch (attribute)
        {
          case SkillAttribute.Rock:
            costType = _costMap[EnhanceType.RockSkillLevel];
            cost = entry.RockEnhanceCost;
            break;
          case SkillAttribute.Fire:
            costType = _costMap[EnhanceType.FireSkillLevel];
            cost = entry.FireEnhanceCost;
            break;
          case SkillAttribute.Water:
            costType = _costMap[EnhanceType.WaterSkillLevel];
            cost = entry.WaterEnhanceCost;
            break;
        }
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool TryGetNextCryptidEnhanceCost(in int nextLevel, out CostType costType, out int cost)
    {
      costType = CostType.Soul;
      cost = 0;

      if (TryGetNextLevelEntry(nextLevel, out PerLevelCostEntry entry))
      {
        costType = _costMap[EnhanceType.CryptidLevel];
        cost = entry.CryptidEnhanceCost;
        return true;
      }
      else
      {
        return false;
      }
    }

    bool TryGetNextLevelEntry(in int nextLevel, out PerLevelCostEntry levelEntry)
    {
      levelEntry = null;

      // 예) Level n에 도달하기 위한 비용은 n - 2 번 인덱스에 저장되어 있음.
      int listIndex = nextLevel - 2;

      if (listIndex < CostAtLevel.Count)
      {
        levelEntry = CostAtLevel[listIndex];
        return true;
      }
      else
      {
        Debug.Log("다음 레벨 없습니다.");
        return false;
      }
    }

    Dictionary<EnhanceType, CostType> _costMap = new()
    {
      { EnhanceType.PurchaseLevel, CostType.Gold },
      { EnhanceType.RockSkillLevel, CostType.Soul },
      { EnhanceType.WaterSkillLevel, CostType.Soul },
      { EnhanceType.FireSkillLevel, CostType.Soul },
      { EnhanceType.CryptidLevel, CostType.Soul },
    };

    [Header("Index + 2 레벨에 도달하기 위한 비용에 대한 리스트")][Space]
    public List<PerLevelCostEntry> CostAtLevel = new();
  }
}