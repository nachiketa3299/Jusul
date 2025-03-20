using UnityEngine;

namespace Jusul.Utility
{

  public static class Timer
  {
    public static void CalculateRemainigTime(float remainingTime, out int minutes, out int seconds)
    {
      remainingTime = Mathf.Max(remainingTime, 0);
      minutes = Mathf.FloorToInt(remainingTime / 60);
      seconds = Mathf.FloorToInt(remainingTime % 60);
    }

    public static void CalculateRemainigTime(float remainingTime, out int seconds)
    {
      remainingTime = Mathf.Max(remainingTime, 0);
      seconds = Mathf.FloorToInt(remainingTime);
    }
  }
}