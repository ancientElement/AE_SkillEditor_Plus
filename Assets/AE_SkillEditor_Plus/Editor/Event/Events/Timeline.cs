namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class TimelineScaleEvent : BaseEvent
    {
        public TimelineScaleEvent()
        {
            this.AeuiEventType = AEUIEventType.TimelineScale;
        }
    }
    
    public class TimelineDragEndEvent : BaseEvent
    {
        public TimelineDragEndEvent()
        {
            this.AeuiEventType = AEUIEventType.TimelineDragEnd;
        }
    }

    public class TimelineDragEvent : BaseEvent
    {
        public TimelineDragEvent()
        {
            this.AeuiEventType = AEUIEventType.TimelineDrag;
        }
    }
}