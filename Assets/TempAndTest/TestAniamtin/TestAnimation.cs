using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestAnimation : MonoBehaviour
{
    public AnimationClip Clip;
    public float Timer;

    public void Update()
    {
        if (Clip != null)
            Clip.SampleAnimation(gameObject, Timer);
    }
}