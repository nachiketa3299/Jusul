using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class MainUIInitializationHandler : MonoBehaviour
  {
    [Header("초기화 UI 구성 요소")][Space]
    [SerializeField] SkillTooltipPanel _skillTooltipPanel;
    [SerializeField] InfoPanel _infoPanel;
    [SerializeField] SkillUpgradeTable _skillUpgradeTable;
    [SerializeField] PopUpMenuHandler _popUpMenuHandler;
    [SerializeField] MainButtons _mainButtons;

    public void InitializationOnAwake()
    {
      _skillTooltipPanel.InitializationOnAwake();
      _infoPanel.InitializationOnAwake();
      _skillUpgradeTable.InitializationOnAwake();
      _popUpMenuHandler.InitializationOnAwake();
      _mainButtons.InitializationOnAwake();
    }
  }
}