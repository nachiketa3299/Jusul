using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class ResourcePanel : MonoBehaviour
  {
    [Header("하위 텍스트와 연결")][Space]

    [SerializeField] TMP_Text _totalGold;
    [SerializeField] TMP_Text _totalSoul;
    [SerializeField] TMP_Text _totalSkillCount;

    void ResourcePanel_GoldAmountChanged(int amount)
    {
      _totalGold.text = $"{amount}";
    }

    void ResourcePanel_SoulAmountChanged(int amount)
    {
      _totalSoul.text = $"{amount}";
    }

    void ResourcePanel_TotalSkillCountChanged( int amount, int total)
    {
      _totalSkillCount.text = $"{amount}/{total}";
    }

    void Start()
    {
      PlayerController.Instance.GoldAmountChanged 
        += ResourcePanel_GoldAmountChanged;
      PlayerController.Instance.SoulAmountChanged 
        += ResourcePanel_SoulAmountChanged;
      PlayerController.Instance.TotalSkillCountChanged 
        += ResourcePanel_TotalSkillCountChanged;
    }

    void OnDestroy()
    {
      PlayerController.Instance.GoldAmountChanged 
        -= ResourcePanel_GoldAmountChanged;
      PlayerController.Instance.SoulAmountChanged 
        -= ResourcePanel_SoulAmountChanged;
      PlayerController.Instance.TotalSkillCountChanged 
        -= ResourcePanel_TotalSkillCountChanged;
    }
  }
}