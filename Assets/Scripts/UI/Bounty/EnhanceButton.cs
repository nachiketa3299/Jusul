using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [Serializable]
  public enum EnhanceType
  {
    Purchase,
    Rock,
    Fire,
    Water,
    Cryptid
  }

  public class EnhanceButton : ButtonBase
  {
    // 런타임에 변경 하지 말 것
    [SerializeField] EnhanceType _enhanceType = EnhanceType.Purchase;
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _costText;

    void Start()
    {
      _button.onClick.AddListener(EnhanceButton_ButtonClicked);

      switch (_enhanceType)
      {
      case EnhanceType.Purchase:
        PlayerController.Instance.PurchaseLevelChanged
          += EnhanceButton_CostChanged;
        PlayerController.Instance.GoldAmountChanged
          += EnhanceButton_GoldAmountChanged;
        EnhanceButton_GoldAmountChanged(PlayerController.Instance.GoldAmount);
        break;
      case EnhanceType.Rock:
        PlayerController.Instance.RockLevelChanged
          += EnhanceButton_CostChanged;
        PlayerController.Instance.SoulAmountChanged
          += EnhanceButton_SoulAmountChanged;
        EnhanceButton_SoulAmountChanged(PlayerController.Instance.SoulAmount);
        break;
      case EnhanceType.Fire:
        PlayerController.Instance.FireLevelChanged
          += EnhanceButton_CostChanged;
        PlayerController.Instance.SoulAmountChanged
          += EnhanceButton_SoulAmountChanged;
        EnhanceButton_SoulAmountChanged(PlayerController.Instance.SoulAmount);
        break;
      case EnhanceType.Water:
        PlayerController.Instance.WaterLevelChanged
          += EnhanceButton_CostChanged;
        PlayerController.Instance.SoulAmountChanged
          += EnhanceButton_SoulAmountChanged;
        EnhanceButton_SoulAmountChanged(PlayerController.Instance.SoulAmount);
        break;
      case EnhanceType.Cryptid:
        PlayerController.Instance.CryptidLevelChanged
          += EnhanceButton_CostChanged;
        PlayerController.Instance.SoulAmountChanged
          += EnhanceButton_SoulAmountChanged;
        EnhanceButton_SoulAmountChanged(PlayerController.Instance.SoulAmount);
        break;
      }

      int initLevel = PlayerController.Instance.GetCurrentLevel(_enhanceType);
      int initCost = PlayerController.Instance.GetNextLevelCost(_enhanceType);
      EnhanceButton_CostChanged(initLevel, initCost);
    }

    void UpdateState(int amount)
    {
      int require = PlayerController.Instance.GetNextLevelCost(_enhanceType);

      if (amount < require)
      {
        _costText.color = Color.red;
      }
      else
      {
        _costText.color = Color.white;
      }
    }

    void EnhanceButton_GoldAmountChanged(int amount)
    {
      UpdateState(amount);
    }

    void EnhanceButton_SoulAmountChanged(int amount)
    {
      UpdateState(amount);
    }

    void EnhanceButton_CostChanged(int currentLevel, int nextCost)
    {
      _levelText.text = currentLevel.ToString();
      _costText.text = nextCost.ToString();

      bool isGold = _enhanceType == EnhanceType.Purchase;

      if (isGold)
      {
        int goldAmount = PlayerController.Instance.GoldAmount;
        UpdateState(goldAmount);
      }
      else
      {
        int soulAmount = PlayerController.Instance.SoulAmount;
        UpdateState(soulAmount);
      }
    }

    void EnhanceButton_ButtonClicked()
    {
      switch (_enhanceType)
      {
      case EnhanceType.Purchase:
        PlayerController.Instance.TryEnhancePurchase();
        break;
      case EnhanceType.Rock:
        PlayerController.Instance.TryEnhanceRock();
        break;
      case EnhanceType.Fire:
        PlayerController.Instance.TryEnhanceFire();
        break;
      case EnhanceType.Water:
        PlayerController.Instance.TryEnhanceWater();
        break;
      case EnhanceType.Cryptid:
        PlayerController.Instance.TryEnhanceCryptid();
        break;
      }
    }
  }
}