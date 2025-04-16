using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class EnhanceModule : MonoBehaviour
  {
    [SerializeField] EnhanceCostTable _enhanceCostTable;

    public void InitializeOnStart()
    {
    }

    public bool TryGetNextAttributeEnhanceCost(in int nextLevel, in SkillAttribute attribute, out CostType costType, out int cost)
    {
      return _enhanceCostTable.TryGetNextAttributeEnhanceCost(nextLevel, attribute, out costType, out cost);
    }
  }
}