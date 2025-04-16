using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Jusul
{
  public class Projectile_HeavyRain : ProjectileBase
  {
    int _count = 5;

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

      if (collision.TryGetComponent<Enemy>(out var enemy))
      {
        DamageIndicationManager.Instance.IndicateDamage(_skillBase, enemy);
        enemy.ApplyDamage(_finalDamage);
        --_count;

        if (_count == 0)
        {
          Destroy(gameObject);
        }
      }
    }
  }
}