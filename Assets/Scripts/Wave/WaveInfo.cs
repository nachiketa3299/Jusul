using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [Serializable]
  public class Reward
  {
    public int Gold;
    public int Soul;
  }

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
    public Reward Reward;
    public bool IsBossWave = false;

    public List<WaveEntry> Entries;
  }
}