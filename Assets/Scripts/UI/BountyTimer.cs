using Jusul.Utility;

using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BountyTimer : MonoBehaviour
  {
    [Header("플레이어 연결")][Space]
    [SerializeField] BountyModule _bountyModule;

    [Header("Roots")][Space]
    [SerializeField] RectTransform _timerRoot;
    [SerializeField] RectTransform _notifierRoot;

    [Header("Timer")][Space]
    [SerializeField] TMP_Text _remainingTime;

    void Awake()
    {
      _notifierRoot.gameObject.SetActive(false);
      _timerRoot.gameObject.SetActive(true);

      _bountyModule.BountyCooldownStarted
        += BountyTimer_BountyCooldownStarted;
      _bountyModule.BountyCooldownPassed
        += BountyTimer_BountyCooldownPassed;
      _bountyModule.BountyCooldownEnded
        += BountyTimer_BountyCooldownEnded;
    }

    void OnDestory()
    {
      _bountyModule.BountyCooldownStarted
        -= BountyTimer_BountyCooldownStarted;
      _bountyModule.BountyCooldownPassed
        -= BountyTimer_BountyCooldownPassed;
      _bountyModule.BountyCooldownEnded
        -= BountyTimer_BountyCooldownEnded;
    }

    void BountyTimer_BountyCooldownStarted()
    {
      _notifierRoot.gameObject.SetActive(false);
      _timerRoot.gameObject.SetActive(true);
    }

    void BountyTimer_BountyCooldownPassed(float elapsedTime, float maxTime)
    {
      Timer.CalculateRemainigTime(maxTime - elapsedTime, out int seconds);
      _remainingTime.text = $"{seconds:D2}";
    }

    void BountyTimer_BountyCooldownEnded()
    {
      _notifierRoot.gameObject.SetActive(true);
      _timerRoot.gameObject.SetActive(false);
    }
  }
}