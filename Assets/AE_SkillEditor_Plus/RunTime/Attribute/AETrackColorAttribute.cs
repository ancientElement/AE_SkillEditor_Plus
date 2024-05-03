using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Attribute
{
    /// <summary>
    /// 描述轨道颜色
    /// </summary>
    public class AETrackColorAttribute : System.Attribute
    {
        public Color Color;

        public AETrackColorAttribute(float r, float g, float b)
        {
            Color = new Color(r, g, b);
        }
    }
}