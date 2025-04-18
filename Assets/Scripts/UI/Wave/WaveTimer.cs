using TMPro;
using UnityEngine;
using Jusul.Utility;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class WaveTimer : MonoBehaviour
  {
    [SerializeField] TMP_Text _waveTitle;
    [SerializeField] TMP_Text _waveTimer;

    void Start()
    {
      WaveManager.Instance.WaveTimerStarted += WaveTimer_WaveStarted;
      WaveManager.Instance.WaveTimerPassed += WaveTimer_WavePassed;
      WaveManager.Instance.WaveTimerEnded += WaveTimer_WaveEnded;
    }

    void OnDisable()
    {
      WaveManager.Instance.WaveTimerStarted -= WaveTimer_WaveStarted;
      WaveManager.Instance.WaveTimerPassed -= WaveTimer_WavePassed;
      WaveManager.Instance.WaveTimerEnded -= WaveTimer_WaveEnded;
    }

    void WaveTimer_WaveStarted(int waveIndex, float elapsedTime, float maxTime)
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
      _waveTimer.text = $"{min:D2} : {sec:D2}";
    }

    void WaveTimer_WavePassed(int waveIndex, float elapsedTime, float maxTime)
    {
      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int min, out int sec);
      _waveTimer.text = $"{min:D2} : {sec:D2}";
    }

    void WaveTimer_WaveEnded(int waveIndex, float elapsedTime, float maxTime)
    {
      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int min, out int sec);
      _waveTimer.text = $"{min:D2} : {sec:D2}";
    }
  }
}