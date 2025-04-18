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
    public RewardEntry Reward => _reward;
    public Sprite Sprite => _spriteRenderer.sprite;

    public override void Initialize(int laneIndex)
    {
      base.Initialize(laneIndex);

      _healthBar.gameObject.SetActive(true);
    }

    // TODO: ApplyDamage가 작동하는 방식 바꾸기
    // UI 활성화를 분리하면, 굳이 상속 받지 않아도 됨
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