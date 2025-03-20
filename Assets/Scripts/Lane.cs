using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class Lane : MonoBehaviour
  {
    [SerializeField] Transform _characterPivot;
    [SerializeField] Transform _enemySpawnPivot;

    List<Enemy> _enemies = new();
    JCharacterController _controller;
    Character _character;

    public int LaneIndex { get; set; }

    bool _isGameOvered = false;

    public bool IsGameOvered => _isGameOvered;

    public Transform CharacterPivot => _characterPivot;
    public Transform EnemySpawnPivot => _enemySpawnPivot;

    public void PushEnemy(Enemy enemyPrefab)
    {
      Vector3 position = _enemySpawnPivot.position;
      Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
      enemy.Initialize(LaneIndex);
      enemy.Advance();
    }

    public void PopEnemy(Enemy enemy)
    {
      _enemies.Remove(enemy);
    }

    public void SetCharacter(Character character)
    {
      _character = character;
      _character.transform.SetPositionAndRotation(_characterPivot.position, Quaternion.identity);
    }

    public void AddRewardToPlayer(Reward reward)
    {
      if (reward.Gold > 0)
      {
        _controller.AddGold(reward.Gold);
      }

      if (reward.Soul > 0)
      {
        _controller.AddSoul(reward.Soul);
      }
    }

    public void SetPlayer(JCharacterController controller)
    {
      _controller = controller;
    }

    public Character GetCharacter()
    {
      return _character;
    }

    public List<Enemy> GetEnemyList()
    {
      return _enemies;
    }

    public void SetGameOver(bool isOvered)
    {
      _isGameOvered = isOvered;
    }
  } 
}