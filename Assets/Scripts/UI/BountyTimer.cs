using Jusul.Utility;

using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BountyTimer : MonoBehaviour
  {
    [Header("Roots")][Space]
    [SerializeField] RectTransform _timerRoot;
    [SerializeField] RectTransform _notifierRoot;

    [Header("Timer")][Space]
    [SerializeField] TMP_Text _remainingTime;

    void Awake()
    {
      _notifierRoot.gameObject.SetActive(false);
      _timerRoot.gameObject.SetActive(true);
    }

    void Start()
    {
      PlayerController.Instance.BountyCooldownStarted
        += BountyTimer_BountyCooldownStarted;
      PlayerController.Instance.BountyCooldownPassed
        += BountyTimer_BountyCooldownPassed;
      PlayerController.Instance.BountyCooldownEnded
        += BountyTimer_BountyCooldownEnded;
    }

    void OnDestory()
    {
      PlayerController.Instance.BountyCooldownStarted
        -= BountyTimer_BountyCooldownStarted;
      PlayerController.Instance.BountyCooldownPassed
        -= BountyTimer_BountyCooldownPassed;
      PlayerController.Instance.BountyCooldownEnded
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