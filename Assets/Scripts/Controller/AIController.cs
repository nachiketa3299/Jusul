using System.Collections;

using UnityEngine;

namespace Jusul
{

  public class AIController : JusulCharacterControllerBase
  {
    [SerializeField] float _aiUpdateInterval = 0.5f;

    IEnumerator AIRoutine()
    {
      var aiTimer = new WaitForSeconds(_aiUpdateInterval);

      while (!LaneManager.Instance.GetLaneAt(_laneIndex).IsGameOvered)
      {
        yield return aiTimer;

        // 구매
        TryPurchaseSkill(out SkillBase purchasedSkill);

        yield return aiTimer;
        // 업그레이드
        if (_skillModule.TryPickSkillToUpgrade(out SkillBase skillToUpgrade))
        {
          TryUpgradeSkill(in skillToUpgrade, out SkillBase upgradedSkill);
        }

        yield return aiTimer;
        // 바운티 소환
        TrySpawnBounty(_bountyModule.PickRandomBountyEnemy());
      }
    }

    void Start()
    {
      StartCoroutine(AIRoutine());
    }
  }
}