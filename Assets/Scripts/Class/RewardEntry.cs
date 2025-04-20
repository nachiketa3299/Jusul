using System;

namespace Jusul
{
  [Serializable]
  public class RewardEntry
  {
    public int GoldAmount;
    public int SoulAmount;

    public bool HasGoldReward => GoldAmount > 0;
    public bool HasSoulReward => SoulAmount > 0;
  }
}