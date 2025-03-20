using TMPro;
using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class DamageIndicator : MonoBehaviour
  {
    [Header("UI Settings")][Space]
    [SerializeField] TMP_Text _damageText;


    [Header("Colors")][Space]

    [SerializeField] Color _colorOnRockAttribute;
    [SerializeField] Color _colorOnFireAttribute;
    [SerializeField] Color _colorOnWaterAttribute;

    [Header("Animation Settings")][Space]
    [SerializeField] Animation _animation;

    SkillBase _skill;

    public void Initialize(SkillBase skill, Enemy enemy)
    {
      _skill = skill;

      _damageText.text = $"{skill.AttackPower}";

      transform.position = enemy.transform.position;

      switch (skill.Attribute)
      {
      case SkillAttribute.Rock:
        _damageText.color = _colorOnRockAttribute;
        break;
      case SkillAttribute.Fire:
        _damageText.color = _colorOnFireAttribute;
        break;
      case SkillAttribute.Water:
        _damageText.color = _colorOnWaterAttribute;
        break;
      }
    }

    public void DamageIndicationAnimationEnd()
    {
      Destroy(gameObject);
    }

    public void Activate()
    {
      _animation.Play();
    }
  }
}