using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [DisallowMultipleComponent]
  public abstract class JCharacterController : MonoBehaviour 
  {
    [Header("Character Prefab")][Space]
    [SerializeField] protected Character _characterPrefab;

    [Header("Skill Infos")][Space]
    [SerializeField] SkillTable _skillTable;
    [SerializeField] SkillRarityProbability _skillRarityProbability;

    [Header("Resources")][Space]
    [SerializeField]  int _initialGold = 100;
    [SerializeField]  int _initialSoul = 2;

    [Header("Purchase")][Space]
    [SerializeField] int _initialSkillPurchasePrice = 20;

    [Header("Bounty")][Space]
    [SerializeField] int _bountyCooldownSeconds = 10;
    [SerializeField] protected BountyTable _bountyTable;

    [Header("Enhance Levels")][Space]
    [SerializeField] EnhanceLevelCostTable _enhanceLevelTable;

    public int GetNextLevelCost(EnhanceType type)
    {
      switch (type)
      {
      case EnhanceType.Purchase:
        return _enhanceLevelTable.LevelsAt[_purchaseLevel].PurchaseLevel;
      case EnhanceType.Rock:
        return _enhanceLevelTable.LevelsAt[_rockLevel].RockSkillLevel;
      case EnhanceType.Fire:
        return _enhanceLevelTable.LevelsAt[_fireLevel].FireSkillLevel;
      case EnhanceType.Water:
        return _enhanceLevelTable.LevelsAt[_waterLevel].WaterSkillLevel;
      case EnhanceType.Cryptid:
        return _enhanceLevelTable.LevelsAt[_cryptidLevel].CryptidLevel;
      }

      return 0;
    }

    public int GetCurrentAttributeLevel(SkillAttribute attribute)
    {
      switch (attribute)
      {
        case SkillAttribute.Rock: return _rockLevel;
        case SkillAttribute.Fire: return _fireLevel;
        case SkillAttribute.Water: return _waterLevel;
      }
      return 0;
    }


    public int GetCurrentLevel(EnhanceType type)
    {
      switch (type)
      {
      case EnhanceType.Purchase:
        return _purchaseLevel;
      case EnhanceType.Rock:
        return _rockLevel;
      case EnhanceType.Fire:
        return _fireLevel;
      case EnhanceType.Water:
        return _waterLevel;
      case EnhanceType.Cryptid:
        return _cryptidLevel;
      }
      return 0;
    }

    public Action<int, int> PurchaseLevelChanged;
    public Action<int, int> RockLevelChanged;
    public Action<int, int> FireLevelChanged;
    public Action<int, int> WaterLevelChanged;
    public Action<int, int> CryptidLevelChanged;

    protected int _purchaseLevel = 0;
    protected int _rockLevel = 0;
    protected int _fireLevel = 0;
    protected int _waterLevel = 0;
    protected int _cryptidLevel = 0;

    protected bool _canSpawnBounty = false;
    public Action BountyCooldownStarted;
    public Action<float, float> BountyCooldownPassed;
    public Action BountyCooldownEnded;

    const int MAX_SKILL_COUNT = 25;
    protected Character _controllingCharacter;
    protected int _currentSoul;
    protected int _currentSkillCount;
    protected int _currentGold;
    protected int _currentSkillPurchasePrice;

    int _totalSkillCount = 0;

    protected Dictionary<SkillBase, int> _skillCounts = new();
    protected Dictionary<SkillBase, bool> _isSkillCoolDown = new();
    protected Dictionary<SkillBase, Coroutine> _cooldownRoutines = new();

    public Dictionary<SkillBase, int> SkillCounts => _skillCounts;

    public Action<SkillBase, int> SkillCountChanged;
    public Action<int, int> TotalSkillCountChanged;

    public Action<SkillBase, float> SkillCooldownChanged;

    public Action<int> GoldAmountChanged;
    public Action<int> SoulAmountChanged;
    public Action<int> SkillPurchasePriceChanged;

    protected int  _skillRarityProbabilityLevel = 0;

    protected int _laneIndex;

    public int SoulAmount => _currentSoul;
    public int GoldAmount => _currentGold;
    public int SkillPurchasePrice => _currentSkillPurchasePrice;

    // info 바탕으로 캐릭터 인스턴스 만들고 초기화
    // Game Bootstrap Manager의 Awake에서 호출됨
    public void Initialize(int laneIndex, PlayerInfo info)
    {
      _laneIndex = laneIndex;

      Character character = Instantiate(_characterPrefab);
      character.name = $"Character_{info.PlayerId}";

      // 캐릭터를 지정한 정보로 업데이트
      character.Initialize(laneIndex, info);
      _controllingCharacter = character;

      // 골드 양 초기화
      _currentGold = _initialGold;
      // GoldAmountChanged?.Invoke(_currentGold);

      // 영혼 양 초기화
      _currentSoul = _initialSoul;
      // SoulAmountChanged?.Invoke(_currentSoul);

      // 구매 비용 초기화
      _currentSkillPurchasePrice = _initialSkillPurchasePrice;

      // 속성별로 모든 스킬을 일단 0개로 초기화
      _totalSkillCount = 0;

      // 스킬 테이블에 있는 것을 하나로 뭉치기
      // 쿨다운 테이블도 만들기
      foreach (SkillBase rockSkill in _skillTable.RockSkills)
      {
        _skillCounts.Add(rockSkill, 0);
        _isSkillCoolDown.Add(rockSkill, false);
        _cooldownRoutines.Add(rockSkill, null);
      }

      foreach (SkillBase fireSkill in _skillTable.FireSkills)
      {
        _skillCounts.Add(fireSkill, 0);
        _isSkillCoolDown.Add(fireSkill, false);
        _cooldownRoutines.Add(fireSkill, null);
      }

      foreach (SkillBase waterSkill in _skillTable.WaterSkills)
      {
        _skillCounts.Add(waterSkill, 0);
        _isSkillCoolDown.Add(waterSkill, false);
        _cooldownRoutines.Add(waterSkill, null);
      }

      // TotalSkillCountChanged?.Invoke(_totalSkillCount, MAX_SKILL_COUNT);

      // 바운티 타이머 시작
      StartCoroutine(BountyCooldownRoutine());
    }

    public SkillBase TryMineSkill(SkillRarity rarity, int soulCost)
    {

      if (_currentSoul < soulCost)
      {
        return null;
      }

      _currentSoul -= soulCost;
      SoulAmountChanged?.Invoke(_currentSoul);

      SkillAttribute attribute = (SkillAttribute)UnityEngine.Random.Range(0, 3);
      SkillBase minedSkill = _skillTable.GetSkill(attribute, rarity);

      ++_skillCounts[minedSkill];

      ++_totalSkillCount;
      TotalSkillCountChanged?.Invoke(_totalSkillCount, MAX_SKILL_COUNT);

      return minedSkill;
    }

    IEnumerator BountyCooldownRoutine()
    {
      float elapsedTime = 0f;
      BountyCooldownStarted?.Invoke();
      _canSpawnBounty = false;
      while (elapsedTime < _bountyCooldownSeconds)
      {
        BountyCooldownPassed?.Invoke(elapsedTime, _bountyCooldownSeconds);
        elapsedTime += Time.deltaTime;
        yield return null;
      }
      BountyCooldownEnded?.Invoke();
      _canSpawnBounty = true;
    }

    public void TrySpawnBounty(BountyEnemy bountyEnemy)
    {
      if (!_canSpawnBounty)
      {
        return;
      }

      LaneManager.Instance.SpawnEnemyAtLane(_laneIndex, bountyEnemy);

      StartCoroutine(BountyCooldownRoutine());
    }

    IEnumerator SkillsCooldownRoutine(SkillBase skill)
    {
      _isSkillCoolDown[skill] = true;

      float elapsedTime = 0.0f;
      while (elapsedTime <= skill.Cooldown)
      {
        elapsedTime += Time.deltaTime;

        SkillCooldownChanged?.Invoke(skill, Mathf.Clamp01(elapsedTime / skill.Cooldown));

        yield return null;
      }

      _isSkillCoolDown[skill] = false;
    }

    public void AddGold(int goldAmount)
    {
      _currentGold += goldAmount;

      GoldAmountChanged?.Invoke(_currentGold);
    }

    public void AddSoul(int soulAmount)
    {
      _currentSoul += soulAmount;
      SoulAmountChanged?.Invoke(_currentSoul);
    }

    // 스킬 구매 (TODO)
    public SkillBase TryPurchaseSkill()
    {
      // 돈 부족하면 안됨

      if (_currentSkillPurchasePrice > _currentGold)
      {
        return null;
      }

      // 스킬 수 제한 도달했어도 안됨

      if (_totalSkillCount >= MAX_SKILL_COUNT)
      {
        return null;
      }

      // 뽑기

      SkillAttribute attribute = (SkillAttribute)UnityEngine.Random.Range(0, 3);
      SkillRarity rarity = _skillRarityProbability.PickWithLevel(_skillRarityProbabilityLevel);

      SkillBase skill = _skillTable.GetSkill(attribute, rarity);

      // 구매

      _currentGold -= _currentSkillPurchasePrice;
      GoldAmountChanged?.Invoke(_currentGold);
      ++_currentSkillPurchasePrice;
      SkillPurchasePriceChanged?.Invoke(_currentSkillPurchasePrice);

      // 구매한 스킬 실제로 증가시키기

      // NOTE: 증가 시킨 UI 변경 자체는 BeamEffect가 도달한 후에 적용되어야함
      ++_skillCounts[skill];

      ++_totalSkillCount;
      TotalSkillCountChanged?.Invoke(_totalSkillCount, MAX_SKILL_COUNT);

      return skill;
    }

    public SkillBase TryUpgradeSkill(SkillBase skill)
    {
      // 천벌이면 다음 티어가 없어서 업그레이드 할 수가 없음
      if (skill.Rarity == SkillRarity.Chunbul)
      {
        return null;
      }

      // 3개가 안 되면 잘못된 요청임
      if (_skillCounts[skill] < 3)
      {
        return null;
      }

      // 다음 티어 스킬 바위/불/물 속성 중 랜덤으로 하나를 뽑음
      SkillRarity nextRarity = (SkillRarity)((int)skill.Rarity + 1);
      SkillAttribute attribute = (SkillAttribute)UnityEngine.Random.Range(0, 3);
      SkillBase pickedSkill = _skillTable.GetSkill(attribute, nextRarity);

      // 우선 세개 뺀다 (이건 즉시 빼도 상관 없음)
      _skillCounts[skill] -= 3;

      // 0개인 경우 쿨다운 중지 시켜야 함
      if (_skillCounts[skill] == 0)
      {
        if (_cooldownRoutines[skill] != null)
        {
          _isSkillCoolDown[skill] = false;
          SkillCooldownChanged?.Invoke(skill, 0f);

          StopCoroutine(_cooldownRoutines[skill]);

          _cooldownRoutines[skill] = null;
        }
      }

      // UI 업데이트가 즉시 이루어져야 함
      SkillCountChanged?.Invoke(skill, _skillCounts[skill]);
      _totalSkillCount -= 3;

      // 업그레이드한 스킬의 카운트를 증가시키기
      // NOTE: 증가 시킨 UI 변경 자체는 BeamEffect가 도달한 후에 적용되어야 함
      ++_skillCounts[pickedSkill];
      // SkillCountChanged?.Invoke(pickedSkill, _skillCounts[pickedSkill]);
      ++_totalSkillCount;

      TotalSkillCountChanged?.Invoke(_totalSkillCount, MAX_SKILL_COUNT);

      return pickedSkill;
    }

    // 캐릭터를 지정된 레인으로 이동시키기
    public void MoveControllingCharacterToLaneStart()
    {
      LaneManager.Instance.SetCharacterAtLane(_laneIndex, _controllingCharacter);
    }

    public void TryEnhancePurchase()
    {
      int nextLevelGoldCost 
        = _enhanceLevelTable.LevelsAt[_purchaseLevel].PurchaseLevel;

      if (_currentGold < nextLevelGoldCost)
      {
        return;
      }

      _currentGold -= nextLevelGoldCost;
      GoldAmountChanged?.Invoke(_currentGold);

      ++_purchaseLevel;
      PurchaseLevelChanged?.Invoke
      (
        _purchaseLevel,
        _enhanceLevelTable.LevelsAt[_purchaseLevel].PurchaseLevel
      );
    }

    public void TryEnhanceRock()
    {
      int nextLevelSoulCost
        = _enhanceLevelTable.LevelsAt[_rockLevel].RockSkillLevel;
      
      if (_currentSoul < nextLevelSoulCost)
      {
        return;
      }

      _currentSoul -= nextLevelSoulCost;
      SoulAmountChanged?.Invoke(_currentSoul);

      ++_rockLevel;
      RockLevelChanged?.Invoke
      (
        _rockLevel,
        _enhanceLevelTable.LevelsAt[_rockLevel].RockSkillLevel
      );
    }

    public void TryEnhanceFire()
    {
      int nextLevelSoulCost
        = _enhanceLevelTable.LevelsAt[_fireLevel].FireSkillLevel;
      
      if (_currentSoul < nextLevelSoulCost)
      {
        return;
      }

      _currentSoul -= nextLevelSoulCost;
      SoulAmountChanged?.Invoke(_currentSoul);

      ++_fireLevel;
      FireLevelChanged?.Invoke
      (
        _fireLevel,
        _enhanceLevelTable.LevelsAt[_fireLevel].FireSkillLevel
      );
    }

    public void TryEnhanceWater()
    {
      int nextLevelSoulCost
        = _enhanceLevelTable.LevelsAt[_waterLevel].WaterSkillLevel;
      
      if (_currentSoul < nextLevelSoulCost)
      {
        return;
      }

      _currentSoul -= nextLevelSoulCost;
      SoulAmountChanged?.Invoke(_currentSoul);

      ++_waterLevel;
      WaterLevelChanged?.Invoke
      (
        _waterLevel,
        _enhanceLevelTable.LevelsAt[_waterLevel].WaterSkillLevel
      );
    }

    public void TryEnhanceCryptid()
    {
      int nextLevelSoulCost
        = _enhanceLevelTable.LevelsAt[_cryptidLevel].CryptidLevel;
      
      if (_currentSoul < nextLevelSoulCost)
      {
        return;
      }

      _currentSoul -= nextLevelSoulCost;
      SoulAmountChanged?.Invoke(_currentSoul);

      ++_cryptidLevel;
      CryptidLevelChanged?.Invoke
      (
        _cryptidLevel,
        _enhanceLevelTable.LevelsAt[_cryptidLevel].CryptidLevel
      );
    }

    protected virtual void Awake() {}

    bool _isStarted = false;

    void Update()
    {
      // 지연된 초기화
      if (!_isStarted)
      {
        _isStarted = true;

        TotalSkillCountChanged?.Invoke(_totalSkillCount, MAX_SKILL_COUNT);
        GoldAmountChanged?.Invoke(_currentGold);
        SoulAmountChanged?.Invoke(_currentSoul);
        SkillPurchasePriceChanged?.Invoke(_currentSkillPurchasePrice);

        foreach (var (skill, count) in _skillCounts)
        {
          SkillCountChanged?.Invoke(skill, count);
        }
      }

      // 0개 넘는 스킬 중 쿨다운 아닌 것들 Fire
      foreach (var (skill, count) in _skillCounts)
      {
        if (count == 0) 
          continue;
        
        if (_isSkillCoolDown[skill])
          continue;
        
        skill.Fire(_controllingCharacter, _laneIndex);
        _controllingCharacter.Fire();

        _cooldownRoutines[skill] = StartCoroutine(SkillsCooldownRoutine(skill));
      }
    }

    }
}