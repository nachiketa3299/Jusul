using System;
using System.Collections;
using UnityEngine;

namespace Jusul
{
  public class Projectile_BurnInitData : ProjectileInitData
  {
    public EnemyBase Target { get; private set; }
    public Projectile_BurnInitData(int laneIndex, SkillBase skillBase, int finalDamage, EnemyBase target) : base(laneIndex, skillBase, finalDamage)
    {
      Target = target;
    }
  }

  public class Projectile_Burn : ProjectileBase 
  {
    [SerializeField] float _explosionRadius = 1.0f;

    EnemyBase _target;

    public override void InitializeAfterInstantiation(ProjectileInitData initData)
    {
      base.InitializeAfterInstantiation(initData);

      _target = ((Projectile_BurnInitData)initData).Target;

      if (initData is Projectile_BurnInitData burnInitData)
      {
        _target = burnInitData.Target;
      }
      else
      {
        throw new ArgumentException();
      }
    }

    public override void Activate()
    {
      base.Activate();

      StartCoroutine(ActivationRoutine());
    }

    IEnumerator ActivationRoutine()
    {
      int dot = (int)((float)_finalDamage * 0.15);
      WaitForSeconds timer = new WaitForSeconds(1f);

      // 주의: 부모가 사라지면, 같이 사라짐
      while (_target != null)
      {
        _target.ApplyDamage(_skillBase, dot);
        yield return timer;
      }
    }

    void OnDestroy()
    {
      Collider2D[] hitEnemies
        = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, LayerMask.GetMask("Enemy"));;
      
      foreach (Collider2D hitEnemy in hitEnemies)
      {
        if (hitEnemy.TryGetComponent<EnemyBase>(out var inRadiusEnemy))
        {
          if (inRadiusEnemy.LaneIndex == _laneIndex)
          {
            inRadiusEnemy.ApplyDamage(_skillBase, _finalDamage);
          }
        }
      }
    }
  }
}