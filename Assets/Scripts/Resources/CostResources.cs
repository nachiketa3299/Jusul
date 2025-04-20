using System;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "CostResources", menuName = "Jusul/CostResources")]
  public class CostResources : ScriptableObject
  {

    [Serializable]
    public class CostResourceEntry
    {
      public Color Color;
      public Sprite Sprite;
      public string DisplayName;
    }

    [Header("0번은 Soul, 1번은 Gold")][Space]
    public List<CostResourceEntry> Resources = new();

    [Header("아이콘")][Space]
    public Sprite IconOnGold;
    public Sprite IconOnSoul;

    [Header("컬러")][Space]
    public Color ColorOnGold;
    public Color ColorOnSoul;

    public Color GetColorByCostType(CostType costType)
    {
      return costType switch
      {
        CostType.Soul => ColorOnSoul,
        CostType.Gold => ColorOnGold,
        _ => ColorOnGold
      };
    }

    public Sprite GetIconByCostType(CostType costType)
    {
      return costType switch
      {
        CostType.Soul => IconOnSoul,
        CostType.Gold => IconOnGold,
        _ => IconOnGold
      };
    }
  }
}