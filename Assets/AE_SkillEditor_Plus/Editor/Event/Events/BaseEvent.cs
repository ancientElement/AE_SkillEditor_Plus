namespace AE_SkillEditor_Plus.Event
{
    public class BaseEvent
    {
        public EventType EventType { get; set; }
        public int TrackIndex;
        public int ClipIndex;
    }

    public class MoveEvent : BaseEvent
    {
        public MoveEvent()
        {
            this.EventType = EventType.Move;
        }

        public float OffsetMouseX; //鼠标相对Start的偏移 单位是Rect的x
    }

    public class ResizeEvent : BaseEvent
    {
        public ResizeEvent()
        {
            this.EventType = EventType.Resize;
        }

        public float OffsetMouseX; //鼠标相对End的偏移 单位是Rect的x
    }

    public class RightClickEvent : BaseEvent
    {
        public RightClickEvent()
        {
            this.EventType = EventType.RightClick;
        }
    }

    public class KeyboradEvent : BaseEvent
    {
        public KeyboradEvent()
        {
            this.EventType = EventType.Keyborad;
        }

        public Shortcut Shortcut;
    }
}