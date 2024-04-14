namespace AE_SkillEditor_Plus.Event
{
    public class TimelineScaleEvent : BaseEvent
    {
        public TimelineScaleEvent()
        {
            this.EventType = EventType.TimelineScale;
        }
    }

    public class TimelineDragEvent : BaseEvent
    {
        public TimelineDragEvent()
        {
            this.EventType = EventType.TimelineDrag;
        }
    }
}