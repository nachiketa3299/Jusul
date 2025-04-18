using UnityEngine;

namespace Jusul
{
  public class PopUpButton_Enhance : PopUpButton
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

    protected override void Awake()
    {
      base.Awake();
      _indicator.gameObject.SetActive(false);
    }
  }
}