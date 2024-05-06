using System;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;

namespace AE_SkillEditor_Plus.Example.BuiltTracks.Audio
{
    [AETrackName(Name = "音频")]
    [AETrackColor(253f / 255, 194f / 255, 4f / 255)]
    [AEBindClip(ClipType = typeof(AEAudioClip))]
    [Serializable]
    public class AEAudioTrack : StandardTrack, IEditorBehaviour, IRuntimeBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip) => new AEAudioEditorBehaviour(clip);
        public AEPlayableBehaviour CreateRuntimeBehaviour(StandardClip clip) => new AEAudioRuntimeBehaviour(clip);
    }
}