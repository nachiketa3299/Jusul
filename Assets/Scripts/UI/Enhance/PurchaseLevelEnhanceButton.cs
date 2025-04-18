using UnityEngine;

namespace Jusul
{
  public class PurchaseLevelEnhanceButton : EnhanceButton
  {
    [SerializeField] SkillModule _skillModule;

    void OnSkillPurchaseLevelInitialized(int initLevel)
    {
      _currentLevelText.text = initLevel.ToString();

      if (_enhanceModule.TryGetNextPurchaseEnhanceCost(initLevel + 1, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costIcons.GetIconByCostType(_costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    void OnSkillPurchaseLevelChanged(int prevLevel, int currentLevel)
    {
      _currentLevelText.text = currentLevel.ToString();

      if (_enhanceModule.TryGetNextPurchaseEnhanceCost(currentLevel + 1, out CostType costType, out int nextLevelCost))
      {
        _costType = costType;
        _nextLevelCost = nextLevelCost;

        _costIcon.sprite = _costIcons.GetIconByCostType(_costType);
        _nextLevelCostText.text = nextLevelCost.ToString();
      }
    }

    protected override void OnClick()
    {
      PlayerController.Instance.TryEnhanceSkillPurchaseLevelByUI();
    }

    protected override void Awake()
    {
      base.Awake();

      _skillModule.SkillPurchaseLevelInitialized += OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged += OnSkillPurchaseLevelChanged;
    }

    protected override void OnDestroy()
    {
      base.Awake();

      _skillModule.SkillPurchaseLevelInitialized -= OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged -= OnSkillPurchaseLevelChanged;
    }
  }
}