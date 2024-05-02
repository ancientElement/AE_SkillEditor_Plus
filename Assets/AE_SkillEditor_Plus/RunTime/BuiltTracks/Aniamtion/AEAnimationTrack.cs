using System;
using AE_SkillEditor_Plus.Editor;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    [AETrackName(Name = "动画")]
    [AETrackColor(127f / 255, 252f / 255, 228f / 255)]
    [AEBindClip(ClipType = typeof(AEAnimationClip))]
    [ClipStyle(ClassName = "CustomAnimationClip")]
    [Serializable]
    public class AEAnimationTrack : StandardTrack, IEditorBehaviour, IClipStyle
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip) => new AEAnimationBehaviour(clip);

        public ClipUIAction UpdateUI => (Rect rect, int[] hightLight, Color color,
            string clipName, int trackIndex, int clipIndex) =>
        {
            // Debug.Log("额外的Clip样式");
        };
    }
}