using System;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public interface ITrackRuntimeDriver
    {
        public Action<int, StandardTrack> Tick { get; }
    }
}