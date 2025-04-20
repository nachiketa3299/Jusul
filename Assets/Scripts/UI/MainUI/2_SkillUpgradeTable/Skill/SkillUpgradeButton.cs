using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  /// <summary>
  /// 지정된 스킬의 소유한 개수와 남은 쿨다운을 표시하고, 
  /// 누르면 다음 등급의 스킬로 업그레이드하는 버튼
  /// </summary>
  public class SkillUpgradeButton : ButtonBase
  {
    [Header("연결된 스킬")][Space]
    [SerializeField] SkillBase _skill;


    [Header("하위 UI 요소 연결")][Space]

    [SerializeField] Image _outline;
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _counter;
    [SerializeField] Image _icon;
    [SerializeField] Slider _cooldownBar;
    [SerializeField] GameObject _upgradeOverlay;

    [Space]

    [SerializeField] Color _colorOnUpgradable;

    bool _isFocused = false;
    bool _isUpgradableFocus = false;

    SkillUpgradeTableGrids _table;

    public SkillBase Skill => _skill;

    /// <summary>
    /// 테이블 및 버튼 이벤트 등록
    /// </summary>
    public void Initialize(SkillUpgradeTableGrids table, int count)
    {
      _table = table;
      _button.onClick.AddListener(OnClick);
      SetCount(count);
    }

    void OnClick()
    {
      if (!_isFocused)
      {
        FocusState();

        _table.FocusOnButton(this);
      }
      else
      {
        if (_isUpgradableFocus)
        {
          // 업그레이드
          PlayerController.Instance.TryUpgradeSkillByUI(_skill, this);
        }

        UnfocusState();

        _table.Unfocus();
      }
    }

    public void FocusState()
    {
      _isFocused = true;

      if (_table.GetSkillCount(_skill) > 0)
      {
        // Upgrade - Outline
        _upgradeOverlay.gameObject.SetActive(true);
        _outline.gameObject.SetActive(true);
        _isUpgradableFocus = true;
      }
      else
      {
        // Just Outline
        _outline.gameObject.SetActive(true);
      }
    }

    public void UnfocusState()
    {
      _isFocused = false;
      _isUpgradableFocus = false;

      _upgradeOverlay.gameObject.SetActive(false);
      _outline.gameObject.SetActive(false);
    }

    public void SetCount(int count)
    {
      _counter.text = count.ToString();

      if (count >= 3)
      {
        _counter.color = _colorOnUpgradable;
      }
      else
      {
        _counter.color = Color.white;
      }
    }

    public void SetSkillCooldown(float ratio)
    {
      _cooldownBar.value = ratio;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
      if (_skill == null)
      {
        return;
      }

      if (_skill.Icon == null)
      {
        return;
      }

      _icon.sprite = _skill.Icon;

      #if UNITY_EDITOR
      UnityEditor.EditorUtility.SetDirty(this);
      #endif
    }
#endif
  }
}