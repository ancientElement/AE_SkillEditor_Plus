namespace AE_SkillEditor_Plus.Event
{
    //事件类型
    public enum EventType
    {
        ClipMove,
        ClipResize,
        ClipRightClick,
        ClipKeyborad,
        Controller,
        TimelineScale,
        TimelineDrag
    }
    
    //快捷键
    public enum Shortcut 
    {
        CtrlC,
        CtrlV,
        CtrlX
    }
    
    //控件
    public enum ControllerType 
    {
        Play,
        ToPre,
        ToNext,
        ToMostBegin,
        ToMostEnd,
    }
}