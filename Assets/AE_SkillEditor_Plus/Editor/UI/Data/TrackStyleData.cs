using System.Collections.Generic;
using System.Security.Policy;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI.Data
{
    public class TrackStyleData
    {
        public string Name;
        public Color Color;
        public List<ClipStyleData> Clips;
        public ClipUIAction UpdateUI;
    }
}