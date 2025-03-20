using System;
using System.Collections;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class BeamEffect : MonoBehaviour
  {
    [SerializeField] float _duration = 1.0f;
    [SerializeField] float _controlCoefficient = 0.3f;

    public Action<SkillUpgradeButton> BeamDestroyed;

    float _elapsedTime;

    Vector3 _start;
    Vector3 _end;
    Vector3 _control;

    SkillUpgradeButton _targetButton;

    public void Initialize(Vector3 start, SkillUpgradeButton endButton)
    {
      _start = start;
      _start.z = -2;

      _end = endButton.transform.position;
      _end.z = -2;

      _elapsedTime = 0.0f;

      _targetButton = endButton;

      Vector3 mid = (_start + _end) * 0.5f;
      _control = mid + Vector3.up * Vector3.Distance(_start, _end) * _controlCoefficient;
      _control.z = -2;
    }

    public void Activate()
    {
      StartCoroutine(ActivationRoutine());
    }

    IEnumerator ActivationRoutine()
    {
      while (_elapsedTime < _duration)
      {
        _elapsedTime += Time.deltaTime;
        float ratio = Mathf.Clamp01(_elapsedTime / _duration);

        Vector3 position = (1 - ratio) * (1 - ratio) * _start 
          + 2 * (1 - ratio) * ratio * _control
          + ratio * ratio * _end;
        position.z = -2;

        transform.position = position;
        
        if (ratio >= 1.0f)
        {
          // 도착
          Destroy(gameObject);
        }

        yield return null;
      }
    }

    void OnDestroy()
    {
      _targetButton.RefreshSkillCount();
    }
  }
}