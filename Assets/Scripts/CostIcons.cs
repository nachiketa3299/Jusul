using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "CostIcons", menuName = "Jusul/CostIcons")]
  public class CostIcons : ScriptableObject
  {
    public Sprite IconOnSoul;
    public Sprite IconOnGold;

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