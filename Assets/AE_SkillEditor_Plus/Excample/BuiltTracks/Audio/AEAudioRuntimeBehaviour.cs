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

        public override void OnEnter(GameObject context,int fps, int currentFrameID)
        {
            base.OnEnter(context,fps, currentFrameID);
            if (context == null) return;
            // Debug.Log("VAR");
            if (Clip.AudioClip != null)
            {
                var position = context.transform.TransformPoint(Clip.Position);
                AudioSource.PlayClipAtPoint(Clip.AudioClip, position);
            }
        }
    }
}