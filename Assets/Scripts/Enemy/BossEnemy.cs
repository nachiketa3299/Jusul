using TMPro;

using UnityEngine;

namespace Jusul
{
  public class BossEnemy : Enemy
  {
    [SerializeField] TMP_Text healthCount;

    public override void Initialize(int laneIndex)
    {
      base.Initialize(laneIndex);

      _healthBar.gameObject.SetActive(true);
    }

    public override void ApplyDamage(SkillBase skill, int finalDamage)
    {
      _currentHealth -= finalDamage;

      _healthBar.value = Mathf.Clamp01((float)_currentHealth / _maxHealth);

      if (_currentHealth <= 0)
      {
        _deathByCharacter = true;
        Destroy(gameObject);
        return;
      }
    }
  }
}