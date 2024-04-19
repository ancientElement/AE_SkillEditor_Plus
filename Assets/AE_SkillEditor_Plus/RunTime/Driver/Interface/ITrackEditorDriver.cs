using System;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public interface ITrackEditorDriver
    {
        public Action<int, StandardTrack> Tick { get; }
    }
}