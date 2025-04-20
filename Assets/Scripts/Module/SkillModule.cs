using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Jusul
{
  /// <summary>
  /// 스킬 모듈은 어떤 캐릭터가 보유한 모든 스킬에 대해 다음을 관리한다:
  /// <list type="bullet">
  ///   <item>스킬을 발사를 제어한다.</item>
  ///   <item>스킬의 개수와 쿨다운 현황을 관리한다.</item>
  ///   <item>새로운 스킬을 뽑을 수 있게 한다. (구매/업그레이드/채굴)</item>
  /// </list>
  /// </summary>
  [DisallowMultipleComponent]
  public class SkillModule : MonoBehaviour
  {
    [Header("스킬 전체 정보")][Space]
    [SerializeField] int _maxSkillCount = 20;
    [SerializeField] SkillTable _skillTable;
    [SerializeField] SkillEnhanceRateTable _skillEnhanceRateTable;

    [Header("스킬 뽑기 정보")][Space]
    [SerializeField] SkillRarityTable _skillRarityProbability;

    [Header("구매 가격")][Space]
    [SerializeField] int _initialSkillPurchasePrice = 20;

    public event Action<Dictionary<SkillBase, RuntimeSkillData>> SkillInfoInitialized;
    public event Action<int> SkillPurchasePriceInitialized;
    public event Action<int, int> SkillPurchasePriceChanged;
    public event Action<int, int> TotalSkillCountInitialized;
    public event Action<int, int> TotalSkillCountChanged;
    public event Action<SkillBase, float> SkillCooldownRatioChanged;

    public event Action<SkillAttribute, int> SkillAttributeLevelInitialized;
    public event Action<SkillAttribute, int, int> SkillAttributeLevelChanged;

    public event Action<int> SkillPurchaseLevelInitialized;
    public event Action<int, int> SkillPurchaseLevelChanged;

    public int SkillPurchasePrice => _currentSkillPurchasePrice;

    CharacterModel _character;
    int _laneIndex;
    int _totalSkillCount;
    int _currentSkillPurchasePrice;

    int _rockSKillLevel;
    int _fireSkillLevel;
    int _waterSkillLevel;

    int _skillPurchaseLevel;

    Dictionary<SkillBase, RuntimeSkillData> _skillInfos = new();

    public bool CanAddNewSkill => _totalSkillCount < _maxSkillCount;

    public void InitializeOnStart(int laneIndex, CharacterModel character)
    {
      _laneIndex = laneIndex;
      _character = character;
      _totalSkillCount = 0;

      // 현재 스킬 구매 가격을 초기 가격으로 설정
      _currentSkillPurchasePrice = _initialSkillPurchasePrice;

      // 스킬 구매 레벨을 1으로 설정
      _skillPurchaseLevel = 1;

      // 속성별 스킬 레벨을 모두 1로 초기화
      _rockSKillLevel = _waterSkillLevel = _fireSkillLevel = 1;

      foreach (SkillBase skill in _skillTable.AllSkills)
      {
        _skillInfos.Add(skill, new RuntimeSkillData());
      }

      // 각종 초기화 완료 이벤트 발생
      SkillInfoInitialized?.Invoke(_skillInfos);
      SkillPurchasePriceInitialized?.Invoke(_currentSkillPurchasePrice);

      TotalSkillCountInitialized?.Invoke(_totalSkillCount, _maxSkillCount);

      SkillAttributeLevelInitialized?.Invoke(SkillAttribute.Rock, _rockSKillLevel);
      SkillAttributeLevelInitialized?.Invoke(SkillAttribute.Fire, _fireSkillLevel);
      SkillAttributeLevelInitialized?.Invoke(SkillAttribute.Water, _waterSkillLevel);

      SkillPurchaseLevelInitialized?.Invoke(_skillPurchaseLevel);

      Debug.Log("Initialization Called");
    }

    public int GetSkillCount(SkillBase skill) 
    {
      return _skillInfos[skill].Count;
    }

    public int GetSkillAttributeLevel(SkillAttribute attribute)
    {
      return attribute switch 
      {
        SkillAttribute.Rock => _rockSKillLevel,
        SkillAttribute.Fire => _fireSkillLevel,
        SkillAttribute.Water => _waterSkillLevel,
        _ => 0
      };
    }

    public int GetSkillPurchaseLevel()
    {
      return _skillPurchaseLevel;
    }

    public void SetSkillAttributeLevel(SkillAttribute attribute, int newLevel)
    {
      switch (attribute)
      {
        case SkillAttribute.Rock:
        {
          int prev = _rockSKillLevel;
          _rockSKillLevel = newLevel;
          SkillAttributeLevelChanged?.Invoke(attribute, prev, _rockSKillLevel);
          break;
        }
        case SkillAttribute.Fire:
        {
          int prev = _fireSkillLevel;
          _fireSkillLevel = newLevel;
          SkillAttributeLevelChanged?.Invoke(attribute, prev, _fireSkillLevel);
          break;
        }
        case SkillAttribute.Water:
        {
          int prev = _waterSkillLevel;
          _waterSkillLevel = newLevel;
          SkillAttributeLevelChanged?.Invoke(attribute, prev, _waterSkillLevel);
          break;
        }
      }
    }

    public void SetSkillPurchaseLevel(int newLevel)
    {
      int prevLevel = _skillPurchaseLevel;
      _skillPurchaseLevel = newLevel;

      SkillPurchaseLevelChanged?.Invoke(prevLevel, newLevel);
    }

    /// <summary>
    /// 지정된 skill의 쿨다운 동안 data를 관리한다.
    /// </summary>
    IEnumerator CooldownRoutine(SkillBase skill, RuntimeSkillData data)
    {
      data.IsCooldown = true;

      float elapsedTime = 0.0f;

      while (elapsedTime <= skill.Cooldown)
      {
        elapsedTime += Time.deltaTime;
        float ratio = Mathf.Clamp01(elapsedTime / skill.Cooldown);

        SkillCooldownRatioChanged?.Invoke(skill, ratio);

        yield return null;
      }

      // 쿨다운이 끝났으면 null로 만들어 준다.
      data.CooldownRoutine = null;
      data.IsCooldown = false;
    }

    public void AddSkill(SkillBase skill, int count)
    {
      TotalSkillCountChanged?.Invoke(_totalSkillCount += count, _maxSkillCount);

      // 실제로 개수가 변경되는 부분
      _skillInfos[skill].Count += count;

      // 변화 결과 스킬 개수가 0이 되었고,
      // 이전에 진행 중이던 쿨다운이 있었으면 중지 시킨다
      if (_skillInfos[skill].Count == 0)
      {
        if (_skillInfos[skill].CooldownRoutine != null)
        {
          _skillInfos[skill].IsCooldown = false;

          SkillCooldownRatioChanged?.Invoke(skill, 0f);

          StopCoroutine(_skillInfos[skill].CooldownRoutine);
          _skillInfos[skill].CooldownRoutine = null;
        }
      }
    }

    public void IncreaseSkillPurchasePrice()
    {
      int prev = _currentSkillPurchasePrice;

      SkillPurchasePriceChanged?.Invoke(prev, _currentSkillPurchasePrice += 1);
    }

    public SkillBase TryPickSkill()
    {
      if (_totalSkillCount >= _maxSkillCount)
      {
        return null;
      }

      // 현재 픽 레벨에 맞는 등급 뽑기
      SkillRarity rarity = _skillRarityProbability.PickWithLevel(_skillPurchaseLevel);
      // Rock, Water, Fire 중 하나를 랜덤으로 뽑기
      SkillAttribute attribute = (SkillAttribute)Random.Range(0, 3);
      // 최종적으로 뽑은 스킬
      SkillBase pickedSkill = _skillTable.GetSkill(attribute, rarity);

      return pickedSkill;
    }

    public SkillBase TryUpgradeSkill(SkillBase skill)
    {

      // 3개가 안 되면, 잘못된 요청임
      if (_skillInfos[skill].Count < 3)
      {
        return null;
      }

      // 다음 등급을 계산
      SkillRarity nextRarity = (SkillRarity)((int)skill.Rarity + 1);
      // Rock, Water, Fire 중 하나를 랜덤으로 뽑기
      SkillAttribute attribute = (SkillAttribute)Random.Range(0, 3);
      // 최종적으로 뽑은 스킬
      SkillBase pickedSkill = _skillTable.GetSkill(attribute, nextRarity);

      return pickedSkill;
    }

    public SkillBase TryMineSkill(in SkillRarity rarity)
    {
      if (_totalSkillCount >= _maxSkillCount)
      {
        return null;
      }

      // Rock, Water, Fire 중 하나를 랜덤으로 뽑기
      SkillAttribute attribute = (SkillAttribute)Random.Range(0, 3);
      // 최종적으로 뽑은 스킬
      SkillBase pickedSkill = _skillTable.GetSkill(attribute, rarity);

      return pickedSkill;
    }

    // AI 쪽에서 호출
    public bool TryPickSkillToUpgrade(out SkillBase skillToUpgrade)
    {
      skillToUpgrade = null;

      List<SkillBase> upgradable = new();

      foreach (var (skill, data) in _skillInfos)
      {
        if (data.Count < 3)
        {
          continue;
        }
        upgradable.Add(skill);
      }

      if (upgradable.Count == 0)
      {
        return false;
      }

      int index = Random.Range(0, upgradable.Count);
      skillToUpgrade = upgradable[index];

      return false;
    }

    void Update()
    {
      foreach(var (skill, data) in _skillInfos)
      {
        // 갯수가 0개이거나 쿨다운 중이면 Pass
        if (data.Count == 0 || data.IsCooldown) 
        {
          continue;
        }

        // 강화 정보를 받아옴
        if (_skillEnhanceRateTable.TryGetCurrentAttributeEnhanceRate(GetSkillAttributeLevel(skill.Attribute), skill.Attribute, out float enhanceRate))
        {
          // 인덱스와 캐릭터, 강화 정보로 데미지 계산 후 스킬 발사
          skill.Fire(_character, _laneIndex, (int)(enhanceRate * skill.AttackPower));
          _character.PlayFiringAnimation();
        }

        data.CooldownRoutine = StartCoroutine(CooldownRoutine(skill, data));
      }
    }
  }
}