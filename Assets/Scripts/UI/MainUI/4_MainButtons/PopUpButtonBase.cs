using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class PopUpButtonBase : MonoBehaviour
  {
    [Header("팝업 UI 연결")][Space]
    [SerializeField] PopUpMenuHandler _popUpMenuHandler;
    [SerializeField] PopUpMenuBase _connectedPopUpMenu;

    [Header("Self")][Space]
    [SerializeField] Image _buttonIcon;
    [SerializeField] Button _button;
    [SerializeField] Image _buttonBackground;
    [SerializeField] TMP_Text _buttonText;

    Sprite _spriteWhenPopUpNotOpened;
    Color _colorWhenPopUpNoOpened;

    void MakeCloseButton(Sprite sprite, Color color)
    {
      _buttonIcon.sprite = sprite;
      _buttonBackground.color = color;

      _buttonText.gameObject.SetActive(false);
    }

    void RevertToNormalButton()
    {
      _buttonIcon.sprite = _spriteWhenPopUpNotOpened;
      _buttonBackground.color = _colorWhenPopUpNoOpened;

      _buttonText.gameObject.SetActive(true);
    }

    protected virtual void OnClickedWhenPopUpNotOpened()
    {
      _popUpMenuHandler.OpenPopUp(_connectedPopUpMenu);
    }

    protected virtual void OnClickedWhenPopUpOpened()
    {
      _popUpMenuHandler.ClosePopUp(_connectedPopUpMenu);
    }

    public void EnterCloseButtonState(Sprite closeSprite, Color closeColor)
    {
      MakeCloseButton(closeSprite, closeColor);

      _button.onClick.RemoveAllListeners();
      _button.onClick.AddListener(OnClickedWhenPopUpOpened);
    }

    public void ExitCloseButtonState()
    {
      RevertToNormalButton();

      _button.onClick.RemoveAllListeners();
      _button.onClick.AddListener(OnClickedWhenPopUpNotOpened);
    }

    public virtual void InitializationOnAwake()
    {
      _spriteWhenPopUpNotOpened = _buttonIcon.sprite;
      _colorWhenPopUpNoOpened = _buttonBackground.color;

      _button.onClick.AddListener(OnClickedWhenPopUpNotOpened);
    }
  }
}