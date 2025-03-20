using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class Character : MonoBehaviour
  {
    [Header("UI Settings")][Space]

    [SerializeField] TMP_Text _playerIdText;
    [SerializeField] Slider _healthBar;

    [Header("Properties")][Space]

    [SerializeField] int _maxHealth = 1000;


    [Header("Animation")][Space]

    [SerializeField] Animator _animator;

    public Transform CastingPosition;

    string _playerId;
    int _currentHealth;
    int _laneIndex;
    bool _isDied = false;

    public bool IsDied => _isDied;

    public void Initialize(int laneIndex, PlayerInfo info)
    {
      _laneIndex = laneIndex;

      _playerId = info.PlayerId;
      _playerIdText.text = _playerId;

      _currentHealth = _maxHealth;

      _healthBar.value = Mathf.Clamp01((float)_currentHealth/_maxHealth);
    }

    public void Fire()
    {
      _animator.SetTrigger("Attack");
    }

    public void ApplyDamage(int damage)
    {
      _currentHealth -= damage;
      _healthBar.value = Mathf.Clamp01((float)_currentHealth/_maxHealth);

      if (_currentHealth <= 0)
      {
        _isDied = true;
        LaneManager.Instance.GetLaneAt(_laneIndex).SetGameOver(true);
      }
    }
  }
}