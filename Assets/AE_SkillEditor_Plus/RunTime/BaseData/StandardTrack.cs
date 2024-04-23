using System;
using System.Collections.Generic;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable]
    public class StandardTrack
    {
        [SerializeReference] 
        public List<StandardClip> Clips;

        public StandardTrack()
        {
            Clips = new List<StandardClip>();
        }
    }
}