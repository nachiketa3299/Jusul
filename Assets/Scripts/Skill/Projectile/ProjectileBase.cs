using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class ProjectileBase : MonoBehaviour
  {
    [SerializeField] protected DamageIndicator _damageTextPrefab;
    [SerializeField] protected float _speed = 1.0f;

    protected SkillBase _skillBase;

    protected int _laneIndex;

    public void Initialize(int laneIndex, SkillBase skill)
    {
      _laneIndex = laneIndex;
      _skillBase = skill;
    }

    public virtual void Activate() {}
  }
}