using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  public class LaneManager : MonoBehaviour
  {
    [Header("레인 인덱스 순으로 정렬 필수")][Space]
    [SerializeField] List<Lane> _lanes = new(4);

    static LaneManager _instance;
    public static LaneManager Instance => _instance;

    void Awake()
    {
      _instance = this;

      for (int laneIndex = 0; laneIndex < _lanes.Count; ++laneIndex)
      {
        _lanes[laneIndex].LaneIndex = laneIndex;
      }
    }

    public Lane GetLaneAt(int laneIndex) 
    {
      return _lanes[laneIndex];
    }

    public bool IsGameOvered(int laneIndex)
    {
      return _lanes[laneIndex].IsGameOvered;
    }

    public void SpawnEnemyAtLane(int laneIndex, Enemy enemyPrefab)
    {
      _lanes[laneIndex].PushEnemy(enemyPrefab);
    }

    public void RemoveEnemyAtLane(int laneIndex, Enemy enemy)
    {
      _lanes[laneIndex].PopEnemy(enemy);
    }

    public void SetCharacterAtLane(int laneIndex, Character character)
    {
      _lanes[laneIndex].SetCharacter(character);
    }

    public Character GetCharacterAtLane(int laneIndex)
    {
      return _lanes[laneIndex].GetCharacter();
    }

    public void SetLanePlayer(int laneIndex, JCharacterController controller)
    {
      _lanes[laneIndex].SetPlayer(controller);
    }

    public void AddRewardToPlayerAtLane(int laneIndex, Reward reward)
    {
      _lanes[laneIndex].AddRewardToPlayer(reward);
    }

    public List<Enemy> GetEnemyListAtLane(int laneIndex)
    {
      return _lanes[laneIndex].GetEnemyList();
    }
  }
}