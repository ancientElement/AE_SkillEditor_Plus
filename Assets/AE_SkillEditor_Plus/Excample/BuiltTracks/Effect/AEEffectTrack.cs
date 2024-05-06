using System;
using System.ComponentModel;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;

namespace AE_SkillEditor_Plus.Example.BuiltTracks.Effect
{
    [AETrackName(Name = "特效")]
    [AETrackColor(127f / 255, 214f / 255, 252f / 255)]
    [AEBindClip(ClipType = typeof(AEEffectClip))]
    [Serializable]
    public class AEEffectTrack : StandardTrack, IEditorBehaviour, IRuntimeBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip) => new AEEffectBehaviour(clip);
        public AEPlayableBehaviour CreateRuntimeBehaviour(StandardClip clip) => new AEEffectBehaviour(clip);
    }
}