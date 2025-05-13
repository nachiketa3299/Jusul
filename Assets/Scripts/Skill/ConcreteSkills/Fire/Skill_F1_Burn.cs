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

    public override void Fire(CharacterModel caster, int laneIndex, int finalDamage)
    {
      base.Fire(caster, laneIndex, finalDamage);

      // 레인에서 적들 리스트 가져오기

      List<EnemyBase> enemies = LaneManager.Instance.GetEnemyListAtLane(laneIndex);

      // 발사 순간 레인에 아무 적도 없으면 그냥 넘김
      if (enemies.Count == 0)
      {
        return;
      }

      int attempt = 0;
      EnemyBase target;
      bool isAlreadyBurntEnemy = false;

      do 
      {
        target = enemies[Random.Range(0, enemies.Count)];
        ++attempt;
        isAlreadyBurntEnemy = target.StatusEffect == StatusEffect.Burn;

      } while (isAlreadyBurntEnemy && attempt < _maxAttempt);

      if (isAlreadyBurntEnemy)
      {
        return;
      }

      // 여기까지 와야 EnemyBase가 지정됨

      ProjectileBase projectile = Instantiate(ProjectilePrefab, target.transform);
      projectile.InitializeAfterInstantiation(new Projectile_Burn_InitData(laneIndex, this, finalDamage, target));
      projectile.Activate();
    }
  }
}