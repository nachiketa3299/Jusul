using UnityEngine;

namespace Jusul
{
  public class EnhanceButton_PurchaseLevelEnhanceButton : EnhanceButtonBase
  {
    [SerializeField] SkillModule _skillModule;

    void OnSkillPurchaseLevelInitialized(int initLevel)
    {
      _currentLevelText.text = initLevel.ToString();

      if (_enhanceModule.TryGetNextSkillPurchaseEnhanceCost(initLevel + 1, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costResources.GetIconByCostType(_costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    void OnSkillPurchaseLevelChanged(int prevLevel, int currentLevel)
    {
      _currentLevelText.text = currentLevel.ToString();

      if (_enhanceModule.TryGetNextSkillPurchaseEnhanceCost(currentLevel + 1, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costResources.GetIconByCostType(_costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    void OnMaxSkillPurchaseLevelReached(int maxLevel)
    {
      _button.onClick.RemoveAllListeners();

      _nextLevelCostText.text = "MAX";
      _currentLevelText.text = maxLevel.ToString();
    }

    protected override void OnClick()
    {
      PlayerController.Instance.TryEnhanceSkillPurchaseLevelByUI();
    }

    public override void InitializeOnAwake()
    {
      base.InitializeOnAwake();

      _skillModule.SkillPurchaseLevelInitialized += OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged += OnSkillPurchaseLevelChanged;

      _enhanceModule.MaxSkillPurchaseLevelReached += OnMaxSkillPurchaseLevelReached;
    }

    protected override void OnDestroy()
    {
      _skillModule.SkillPurchaseLevelInitialized -= OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged -= OnSkillPurchaseLevelChanged;

      _enhanceModule.MaxSkillPurchaseLevelReached -= OnMaxSkillPurchaseLevelReached;
    }
  }
}