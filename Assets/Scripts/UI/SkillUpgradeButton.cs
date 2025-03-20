using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Jusul
{
  public class SkillUpgradeButton : ButtonBase
  {
    [Header("Skill Settings")][Space]
    [SerializeField] SkillBase _skill;


    [Header("UI Settings")][Space]

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

    SkillUpgradeTable _table;

    public SkillBase Skill => _skill;

    public void Initialize(SkillUpgradeTable table)
    {
      _table = table;
      _button.onClick.AddListener(SkillUpgradeButton_ButtonClicked);
    }

    void SkillUpgradeButton_ButtonClicked()
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
          PlayerController.Instance.TryUpgradeSkillByButton(_skill, this);
        }

        UnfocusState();

        _table.Unfocus();
      }
    }

    public void FocusState()
    {
      _isFocused = true;

      if (PlayerController.Instance.SkillCounts[_skill] > 0)
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

    public void SetSkillCount(int count)
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

    public void RefreshSkillCount()
    {
      SetSkillCount(PlayerController.Instance.SkillCounts[_skill]);
    }

    public void SetSkillCooldown(float ratio)
    {
      _cooldownBar.value = ratio;
    }

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

  }
}