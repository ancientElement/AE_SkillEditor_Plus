using System;
using System.Collections.Generic;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable]
    public class StandardTrack
    {
        public List<StandardClip> Clips;

        public StandardTrack()
        {
            Clips = new List<StandardClip>();
        }
    }
}