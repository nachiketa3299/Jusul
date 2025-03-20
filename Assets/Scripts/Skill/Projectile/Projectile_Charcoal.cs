using System.Collections;

using UnityEngine;

namespace Jusul
{
  public class Projectile_Charcoal : ProjectileBase
  {
    [SerializeField] float _explosionRadius = 2.0f;
    
    public override void Activate()
    {
      base.Activate();
      StartCoroutine(ActivationRoutine());

      Destroy(gameObject, 5.0f);
    }

    IEnumerator ActivationRoutine()
    {
      while (true)
      {
        transform.Translate(_speed * Time.deltaTime * Vector2.down);
        yield return null;
      }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.layer == LayerMask.NameToLayer("ProjectileDead")) 
      {
        Destroy(gameObject);
      }

      if (collision.TryGetComponent<Enemy>(out var enemy))
      {
        Vector2 collisionPoint = collision.ClosestPoint(transform.position);

        Collider2D[] hitEnemies 
          = Physics2D.OverlapCircleAll(collisionPoint, _explosionRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D hitEnemy in hitEnemies)
        {
          if (hitEnemy.TryGetComponent<Enemy>(out var inRadiusEnemy))
          {
            // 같은 레인에 있는 경우에만 타격
            if (inRadiusEnemy.LaneIndex == _laneIndex)
            {
              DamageIndicationManager.Instance.IndicateDamage(_skillBase, enemy);
              inRadiusEnemy.ApplyDamage(_skillBase.AttackPower);
            }
          }
        }

        Destroy(gameObject);
      }
    }
  }
}