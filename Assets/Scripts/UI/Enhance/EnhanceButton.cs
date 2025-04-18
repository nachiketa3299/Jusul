using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{

  public abstract class EnhanceButton : ButtonBase
  {

    [Header("플레이어 연결")][Space]
    [SerializeField] protected EnhanceModule _enhanceModule;
    [SerializeField] protected ResourceModule _resourceModule;

    [Header("비용 아이콘")][Space]
    [SerializeField] protected CostIcons _costIcons;

    [Header("하위 UI 요소 연결")][Space]

    [SerializeField] protected Button _button;
    [SerializeField] protected TMP_Text _currentLevelText;
    [SerializeField] protected Image _costIcon;
    [SerializeField] protected TMP_Text _nextLevelCostText;

    protected CostType _costType;
    protected int _nextLevelCost;

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

    protected void OnGoldAmountChanged(int prevAmount, int currentAmount)
    {
      UpdateTextColor(CostType.Gold, currentAmount);
    }

    protected void OnSoulAmountChanged(int prevAmount, int currentAmount)
    {
      UpdateTextColor(CostType.Soul, currentAmount);
    }

    protected abstract void OnClick();

    protected virtual void Awake()
    {
      _button.onClick.AddListener(OnClick);

      _resourceModule.GoldAmountChanged += OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged += OnSoulAmountChanged;
    }

    protected virtual void OnDestroy()
    {
      _button.onClick.RemoveAllListeners();

      _resourceModule.GoldAmountChanged -= OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged -= OnSoulAmountChanged;
    }
  }
}