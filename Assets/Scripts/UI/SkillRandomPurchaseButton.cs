using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class SkillRandomPurchaseButton : ButtonBase
  {
    [Header("UI Settings")][Space]
    [SerializeField] TMP_Text _priceText;
    [SerializeField] Button _button;
    [SerializeField] SkillUpgradeTable _upgradeTable;
    [SerializeField] Image _notifier;

    void Start()
    {
      _button.onClick.AddListener(SkillRandomPurchaseButton_ButtonClicked);
      _notifier.gameObject.SetActive(false);

      PlayerController.Instance.SkillPurchasePriceChanged
        += SkillRandomPurchaseButton_SkillPurchasePriceChanged;
      PlayerController.Instance.GoldAmountChanged
        += SkillRandomPurchaseButton_GoldAmountChanged;

      SkillRandomPurchaseButton_GoldAmountChanged(PlayerController.Instance.GoldAmount);
      SkillRandomPurchaseButton_SkillPurchasePriceChanged(PlayerController.Instance.SkillPurchasePrice);
    }

    void OnDestroy()
    {
      PlayerController.Instance.SkillPurchasePriceChanged
        -= SkillRandomPurchaseButton_SkillPurchasePriceChanged;
    }

    void UpdateState()
    {
      int price = PlayerController.Instance.SkillPurchasePrice;
      int have = PlayerController.Instance.GoldAmount;

      if (have < price)
      {
        _priceText.color = Color.red;
        _notifier.gameObject.SetActive(false);
      }
      else
      {
        _priceText.color = Color.white;
        _notifier.gameObject.SetActive(true);
      }
    }

    void SkillRandomPurchaseButton_GoldAmountChanged(int amount)
    {
      UpdateState();
    }

    void SkillRandomPurchaseButton_SkillPurchasePriceChanged(int price)
    {
      _priceText.text = price.ToString();
      UpdateState();
    }

    void SkillRandomPurchaseButton_ButtonClicked()
    {
      _upgradeTable.Unfocus();
      PlayerController.Instance.TryPurchaseSkillByButton(this);
    }
  }
}