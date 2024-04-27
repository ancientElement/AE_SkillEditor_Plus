using System;
using AE_SkillEditor_Plus.Editor;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    [AETrackName(Name = "动画")]
    [AETrackColor(127f/255, 252f/255, 228f/255)]
    [AEBindClip(ClipType = typeof(AEAnimationClip))]
    [Serializable]
    public class AEAnimationTrack : StandardTrack, IEditorBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip)
        {
            return new AEAnimationBehaviour(clip);
        }
    }
}