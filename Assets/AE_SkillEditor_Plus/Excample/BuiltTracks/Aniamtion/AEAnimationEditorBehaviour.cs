using System;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example.BuiltTracks
{
    public class AEAnimationEditorBehaviour : AEPlayableBehaviour
    {
        private AEAnimationClip Clip;
        
        public AEAnimationEditorBehaviour(StandardClip clip) : base(clip)
        {
            Clip = (clip as AEAnimationClip);
        }

        public override void Tick(int currentFrameID, int fps, GameObject context)
        {
            if (context == null) return;
            if (Clip.AnimationClip == null) return;
            base.Tick(currentFrameID, fps, context);
            Clip.AnimationClip.SampleAnimation(context, (float)currentFrameID / fps);
            context.transform.position += Clip.StartPosition;
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
        }
    }
}