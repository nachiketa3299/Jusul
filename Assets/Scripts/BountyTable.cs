using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "BountyTable", menuName = "Jusul/Bounty/BontyTable")]
  public class BountyTable : ScriptableObject
  {
    public List<BountyEnemy> BountyList = new();

    public BountyEnemy PickRandomBounty()
    {
      return BountyList[Random.Range(0, BountyList.Count)];
    }
  }
}