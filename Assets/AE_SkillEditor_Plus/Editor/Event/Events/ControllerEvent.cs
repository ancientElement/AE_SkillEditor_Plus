namespace AE_SkillEditor_Plus.Event
{
    public class ControllerEvent : BaseEvent
    {
        public ControllerEvent()
        {
            this.EventType = EventType.Controller;
        }

        public ControllerType ControllerType;
    }
}