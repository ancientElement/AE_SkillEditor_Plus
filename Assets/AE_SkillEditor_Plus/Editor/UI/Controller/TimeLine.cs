using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.UI.Controller
{
    public static class TimeLine
    {
        public static void UpdateGUI(ClipEditorWindow window, float widthPreFrame, Rect rect)
        {
            GUI.backgroundColor = new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f);
            GUI.Box(rect, "", "AC BoldHeader");

            Handles.color = Color.white; // 设置线条颜色
            float x = rect.x;
            float maxX = rect.xMax;
            int i = 0;
            while (x < maxX)
            {
                GUI.Label(new Rect(x, rect.y, 140, 20), i.ToString()); // 绘制数字
                Handles.DrawLine(new Vector2(x, rect.y + rect.height * 0.2f), new Vector2(x, rect.yMax)); // 绘制线条
                i += 50;
                x += widthPreFrame * 50; // 根据每帧间隔移动位置
            }
        }
    }
}