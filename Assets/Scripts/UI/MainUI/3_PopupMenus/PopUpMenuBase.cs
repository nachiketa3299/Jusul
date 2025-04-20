using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class PopUpMenuBase : MonoBehaviour
  {
    public PopUpButtonBase ConnectedPopUpButton;

    public virtual void InitializeOnAwake() {}
  }
}