using System;

using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// <see cref="SkillUpgradeTable"/>에 새로운 스킬이 생성될 때(구매, 업그레이드, 채굴) 적용되는 
  /// 빔(Beam) 이펙트 효과를 제어한다.
  /// </summary>
  [DisallowMultipleComponent]
  public class BeamEffectManager : MonoBehaviour
  {
    [Header("생성할 빔 프리팹")][Space]
    [SerializeField] BeamEffect _beamPrefab;

    static BeamEffectManager _instance;
    public static BeamEffectManager Instance => _instance;

    public void StartBeamEffect(Vector3 start, Vector3 end, Action onHit)
    {
      BeamEffect beam = Instantiate(_beamPrefab, start, Quaternion.identity);
      beam.Initialize(start, end, onHit);
      beam.Activate();
    } 

    void Awake()
    {
      _instance = this;
    }
  }
}