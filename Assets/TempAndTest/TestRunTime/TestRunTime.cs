using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace TempAndTest.TestRunTime
{
    public class TestRunTime : MonoBehaviour
    {
        private AETimelineTick TimelineTick;

        public List<AETimelineAsset> Assets;
        public int FPS;

        private void Start()
        {
            TimelineTick = new AETimelineTick();
        }

        private void Update()
        {
            TimelineTick.Tick(Time.deltaTime, FPS, gameObject);
        }

        [ContextMenu("测试运行时0")]
        private void Test0Fun()
        {
            TimelineTick.PlayAsset(Assets[0]);
        }

        [ContextMenu("测试运行时1")]
        private void Test1Fun()
        {
            TimelineTick.PlayAsset(Assets[1]);
        }
    }
}