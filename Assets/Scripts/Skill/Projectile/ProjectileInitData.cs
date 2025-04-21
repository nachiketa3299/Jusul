namespace Jusul
{
  public class ProjectileInitData
  {
    public int LaneIndex { get; private set; }
    public SkillBase SkillBase { get; private set; }
    public int FinalDamage { get; private set; }

    public ProjectileInitData(int laneIndex, SkillBase skillBase, int finalDamage)
    {
      LaneIndex = laneIndex;
      SkillBase = skillBase;
      FinalDamage = finalDamage;
    }
  }
}