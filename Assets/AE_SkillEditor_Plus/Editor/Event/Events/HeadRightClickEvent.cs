namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class HeadRightClickEvent : BaseEvent
    {
        public int TrackIndex;
        
        public HeadRightClickEvent()
        {
            EventType = AEUIEventType.HeadRightClick;
        }
    }
}