using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class InfoPanel : MonoBehaviour
  {
    [Header("하위 UI 연결")][Space]
    [SerializeField] WaveTimer _waveTimer;
    [SerializeField] ResourcePanel _resourcePanel;
    [SerializeField] SkillPurchaseProbabilityIndicator _skillPurchaseProbabilityIndicator;

    public void InitializationOnAwake()
    {

    }
  }
}