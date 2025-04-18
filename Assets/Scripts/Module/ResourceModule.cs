using System;

using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// 캐릭터가 가지고 있는 자원(골드, 소울) 관리
  /// </summary>
  [DisallowMultipleComponent]
  public class ResourceModule : MonoBehaviour
  {
    [Header("초기 자원량 설정")][Space]
    [SerializeField] int _initialGoldAmount = 100;
    [SerializeField] int _initialSoulAmount = 2;

    public event Action<int> GoldAmountInitialized;
    public event Action<int, int> GoldAmountChanged;

    public event Action<int> SoulAmountInitialized;
    public event Action<int, int> SoulAmountChanged;

    int _currentSoulAmount;
    int _currentGoldAmount;

    public void InitializeOnStart()
    {
      _currentGoldAmount = _initialGoldAmount;
      _currentSoulAmount = _initialSoulAmount;

      GoldAmountInitialized?.Invoke(_currentGoldAmount);
      SoulAmountInitialized?.Invoke(_currentSoulAmount);
    }

    public bool CanAffordCost(CostType costType, int cost)
    {
      switch (costType)
      {
        case CostType.Soul:
          return CanAffordSoulAmount(cost);
        case CostType.Gold:
          return CanAffordGoldAmount(cost);
        default:
          return false;
      }
    }

    public bool CanAffordGoldAmount(int goldAmount)
    {
      return _currentGoldAmount >= goldAmount;
    }

    public bool CanAffordSoulAmount(int soulAmount)
    {
      return _currentSoulAmount >= soulAmount;
    }

    public void AddCostAmount(CostType costType, int cost)
    {
      switch (costType)
      {
        case CostType.Soul:
          AddSoulAmount(cost);
          break;
        case CostType.Gold:
          AddGoldAmount(cost);
          break;
      }
    }

    public void AddReward(RewardEntry reward)
    {
      if (reward.GoldAmount > 0)
      {
        AddGoldAmount(reward.GoldAmount);
      }

      if (reward.SoulAmount > 0)
      {
        AddSoulAmount(reward.SoulAmount);
      }
    }

    public void AddGoldAmount(int goldAmount)
    {
      int prevAmount = _currentGoldAmount;
      _currentGoldAmount += goldAmount;

      GoldAmountChanged?.Invoke(prevAmount, _currentGoldAmount);
    }

    public void AddSoulAmount(int soulAmount)
    {
      int prevAmount = _currentSoulAmount;
      _currentSoulAmount += soulAmount;

      SoulAmountChanged?.Invoke(prevAmount, _currentSoulAmount);
    }
  }
}