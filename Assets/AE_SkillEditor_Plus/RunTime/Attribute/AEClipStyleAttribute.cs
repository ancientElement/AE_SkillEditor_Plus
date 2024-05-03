namespace AE_SkillEditor_Plus.RunTime.Attribute
{
    /// <summary>
    /// 描述自定义Clip样式
    /// </summary>
    public class AEClipStyleAttribute : System.Attribute
    {
        public string ClassName; //定义自定义样式的类名
        public bool Override; //覆盖还是新增
    }
}