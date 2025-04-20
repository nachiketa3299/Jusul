using UnityEngine;

namespace Jusul
{
  public class PopUpMenu_Mine : PopUpMenuBase
  {
    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] MineButton _firstMineButton;
    [SerializeField] MineButton _secondMineButton;
    [SerializeField] MineButton _thirdMineButton;

    public override void InitializeOnAwake()
    {
      base.InitializeOnAwake();

      _firstMineButton.InitializationOnAwake();
      _secondMineButton.InitializationOnAwake();
      _thirdMineButton.InitializationOnAwake();
    }
  }
}