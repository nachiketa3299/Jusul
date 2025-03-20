using System.Collections.Generic;
using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "WaveInfoTable", menuName = "Jusul/Wave/WaveInfoTable")]
  public class WaveInfoTable : ScriptableObject
  {
    public List<WaveInfo> Waves;
  }
}