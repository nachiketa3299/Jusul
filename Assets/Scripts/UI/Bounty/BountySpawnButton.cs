using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BountySpawnButton : MonoBehaviour
  {
    [Header("Resources")][Space]
    [SerializeField] Color _colorOnGoldReward;
    [SerializeField] Color _colorOnSoulReward;
    [SerializeField] Sprite _iconOnGold;
    [SerializeField] Sprite _iconOnSoul;

    [Header("Reward Panel")][Space]
    [SerializeField] Image _rewardBackground;
    [SerializeField] Image _rewardIcon;
    [SerializeField] TMP_Text _rewardText;

    [Header("Icon")][Space]
    [SerializeField] Image _icon;

    [Header("Health Panel")][Space]
    [SerializeField] TMP_Text _healthText;

    [Header("Button")][Space]
    [SerializeField] Button _button;

    BountyEnemy _enemyPrefab;


    public void Initialize(BountyEnemy enemyPrefab)
    {
      _enemyPrefab = enemyPrefab;

      _healthText.text = $"♥ {_enemyPrefab.MaxHealth}";

      if (_enemyPrefab.Reward.GoldAmount > 0)
      {
        _rewardBackground.color = _colorOnGoldReward;
        _rewardIcon.sprite = _iconOnGold;
        _rewardText.text = _enemyPrefab.Reward.GoldAmount.ToString();
      }
      else if (_enemyPrefab.Reward.SoulAmount > 0)
      {
        _rewardBackground.color = _colorOnSoulReward;
        _rewardIcon.sprite = _iconOnSoul;
        _rewardText.text = _enemyPrefab.Reward.SoulAmount.ToString();
      }

      _icon.sprite = _enemyPrefab.Sprite;

      _button.onClick.AddListener(BountySpawnButton_ButtonClicked);
    }

    void BountySpawnButton_ButtonClicked()
    {
      PlayerController.Instance.TrySpawnBountyByUI(_enemyPrefab);
    }
  }
}