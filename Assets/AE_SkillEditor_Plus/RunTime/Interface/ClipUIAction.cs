using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public delegate void ClipUIAction(Rect rect, int[] hightLight, Color color,
        string clipName, int trackIndex, int clipIndex);
}