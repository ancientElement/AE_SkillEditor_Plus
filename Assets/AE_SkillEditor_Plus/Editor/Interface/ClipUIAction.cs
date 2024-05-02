using AE_SkillEditor_Plus.Editor.Window;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public delegate void ClipUIAction(AETimelineEditorWindow window,Rect rect, int[] hightLight, Color color,
        string clipName, float widthPerFrame,int trackIndex, int clipIndex);
}