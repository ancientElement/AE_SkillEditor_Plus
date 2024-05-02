﻿using System;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks
{
    [Serializable]
    public class AEAnimationClip : StandardClip
    {
        public AnimationClip AnimationClip;
#if UNITY_EDITOR
        public Vector3 StartPosition;
#endif
    }
}