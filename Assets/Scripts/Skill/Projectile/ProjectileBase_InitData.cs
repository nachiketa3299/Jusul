namespace Jusul
{
  public class ProjectileBase_InitData
  {
    public int LaneIndex { get; private set; }
    public SkillBase SkillBase { get; private set; }
    public int FinalDamage { get; private set; }

    public ProjectileBase_InitData(int laneIndex, SkillBase skillBase, int finalDamage)
    {
      LaneIndex = laneIndex;
      SkillBase = skillBase;
      FinalDamage = finalDamage;
    }
  }
}