using System.Collections.Generic;
using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    //轨道体
    public static class TrackBodyStyle
    {
        public static void UpdateUI(AETimelineEditorWindow window, Rect rect, int[] highLight, float widthPreFrame,
            TrackStyleData data,
            int trackIndex)
        {
            // Debug.Log(rect);
            //绘制背景
            EditorGUI.DrawRect(new Rect(rect.x,rect.y,rect.width,rect.height), new Color(64f / 255, 64f / 255, 64f / 255));
            //划分
            for (int i = 0; i < data.Clips.Count; i++)
            {
                var clipData = data.Clips[i];
                //为Clip划分rect
                var clipRect = new Rect(
                    rect.x + clipData.StartID * widthPreFrame,
                    rect.y,
                    (clipData.EndID - clipData.StartID) * widthPreFrame,
                    rect.height);
                TrackClipStyle.UpdateUI(window, clipRect, highLight, data.Color, clipData.Name, trackIndex, i);
            }

            //处理事件
            ProcessEvent(window, rect, highLight, widthPreFrame, data, trackIndex);
        }

        private static void ProcessEvent(AETimelineEditorWindow window, Rect rect, int[] highLight, float widthPreFrame,
            TrackStyleData data, int trackIndex)
        {
            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 1)
            {
                EventCenter.TrigerEvent(window,
                    new BodyRightClick() { MouseFrameID = window.MouseCurrentFrameID, TrackIndex = trackIndex });
            }
        }
    }
}