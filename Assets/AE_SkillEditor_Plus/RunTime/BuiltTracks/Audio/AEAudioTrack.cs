using System;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks.Audio
{
    [AETrackName(Name = "音频")]
    [AETrackColor(253f/255, 194f/255, 4f/255)]
    [AEBindClip(ClipType = typeof(AEAudioClip))]
    [Serializable]
    public class AEAudioTrack : StandardTrack,IEditorBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip)
        {
            return new AEAudioBehaviour(clip);
        }
    }
}