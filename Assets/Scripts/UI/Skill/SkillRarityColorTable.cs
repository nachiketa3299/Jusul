using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "SkilRarityColorTable", menuName = "Jusul/Skill/SkillRarityColorTable")]
  public class SkillRarityColorTable : ScriptableObject
  {
    public Color ColorOnNormal;
    public Color ColorOnRare;
    public Color ColorOnHero;
    public Color ColorOnLegend;
    public Color ColorOnAncestor;
    public Color ColorOnScourge;

    public Color GetColorByRarity(SkillRarity rarity)
    {
      return rarity switch
      {
        SkillRarity.Normal => ColorOnNormal,
        SkillRarity.Rare => ColorOnRare,
        SkillRarity.Hero => ColorOnHero,
        SkillRarity.Legend => ColorOnLegend,
        SkillRarity.Ancestor => ColorOnAncestor,
        SkillRarity.Scourge => ColorOnScourge,
        _ => ColorOnNormal
      };
    }
  }
}