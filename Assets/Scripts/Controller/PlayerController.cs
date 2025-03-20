using UnityEngine;

namespace Jusul
{
  public class PlayerController : JCharacterController
  {
    [Header("UI Connection")][Space]
    [SerializeField] SkillUpgradeTable _skillUpgradeTable;

    static PlayerController _instance;
    public static PlayerController Instance => _instance;


    public void MineSkillByButton(MineButton button, SkillRarity rarity, int soulCost)
    {
      SkillBase minedSkill = TryMineSkill(rarity, soulCost);

      if (minedSkill == null)
      {
        return;
      }

      SkillUpgradeButton targetButton
        = _skillUpgradeTable.GetUpgradeButtonBySkill(minedSkill);

      BeamEffectManager.Instance.SpawnBeam(button, targetButton);
    }

    public void TryPurchaseSkillByButton(SkillRandomPurchaseButton button)
    {
      // 구매한 스킬이 무엇인지 확인
      SkillBase purchasedSkill = TryPurchaseSkill();

      if (purchasedSkill == null)
      {
        return;
      }

      // 빔이 도달할 버튼
      SkillUpgradeButton targetButton 
        = _skillUpgradeTable.GetUpgradeButtonBySkill(purchasedSkill);

      // 빔이 도달한 후 UI 업데이트가 이루어져야 함
      // (항상 타겟 버튼만 지연되어서 업데이트 되면 됨)
      BeamEffectManager.Instance.SpawnBeam(button, targetButton);
    }

    public void TryUpgradeSkillByButton(SkillBase skill, SkillUpgradeButton button)
    {
      // 업그레이드한 스킬이 무엇인지 확인
      SkillBase upgradedSkill = TryUpgradeSkill(skill);

      if (upgradedSkill == null)
      {
        return;
      }

      // 빔이 도달할 버튼
      SkillUpgradeButton targetButton 
        = _skillUpgradeTable.GetUpgradeButtonBySkill(upgradedSkill);
      
      // 빔이 도달한 후에야 UI 업데이트가 이루어져야 함
      // (항상 타겟 버튼만 지연되어서 업데이트 되면 됨)
      BeamEffectManager.Instance.SpawnBeam(button, targetButton);
    }

    protected override void Awake()
    {
      _instance = this;

      base.Awake();
    }

    void Start()
    {
      _skillUpgradeTable.Initialize(_skillCounts);
    }

  }
}