using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  // 레인에 안 죽은 무작위 적이 화상 상태가 됨...
  // 화상 상태인 적은 매 초 15% 피해 받으며, 사망시 폭발하여 주변 적에게 100% 범위 피해

  [CreateAssetMenu(fileName ="Skill_F1_Burn", menuName ="Jusul/Skill/F1_Burn")]
  public class SKill_F1_Burn : SkillBase
  {
    int _maxAttempt = 3;

    public override void Fire(Character caster, int laneIndex)
    {
      base.Fire(caster, laneIndex);

      // 레인에서 적들 리스트 가져오기

      List<Enemy> enemies = LaneManager.Instance.GetEnemyListAtLane(laneIndex);

      if (enemies.Count == 0)
      {
        return;
      }

      int attempt = 0;

      Enemy target;
      do
      {
        target = enemies[Random.Range(0, enemies.Count)];;
        ++attempt;
      } while (target.StatusEffect != StatusEffect.None && attempt < _maxAttempt);

      if (target.StatusEffect == StatusEffect.None)
      {
        target.SetStatusEffect(StatusEffect.Burn);
      }
    }
  }
}