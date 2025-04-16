using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public enum StatusEffect
  {
    None, 
    Burn
  }

  [DisallowMultipleComponent]
  public class Enemy : MonoBehaviour
  {
    [Header("UI Settings")][Space]
    [SerializeField] protected Slider _healthBar;

    [Header("Stats")][Space]
    [SerializeField] float _speed;
    [SerializeField] float _range;
    [SerializeField] int _damage;
    [SerializeField] float _attackCooldown;
    [SerializeField] protected int _maxHealth;

    [Header("Reward")][Space]
    [SerializeField] protected Reward _reward;

    protected int _laneIndex;

    StatusEffect _statusEffect = StatusEffect.None;
    
    public StatusEffect StatusEffect => _statusEffect;
    public int LaneIndex => _laneIndex;
    public void SetLaneIndex(int laneIndex) => _laneIndex = laneIndex;

    protected int _currentHealth;

    protected bool _deathByCharacter = false;
    bool _hasAttacked = false;

    Transform _stopPivot;

    public virtual void Initialize(int laneIndex)
    {
      _laneIndex = laneIndex;

      _currentHealth = _maxHealth;

      _healthBar.value = Mathf.Clamp01((float)_currentHealth / _maxHealth);
      _healthBar.gameObject.SetActive(false);

      _stopPivot = LaneManager.Instance.GetLaneAt(_laneIndex).EnemyStopPivot;
    }

    public virtual void StartAdvanceRoutine()
    {
      StartCoroutine(AdvanceRoutine());
    }

    public virtual void ApplyDamage(int damage)
    {
      if (!_hasAttacked)
      {
        _hasAttacked = true;
        _healthBar.gameObject.SetActive(true);
      }

      _currentHealth -= damage;
      _healthBar.value = Mathf.Clamp01((float)_currentHealth/_maxHealth);

      if (_currentHealth <= 0)
      {
        _deathByCharacter = true;
        Destroy(gameObject);
        return;
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
    }

    public void SetStatusEffect(StatusEffect statusEffect)
    {
    }
  }
}