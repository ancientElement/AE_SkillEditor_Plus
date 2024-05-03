using System;
using AE_SkillEditor_Plus.RunTime;
using UnityEngine;

namespace AE_SkillEditor_Plus.Excample.BuiltTracks
{
    [Serializable]
    public class AEAnimationClip : StandardClip
    {
        public AnimationClip AnimationClip;
        public float FadeIn;
#if UNITY_EDITOR
        public Vector3 StartPosition;
#endif
    }
}