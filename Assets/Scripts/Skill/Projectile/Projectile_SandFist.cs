using UnityEngine;

namespace Jusul
{
  public class Projectile_SandFist : ProjectileBase
  {
    [SerializeField] Animator _animator;

    public override void Activate()
    {
      base.Activate();
      _animator.SetTrigger("Activate");
    }

    public void ActivateAnimationEnd()
    {
      Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.layer == LayerMask.NameToLayer("ProjectileDead"))
      {
        Destroy(gameObject);
      }

      if (collision.TryGetComponent<EnemyBase>(out var enemy))
      {
        if (enemy.LaneIndex == _laneIndex)
        {
          enemy.ApplyDamage(_skillBase, _finalDamage);
        }
      }
    }
  }
}