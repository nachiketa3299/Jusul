using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class ProjectileBase : MonoBehaviour
  {
    [SerializeField] protected DamageIndicator _damageTextPrefab;
    [SerializeField] protected float _speed = 1.0f;

    protected SkillBase _skillBase;
    protected int _finalDamage;

    protected int _laneIndex;

    public void Initialize(int laneIndex, SkillBase skill, int finalDamage)
    {
      _laneIndex = laneIndex;
      _skillBase = skill;
      _finalDamage = finalDamage;
    }

    public virtual void Activate() {}
  }
}