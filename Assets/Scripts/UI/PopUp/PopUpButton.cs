using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class PopUpButton : MonoBehaviour
  {
    [Header("PopUp Connection")][Space]
    [SerializeField] PopUpMenu _connectedPopUpMenu;

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
      PopUpMenuManager.Instance.OpenPopUp(_connectedPopUpMenu);
    }

    protected virtual void OnClickedWhenPopUpOpened()
    {
      PopUpMenuManager.Instance.ClosePopUp(_connectedPopUpMenu);
    }

    public void EnterOpenState(Sprite closeSprite, Color closeColor)
    {
      MakeCloseButton(closeSprite, closeColor);

      _button.onClick.RemoveAllListeners();
      _button.onClick.AddListener(OnClickedWhenPopUpOpened);
    }

    public void ExitOpenState()
    {
      RevertToNormalButton();

      _button.onClick.RemoveAllListeners();
      _button.onClick.AddListener(OnClickedWhenPopUpNotOpened);
    }

    protected virtual void Awake() 
    {
      _spriteWhenPopUpNotOpened = _buttonIcon.sprite;
      _colorWhenPopUpNoOpened = _buttonBackground.color;

      _button.onClick.AddListener(OnClickedWhenPopUpNotOpened);
    }

  }
}