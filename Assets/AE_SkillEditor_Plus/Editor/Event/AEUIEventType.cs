namespace AE_SkillEditor_Plus.AEUIEvent
{
    //事件类型
    public enum AEUIEventType
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
        BodyRightClick,
        TimelineDragEnd,
        LeftMouseUp
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