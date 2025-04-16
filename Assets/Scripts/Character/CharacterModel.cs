using System;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class CharacterModel : MonoBehaviour
  {
    [Header("체력 설정")][Space]
    [SerializeField] int _maxHealth = 1000;

    [Header("캐스팅 위치 설정")][Space]
    [SerializeField] Transform _castingPivot;

    [Header("애니메이터 연결")][Space]
    [SerializeField] Animator _animator;

    public event Action<PlayerInfo> PlayerInfoInitialized;
    public event Action<float> HealthRatioInitialized;
    public event Action<float> HealthRatioChanged;

    public Transform CastingPosition => _castingPivot;
    float HealthRatio => Mathf.Clamp01((float)_currentHealth / _maxHealth);

    string _playerId; // 현재 유용하게 쓰이지는 않음
    int _currentHealth;
    int _laneIndex;
    bool _isDied = false;

    static readonly int FireHash = Animator.StringToHash("Fire");

    public bool IsDied => _isDied;

    public void InitializeOnStart(int laneIndex, PlayerInfo playerInfo)
    {
      _laneIndex = laneIndex;
      _playerId = playerInfo.PlayerId;

      name = $"Character_{playerInfo.PlayerId}";

      _currentHealth = _maxHealth;

      PlayerInfoInitialized?.Invoke(playerInfo);
      HealthRatioInitialized?.Invoke(HealthRatio);
    }

    public void ApplyDamage(int damage)
    {
      _currentHealth -= damage;

      HealthRatioChanged?.Invoke(HealthRatio);

      if (_currentHealth <= 0)
      {
        _isDied = true;
        LaneManager.Instance.GetLaneAt(_laneIndex).SetGameOver(true);
      }
    }

    public void PlayFiringAnimation()
    {
      _animator.SetTrigger(FireHash);
    }

#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
      // 캐릭터의 스킬 발사 지점을 그림
      Gizmos.color = Color.magenta;

      if (_castingPivot != null)
      {
        Gizmos.DrawWireSphere(_castingPivot.position, 0.1f);
      }
    }
#endif
  }
}