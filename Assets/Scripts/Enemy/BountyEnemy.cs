using Jusul.Utility;

using System.Collections;

using TMPro;

using UnityEngine;

namespace Jusul
{
  public class BountyEnemy : Enemy
  {
    [SerializeField] int _timeLimitSeconds = 10;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] TMP_Text _timerText;

    public int MaxHealth => _maxHealth;
    public Reward Reward => _reward;
    public Sprite Sprite => _spriteRenderer.sprite;

    public override void Initialize(int laneIndex)
    {
      base.Initialize(laneIndex);

      _healthBar.gameObject.SetActive(true);
    }

    public override void ApplyDamage(int damage)
    {
      _currentHealth -= damage;
      _healthBar.value = Mathf.Clamp01((float)_currentHealth/_maxHealth);

      if (_currentHealth <= 0)
      {
        _deathByCharacter = true;
        Destroy(gameObject);
        return;
      }
    }

    public override void StartAdvanceRoutine()
    {
      base.StartAdvanceRoutine();
      StartCoroutine(TimeLimitRoutine());
    }

    IEnumerator TimeLimitRoutine()
    {
      float elapsedTime = 0f;

      while (elapsedTime < _timeLimitSeconds)
      {
        elapsedTime += Time.deltaTime;
        Timer.CalculateRemainigTime(_timeLimitSeconds - elapsedTime, out int seconds);
        _timerText.text = $"{seconds:D2}";

        yield return null;
      }

      Destroy(gameObject);
    }
  }
}