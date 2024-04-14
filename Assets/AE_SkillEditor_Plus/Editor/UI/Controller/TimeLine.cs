using AE_SkillEditor_Plus.Event;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.UI.Controller
{
    public static class TimeLine
    {
        private static TimelineScaleEvent scaleEvent;
        private static TimelineDragEvent dragEvent;

        static TimeLine()
        {
            scaleEvent = new TimelineScaleEvent();
            dragEvent = new TimelineDragEvent();
        }

        public static void UpdateGUI(ClipEditorWindow window, int currentFrameID, float widthPreFrame, Rect rect)
        {
            GUI.backgroundColor = new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f);
            GUI.Box(rect, "", "AC BoldHeader");

            //绘制时间轴
            Handles.color = Color.white; // 设置线条颜色
            float x = rect.x;
            float maxX = rect.xMax;
            int lineInterval = (int)(100 / widthPreFrame);
            // int lineInterval = 50;
            lineInterval = Mathf.Max(1, lineInterval);
            int i = 0;
            while (x < maxX)
            {
                GUI.Label(new Rect(x, rect.y + rect.height * 0.3f, 140f, 20f), i.ToString()); // 绘制数字
                Handles.DrawLine(new Vector2(x, rect.y + rect.height * 0.4f), new Vector2(x, rect.yMax)); // 绘制线条
                i += lineInterval;
                x += widthPreFrame * lineInterval; // 根据每帧间隔移动位置
            }

            //绘制当前帧指向线条
            Handles.color = Color.white; // 设置线条颜色
            float currentFrameRectX = rect.x + currentFrameID * widthPreFrame;
            Handles.DrawLine(new Vector2(currentFrameRectX, rect.y + rect.height * 0.5f),
                new Vector2(currentFrameRectX, window.position.yMax)); // 绘制线条\
            //绘制标签
            GUI.backgroundColor = Color.white;
            GUI.Box(new Rect(currentFrameRectX - 5f, rect.y, 10f, rect.height - 5f), "", "WhiteBackground");
            //绘制帧数
            string currentFrameIDStr = currentFrameID.ToString();
            int strLength = currentFrameIDStr.Length;
            float oneNumWidth = 7.5f;
            GUI.color = Color.black;
            GUI.Label(
                new Rect(currentFrameRectX - oneNumWidth * 0.5f * strLength, rect.y, oneNumWidth * strLength,
                    rect.height - 15f),
                "", "WhiteBackground");
            GUI.color = Color.white;
            GUI.Label(
                new Rect(currentFrameRectX - oneNumWidth * 0.5f * strLength, rect.y, oneNumWidth * strLength,
                    rect.height - 15f),
                currentFrameIDStr, "WhiteBackground");

            ProcessEvent(window, rect);
        }

        private static void ProcessEvent(ClipEditorWindow window, Rect rect)
        {
            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, dragEvent);
            }

            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, dragEvent);
            }

            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.ScrollWheel)
            {
                EventCenter.TrigerEvent(window, scaleEvent);
                UnityEngine.Event.current.Use();
            }
        }
    }
}