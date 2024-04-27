using System;
using System.Collections.Generic;
// using AE_SkillEditor_Plus.TempAndTest.TestData;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime
{
    [Serializable] //TODO: 序列化形式
    public class AETimelineAsset : ScriptableObject
    {
        // public string Name;
        [SerializeReference] public List<StandardTrack> Tracks;
        public int Duration;

        public AETimelineAsset()
        {
            Tracks = new List<StandardTrack>();
        }
    }
}