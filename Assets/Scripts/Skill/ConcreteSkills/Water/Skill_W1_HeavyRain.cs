using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_W1_HeavyRain", menuName ="Jusul/Skill/W1_HeavyRain")]
  public class SKill_W1_HeavyRain : SkillBase
  {
    public override void Fire(Character caster, int laneIndex)
    {
      Vector3 castingPosition = caster.CastingPosition.position;

      ProjectileBase projectile = Instantiate(ProjectilePrefab, castingPosition, Quaternion.identity);
      projectile.Initialize(laneIndex, this);

      projectile.Activate();
    }
  }
}