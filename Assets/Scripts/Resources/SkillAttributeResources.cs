using System;
using System.Collections.Generic;

using UnityEngine;

namespace Jusul
{
  [CreateAssetMenu(fileName = "SkillAttributeResrouces", menuName = "Jusul/Resources/SkillAttributeResources")]
  public class SkillAttributeResources : ScriptableObject
  {
    [Serializable]
    public class SkillAttributeResourceEntry
    {
      public Color Color;
      public Sprite Sprite;
      public string DisplayName;
    }

    public List<SkillAttributeResourceEntry> Resources = new();
  }
}