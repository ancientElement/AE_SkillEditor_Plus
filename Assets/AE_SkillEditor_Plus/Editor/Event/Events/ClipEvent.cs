namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class ClipEvent : BaseEvent
    {
        public int TrackIndex;
        public int ClipIndex;
    }

    public class ClipMoveEvent : ClipEvent
    {
        public ClipMoveEvent()
        {
            this.EventType = AEUIEventType.ClipMove;
        }

        public float OffsetMouseX; //鼠标相对Start的偏移 单位是Rect的x
    }
    
    public class ClipMoveEndEvent : ClipEvent
    {
        public ClipMoveEndEvent()
        {
            this.EventType = AEUIEventType.ClipMoveEnd;
        }
    }

    public class ClipClickEvent : ClipEvent
    {
        public ClipClickEvent()
        {
            this.EventType = AEUIEventType.ClipClick;
        }

        public float OffsetMouseX; //鼠标相对Start的偏移 单位是Rect的x
    }

    public class ClipResizeEvent : ClipEvent
    {
        public ClipResizeEvent()
        {
            this.EventType = AEUIEventType.ClipResize;
        }

        public float OffsetMouseX; //鼠标相对End的偏移 单位是Rect的x
    }

    public class ClipResizeEndEvent : ClipEvent
    {
        public ClipResizeEndEvent()
        {
            this.EventType = AEUIEventType.ClipResizeEnd;
        }
    }
    
    public class ClipRightClickEvent : ClipEvent
    {
        public ClipRightClickEvent()
        {
            this.EventType = AEUIEventType.ClipRightClick;
        }
    }

    public class KeyboradEvent : ClipEvent
    {
        public KeyboradEvent()
        {
            this.EventType = AEUIEventType.ClipKeyborad;
        }

        public Shortcut Shortcut;
    }
}