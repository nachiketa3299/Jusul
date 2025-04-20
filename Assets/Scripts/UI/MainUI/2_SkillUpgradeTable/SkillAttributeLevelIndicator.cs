using TMPro;

using UnityEngine;

namespace Jusul
{

  [DisallowMultipleComponent]
  public class SkillAttributeLevelIndicator : MonoBehaviour
  {
    [Header("플레이어 연결")][Space]

    [SerializeField] SkillModule _skillModule;

    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] TMP_Text _rockText;
    [SerializeField] TMP_Text _fireText;
    [SerializeField] TMP_Text _waterText;

    public void InitializeOnAwake()
    {
      _skillModule.SkillAttributeLevelInitialized += OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged += OnSkillAttributeLevelChanged;
    }

    void OnSkillAttributeLevelInitialized(SkillAttribute attribute, int initLevel)
    {
      switch (attribute)
      {
        case SkillAttribute.Rock:
          _rockText.text = initLevel.ToString();
          break;
        case SkillAttribute.Fire:
          _fireText.text = initLevel.ToString();
          break;
        case SkillAttribute.Water:
          _waterText.text = initLevel.ToString();
          break;
      }
    }

    void OnSkillAttributeLevelChanged(SkillAttribute attribute, int prevLevel, int currentLevel)
    {
      switch (attribute)
      {
        case SkillAttribute.Rock:
          _rockText.text = currentLevel.ToString();
          break;
        case SkillAttribute.Fire:
          _fireText.text = currentLevel.ToString();
          break;
        case SkillAttribute.Water:
          _waterText.text = currentLevel.ToString();
          break;
      }
    }

    void OnDestroy()
    {
      _skillModule.SkillAttributeLevelInitialized -= OnSkillAttributeLevelInitialized;
      _skillModule.SkillAttributeLevelChanged -= OnSkillAttributeLevelChanged;
    }
  }
}