using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class PopUpMenuManager : MonoBehaviour
  {
    [SerializeField] Sprite _closeSprite;
    [SerializeField] Color _closeColor = Color.red;

    [SerializeField] List<PopUpMenu> _popUpMenus = new();

    PopUpMenu _focusedPopUp;

    static PopUpMenuManager _instance;
    public static PopUpMenuManager Instance => _instance;


    void Awake()
    {
      _instance = this;

      foreach (PopUpMenu popUpMenu in _popUpMenus)
      {
        popUpMenu.Initialize();
      }
    }

    public void OpenPopUp(PopUpMenu connectedPopUpMenu)
    {
      // 기존에 열려 있는 팝업이 있었다?
      if (_focusedPopUp != null)
      {
        _focusedPopUp.ConnectedPopUpButton.ExitOpenState();
        _focusedPopUp.gameObject.SetActive(false);
      }

      _focusedPopUp = connectedPopUpMenu;
      _focusedPopUp.gameObject.SetActive(true);

      _focusedPopUp.ConnectedPopUpButton.EnterOpenState(_closeSprite, _closeColor);
    }

    public void ClosePopUp(PopUpMenu connectedPopUpMenu)
    {
      _focusedPopUp.ConnectedPopUpButton.ExitOpenState();
      _focusedPopUp.gameObject.SetActive(false);
    }
  }
}