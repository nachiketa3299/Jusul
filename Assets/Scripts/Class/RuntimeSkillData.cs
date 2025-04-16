using UnityEngine;

namespace Jusul
{
  /// <summary>
  /// 게임 플레이 중 스킬의 개수와 쿨다운 현황을 기록한다.
  /// </summary>
  public class RuntimeSkillData 
  {
    public int Count = 0;
    public bool IsCooldown = false;
    public Coroutine CooldownRoutine = null;
  }
}