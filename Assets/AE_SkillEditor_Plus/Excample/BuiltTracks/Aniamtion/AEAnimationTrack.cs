using System;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example.BuiltTracks
{
    [AETrackName(Name = "动画")]
    [AETrackColor(127f / 255, 252f / 255, 228f / 255)]
    [AEBindClip(ClipType = typeof(AEAnimationClip))]
    [AEClipStyle(ClassName = "AE_SkillEditor_Plus.Excample.BuiltTracks.CustomAnimationClip")]
    [Serializable]
    public class AEAnimationTrack : StandardTrack, IEditorBehaviour, IRuntimeBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip) => new AEAnimationEditorBehaviour(clip);
        public AEPlayableBehaviour CreateRuntimeBehaviour(StandardClip clip) => new AEAnimationRuntimeBehaviour(clip);
    }
}