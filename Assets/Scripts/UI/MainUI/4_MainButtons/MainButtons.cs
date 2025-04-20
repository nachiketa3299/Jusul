using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class MainButtons : MonoBehaviour
  {
    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] SkillPurchaseButton _skillPurchaseButton;

    [Space]

    [SerializeField] PopUpButton_Bounty _bountyButton;
    [SerializeField] PopUpButton_Cryptid _cryptidButton;
    [SerializeField] PopUpButton_Enhance _enhanceButton;
    [SerializeField] PopUpButton_Mine _mineButton;

    public void InitializationOnAwake()
    {
      _bountyButton.InitializationOnAwake();
      _cryptidButton.InitializationOnAwake();
      _enhanceButton.InitializationOnAwake();
      _mineButton.InitializationOnAwake();
    }
  }
}