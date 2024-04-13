using System;
using AE_SkillEditor_Plus.Event;
using AE_SkillEditor_Plus.UI.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using EventType = AE_SkillEditor_Plus.Event.EventType;

namespace AE_SkillEditor_Plus.UI
{
    //clip
    public static class TrackClipStyle
    {
        private const float tailWdithPersent = 0.1f;

        private static bool moveEventMouseDown;
        private static bool resizeEventMouseDown;
        private static ClipEvent mouseEvent;
        private static KeyboradEvent keyboradEvent;
        private static bool needClickClip;//是否需要点击Clip

        public static void UpdateUI(ClipEditorWindow window, Rect rect, ClipStyleData data, int trackIndex,
            int clipIndex)
        {
            GUI.backgroundColor = data.Color;
            GUI.Box(rect, data.Name, "AC BoldHeader");

            ProcessEvent(window, rect, trackIndex, clipIndex);
        }

        private static void ProcessEvent(ClipEditorWindow window, Rect rect, int trackIndex, int clipIndex)
        {
            //左键按下
            var bodyRect = new Rect(rect.x, rect.y, rect.width * (1 - tailWdithPersent), rect.height);
            if (bodyRect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 0)
            {
                moveEventMouseDown = true;
                mouseEvent = new ClipMoveEvent()
                {
                    ClipIndex = clipIndex,
                    TrackIndex = trackIndex,
                    OffsetMouseX = UnityEngine.Event.current.mousePosition.x - rect.x
                };
                needClickClip = false;
                keyboradEvent = new KeyboradEvent()
                {
                    ClipIndex = clipIndex,
                    TrackIndex = trackIndex
                };
            }

            //----监听拖动----
            //左键松开
            if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp &&
                UnityEngine.Event.current.button == 0) moveEventMouseDown = false;
            //左键拖动
            if (moveEventMouseDown &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, mouseEvent);
            }

            //----监听尾部大小重置----
            var tailRect = new Rect(rect.x + rect.width * (1 - tailWdithPersent), rect.y, rect.width * tailWdithPersent,
                rect.height);
            EditorGUIUtility.AddCursorRect(tailRect, MouseCursor.ResizeHorizontal);
            //左键按下
            if (tailRect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 0)
            {
                resizeEventMouseDown = true;
                mouseEvent = new ClipResizeEvent()
                {
                    ClipIndex = clipIndex,
                    TrackIndex = trackIndex,
                    OffsetMouseX = UnityEngine.Event.current.mousePosition.x - rect.x - rect.width
                };
            }

            //左键松开
            if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp &&
                UnityEngine.Event.current.button == 0) resizeEventMouseDown = false;
            //左键
            if (resizeEventMouseDown &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDrag &&
                UnityEngine.Event.current.button == 0)
            {
                EventCenter.TrigerEvent(window, mouseEvent);
            }

            //----快捷键----
            //Ctrl C
            if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown && UnityEngine.Event.current.control &&
                UnityEngine.Event.current.keyCode == KeyCode.C)
            {
                if (!needClickClip)
                {
                    keyboradEvent.Shortcut = Shortcut.CtrlC;
                    EventCenter.TrigerEvent(window, keyboradEvent);
                    UnityEngine.Event.current.Use();
                    needClickClip = true;
                }
            }

            //Ctrl V
            if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown && UnityEngine.Event.current.control &&
                UnityEngine.Event.current.keyCode == KeyCode.V)
            {
                keyboradEvent.Shortcut = Shortcut.CtrlV;
                EventCenter.TrigerEvent(window, keyboradEvent);
                UnityEngine.Event.current.Use();
            }

            //Ctrl X
            if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown && UnityEngine.Event.current.control &&
                UnityEngine.Event.current.keyCode == KeyCode.X)
            {
                if (!needClickClip)
                {
                    keyboradEvent.Shortcut = Shortcut.CtrlX;
                    EventCenter.TrigerEvent(window, keyboradEvent);
                    UnityEngine.Event.current.Use();
                    needClickClip = true;
                }
            }
        }
    }
}