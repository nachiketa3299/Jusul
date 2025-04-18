using UnityEngine;

namespace Jusul
{
  public class SkillAttributeLevelEnhanceButton : EnhanceButton
  {
    [SerializeField] SkillModule _skillModule;

    [Header("지정할 속성")][Space]
    [SerializeField] SkillAttribute _attributeToEnhance;

    void OnSkillAttributeLevelInitialized(SkillAttribute attribute, int initLevel)
    {
      // 이 버튼이 담당한 속성이 아니면 넘어가기
      if (attribute != _attributeToEnhance)
      {
        return;
      }

      _currentLevelText.text = initLevel.ToString();

      if (_enhanceModule.TryGetNextAttributeEnhanceCost(initLevel + 1, attribute, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costIcons.GetIconByCostType(_costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    void OnSkillAttributeLevelChanged(SkillAttribute attribute, int prevLevel, int currentLevel)
    {
      // 이 버튼이 담당한 속성이 아니면 넘어가기
      if (attribute != _attributeToEnhance)
      {
        return;
      }

      _currentLevelText.text = currentLevel.ToString();

      if (_enhanceModule.TryGetNextAttributeEnhanceCost(currentLevel + 1, attribute, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costIcons.GetIconByCostType(costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    protected override void OnClick()
    {
      PlayerController.Instance.TryEnhanceSkillAttributeLevelByUI(_attributeToEnhance);
    }

    protected override void Awake()
    {
      base.Awake();

      _skillModule.SkillAttributeLevelInitialized += OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged += OnSkillAttributeLevelChanged;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();

      _skillModule.SkillAttributeLevelInitialized -= OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged -= OnSkillAttributeLevelChanged;
    }
  }
}