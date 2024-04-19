using System;
using AE_SkillEditor_Plus.Editor.Window;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public interface IClipEvent
    {
        public Action<AETimelineEditorWindow, Rect, Color, string, int, int> ProcessEvent { get; }
    }
}