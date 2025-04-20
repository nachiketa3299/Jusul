using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class ResourcePanel : MonoBehaviour
  {
    [Header("플레이어 연결")][Space]
    [SerializeField] ResourceModule _resourceModule;
    [SerializeField] SkillModule _skillModule;

    [Header("하위 텍스트와 연결")][Space]
    [SerializeField] TMP_Text _totalGold;
    [SerializeField] TMP_Text _totalSoul;
    [SerializeField] TMP_Text _totalSkillCount;

    public void InitializationOnAwake()
    {
      _skillModule.TotalSkillCountInitialized += OnTotalSkillCountInitialized;
      _skillModule.TotalSkillCountChanged += OnTotalSkillCountChanged;

      _resourceModule.GoldAmountChanged += OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged += OnSoulAmountChanged;
    }

    void OnGoldAmountChanged(int prev, int current)
    {
      _totalGold.text = current.ToString();
    }

    void OnSoulAmountChanged(int prev, int current)
    {
      _totalSoul.text = current.ToString();
    }

    void OnTotalSkillCountInitialized(int totalSkillCount, int maxSkillCount)
    {
      OnTotalSkillCountChanged(totalSkillCount, maxSkillCount);
    }

    void OnTotalSkillCountChanged(int totalSkillCount, int maxSkillCount)
    {
      _totalSkillCount.text = $"{totalSkillCount}/{maxSkillCount}";
    }

    void OnDestroy()
    {
      _skillModule.TotalSkillCountInitialized -= OnTotalSkillCountInitialized;
      _skillModule.TotalSkillCountChanged -= OnTotalSkillCountChanged;

      _resourceModule.GoldAmountChanged -= OnGoldAmountChanged;
      _resourceModule.SoulAmountChanged -= OnSoulAmountChanged;
    }
  }
}