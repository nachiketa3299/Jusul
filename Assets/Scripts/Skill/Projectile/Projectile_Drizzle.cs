using System.Collections;
using UnityEngine;

namespace Jusul
{
  public class Projectile_Drizzle : ProjectileBase
  {
    public override void Activate()
    {
      base.Activate();
      StartCoroutine(ActivationRoutine());
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

      if (collision.TryGetComponent<EnemyBase>(out var enemy))
      {
        // 같은 레인에 있는 경우에만 타격
        if (enemy.LaneIndex == _laneIndex)
        {
          enemy.ApplyDamage(_skillBase, _finalDamage);
          Destroy(gameObject);
        }
      }
    }
  }
}