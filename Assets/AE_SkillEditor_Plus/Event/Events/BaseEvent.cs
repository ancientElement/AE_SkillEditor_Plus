namespace AE_SkillEditor_Plus.Event
{
    public class BaseEvent
    {
        public EventType EventType;
        public int TrackIndex;
        public int ClipIndex;
    }

    public class MoveEvent : BaseEvent
    {
        public float MouseDeltaX;
    }

    public class ResizeEvent : BaseEvent
    {
        public float MouseStart;
        public float MouseCurrent;
    }

    public class RightClickEvent : BaseEvent
    {
        public float MouseCurrent;
    }

    public class CtrlCEvent : BaseEvent
    {
        public float MouseCurrent;
    }
    
    public class CtrlVEvent : BaseEvent
    {
        public float MouseCurrent;
    }
}