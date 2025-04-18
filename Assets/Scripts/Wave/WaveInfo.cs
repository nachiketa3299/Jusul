using System;
using System.Collections.Generic;

using UnityEngine;


namespace Jusul
{
  // TODO: WaveInfo 보다 더 나은 이름이 존재하는지 확인할 것
  [CreateAssetMenu(fileName="Wave_00", menuName ="Jusul/Wave/Wave")]
  public class WaveInfo : ScriptableObject
  {
    [Serializable]
    public class WaveEntry
    {
      public Enemy EnemyPrefab;
      public int Count;
    }

    public int Duration;
    public RewardEntry Reward;
    public bool IsBossWave = false;

    public List<WaveEntry> Entries;
  }
}