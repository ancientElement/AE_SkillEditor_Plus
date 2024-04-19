namespace AE_SkillEditor_Plus.Event
{
    public class BodyRightClick : BaseEvent
    {
        public int TrackIndex;
        public int MouseFrameID;
        public BodyRightClick()
        {
            this.EventType = EventType.BodyRightClick;
        }
    }
}