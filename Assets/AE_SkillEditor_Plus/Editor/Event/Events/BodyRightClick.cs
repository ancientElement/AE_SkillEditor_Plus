namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class BodyRightClick : BaseEvent
    {
        public int TrackIndex;
        public int MouseFrameID;
        public BodyRightClick()
        {
            this.EventType = AEUIEventType.BodyRightClick;
        }
    }
}