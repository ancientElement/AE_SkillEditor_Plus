using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace AE_SkillEditor_Plus.Editor
{
    [TrackColor(0, 0, 1)]
    public class TestTrackData
    {
        public string Name;
        public List<TestClipData> Clips;
    }

    public class TestClipData
    {
        public string Name;
        public int StartID;
        public int Duration;
    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class TrackColorAttribute : Attribute
    {
        public Color Color;
        public TrackColorAttribute(float r, float g, float b)
        {
            Color = new Color(r, g, b);
        }
    }
}