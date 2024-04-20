using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    //轨道头
    public static class TrackHeadStyle
    {
        public static void UpdateUI(AETimelineEditorWindow window, Rect rect, TrackStyleData data, int trackIndex)
        {
            rect = new Rect(rect.x + 10f, rect.y, rect.width - 20f, rect.height);
            // Debug.Log(data.Name + rect);
            //绘制背景
            EditorGUI.DrawRect(rect, new Color(97f / 255, 97f / 255, 97f / 255));
            //绘制轨道颜色标识
            EditorGUI.DrawRect(new Rect(rect.x + 2f, rect.y + 1.5f, rect.width * 0.05f, rect.height - 3f), data.Color);
            //绘制标签
            GUI.Label(rect, data.Name, "Box");

            ProcessEvent(window, rect, trackIndex);
        }

        private static void ProcessEvent(AETimelineEditorWindow window, Rect rect, int trackIndex)
        {
            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 1)
            {
                EventCenter.TrigerEvent(window, new HeadRightClickEvent() { TrackIndex = trackIndex });
            }
        }
    }
}