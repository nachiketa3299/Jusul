using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// (싱글턴) 플레이어의 컨트롤러
  /// UI로 JCharacterController의 기능에 접근
  /// </summary>
  public class PlayerController : JusulCharacterControllerBase
  {
    [Header("UI 연결")][Space]
    [SerializeField] SkillUpgradeTableGrids _skillUpgradeGrids;

    static PlayerController _instance;
    public static PlayerController Instance => _instance;

    public bool TryPurchaseSkillByUI(SkillPurchaseButton purchaseButton)
    {
      if (TryPurchaseSkill(out SkillBase purchasedSkill))
      {
        SkillUpgradeButton purchasedButton = _skillUpgradeGrids.GetButtonBySkill(purchasedSkill);

        // 빔 이펙트 지연 업데이트
        BeamEffectManager.Instance.StartBeamEffect
        (
          purchaseButton.transform.position, 
          purchasedButton.transform.position, 
          () => { _skillUpgradeGrids.UpdateSkillCount(purchasedSkill); }
        );

        return true;
      }
      else
      {
        return false;
      }
    }

    public bool TryUpgradeSkillByUI(SkillBase skillToUpgrade, SkillUpgradeButton upgradeButton)
    {
      if (TryUpgradeSkill(in skillToUpgrade, out SkillBase upgradedSkill))
      {
        SkillUpgradeButton upgradedButton = _skillUpgradeGrids.GetButtonBySkill(upgradedSkill);

        // 업그레이드 이전 스킬 UI 업데이트를 강제로 발생시킴
        _skillUpgradeGrids.UpdateSkillCount(skillToUpgrade);

        // 빔 이펙트 지연 업데이트
        BeamEffectManager.Instance.StartBeamEffect
        (
          upgradeButton.transform.position, 
          upgradedButton.transform.position, 
          () => { _skillUpgradeGrids.UpdateSkillCount(upgradedSkill); }
        );

        return true;
      }
      else
      {
        return false;
      }
    }

    public bool TryMineSkillByUI(MineButton mineButton, SkillRarity rarity, int soulCost)
    {
      if (TryMineSkill(in rarity, in soulCost, out SkillBase minedSkill))
      {
        SkillUpgradeButton minedButton = _skillUpgradeGrids.GetButtonBySkill(minedSkill);

        // 빔 이펙트 지연 업데이트
        BeamEffectManager.Instance.StartBeamEffect
        (
          mineButton.transform.position,
          minedButton.transform.position,
          () => { _skillUpgradeGrids.UpdateSkillCount(minedSkill); }
        );

        return true;
      }
      else
      {
        return false;
      }
    }

    public bool TrySpawnBountyByUI(BountyEnemy bountyEnemy)
    {
      return TrySpawnBounty(bountyEnemy);
    }

    public bool TryEnhanceSkillAttributeLevelByUI(in SkillAttribute attribute)
    {
      return TryEnhanceSkillAttributeLevel(attribute);
    }

    public bool TryEnhanceSkillPurchaseLevelByUI()
    {
      return TryEnhanceSkillPurchaseLevel();
    }

    // 싱글턴으로 만들어주는 작업
    protected override void Awake()
    {
      _instance = this;

      base.Awake();
    }
  }
}