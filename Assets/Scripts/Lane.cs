using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class Lane : MonoBehaviour
  {
    [Header("Character Setting")][Space]
    [SerializeField] Transform _characterPivot;

    [Header("Enemy Settings")][Space]
    [SerializeField] Transform _enemySpawnPivot;
    [SerializeField] Transform _enemyStopPivot;

    List<EnemyBase> _enemies = new();
    JusulCharacterControllerBase _controller;
    CharacterModel _character;

    public int LaneIndex { get; set; }

    bool _isGameOvered = false;

    public bool IsGameOvered => _isGameOvered;

    public Transform CharacterPivot => _characterPivot;
    public Transform EnemyStopPivot => _enemyStopPivot;

    public void PushEnemy(EnemyBase enemyPrefab)
    {
      Vector3 position = _enemySpawnPivot.position;
      EnemyBase enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
      enemy.InitializeAfterInstantiation(LaneIndex);

      _enemies.Add(enemy);

      enemy.StartAdvanceRoutine();
    }

    public void PopEnemy(EnemyBase enemy)
    {
      _enemies.Remove(enemy);
    }

    public void SetCharacter(CharacterModel character)
    {
      _character = character;
      _character.transform.SetPositionAndRotation(_characterPivot.position, Quaternion.identity);
    }

    public void AddRewardToPlayer(RewardEntry reward)
    {
      _controller.AddReward(reward);
    }

    public void SetPlayer(JusulCharacterControllerBase controller)
    {
      _controller = controller;
    }

    public CharacterModel GetCharacter()
    {
      return _character;
    }

    public List<EnemyBase> GetEnemyList()
    {
      return _enemies;
    }

    public void SetGameOver(bool isOvered)
    {
      _isGameOvered = isOvered;
    }

    void OnDrawGizmosSelected() 
    {
      // 캐릭터 위치 표시

      Gizmos.color = Color.magenta;

      if (_characterPivot != null) 
      {
        Gizmos.DrawWireSphere(_characterPivot.position, 0.1f);
      }

      // 적 생성/정지 위치 표시

      Gizmos.color = Color.cyan;

      if (_enemySpawnPivot != null)
      {
        Gizmos.DrawWireSphere(_enemySpawnPivot.position, 0.1f);
      }

      if (_enemyStopPivot != null)
      {
        Gizmos.DrawWireSphere(_enemyStopPivot.position, 0.1f);
      }

      if (_enemySpawnPivot != null && _enemyStopPivot != null) 
      {
        Gizmos.DrawLine(_enemySpawnPivot.position, _enemyStopPivot.position);
      }
    }
  } 
}