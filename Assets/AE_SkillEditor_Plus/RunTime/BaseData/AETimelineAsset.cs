using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.TempAndTest.TestData;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable]
    public class AETimelineAsset
    {
        public string Name;
        public List<StandardTrack> Tracks;

        public AETimelineAsset()
        {
            Tracks = new List<StandardTrack>();
        }
    }
}