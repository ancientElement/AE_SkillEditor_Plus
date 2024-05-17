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

        public override void OnEnter(GameObject context,int fps, int currentFrameID)
        {
            base.OnEnter(context,fps, currentFrameID);
            // Debug.Log("Enter " + currentFrameID);
            Animator = context.GetComponent<Animator>();
            Animator.CrossFade(Clip.AnimationClip.name, Clip.FadeIn);
            interval = Time.time;
        }

        public override void OnExit(GameObject context,int fps, int currentFrameID)
        {
            base.OnExit(context,fps, currentFrameID);
            interval = Time.time - interval;
            Debug.Log(interval);
        }
    }
}