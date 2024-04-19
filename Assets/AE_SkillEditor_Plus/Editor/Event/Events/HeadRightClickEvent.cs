namespace AE_SkillEditor_Plus.Event
{
    public class HeadRightClickEvent : BaseEvent
    {
        public int TrackIndex;
        
        public HeadRightClickEvent()
        {
            EventType = EventType.HeadRightClick;
        }
    }
}