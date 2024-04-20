namespace AE_SkillEditor_Plus.AEUIEvent
{
    public class ControllerEvent : BaseEvent
    {
        public ControllerEvent()
        {
            this.AeuiEventType = AEUIEventType.Controller;
        }

        public ControllerType ControllerType;
    }
}