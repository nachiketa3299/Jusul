using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class EnemyBase : MonoBehaviour
  {
    // TODO: HealthBar 별도의 컴포넌트로 떼기
    // TODO: Enemy는 UI 요소를 알지 못해도 되도록 하기
    [Header("UI 연결")][Space]
    [SerializeField] protected Slider _healthBar;

    [Header("스탯")][Space]
    [SerializeField] float _speed;
    [SerializeField] float _range;
    [SerializeField] int _damage;
    [SerializeField] float _attackCooldown;
    [SerializeField] protected int _maxHealth;

    [Header("보상")][Space]
    [SerializeField] protected RewardEntry _reward;

    protected int _laneIndex;

    public event Action<EnemyBase, int> EnemyHealthInitialized;

    public event Action<EnemyBase, SkillBase, int> EnemyDamagedBySkill;

    public int LaneIndex => _laneIndex;

    protected int _currentHealth;

    protected bool _deathByCharacter = false;
    bool _hasAttacked = false;

    Transform _stopPivot;

    StatusEffect _statusEffect;

    public StatusEffect StatusEffect => _statusEffect;

    void OnEnemyDamagedBySkill(EnemyBase enemy, SkillBase skill, int finalDamage)
    {
      DamageIndicationManager.Instance.IndicateDamage(this, skill, finalDamage);
    }

    public virtual void InitializeAfterInstantiation(int laneIndex)
    {
      _laneIndex = laneIndex;

      _currentHealth = _maxHealth;

      _healthBar.value = Mathf.Clamp01((float)_currentHealth / _maxHealth);
      _healthBar.gameObject.SetActive(false);

      _stopPivot = LaneManager.Instance.GetLaneAt(_laneIndex).EnemyStopPivot;

      _statusEffect = StatusEffect.None;

      EnemyHealthInitialized?.Invoke(this, _maxHealth);
      EnemyDamagedBySkill += OnEnemyDamagedBySkill;
    }

    public virtual void StartAdvanceRoutine()
    {
      StartCoroutine(AdvanceRoutine());
    }

    public virtual void ApplyDamage(SkillBase skill, int finalDamage)
    {
      _currentHealth -= finalDamage;
      _healthBar.value = Mathf.Clamp01((float)_currentHealth / _maxHealth);

      // 여기서 UI 등의 업데이트
      EnemyDamagedBySkill?.Invoke(this, skill, finalDamage);

      // 체력이 다 해서 죽은 것은, 무조건 플레이어에 의한 죽음임
      if (_currentHealth <= 0)
      {
        _deathByCharacter = true;
        Destroy(gameObject);
        return;
      }

      // 한번 맞아야 체력 게이지를 표시함
      if (!_hasAttacked)
      {
        _hasAttacked = true;
        _healthBar.gameObject.SetActive(true);
      }
    }

    IEnumerator AdvanceRoutine()
    {
      while (Vector2.Distance(_stopPivot.position, transform.position) > 0.1f)
      {
        Vector2 direction = (_stopPivot.position - transform.position).normalized;
        transform.Translate(_speed * Time.deltaTime * direction);

        yield return null;
      }

      // 도달했으면 공격 시작
      StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
      CharacterModel character = LaneManager.Instance.GetCharacterAtLane(_laneIndex);

      while (!character.IsDied)
      {
        float elapsedTime = 0.0f;

        while (elapsedTime <= _attackCooldown)
        {
          elapsedTime += Time.deltaTime;
          yield return null;
        }

        character.ApplyDamage(_damage);
      }

      // 여기까지 오면 캐릭터를 사살한 것임
      Destroy(gameObject, 2f);
    }

    void OnDestroy()
    {
      LaneManager.Instance.RemoveEnemyAtLane(_laneIndex, this);

      if (_deathByCharacter)
      {
        // 해당 레인 플레이어의 골드 증가
        LaneManager.Instance.AddRewardToPlayerAtLane(_laneIndex, _reward);
      }

      EnemyDamagedBySkill -= OnEnemyDamagedBySkill;
    }
  }
}