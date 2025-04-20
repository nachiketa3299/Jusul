using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  public class LaneManager : MonoBehaviour, ISingleton
  {
    [Header("레인 씬 레퍼런스(인덱스 순으로 정렬)")][Space]
    [SerializeField] List<Lane> _lanes = new(4);

    static LaneManager _instance;
    public static LaneManager Instance => _instance;

    public void InitializeSingleton()
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

    public void SetCharacterAtLane(int laneIndex, CharacterModel character)
    {
      _lanes[laneIndex].SetCharacter(character);
    }

    public CharacterModel GetCharacterAtLane(int laneIndex)
    {
      return _lanes[laneIndex].GetCharacter();
    }

    public void SetLanePlayer(int laneIndex, JusulCharacterControllerBase controller)
    {
      _lanes[laneIndex].SetPlayer(controller);
    }

    public void AddRewardToPlayerAtLane(int laneIndex, RewardEntry reward)
    {
      _lanes[laneIndex].AddRewardToPlayer(reward);
    }

    public List<Enemy> GetEnemyListAtLane(int laneIndex)
    {
      return _lanes[laneIndex].GetEnemyList();
    }
  }
}