using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class DamageIndicationManager : MonoBehaviour, ISingleton
  {
    [Header("데이미 인디케이터 프리팹")][Space]
    [SerializeField] DamageIndicator _damageIndicatorPrefab;

    static DamageIndicationManager _instance;
    public static DamageIndicationManager Instance => _instance;

    public void IndicateDamage(EnemyBase enemy, SkillBase skill, int finalDamage)
    {
      DamageIndicator indicator = Instantiate(_damageIndicatorPrefab, transform);
      indicator.Initialize(enemy, skill, finalDamage);
      indicator.Activate();
    }

    public void InitializeSingleton()
    {
      _instance = this;
    }
  }
}