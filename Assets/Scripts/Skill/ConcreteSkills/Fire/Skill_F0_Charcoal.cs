
using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_F0_Charcoal", menuName ="Jusul/Skill/F0_Charcoal")]
  public class SKill_F0_Charcoal : SkillBase
  {
    public override void Fire(CharacterModel caster, int laneIndex, int finalDamage)
    {
      base.Fire(caster, laneIndex, finalDamage);

      Vector3 castingPosition = caster.CastingPosition.position;

      ProjectileBase projectile = Instantiate(ProjectilePrefab, castingPosition, Quaternion.identity);
      projectile.InitializeAfterInstantiation(new ProjectileBase_InitData(laneIndex, this, finalDamage));

      projectile.Activate();
    }
  }
}