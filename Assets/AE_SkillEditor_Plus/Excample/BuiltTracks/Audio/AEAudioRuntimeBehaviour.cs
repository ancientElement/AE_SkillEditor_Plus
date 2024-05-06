using AE_SkillEditor_Plus.RunTime;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example.BuiltTracks.Audio
{
    public class AEAudioRuntimeBehaviour : AEAudioEditorBehaviour
    {
        private AEAudioClip Clip;

        public AEAudioRuntimeBehaviour(StandardClip clip) : base(clip)
        {
            Clip = clip as AEAudioClip;
        }

        public override void OnEnter(GameObject context, int currentFrameID)
        {
            base.OnEnter(context, currentFrameID);
            if (context == null) return;
            // Debug.Log("VAR");
            if (currentFrameID == 0 && Clip.AudioClip != null)
            {
                var position = context.transform.TransformPoint(Clip.Position);
                AudioSource.PlayClipAtPoint(Clip.AudioClip, position);
            }
        }
    }
}