using System;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable]
    //TODO:修改为ScriptableObject嵌套
    public class StandardClip : ScriptableObject
    {
        public string Name;
        public int StartID;
        public int Duration;
    }
}