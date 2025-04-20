using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BountySpawnButton : MonoBehaviour
  {
    [Header("보상 이미지 정보 연결")][Space]
    [SerializeField] CostResources _costResources;

    [Header("상단 보상 패널")][Space]
    [SerializeField] Image _rewardBackground;
    [SerializeField] Image _rewardIcon;
    [SerializeField] TMP_Text _rewardText;

    [Header("현상금 몬스터 아이콘")][Space]
    [SerializeField] Image _icon;

    [Header("하단 체력 패널")][Space]
    [SerializeField] TMP_Text _healthText;

    [Header("버튼")][Space]
    [SerializeField] Button _button;

    BountyEnemy _enemyPrefab;

    public void InitializeOnAwake(BountyEnemy enemyPrefab)
    {
      _enemyPrefab = enemyPrefab;

      _healthText.text = $"♥ {_enemyPrefab.MaxHealth}";

      // 둘 중에 하나만 적용
      if (_enemyPrefab.Reward.HasGoldReward)
      {
        _rewardBackground.color = _costResources.GetColorByCostType(CostType.Gold);
        _rewardIcon.sprite = _costResources.GetIconByCostType(CostType.Gold);

        _rewardText.text = _enemyPrefab.Reward.GoldAmount.ToString();
      }
      else if (_enemyPrefab.Reward.HasSoulReward)
      {
        _rewardBackground.color = _costResources.GetColorByCostType(CostType.Soul);
        _rewardIcon.sprite = _costResources.GetIconByCostType(CostType.Soul);

        _rewardText.text = _enemyPrefab.Reward.SoulAmount.ToString();
      }

      _icon.sprite = _enemyPrefab.Sprite;

      _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
      PlayerController.Instance.TrySpawnBountyByUI(_enemyPrefab);
    }

    void OnDestroy()
    {
      _button.onClick.RemoveAllListeners();
    }
  }
}