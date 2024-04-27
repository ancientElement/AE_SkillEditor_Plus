using System;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEngine;
using UnityEngine.Timeline;

namespace AE_SkillEditor_Plus.TempAndTest.TestData
{
    // [AETrackName(Name = "测试轨道1")]
    // [AEBindClip(ClipType = typeof(TestClipData))]
    // [AETrackColor(1, 0, 0)]
    // [Serializable] //测试轨道
    // public class TestTrackData : StandardTrack, IEditorBehaviour, IRuntimeBehaviour
    // {
    //     public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip)
    //         => new TestBehaviour(clip);
    //
    //     public AEPlayableBehaviour CreateRuntimeBehaviour(StandardClip clip)
    //         => new TestBehaviour(clip);
    // }
    //
    // [Serializable]
    // public class TestClipData : StandardClip //测试Clip
    // {
    //     public string Str;
    //     public string aniationName;
    //     public AnimationClip aniationClip;
    // }
    //
    // public class TestBehaviour : AEPlayableBehaviour //测试Behaviour
    // {
    //     public TestBehaviour(StandardClip clip) : base(clip)
    //     {
    //     }
    //
    //     public override void OnEnter(GameObject context) //进入
    //     {
    //         base.OnEnter(context);
    //         Debug.LogWarning("OnEnter");
    //     }
    //
    //     public override void Tick(int currentFrameID, int fps,GameObject context) //Running
    //     {
    //         base.Tick(currentFrameID, fps,context);
    //         Debug.Log("OnUpdate  " + currentFrameID);
    //     }
    //
    //     public override void OnExit(GameObject context) //退出
    //     {
    //         base.OnExit(context);
    //         Debug.LogWarning("OnExit");
    //     }
    // }
}