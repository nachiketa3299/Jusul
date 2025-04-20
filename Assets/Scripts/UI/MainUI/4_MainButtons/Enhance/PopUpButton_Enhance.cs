using UnityEngine;

namespace Jusul
{
  public class PopUpButton_Enhance : PopUpButtonBase
  {
    [SerializeField] SkillPurchaseProbabilityIndicator _indicator;

    protected override void OnClickedWhenPopUpNotOpened()
    {
      base.OnClickedWhenPopUpNotOpened();

      _indicator.gameObject.SetActive(true);
    }

    protected override void OnClickedWhenPopUpOpened()
    {
      base.OnClickedWhenPopUpOpened();
      _indicator.gameObject.SetActive(false);
    }

    public override void InitializationOnAwake()
    {
      base.InitializationOnAwake();
      _indicator.gameObject.SetActive(false);
    }
  }
}