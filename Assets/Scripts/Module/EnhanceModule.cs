using System;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class EnhanceModule : MonoBehaviour
  {
    [SerializeField] EnhanceCostTable _enhanceCostTable;

    public event Action<int> MaxSkillPurchaseLevelReached;
    public event Action<SkillAttribute, int> MaxSkillAttributeLevelReached;

    public void InitializeOnStart()
    {
    }

    /// <summary>
    /// 다음 강화 비용을 가져오려고 시도한다. 강화 비용을 가져올 수 없으면, 이벤트를 발생시킨다.
    /// </summary>
    public bool TryGetNextSkillPurchaseEnhanceCost(in int nextLevel, out CostType costType, out int cost)
    {
      bool result = _enhanceCostTable.TryGetNextPurchaseEnhanceCost(nextLevel, out costType, out cost);

      if (!result)
      {
        MaxSkillPurchaseLevelReached?.Invoke(nextLevel - 1);
      }

      return result;
    }

    /// <summary>
    /// 다음 강화 비용을 가져오려고 시도한다. 강화 비용을 가져올 수 없으면, 이벤트를 발생시킨다.
    /// </summary>
    public bool TryGetNextAttributeEnhanceCost(in int nextLevel, in SkillAttribute attribute, out CostType costType, out int cost)
    {
      bool result = _enhanceCostTable.TryGetNextAttributeEnhanceCost(nextLevel, attribute, out costType, out cost);

      if (!result)
      {
        MaxSkillAttributeLevelReached?.Invoke(attribute, nextLevel - 1);
      }

      return result;
    }
  }
}