using System;
using AE_SkillEditor_Plus.Editor;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    [AETrackName(Name = "动画")]
    [AETrackColor(127f / 255, 252f / 255, 228f / 255)]
    [AEBindClip(ClipType = typeof(AEAnimationClip))]
    [ClipStyle(ClassName = "AE_SkillEditor_Plus.Editor.BuiltTracks.CustomAnimationClip")]
    [Serializable]
    public class AEAnimationTrack : StandardTrack, IEditorBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip) => new AEAnimationBehaviour(clip);
        //
        // public ClipUIAction UpdateUI => (AETimelineAsset asset, Rect rect, int[] hightLight, Color color,
        //     string clipName, float widthPerFrame, int trackIndex, int clipIndex) =>
        // {
        //     // Debug.Log("额外的Clip样式");
        //     var clip = (asset.Tracks[trackIndex].Clips[clipIndex] as AEAnimationClip).AnimationClip;
        //     if (clip != null)
        //     {
        //         float animationDuration = clip.length * ;
        //         EditorGUI.DrawRect(new Rect(rect.x + animationLenght * 0.5f, rect.y, widthPerFrame, rect.height),
        //             Color.white);
        //     }
        // };
    }
}