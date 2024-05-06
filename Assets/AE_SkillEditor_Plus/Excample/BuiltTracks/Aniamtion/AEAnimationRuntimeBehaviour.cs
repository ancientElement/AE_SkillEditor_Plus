using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example.BuiltTracks
{
    public class AEAnimationRuntimeBehaviour : AEPlayableBehaviour
    {
        private Animator Animator;
        private AEAnimationClip Clip;
        private float interval;

        public AEAnimationRuntimeBehaviour(StandardClip clip) : base(clip)
        {
            Clip = clip as AEAnimationClip;
        }

        public override void OnEnter(GameObject context, int currentFrameID)
        {
            base.OnEnter(context, currentFrameID);
            // Debug.Log("Enter " + currentFrameID);
            Animator = context.GetComponent<Animator>();
            Animator.CrossFade(Clip.AnimationClip.name, Clip.FadeIn);
            interval = Time.time;
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
            interval = Time.time - interval;
            Debug.Log(interval);
        }
    }
}