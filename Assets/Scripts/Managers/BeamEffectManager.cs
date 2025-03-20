using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BeamEffectManager : MonoBehaviour
  {
    [SerializeField] BeamEffect _trailBeamPrefab;

    static BeamEffectManager _instance;
    public static BeamEffectManager Instance => _instance;

    void Awake()
    {
      _instance = this;
    }

    public void SpawnBeam(ButtonBase start, SkillUpgradeButton end)
    {
      BeamEffect beam 
        = Instantiate(_trailBeamPrefab, start.transform.position, Quaternion.identity);
      
      beam.Initialize(start.transform.position, end);
      beam.Activate();
    }
  }
}