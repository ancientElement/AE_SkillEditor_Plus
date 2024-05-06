using System;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example
{
    public class AETimelineDirector : MonoBehaviour
    {
        private AETimelineTick Tick;
        public AETimelineAsset StartAsset;

        private void Start()
        {
            Tick = new AETimelineTick();
            Tick.PlayAsset(StartAsset);
        }

        private void Update()
        {
            Tick.Tick(Time.deltaTime, gameObject);
        }
    }
}