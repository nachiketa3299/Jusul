
using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_F0_Charcoal", menuName ="Jusul/Skill/F0_Charcoal")]
  public class SKill_F0_Charcoal : SkillBase
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