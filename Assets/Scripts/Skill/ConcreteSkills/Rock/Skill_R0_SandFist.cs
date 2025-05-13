using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_R0_SandFist", menuName ="Jusul/Skill/R0_SandFist")]
  public class SKill_R0_SandFist : SkillBase
  {
    readonly List<Vector3> _offsets = new ()
    {
      new Vector3(-0.11f, 0.0f, 0.0f), 
      new Vector3(+0.2f, -0.1f, 0.0f), 
      new Vector3(0.0f, 0.0f, 0.0f)
    };

    readonly List<float> _offsetTimes = new()
    {
      0.0f, 0.1f, 0.2f
    };

    public override void Fire(CharacterModel caster, int laneIndex, int finalDamage)
    {
      List<ProjectileBase> projectiles = new();

      for (int i = 0; i < _offsets.Count; ++i)
      {
        ProjectileBase projectile = Instantiate(ProjectilePrefab);
        projectile.InitializeAfterInstantiation(new ProjectileBase_InitData(laneIndex, this, finalDamage));

        // 부모 좌표 + 오프셋을 월드 기준으로 설정
        projectile.transform.position = caster.CastingPosition.position + _offsets[i];

        projectile.gameObject.SetActive(false);

        projectiles.Add(projectile);
      }

      caster.StartCoroutine(ActivationRoutine(projectiles));
    }

    IEnumerator ActivationRoutine(List<ProjectileBase> projectiles)
    {
      for (int i = 0; i < _offsetTimes.Count; ++i)
      {
        yield return new WaitForSeconds(_offsetTimes[i]);
        projectiles[i].gameObject.SetActive(true);
        projectiles[i].Activate();
      }
    }
  }
}