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
      if (attribute != _attributeToEnhance)
      {
        return;
      }

      _currentLevelText.text = initLevel.ToString();

      if (_enhanceModule.TryGetNextAttributeEnhanceCost(initLevel + 1, attribute, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costIcons.GetIconByCostType(costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    void OnSkillAttributeLevelChanged(SkillAttribute attribute, int prevLevel, int currentLevel)
    {
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

    void UpdateTextColor(CostType costType, int amount)
    {
      if (_costType == costType)
      {
        if (_nextLevelCost <= amount) 
        {
          _nextLevelCostText.color = Color.white;
        }
        else 
        {
          _nextLevelCostText.color = Color.red;
        }
      }
    }

    void OnGoldAmountChanged(int prevAmount, int currentAmount)
    {
      UpdateTextColor(CostType.Gold, currentAmount);
    }

    void OnSoulAmountChanged(int prevAmount, int currentAmount)
    {
      UpdateTextColor(CostType.Soul, currentAmount);
    }

    void OnClick()
    {
      PlayerController.Instance.TryEnhanceSkillAttributeLevelByUI(_attributeToEnhance);
    }

    void Awake()
    {
      _button.onClick.AddListener(OnClick);

      _skillModule.SkillAttributeLevelInitialized += OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged += OnSkillAttributeLevelChanged;

      _resourceModule.GoldAmountChanged += OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged += OnSoulAmountChanged;
    }

    void OnDestroy()
    {
      _button.onClick.RemoveAllListeners();

      _skillModule.SkillAttributeLevelInitialized -= OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged -= OnSkillAttributeLevelChanged;

      _resourceModule.GoldAmountChanged -= OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged -= OnSoulAmountChanged;
    }
  }
}