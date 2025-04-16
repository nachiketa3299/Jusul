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
    [Header("플레이어 연결")][Space]
    [SerializeField] SkillModule _skillModule;

    [Header("스킬 그룹")][Space]
    [SerializeField] HorizontalLayoutGroup _rockGroup;
    [SerializeField] HorizontalLayoutGroup _fireGroup;
    [SerializeField] HorizontalLayoutGroup _waterGroup;

    [Header("툴팁 패널")][Space]
    [SerializeField] SkillTooltipPanel _tooltipPanel;

    // {스킬: 버튼} 맵핑
    Dictionary<SkillBase, SkillUpgradeButton> _buttons = new();

    // 현재 스킬 버튼이 포커스된 상태라면 포커스한 버튼
    SkillUpgradeButton _focusedButton;

    public SkillUpgradeButton GetButtonBySkill(SkillBase skill)
    {
      return _buttons[skill];
    }

    public void SetSkillCount(SkillBase skill, int count)
    {
      _buttons[skill].SetCount(count);
    }

    public void SetSkillCooldownRatio(SkillBase skill, float cooldownRatio)
    {
      _buttons[skill].SetSkillCooldown(cooldownRatio);
    }

  /// <summary>
  /// 지정된 스킬의 갯수를 스킬 모듈에서 다시 읽어 UI를 업데이트한다.
  /// </summary>
    public void UpdateSkillCount(SkillBase skill)
    {
      _buttons[skill].SetCount(_skillModule.GetSkillCount(skill));
    }

    public int GetSkillCount(SkillBase skill)
    {
      return _skillModule.GetSkillCount(skill);
    }

    public void FocusOnButton(SkillUpgradeButton button)
    {
      // 기존에 포커스 하던거 있으면 언포커스

      if (_focusedButton != null)
      {
        _focusedButton.UnfocusState();
      }

      _focusedButton = button;

      _tooltipPanel.gameObject.SetActive(true);
      _tooltipPanel.SetSkillInfo(button.Skill);
    }

    public void Unfocus()
    {
      if (_focusedButton != null)
      {
        _focusedButton.UnfocusState();
        _focusedButton = null;

        _tooltipPanel.gameObject.SetActive(false);
      }
    }

    /// <summary>
    /// 스킬 모듈이 완전히 초기화 된 이후에 호출
    /// </summary>
    void OnSkillInfoInitialized(Dictionary<SkillBase, RuntimeSkillData> skillInfos)
    {
      // 하위 요소 순회하면서 버튼 초기화
      foreach (Transform tr in _rockGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this, skillInfos[button.Skill].Count);
        }
      }

      foreach (Transform tr in _fireGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this, skillInfos[button.Skill].Count);
        }
      }

      foreach (Transform tr in _waterGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this, skillInfos[button.Skill].Count);
        }
      }
    }

    void OnSkillCooldownRatioChanged(SkillBase skill, float ratio)
    {
      _buttons[skill].SetSkillCooldown(ratio);
    }

    void Awake()
    {
      _skillModule.SkillInfoInitialized += OnSkillInfoInitialized;
      _skillModule.SkillCooldownRatioChanged += OnSkillCooldownRatioChanged;

      _tooltipPanel.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
      _skillModule.SkillInfoInitialized -= OnSkillInfoInitialized;
      _skillModule.SkillCooldownRatioChanged -= OnSkillCooldownRatioChanged;
    }
  }
}