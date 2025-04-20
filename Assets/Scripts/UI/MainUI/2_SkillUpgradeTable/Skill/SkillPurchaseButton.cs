using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class SkillPurchaseButton : ButtonBase
  {
    [Header("플레이어 연결")][Space]
    [SerializeField] ResourceModule _resourceModule;
    [SerializeField] SkillModule _skillModule;

    [Header("UI 구성 요소")][Space]
    [SerializeField] TMP_Text _priceText;
    [SerializeField] Button _button;
    [SerializeField] SkillUpgradeTableGrids _upgradeTable;
    [SerializeField] Image _notifier;

    int _skillPurchasePrice;
    int _goldAmount;

    void OnClick()
    {
      _upgradeTable.Unfocus();

      PlayerController.Instance.TryPurchaseSkillByUI(this);
    }

    void UpdateState()
    {
      if (_goldAmount >= _skillPurchasePrice)
      {
        _priceText.color = Color.white;
        _notifier.gameObject.SetActive(true);
      }
      else
      {
        _priceText.color = Color.red;
        _notifier.gameObject.SetActive(false);
      }
    }

    void OnSkillPurchasePriceInitialized(int price)
    {
      _skillPurchasePrice = price;
      _priceText.text = _skillPurchasePrice.ToString();
    }

    void OnSkillPurchasePriceChanged(int prev, int price)
    {
      _skillPurchasePrice = price;
      _priceText.text = _skillPurchasePrice.ToString();

      UpdateState();
    }

    void OnGoldAmountInitialized(int amount)
    {
      _goldAmount = amount;

      UpdateState();
    }

    void OnGoldAmountChanged(int prev, int amount)
    {
      _goldAmount = amount;

      UpdateState();
    }

    void Awake()
    {
      _button.onClick.AddListener(OnClick);
      _notifier.gameObject.SetActive(false);

      _skillModule.SkillPurchasePriceInitialized += OnSkillPurchasePriceInitialized;
      _skillModule.SkillPurchasePriceChanged += OnSkillPurchasePriceChanged;

      _resourceModule.GoldAmountInitialized += OnGoldAmountInitialized;
      _resourceModule.GoldAmountChanged += OnGoldAmountChanged;
    }

    void OnDestroy()
    {
      _skillModule.SkillPurchasePriceInitialized -= OnSkillPurchasePriceInitialized;
      _skillModule.SkillPurchasePriceChanged -= OnSkillPurchasePriceChanged;

      _resourceModule.GoldAmountInitialized -= OnGoldAmountInitialized;
      _resourceModule.GoldAmountChanged -= OnGoldAmountChanged;
    }
  }
}