using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks.Audio
{
    public class AEAudioBehaviour : AEPlayableBehaviour
    {
        private AEAudioClip Clip;
        private GameObject temp;

        public AEAudioBehaviour(StandardClip clip) : base(clip)
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
                temp = new GameObject("One shot audio");
                temp.transform.position = position;
                AudioSource audioSource = (AudioSource)temp.AddComponent(typeof(AudioSource));
                audioSource.clip = Clip.AudioClip;
                audioSource.spatialBlend = 1f;
                audioSource.volume = Clip.Volume;
                audioSource.Play();
            }
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
            if (temp != null) GameObject.DestroyImmediate(temp);
        }
    }
}