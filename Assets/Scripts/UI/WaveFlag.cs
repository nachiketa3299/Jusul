using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class WaveFlag : MonoBehaviour
  {
    [Header("Normal Wave")][Space][Space]

    [SerializeField] GameObject _normalWaveFlagRoot;

    [Header("Wave Index")][Space]
    [SerializeField] TMP_Text _waveIndex;

    [Header("Gold Reward")][Space]
    [SerializeField] GameObject _goldRewardRoot;
    [SerializeField] TMP_Text _goldRewardAmount;

    [Header("Soul Reward")][Space]
    [SerializeField] GameObject _soulRewardRoot;
    [SerializeField] TMP_Text _soulRewardAmount;

    [Header("Animation")][Space]
    [SerializeField] Animator _animator;

    void Start()
    {
      // 시작시 비활성화
      gameObject.SetActive(false);

      WaveManager.Instance.WaveStarted
        += WaveFlag_WaveStarted;
    }

    void OnDestroy()
    {
      WaveManager.Instance.WaveStarted
        -= WaveFlag_WaveStarted;
    }

    void WaveFlag_WaveStarted(int waveIndex, WaveInfo waveInfo)
    {
      _waveIndex.text = waveIndex.ToString();

      if (waveInfo.Reward.Gold != 0)
      {
        _goldRewardRoot.SetActive(true);
        _goldRewardAmount.text = waveInfo.Reward.Gold.ToString();
      }
      else
      {
        _goldRewardRoot.SetActive(false);
      }

      if (waveInfo.Reward.Soul != 0)
      {
        _soulRewardRoot.SetActive(true);
        _soulRewardAmount.text = waveInfo.Reward.Soul.ToString();
      }
      else
      {
        _soulRewardRoot.SetActive(false);
      }

      gameObject.SetActive(true);
      _animator.Play("WaveStart");
    }

    public void WaveFlagAnimationEnd()
    {
      gameObject.SetActive(false);
    }
  }
}