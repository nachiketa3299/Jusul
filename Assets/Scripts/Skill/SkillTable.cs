using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{

  [CreateAssetMenu(menuName = "Jusul/Skill/SkillTable", fileName = "SkillTable")]
  public class SkillTable : ScriptableObject
  {
    // 희귀도순으로 정렬되어 있다고 가정
    public List<SkillBase> RockSkills = new();
    public List<SkillBase> FireSkills = new();
    public List<SkillBase> WaterSkills = new();

    public IEnumerable<SkillBase> AllSkills
    {
      get 
      {
        foreach(SkillBase skill in RockSkills) 
        {
          yield return skill;
        }

        foreach(SkillBase skill in FireSkills) 
        {
          yield return skill;
        }

        foreach(SkillBase skill in WaterSkills) 
        {
          yield return skill;
        }
      }
    }

    public SkillBase GetSkill(SkillAttribute attribute, SkillRarity rarity)
    {
      switch (attribute)
      {
      case SkillAttribute.Rock:
        return RockSkills[(int)rarity];
      case SkillAttribute.Fire:
        return FireSkills[(int)rarity];
      case SkillAttribute.Water:
        return WaterSkills[(int)rarity];
      default:
        return null; // 이런 일이 일어나지 않는다고 가정
      }
    }
  }
}