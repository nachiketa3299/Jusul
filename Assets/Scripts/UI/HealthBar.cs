using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class HealthBar : MonoBehaviour
  { 
    [Header("캐릭터 모델 연결")][Space]
    [SerializeField] CharacterModel _characterModel;

    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] TMP_Text _playerIdText;
    [SerializeField] Slider _slider;

    void OnPlayerInfoInitialized(PlayerInfo playerInfo)
    {
      _playerIdText.text = playerInfo.PlayerId;
    }

    void OnHealthRatioInitialized(float healthRatio)
    {
      _slider.value = healthRatio;
    }

    void OnHealthRatioChanged(float healthRatio)
    {
      _slider.value = healthRatio;
    }

    void Awake()
    {
      _characterModel.PlayerInfoInitialized += OnPlayerInfoInitialized;
      _characterModel.HealthRatioInitialized += OnHealthRatioInitialized;

      _characterModel.HealthRatioChanged += OnHealthRatioChanged;
    }

    void OnDestroy()
    {
      _characterModel.PlayerInfoInitialized -= OnPlayerInfoInitialized;
      _characterModel.HealthRatioInitialized -= OnHealthRatioInitialized;

      _characterModel.HealthRatioChanged -= OnHealthRatioChanged;
    }
  }
}