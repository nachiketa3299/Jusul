using System;
using System.Collections;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class WaveManager : MonoBehaviour
  {
    [Header("Wave Table")][Space]
    [SerializeField] WaveInfoTable _waveTable;

    [Header("Time Gaps")][Space]
    [SerializeField] float _initialWaitTime = 2.0f;
    [SerializeField] float _timeGapBetweenWaves = 3.0f;

    [Header("UI 요소")][Space]
    [SerializeField] WaveFlag _waveFlag;

    [Header("Spawn Settings")][Space]
    [SerializeField] float _spawnDuration = 1.0f;

    static WaveManager _instance;

    public static WaveManager Instance => _instance;


    public Action<int, WaveInfo> WaveStarted;
    public Action<int, float, float> WaveTimerStarted;
    public Action<int, float, float> WaveTimerPassed;
    public Action<int, float, float> WaveTimerEnded;

    public void InitializeSingleton()
    {
      _instance = this;
    }

    void Start()
    {
      StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
      float elapsedTime = 0.0f;
      WaveTimerStarted?.Invoke(-1, 0.0f, _initialWaitTime);
      while (elapsedTime < _initialWaitTime)
      {
        elapsedTime += Time.deltaTime;
        WaveTimerPassed?.Invoke(-1, elapsedTime, _initialWaitTime);
        yield return null;
      }
      WaveTimerEnded?.Invoke(-1, _initialWaitTime, _initialWaitTime);

      for (int i = 0; i < _waveTable.Waves.Count; ++i) 
      {
        elapsedTime = 0.0f;

        // 웨이브
        WaveStarted?.Invoke(i, _waveTable.Waves[i]);
        WaveTimerStarted?.Invoke(i, 0.0f, _waveTable.Waves[i].Duration);
        Coroutine spawnRoutine = StartCoroutine(WaveSpawnRoutine(_waveTable.Waves[i]));
        while (elapsedTime < _waveTable.Waves[i].Duration)
        {
          elapsedTime += Time.deltaTime;
          WaveTimerPassed?.Invoke(i, elapsedTime, _waveTable.Waves[i].Duration);
          yield return null;
        }
        StopCoroutine(spawnRoutine);
        WaveTimerEnded?.Invoke(i, _waveTable.Waves[i].Duration, _waveTable.Waves[i].Duration);

        // 웨이브 종료 보상 추가
        for (int laneIndex = 0; laneIndex < 4; ++laneIndex)
        {
          LaneManager.Instance.AddRewardToPlayerAtLane(laneIndex, _waveTable.Waves[i].Reward);
        }

        elapsedTime = 0.0f;

        // 웨이브 간 시간
        WaveTimerStarted?.Invoke(-1, 0.0f, _timeGapBetweenWaves);
        while (elapsedTime < _timeGapBetweenWaves)
        {
          elapsedTime += Time.deltaTime;
          WaveTimerPassed?.Invoke(i, elapsedTime, _timeGapBetweenWaves);
          yield return null;
        }
        WaveTimerEnded?.Invoke(-1, _timeGapBetweenWaves, _timeGapBetweenWaves);
      }
    }
    
    IEnumerator WaveSpawnRoutine(WaveInfo waveInfo)
    {
      WaitForSeconds timer = new WaitForSeconds(_spawnDuration);

      // 엔트리 갯수별로 소환
      foreach (WaveInfo.WaveEntry entry in waveInfo.Entries)
      {
        for (int i = 0; i < entry.Count; ++i)
        {
          for (int laneIndex = 0; laneIndex < 4; ++laneIndex)
          {
            // 레인이 게임 오버 되었다면 소환할 필요 없음
            if (LaneManager.Instance.IsGameOvered(laneIndex))
              continue;

            // 각 레인에 적을 추가함
            LaneManager.Instance.SpawnEnemyAtLane(laneIndex, entry.EnemyPrefab);
          }

          yield return timer;
        }
      }
    }
  }
}