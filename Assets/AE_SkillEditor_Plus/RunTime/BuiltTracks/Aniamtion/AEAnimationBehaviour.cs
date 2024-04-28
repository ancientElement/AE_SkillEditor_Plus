using System;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    public class AEAnimationBehaviour : AEPlayableBehaviour
    {
        private AEAnimationClip Clip;

        // private Vector3 lastPosition;

        // private float deltaPosition;
        // private static Vector3 prePosition;

        public AEAnimationBehaviour(StandardClip clip) : base(clip)
        {
            Clip = (clip as AEAnimationClip);
        }

        public override void Tick(int currentFrameID, int fps, GameObject context)
        {
            if (context == null) return;
            if (Clip.AnimationClip == null) return;
            base.Tick(currentFrameID, fps, context);
            Clip.AnimationClip.SampleAnimation(context, (float)currentFrameID / fps);
            // Vector3 deltaPosition = context.transform.position - lastPosition;
            // if (Mathf.Rad2Deg * Vector3.Dot(deltaPosition, context.transform.forward) > 160)
            //     context.transform.position += deltaPosition + prePosition;
            // else
            // context.transform.position += Clip.StartPosition;
            // lastPosition = context.transform.position;.
            // context.transform.position += lastPosition.magnitude * context.transform.forward;
            // Debug.Log();
            // Debug.Log(lastPosition);
            // Debug.Log((float)currentFrameID / fps);
            // Debug.Log(context.transform.position);
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
            // prePosition = context.transform.position;
        }
    }
}