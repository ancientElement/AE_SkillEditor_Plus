using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable]
    //TODO:修改为ScriptableObject嵌套
    public class StandardTrack //: ScriptableObject
    {
        [SerializeReference] public List<StandardClip> Clips;

        public StandardTrack()
        {
            Clips = new List<StandardClip>();
        }
    }
}