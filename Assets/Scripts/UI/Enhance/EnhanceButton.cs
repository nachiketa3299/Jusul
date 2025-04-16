using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{

  public class EnhanceButton : ButtonBase
  {

    [Header("플레이어 연결")][Space]
    [SerializeField] protected EnhanceModule _enhanceModule;
    [SerializeField] protected ResourceModule _resourceModule;

    [Header("비용 아이콘")][Space]
    [SerializeField] protected CostIcons _costIcons;

    [Header("하위 UI 요소 연결")][Space]

    [SerializeField] protected Button _button;
    [SerializeField] protected TMP_Text _currentLevelText;
    [SerializeField] protected Image _costIcon;
    [SerializeField] protected TMP_Text _nextLevelCostText;

    protected CostType _costType;
    protected int _nextLevelCost;
  }
}