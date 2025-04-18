using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class WaveFlag : MonoBehaviour
  {
    [Header("웨이브 인덱스")][Space]
    [SerializeField] TMP_Text _waveIndex;

    [Header("웨이브 보상")][Space]
    [SerializeField] GameObject _goldRewardRoot;
    [SerializeField] TMP_Text _goldRewardAmountText;
    [SerializeField] GameObject _soulRewardRoot;
    [SerializeField] TMP_Text _soulRewardAmountText;

    [Header("애니메이션")][Space]
    [SerializeField] Animator _animator;

    void OnWaveStarted(int waveIndex, WaveInfo waveInfo)
    {
      _waveIndex.text = waveIndex.ToString();

      if (waveInfo.Reward.SoulAmount > 0)
      {
        _soulRewardRoot.SetActive(true);
        _soulRewardAmountText.text = waveInfo.Reward.SoulAmount.ToString();
      }
      else
      {
        _soulRewardRoot.SetActive(false);
      }

      if (waveInfo.Reward.GoldAmount > 0)
      {
        _goldRewardRoot.SetActive(true);
        _goldRewardAmountText.text = waveInfo.Reward.GoldAmount.ToString();
      }
      else
      {
        _goldRewardRoot.SetActive(false);
      }


      gameObject.SetActive(true);
      _animator.Play("WaveStart");
    }

    public void WaveFlagAnimationEnd()
    {
      gameObject.SetActive(false);
    }

    void Start()
    {
      // 시작시 비활성화
      gameObject.SetActive(false);

      WaveManager.Instance.WaveStarted += OnWaveStarted;
    }

    void OnDestroy()
    {
      WaveManager.Instance.WaveStarted -= OnWaveStarted;
    }
  }
}