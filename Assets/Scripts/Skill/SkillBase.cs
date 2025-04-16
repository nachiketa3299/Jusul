using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// 스킬의 데이터를 저장하는 SO의 베이스 클래스로, 여기서 유도하여 구체 스킬을 제작한다.
  /// </summary>
  public abstract class SkillBase : ScriptableObject
  {
    public Sprite Icon;
    public string Name;
    public string Description;
    public float Cooldown;
    public int AttackPower;
    public SkillAttackType AttackType;
    public SkillRarity Rarity;
    public SkillAttribute Attribute;

    public ProjectileBase ProjectilePrefab;

    public virtual void Fire(CharacterModel caster, int laneIndex, int finalDamage) { }
  }
}