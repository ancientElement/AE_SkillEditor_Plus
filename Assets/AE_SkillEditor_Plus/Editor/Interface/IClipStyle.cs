using System;
using AE_SkillEditor_Plus.Editor.Window;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public interface IClipStyle
    {
        public Action<AETimelineEditorWindow, Rect, Color, string, int, int> UpdateUI { get; }
    }
}