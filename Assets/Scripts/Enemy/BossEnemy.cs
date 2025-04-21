using TMPro;

using UnityEngine;

namespace Jusul
{
  public class BossEnemy : EnemyBase
  {
    [SerializeField] TMP_Text healthCount;

    public override void InitializeAfterInstantiation(int laneIndex)
    {
      base.InitializeAfterInstantiation(laneIndex);

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