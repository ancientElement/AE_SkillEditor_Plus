using System;

namespace AE_SkillEditor_Plus.RunTime.Attribute
{
    /// <summary>
    /// 描述轨道绑定的Clip类型
    /// </summary>
    public class AEBindClipAttribute : System.Attribute
    {
        public Type ClipType;
    }
}