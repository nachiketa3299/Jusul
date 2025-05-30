using TMPro;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public class SkillPurchaseProbabilityIndicator : MonoBehaviour
  {
    [Header("플레이어 연결")][Space]
    [SerializeField] SkillModule _skillModule;

    [Header("확률 테이블 연결")][Space]
    [SerializeField] SkillRarityTable _skillRarityTable;

    [Header("하위 UI 요소 연결")][Space]
    [SerializeField] TMP_Text _textOnNormal;
    [SerializeField] TMP_Text _titleOnNormal;
    [Space]
    [SerializeField] TMP_Text _textOnRare;
    [SerializeField] TMP_Text _titleOnRare;
    [Space]
    [SerializeField] TMP_Text _textOnHero;
    [SerializeField] TMP_Text _titleOnHero;
    [Space]
    [SerializeField] TMP_Text _textOnLegend;
    [SerializeField] TMP_Text _titleOnLegend;
    [Space]
    [SerializeField] TMP_Text _textOnAncestor;
    [SerializeField] TMP_Text _titleOnAncestor;
    [Space]
    [SerializeField] TMP_Text _textOnScourge;
    [SerializeField] TMP_Text _titleOnScourge;

    [Header("희귀도 컬러 테이블")][Space]
    [SerializeField] SkillRarityResources _skillRarityColorTable;

    [HideInInspector][SerializeField] int _skillPurchaseLevel = 1;

    public void InitializationOnAwake()
    {
      _skillModule.SkillPurchaseLevelInitialized += OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged += OnSkillPurchaseLevelChanged;
    }

    void OnSkillPurchaseLevelInitialized(int initLevel)
    {
      _skillPurchaseLevel = initLevel;
      UpdateProbabilityText();
    }

    void OnSkillPurchaseLevelChanged(int prevLevel, int currentLevel)
    {
      _skillPurchaseLevel = currentLevel;
      UpdateProbabilityText();
    }

    /// <summary>
    /// <strong>현재</strong> 레벨의 확률로 확률을 업데이트
    /// </summary>
    void UpdateProbabilityText()
    {
      _textOnNormal.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Normal):F2}%";
      _textOnRare.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Rare):F2}%";
      _textOnHero.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Hero):F2}%";
      _textOnLegend.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Legend):F2}%";
      _textOnAncestor.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Ancestor):F2}%";
      _textOnScourge.text = $"{_skillRarityTable.GetProbability(_skillPurchaseLevel, SkillRarity.Scourge):F2}%";
    }

    void SetSkillRarityColors()
    {
      _titleOnNormal.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Normal);
      _titleOnRare.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Rare);
      _titleOnHero.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Hero);
      _titleOnLegend.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Legend);
      _titleOnAncestor.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Ancestor);
      _titleOnScourge.color = _skillRarityColorTable.GetColorBySkillRarity(SkillRarity.Scourge);
    }

    void OnDestroy()
    {
      _skillModule.SkillPurchaseLevelInitialized -= OnSkillPurchaseLevelInitialized;
      _skillModule.SkillPurchaseLevelChanged += OnSkillPurchaseLevelChanged;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
      SetSkillRarityColors();
      UpdateProbabilityText();
      UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
  }
}