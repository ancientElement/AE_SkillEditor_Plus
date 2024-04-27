namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class TimelineScaleEvent : BaseEvent
    {
        public TimelineScaleEvent()
        {
            this.EventType = AEUIEventType.TimelineScale;
        }
    }
    
    public class TimelineDragEndEvent : BaseEvent
    {
        public TimelineDragEndEvent()
        {
            this.EventType = AEUIEventType.TimelineDragEnd;
        }
    }

    public class TimelineDragEvent : BaseEvent
    {
        public TimelineDragEvent()
        {
            this.EventType = AEUIEventType.TimelineDrag;
        }
    }
}