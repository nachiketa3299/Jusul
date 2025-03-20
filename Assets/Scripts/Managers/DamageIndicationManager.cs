using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class DamageIndicationManager : MonoBehaviour
  {
    [Header("Prefab")][Space]

    [SerializeField] DamageIndicator _damageIndicatorPrefab;

    static DamageIndicationManager _instance;
    public static DamageIndicationManager Instance => _instance;

    void Awake()
    {
      _instance = this;
    }

    public void IndicateDamage(SkillBase skill, Enemy enemy)
    {
      DamageIndicator indicator = Instantiate(_damageIndicatorPrefab, transform);
      indicator.Initialize(skill, enemy);
      indicator.Activate();
    }
  }
}