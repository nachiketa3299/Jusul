using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_W1_HeavyRain", menuName ="Jusul/Skill/W1_HeavyRain")]
  public class SKill_W1_HeavyRain : SkillBase
  {
    public override void Fire(CharacterModel caster, int laneIndex, int finalDamage)
    {
      Vector3 castingPosition = caster.CastingPosition.position;

      ProjectileBase projectile = Instantiate(ProjectilePrefab, castingPosition, Quaternion.identity);
      projectile.InitializeAfterInstantiation(new ProjectileBase_InitData(laneIndex, this, finalDamage));

      projectile.Activate();
    }
  }
}