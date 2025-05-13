using UnityEngine;

namespace Jusul
{
  public interface IInitializeAfterInstantiation<T>
  {
    public void InitializeAfterInstantiation(T initData);
  }

  [DisallowMultipleComponent]
  public abstract class ProjectileBase : MonoBehaviour, IInitializeAfterInstantiation<ProjectileBase_InitData>
  {
    [SerializeField] protected float _speed = 1.0f;

    protected SkillBase _skillBase;
    protected int _finalDamage;
    protected int _laneIndex;

    public virtual void InitializeAfterInstantiation(ProjectileBase_InitData initData)
    {
      _laneIndex = initData.LaneIndex;
      _skillBase = initData.SkillBase;
      _finalDamage = initData.FinalDamage;
    }

    public virtual void Activate() {}
  }
}