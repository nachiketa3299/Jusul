using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class SkillTooltipPanel : MonoBehaviour
  {
    [Header("리소스")][Space]
    [SerializeField] SkillRarityResources _skillRarityResources;

    [Header("스킬 아이콘 UI")][Space]
    [SerializeField] Image _icon;

    [Header("스킬 이름 UI")][Space]
    [SerializeField] TMP_Text _skillName;

    [Header("희귀도 UI")][Space]
    [SerializeField] TMP_Text _skillRarityText;
    [SerializeField] Image _skillRarityBackground;

    [Header("설명 UI")][Space]
    [SerializeField] TMP_Text _description;

    [Header("공격 정보 UI")][Space]
    [SerializeField] TMP_Text _attackPower;
    [SerializeField] TMP_Text _attackType;
    [SerializeField] Image _attackAttributeIcon;
    [SerializeField] TMP_Text _attackAttribute;
    [SerializeField] TMP_Text _attackCooldown;

    [Header("Icons(바위/불/물)")]
    [SerializeField] List<Sprite> _attributeIcons = new();

    [Header("레이아웃 즉시 리빌드")]
    [SerializeField] RectTransform _flexingBox;

    public void InitializationOnAwake()
    {
      if (gameObject.activeSelf)
      {
        gameObject.SetActive(false);
      }
    }

    public string GetTypeName(SkillAttackType type) => type switch
    {
      SkillAttackType.Near => "근거리",
      SkillAttackType.Mid => "중거리",
      SkillAttackType.Far => "원거리",
      _ => "오류"
    };

    public string GetAttributeName(SkillAttribute attribute) => attribute switch
    {
      SkillAttribute.Rock => "바위",
      SkillAttribute.Fire => "불",
      SkillAttribute.Water => "물",
      _ => "오류"
    };

    public void SetSkillInfo(SkillBase skill)
    {
      _icon.sprite = skill.Icon;

      _skillName.text = skill.Name;

      _skillRarityText.text = _skillRarityResources.GetDisplayNameBySkillRarity(skill.Rarity);
      _skillRarityBackground.color = _skillRarityResources.GetColorBySkillRarity(skill.Rarity);

      _description.text = skill.Description;

      _attackPower.text = $"{skill.AttackPower}";
      _attackType.text = $"{GetTypeName(skill.AttackType)}";
      _attackAttribute.text = $"{GetAttributeName(skill.Attribute)}";
      _attackAttributeIcon.sprite = _attributeIcons[(int)skill.Attribute];
      _attackCooldown.text = $"{skill.Cooldown}s";

      LayoutRebuilder.ForceRebuildLayoutImmediate(_flexingBox);
    }
  }
}