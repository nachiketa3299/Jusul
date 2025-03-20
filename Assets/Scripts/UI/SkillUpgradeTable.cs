using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class SkillUpgradeTable : MonoBehaviour
  {
    [Header("Skill Upgrade Groups")][Space]

    [SerializeField] HorizontalLayoutGroup _rockGroup;
    [SerializeField] HorizontalLayoutGroup _fireGroup;
    [SerializeField] HorizontalLayoutGroup _waterGroup;

    [Header("Tooltip Panel")][Space]
    [SerializeField] SkillTooltipPanel _tooltipPanel;

    // 스킬 - 버튼 맵핑
    Dictionary<SkillBase, SkillUpgradeButton> _buttons = new();

    // 현재 포커스된 상태라면 포커스한 버튼
    SkillUpgradeButton _focusedButton;

    public SkillUpgradeButton GetUpgradeButtonBySkill(SkillBase skill)
    {
      return _buttons[skill];
    }

    public void Initialize(Dictionary<SkillBase, int> skillCounts)
    {
      _tooltipPanel.gameObject.SetActive(false);

      // 하위 요소 순회하면서 버튼 초기화

      foreach (Transform tr in _rockGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this);
        }
      }

      foreach (Transform tr in _fireGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this);
        }
      }

      foreach (Transform tr in _waterGroup.transform)
      {
        if (tr.TryGetComponent<SkillUpgradeButton>(out var button))
        {
          _buttons.Add(button.Skill, button);
          button.Initialize(this);
        }
      }

      // 갯수 초기화

      foreach (var (skill, count) in skillCounts)
      {
        _buttons[skill].SetSkillCount(count);
      }
    }

    void SkillUpgradeTable_SkillCooldownChanged(SkillBase skill, float ratio)
    {
      _buttons[skill].SetSkillCooldown(ratio);
    }

    void SkillUpgradeTable_SkillCountChanged(SkillBase skill, int count)
    {
      _buttons[skill].SetSkillCount(count);
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

    void Start()
    {
      PlayerController.Instance.SkillCooldownChanged 
        += SkillUpgradeTable_SkillCooldownChanged;
      PlayerController.Instance.SkillCountChanged
        += SkillUpgradeTable_SkillCountChanged;
    }

    void OnDestroy()
    {
      PlayerController.Instance.SkillCooldownChanged 
        -= SkillUpgradeTable_SkillCooldownChanged;
      PlayerController.Instance.SkillCountChanged
        -= SkillUpgradeTable_SkillCountChanged;
    }
  }
}