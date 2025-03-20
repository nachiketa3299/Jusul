using System;
using UnityEngine;

namespace Jusul
{
  public enum SkillRarity
  {
    Normal = 0, 
    Rare, 
    Hero, 
    Legend, 
    Acient, 
    Chunbul
  }

  public enum SkillAttribute
  {
    Rock=0, 
    Fire,
    Water
  }

  public enum SkillAttackType
  {
    Near=0,
    Mid,
    Far
  }

  public abstract class SkillBase : ScriptableObject
  {
    public Sprite Icon;
    public string Name;
    public string Description;
    public float Cooldown;
    public int AttackPower;
    public SkillAttackType AttackType;
    public SkillRarity Rarity;
    public SkillAttribute Attribute;

    public ProjectileBase ProjectilePrefab;

    public virtual void Fire(Character caster, int laneIndex) { }
  }
}