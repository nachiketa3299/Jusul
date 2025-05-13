using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName ="Skill_W0_Drizzle", menuName ="Jusul/Skill/W0_Drizzle")]
  public class SKill_W0_Drizzle : SkillBase
  {
    float _offsetX = 0.1f;

    public override void Fire(CharacterModel caster, int laneIndex, int finalDamage)
    {
      // 좌, 중, 우로 살짝 다른 오프셋에서 생성
      int randomPositionIndex = Random.Range(-1, 2);

      Vector3 castingPosition = caster.CastingPosition.position;
      castingPosition.x += randomPositionIndex * _offsetX;

      ProjectileBase projectile = Instantiate(ProjectilePrefab, castingPosition, Quaternion.identity);
      projectile.InitializeAfterInstantiation(new ProjectileBase_InitData(laneIndex, this, finalDamage));

      projectile.Activate();
    }

  }
}