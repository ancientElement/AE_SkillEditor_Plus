namespace AE_SkillEditor_Plus.Event
{
    //事件类型
    public enum EventType
    {
        ClipMove,
        ClipMoveEnd,
        ClipClick,
        ClipResize,
        ClipResizeEnd,
        ClipRightClick,
        ClipKeyborad,
        Controller,
        TimelineScale,
        TimelineDrag,
        HeadRightClick,
        BodyRightClick
    }
    
    //快捷键
    public enum Shortcut 
    {
        CtrlC,
        CtrlV,
        CtrlX,
        Delete
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