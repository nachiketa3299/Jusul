using TMPro;

using UnityEngine;

using Jusul.Utility;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class WaveTimer : MonoBehaviour
  {
    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] TMP_Text _waveTitle;
    [SerializeField] TMP_Text _waveTimer;

    public void InitializationOnAwake()
    {
      WaveManager.Instance.WaveTimerStarted += OnWaveTimerStarted;
      WaveManager.Instance.WaveTimerPassed += OnWaveTimerPassed;
      WaveManager.Instance.WaveTimerEnded += OnWaveTimerEnded;
    }

    void OnWaveTimerStarted(int waveIndex, float elapsedTime, float maxTime)
    {
      if (waveIndex == -1)
      {
        _waveTitle.text = "준비 시간";
      }
      else
      {
        _waveTitle.text = $"웨이브 {waveIndex}";
      }

      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int min, out int sec);
      _waveTimer.text = $"{min:D2}:{sec:D2}";
    }

    void OnWaveTimerPassed(int waveIndex, float elapsedTime, float maxTime)
    {
      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int min, out int sec);
      _waveTimer.text = $"{min:D2}:{sec:D2}";
    }

    void OnWaveTimerEnded(int waveIndex, float elapsedTime, float maxTime)
    {
      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int min, out int sec);
      _waveTimer.text = $"{min:D2}:{sec:D2}";
    }

    void OnDestroy()
    {
      WaveManager.Instance.WaveTimerStarted -= OnWaveTimerStarted;
      WaveManager.Instance.WaveTimerPassed -= OnWaveTimerPassed;
      WaveManager.Instance.WaveTimerEnded -= OnWaveTimerEnded;
    }
  }
}