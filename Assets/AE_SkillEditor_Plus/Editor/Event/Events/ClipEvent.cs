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
            this.AeuiEventType = AEUIEventType.ClipMove;
        }

        public float OffsetMouseX; //鼠标相对Start的偏移 单位是Rect的x
    }
    
    public class ClipMoveEndEvent : ClipEvent
    {
        public ClipMoveEndEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipMoveEnd;
        }
    }

    public class ClipClickEvent : ClipEvent
    {
        public ClipClickEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipClick;
        }

        public float OffsetMouseX; //鼠标相对Start的偏移 单位是Rect的x
    }

    public class ClipResizeEvent : ClipEvent
    {
        public ClipResizeEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipResize;
        }

        public float OffsetMouseX; //鼠标相对End的偏移 单位是Rect的x
    }

    public class ClipResizeEndEvent : ClipEvent
    {
        public ClipResizeEndEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipResizeEnd;
        }
    }
    
    public class ClipRightClickEvent : ClipEvent
    {
        public ClipRightClickEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipRightClick;
        }
    }

    public class KeyboradEvent : ClipEvent
    {
        public KeyboradEvent()
        {
            this.AeuiEventType = AEUIEventType.ClipKeyborad;
        }

        public Shortcut Shortcut;
    }
}