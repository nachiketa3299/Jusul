using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// 컨트롤러들의 베이스 클래스
  /// </summary>
  [DisallowMultipleComponent]
  public abstract class JusulCharacterControllerBase : MonoBehaviour 
  {
    [Header("모듈")][Space]
    [SerializeField] protected SkillModule _skillModule;
    [SerializeField] ResourceModule _resourceModule;
    [SerializeField] protected BountyModule _bountyModule;
    [SerializeField] EnhanceModule _enhanceModule;

    protected CharacterModel _controllingCharacter;
    protected int _laneIndex;

    virtual public void InitializeOnStart(int laneIndex, CharacterModel characterPrefab, PlayerInfo info)
    {
      _laneIndex = laneIndex;

      CharacterModel character = Instantiate(characterPrefab);
      character.InitializeOnStart(laneIndex, info);

      _controllingCharacter = character;

      // 하위 모듈 초기화
      _resourceModule.InitializeOnStart();
      _skillModule.InitializeOnStart(_laneIndex, character);
      _bountyModule.InitializeOnStart(_laneIndex);
      _enhanceModule.InitializeOnStart();
    }

    // 캐릭터를 지정된 레인으로 이동시키기
    public void MoveControllingCharacterToLaneStart()
    {
      LaneManager.Instance.SetCharacterAtLane(_laneIndex, _controllingCharacter);
    }

    public void AddReward(RewardEntry reward)
    {
      _resourceModule.AddReward(reward);
    }

    protected bool TrySpawnBounty(BountyEnemy bountyEnemy)
    {
      return _bountyModule.TrySpawnBounty(bountyEnemy);
    }

    protected bool TryPurchaseSkill(out SkillBase purchasedSkill)
    {
      purchasedSkill = null;

      // 돈 부족하면 안됨
      if (!_resourceModule.CanAffordGoldAmount(_skillModule.SkillPurchasePrice))
      {
        return false;
      }

      if (!_skillModule.CanAddNewSkill)
      {
        return false;
      }

      // 구매
      _resourceModule.AddGoldAmount(-_skillModule.SkillPurchasePrice);
      _skillModule.IncreaseSkillPurchasePrice();

      // 뽑기
      SkillBase pickedSkill = _skillModule.TryPickSkill();

      if (pickedSkill == null)
      {
        return false;
      }

      // 구매한 스킬 실제로 증가시키기
      // NOTE: Player의 UI 변경은 BeamEffect가 도달한 후에 적용되어야함
      _skillModule.AddSkill(pickedSkill, 1);

      purchasedSkill = pickedSkill;

      return true;
    }

    protected bool TryUpgradeSkill(in SkillBase skillToUpgrade, out SkillBase upgradedSkill)
    {
      upgradedSkill = null;

      // 현재 등급이 천벌이면 다음 티어가 없어서 업그레이드 할 수가 없음
      if (skillToUpgrade.Rarity == SkillRarity.Scourge)
      {
        return false;
      }

      SkillBase pickedSkill = _skillModule.TryUpgradeSkill(skillToUpgrade);

      if (pickedSkill == null)
      {
        return false;
      }

      _skillModule.AddSkill(skillToUpgrade, -3);
      // 업그레이드한 스킬의 카운트를 증가시키기
      // NOTE: Player의 UI 변경은 BeamEffect가 도달한 후에 적용되어야함
      _skillModule.AddSkill(pickedSkill, 1);

      upgradedSkill = pickedSkill;
      return true;
    }

    protected bool TryMineSkill(in SkillRarity rarity, in int soulCost, out SkillBase minedSkill)
    {
      minedSkill = null;

      if (!_resourceModule.CanAffordSoulAmount(soulCost))
      {
        return false;
      }

      if (!_skillModule.CanAddNewSkill)
      {
        return false;
      }

      _resourceModule.AddSoulAmount(-soulCost);

      SkillBase pickedSkill = _skillModule.TryMineSkill(rarity);

      if (pickedSkill == null)
      {
        return false;
      }

      _skillModule.AddSkill(pickedSkill, 1);

      minedSkill = pickedSkill;
      return true;
    }

    protected bool TryEnhanceSkillAttributeLevel(in SkillAttribute attribute) 
    {
      int currentLevel = _skillModule.GetSkillAttributeLevel(attribute);
      int targetLevel = currentLevel + 1;

      // 강화 비용 테이블에 비용이 있음
      if (_enhanceModule.TryGetNextAttributeEnhanceCost(targetLevel, attribute, out CostType costType, out int cost))
      {
        // 비용을 낼 수 있음
        if (_resourceModule.CanAffordCost(costType, cost))
        {
          // 비용을 지불함
          _resourceModule.AddCostAmount(costType, -cost);

          // 스킬 속성 레벨 업
          _skillModule.SetSkillAttributeLevel(attribute, targetLevel);

          return true;
        }

        return false;
      }
      else
      {
        return false;
      }
    }

    protected bool TryEnhanceSkillPurchaseLevel()
    {
      int currentLevel = _skillModule.GetSkillPurchaseLevel();
      int targetLevel = currentLevel + 1;

      // 강화 비용 테이블에 비용이 있음
      if (_enhanceModule.TryGetNextSkillPurchaseEnhanceCost(targetLevel, out CostType costType, out int cost))
      {
        // 비용을 낼 수 있음
        if (_resourceModule.CanAffordCost(costType, cost))
        {
          // 비용을 지불함
          _resourceModule.AddCostAmount(costType, -cost);

          // 스킬 구매 레벨 업
          _skillModule.SetSkillPurchaseLevel(targetLevel);

          return true;
        }

        return false;
      }
      else
      {
        return false;
      }
    }

    protected virtual void Awake() {}
  }
}