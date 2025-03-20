using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{

  public class AIController : JCharacterController
  {
    [SerializeField] float _aiUpdateInterval = 0.5f;

    void Start()
    {
      StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine()
    {
      while (!LaneManager.Instance.GetLaneAt(_laneIndex).IsGameOvered)
      {
        yield return new WaitForSeconds(_aiUpdateInterval);

        TryPurchaseSkill();

        SkillBase skillToUpgrade = PickUpgradableSkill();

        if (skillToUpgrade == null)
        {
          continue;
        }

        TryUpgradeSkill(skillToUpgrade);

        TrySpawnBounty(_bountyTable.PickRandomBounty());
      }
    }

    SkillBase PickUpgradableSkill()
    {
      List<SkillBase> upgradable = new();

      foreach (var (skill, count) in _skillCounts)
      {
        if (count < 3)
        {
          continue;
        }
        upgradable.Add(skill);
      }

      if (upgradable.Count == 0)
      {
        return null;
      }

      int index = Random.Range(0, upgradable.Count);

      return upgradable[index];
    }

  }
}