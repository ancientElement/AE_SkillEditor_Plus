namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class ControllerEvent : BaseEvent
    {
        public ControllerEvent()
        {
            this.EventType = AEUIEventType.Controller;
        }

        public ControllerType ControllerType;
    }
}