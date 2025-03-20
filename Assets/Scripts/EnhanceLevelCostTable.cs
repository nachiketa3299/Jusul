using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "EnhanceLevelTable", menuName = "Jusul/EnhanceLevelTable")]
  public class EnhanceLevelCostTable : ScriptableObject
  {
    [Serializable]
    public class LevelEntry
    {
      public int PurchaseLevel;
      public int RockSkillLevel;
      public int FireSkillLevel;
      public int WaterSkillLevel;
      public int CryptidLevel;
    }

    public List<LevelEntry> LevelsAt = new();
  }
}