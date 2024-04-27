using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    public class AEAnimationBehaviour : AEPlayableBehaviour
    {
        private AEAnimationClip Clip;

        public AEAnimationBehaviour(StandardClip clip) : base(clip)
        {
            Clip = (clip as AEAnimationClip);
        }

        public override void Tick(int currentFrameID, int fps, GameObject context)
        {
            base.Tick(currentFrameID, fps, context);
            Clip.AnimationClip.SampleAnimation(context, (float)currentFrameID / fps);
            // Debug.Log((float)currentFrameID / fps);
        }
    }
}