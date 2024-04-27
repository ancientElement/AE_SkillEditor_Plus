using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.UI;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.UI.Controller
{
    public static class TimeLine
    {
        private static TimelineScaleEvent scaleEvent;
        private static TimelineDragEvent dragEvent;

        const float oneNumWidth = 7.5f; //一个数字的宽度
        public static bool leftMouseDown;

        static TimeLine()
        {
            scaleEvent = new TimelineScaleEvent();
            dragEvent = new TimelineDragEvent();
        }

        public static void UpdateGUI(AETimelineEditorWindow window, int currentFrameID,
            float widthPreFrame, Rect rect)
        {
            EditorGUI.DrawRect(rect, new Color(35f / 255f, 35f / 255f, 40f / 255f));

            //绘制时间轴
            Handles.color = Color.white; // 设置线条颜色
            float x = rect.x;
            //计算大间隔 与小间隔
            int smallLineInterval;
            int bigLineInterval;
            bigLineInterval = Mathf.Max((int)(100 / widthPreFrame), 5);
            bigLineInterval = bigLineInterval - (bigLineInterval % 5);
            bigLineInterval = Mathf.Max(bigLineInterval, 5);
            smallLineInterval = bigLineInterval / 5;
            // Debug.Log(bigLineInterval);
            int i = 0;
            int end = i + (int)(rect.width / widthPreFrame);
            while (i < end)
            {
                string numStr = i.ToString();
                if ((i) % bigLineInterval == 0)
                {
                    GUI.Label(new Rect(x, rect.y + rect.height * 0.2f, numStr.Length * (oneNumWidth + 5f), 20f),
                        numStr); // 绘制数字
                    Handles.DrawLine(new Vector2(x, rect.y + rect.height * 0.6f), new Vector2(x, rect.yMax)); // 绘制线条
                }
                else
                {
                    Handles.DrawLine(new Vector2(x, rect.y + rect.height * 0.8f), new Vector2(x, rect.yMax)); // 绘制线条
                }

                i += smallLineInterval;
                x += widthPreFrame * smallLineInterval; // 根据每帧间隔移动位置
            }

            // UpdateCurFrameUI(window, currentFrameID, widthPreFrame, rect);

            ProcessEvent(window, rect);
        }

        public static void UpdateCurFrameUI(AETimelineEditorWindow window, int currentFrameID, float widthPreFrame,
            Rect rect)
        {
            //绘制当前帧指向线条
            Handles.color = Color.white; // 设置线条颜色
            float currentFrameRectX = rect.x + (currentFrameID) * widthPreFrame;
            Handles.DrawLine(new Vector2(currentFrameRectX, rect.y + rect.height * 0.5f),
                new Vector2(currentFrameRectX, window.position.yMax)); // 绘制线条\
            //绘制标签
            GUI.backgroundColor = Color.white;
            GUI.Box(new Rect(currentFrameRectX - 5f, rect.y, 10f, rect.height - 5f), "", "WhiteBackground");
            //绘制帧数
            string currentFrameIDStr = (currentFrameID).ToString();
            int strLength = currentFrameIDStr.Length;

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
        }

        private static void ProcessEvent(AETimelineEditorWindow window, Rect rect)
        {
            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, dragEvent);
                leftMouseDown = true;
            }

            Rect windowRect = new Rect(rect.x, 0, window.position.width-rect.x, window.position.height);
            if ((UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp &&
                 UnityEngine.Event.current.button == 0) ||
                !windowRect.Contains(UnityEngine.Event.current.mousePosition))
            {
                leftMouseDown = false;
                EventCenter.TrigerEvent(window, new TimelineDragEndEvent());
                // Debug.Log("TimelineDragUp");
                //TODO:临时这样写
                TrackClipStyle.moveEventMouseDown = false;
            }

            if (leftMouseDown &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, dragEvent);
            }

            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.ScrollWheel)
            {
                EventCenter.TrigerEvent(window, scaleEvent);
                // UnityEngine.Event.current.Use();
            }
        }
    }
}