using System;
using System.Collections;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BountyModule : MonoBehaviour
  {
    [SerializeField] int _bountyCooldownSeconds = 10;
    [SerializeField] BountyTable _bountyTable;

    public Action BountyCooldownStarted;
    public Action<float, float> BountyCooldownPassed;
    public Action BountyCooldownEnded;

    protected bool _canSpawnBounty = false;
    int _laneIndex;

    public void InitializeOnStart(int laneIndex)
    {
      // 바운티 타이머 시작
      StartCoroutine(BountyCooldownRoutine());
    }

    public bool TrySpawnBounty(BountyEnemy bountyEnemy)
    {
      if (!_canSpawnBounty)
      {
        return false;
      }

      LaneManager.Instance.SpawnEnemyAtLane(_laneIndex, bountyEnemy);

      StartCoroutine(BountyCooldownRoutine());
      return true;
    }

    public BountyEnemy PickRandomBountyEnemy()
    {
      return _bountyTable.PickRandomBountyEnemy();
    }

    IEnumerator BountyCooldownRoutine()
    {
      float elapsedTime = 0f;
      BountyCooldownStarted?.Invoke();
      _canSpawnBounty = false;
      while (elapsedTime < _bountyCooldownSeconds)
      {
        BountyCooldownPassed?.Invoke(elapsedTime, _bountyCooldownSeconds);
        elapsedTime += Time.deltaTime;
        yield return null;
      }
      BountyCooldownEnded?.Invoke();
      _canSpawnBounty = true;
    }
  }
}