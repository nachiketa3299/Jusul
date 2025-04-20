using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class MineButton : ButtonBase
  {
    [Header("플레이어 연결")][Space]
    [SerializeField] ResourceModule _resourceModule;

    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _costText;

    [Header("비용과 채굴 등급")][Space]
    [SerializeField] SkillRarity _rarityToMine = SkillRarity.Hero; 
    [SerializeField] int _soulCost = 5;

    public void InitializationOnAwake()
    {
      _button.onClick.AddListener(OnClick);
      _costText.text = _soulCost.ToString();

      _resourceModule.SoulAmountInitialized += OnSoulAmountInitialized;
      _resourceModule.SoulAmountChanged += OnSoulAmountChanged;
    }

    void OnClick()
    {
      PlayerController.Instance.TryMineSkillByUI(this, _rarityToMine, _soulCost);
    }

    void OnSoulAmountInitialized(int amount)
    {
      if (amount < _soulCost)
      {
        _costText.color = Color.red;
      }
      else
      {
        _costText.color = Color.white;
      }
    }

    void OnSoulAmountChanged(int prev, int amount)
    {
      if (amount < _soulCost)
      {
        _costText.color = Color.red;
      }
      else
      {
        _costText.color = Color.white;
      }
    }

    void OnDestroy()
    {
      _resourceModule.SoulAmountInitialized -= OnSoulAmountInitialized;
      _resourceModule.SoulAmountChanged -= OnSoulAmountChanged;
    }
  }
}