using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  /// <summary>
  /// 스킬 업그레이드 버튼이 격자 구조로 존재하는 테이블
  /// </summary>
  [DisallowMultipleComponent]
  public class SkillUpgradeTable : MonoBehaviour
  {
    [Header("외부 UI 요소 연결")][Space]
    [SerializeField] SkillTooltipPanel _tooltipPanel;

    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] SkillUpgradeTableGrids _skillUpgradeTableGrids;
    [SerializeField] SkillAttributeLevelIndicator _skillAttributeLevelIndicator;

    public void InitializationOnAwake()
    {
      _skillUpgradeTableGrids.InitializationOnAwake();
      _skillAttributeLevelIndicator.InitializeOnAwake();
    }

    public SkillUpgradeButton GetButtonBySkill(SkillBase skill)
    {
      return _skillUpgradeTableGrids.GetButtonBySkill(skill);
    }

    void Awake()
    {
      _tooltipPanel.gameObject.SetActive(false);
    }
  }
}