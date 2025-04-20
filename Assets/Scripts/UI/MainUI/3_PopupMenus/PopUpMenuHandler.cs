using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class PopUpMenuHandler : MonoBehaviour
  {
    [Header("레이아웃 요소")][Space]
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] LayoutElement _layoutElement;

    [Header("닫기 버튼 변화용 리소스")][Space]
    [SerializeField] Sprite _closeSprite;
    [SerializeField] Color _closeColor = Color.red;

    [Header("연결된 팝업 메뉴들")][Space]
    [SerializeField] PopUpMenu_Bounty _bountyMenu;
    [SerializeField] PopUpMenu_Cryptid _cryptidMenu;
    [SerializeField] PopUpMenu_Enhance _enhanceMenu;
    [SerializeField] PopUpMenu_Mine _mineMenu;

    PopUpMenuBase _focusedPopUp;

    public void InitializationOnAwake()
    {
      _layoutElement.enabled = false;

      _bountyMenu.InitializeOnAwake();
      _cryptidMenu.InitializeOnAwake();
      _enhanceMenu.InitializeOnAwake();
      _mineMenu.InitializeOnAwake();
    }

    public void OpenPopUp(PopUpMenuBase connectedPopUpMenu)
    {
      if (!_layoutElement.enabled)
      {
        _layoutElement.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
      }

      // 기존에 열려 있는 팝업이 있었다?
      if (_focusedPopUp != null)
      {
        _focusedPopUp.ConnectedPopUpButton.ExitCloseButtonState();
        _focusedPopUp.gameObject.SetActive(false);
      }

      _focusedPopUp = connectedPopUpMenu;
      _focusedPopUp.gameObject.SetActive(true);

      _focusedPopUp.ConnectedPopUpButton.EnterCloseButtonState(_closeSprite, _closeColor);
    }

    public void ClosePopUp(PopUpMenuBase connectedPopUpMenu)
    {
      _focusedPopUp.ConnectedPopUpButton.ExitCloseButtonState();
      _focusedPopUp.gameObject.SetActive(false);

      _layoutElement.enabled = false;
      LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }
  }
}