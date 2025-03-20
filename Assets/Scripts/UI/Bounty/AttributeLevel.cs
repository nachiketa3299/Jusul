using TMPro;

using UnityEngine;

namespace Jusul
{

  [DisallowMultipleComponent]
  public class AttributeLevel : MonoBehaviour
  {
    [SerializeField] TMP_Text _levelText;
    [SerializeField] SkillAttribute _skillAttribute = SkillAttribute.Fire;

    void Start()
    {
      switch (_skillAttribute)
      {
      case SkillAttribute.Rock:
        PlayerController.Instance.RockLevelChanged
          += SkillAttributeLevelChanged;
        break;
      case SkillAttribute.Fire:
        PlayerController.Instance.FireLevelChanged
          += SkillAttributeLevelChanged;
        break;
      case SkillAttribute.Water:
        PlayerController.Instance.WaterLevelChanged
          += SkillAttributeLevelChanged;
        break;
      }

      SkillAttributeLevelChanged(PlayerController.Instance.GetCurrentAttributeLevel(_skillAttribute), 0);
    }

    void SkillAttributeLevelChanged(int to, int _)
    {
      _levelText.text = to.ToString();
    }
  }
}