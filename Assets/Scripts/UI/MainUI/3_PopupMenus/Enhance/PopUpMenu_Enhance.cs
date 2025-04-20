using UnityEngine;

namespace Jusul
{
  public class PopUpMenu_Enhance : PopUpMenuBase
  {
    [Header("하위 UI 연결")][Space]
    [SerializeField] EnhanceButton_PurchaseLevelEnhanceButton _purchaseLevelEnhanceButtons;
    [SerializeField] EnhanceButton_SkillAttributeLevelEnhanceButton _rockAttribLevelEnhanceButton;
    [SerializeField] EnhanceButton_SkillAttributeLevelEnhanceButton _fireAttribLevelEnhanceButton;
    [SerializeField] EnhanceButton_SkillAttributeLevelEnhanceButton _waterAttribLevelEnhanceButton;

    public override void InitializeOnAwake()
    {
      base.InitializeOnAwake();

      _purchaseLevelEnhanceButtons.InitializeOnAwake();
      _rockAttribLevelEnhanceButton.InitializeOnAwake();
      _fireAttribLevelEnhanceButton.InitializeOnAwake();
      _waterAttribLevelEnhanceButton.InitializeOnAwake();
    }
  }
}