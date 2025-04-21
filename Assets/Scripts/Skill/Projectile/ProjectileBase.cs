using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class ProjectileBase : MonoBehaviour, IInitializeAfterInstantiation<ProjectileInitData>
  {
    [SerializeField] protected float _speed = 1.0f;

    protected SkillBase _skillBase;
    protected int _finalDamage;
    protected int _laneIndex;

    public virtual void InitializeAfterInstantiation(ProjectileInitData initData)
    {
      _laneIndex = initData.LaneIndex;
      _skillBase = initData.SkillBase;
      _finalDamage = initData.FinalDamage;
    }

    public virtual void Activate() {}
  }
}