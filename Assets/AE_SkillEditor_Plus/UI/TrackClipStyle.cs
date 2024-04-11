using System;
using AE_SkillEditor_Plus.Event;
using AE_SkillEditor_Plus.UI.Data;
using UnityEngine;
using UnityEngine.UIElements;
using EventType = AE_SkillEditor_Plus.Event.EventType;

namespace AE_SkillEditor_Plus.UI
{
    //clip
    public static class TrackClipStyle
    {
        private static float mouseStart;

        public static void UpdateUI(ClipEditorWindow window, Rect rect, ClipStyleData data, int trackIndex,
            int clipIndex)
        {
            // Debug.Log(clipStyleData.Color + "--" + clipStyleData.Name + "--" + clipStyleData.StartID + "--" +
            //           clipStyleData.EndID);
            GUI.backgroundColor = data.Color;
            GUI.Box(rect, data.Name, "AC BoldHeader");

            ProcessEvent(window, rect, trackIndex, clipIndex);
        }

        private static void ProcessEvent(ClipEditorWindow window, Rect rect, int trackIndex, int clipIndex)
        {
            //左键按住
            if (rect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag &&
                UnityEngine.Event.current.button == 0)
            {
                var moveEvent = new MoveEvent()
                {
                    ClipIndex = clipIndex,
                    TrackIndex = trackIndex,
                    EventType = EventType.Move, MouseDeltaX = UnityEngine.Event.current.delta.x
                };
                EventCenter.TrigerEvent(window, moveEvent);
            }
        }
    }
}