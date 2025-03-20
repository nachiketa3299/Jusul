using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class MineButton : ButtonBase
  {
    [SerializeField] SkillRarity _rarityToMine = SkillRarity.Hero; 
    [SerializeField] Button _button;
    [SerializeField] int _soulCost = 5;
    [SerializeField] TMP_Text _costText;

    void Start()
    {
      _button.onClick.AddListener(MineButton_ButtonClicked);
      _costText.text = _soulCost.ToString();

      PlayerController.Instance.SoulAmountChanged += MineButton_SoulAmountChanged;

      // 초기화 시점땜에 어쩔수없음
      MineButton_SoulAmountChanged(PlayerController.Instance.SoulAmount);
    }

    void MineButton_ButtonClicked()
    {
      PlayerController.Instance.MineSkillByButton(this, _rarityToMine, _soulCost);
    }

    void MineButton_SoulAmountChanged(int amount)
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
  }

}