using TMPro;
using UnityEngine;
using UnityEngine.WSA;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class DamageIndicator : MonoBehaviour
  {
    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] TMP_Text _damageText;

    [Header("속성별 색상")][Space]
    [SerializeField] Color _colorOnRockAttribute;
    [SerializeField] Color _colorOnFireAttribute;
    [SerializeField] Color _colorOnWaterAttribute;

    [Header("애니메이션")][Space]
    [SerializeField] Animator _animator;

    SkillBase _skill;

    /// <param name="skill">추후 데미지 인디케이션이 확장되는 경우를 위하여, <see cref="SkillBase"/> 
    /// 자체를 넘기는 것으로 </param>
    public void Initialize(Enemy enemy, SkillBase skill, int finalDamage)
    {
      _skill = skill;
      _damageText.text = finalDamage.ToString();
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

    public void OnAnimationEnd()
    {
      Destroy(gameObject);
    }

    public void Activate()
    {
    }
  }
}