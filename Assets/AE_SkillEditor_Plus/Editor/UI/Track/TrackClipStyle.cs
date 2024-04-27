using System;
using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.Editor.UI.Controller;
using AE_SkillEditor_Plus.UI.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AE_SkillEditor_Plus.UI
{
    //clip
    public static class TrackClipStyle
    {
        private const float tailWdithPersent = 0.1f;

        public static bool moveEventMouseDown;
        private static bool resizeEventMouseDown;
        private static ClipEvent mouseEvent;
        private static KeyboradEvent keyboradEvent;
        private static bool needClickClip; //是否需要点击Clip

        public static void UpdateUI(AETimelineEditorWindow window, Rect rect, int[] highLight, Color color,
            string Name, int trackIndex,
            int clipIndex)
        {
            //绘制背景
            if (highLight[0] == trackIndex && highLight[1] == clipIndex)
                EditorGUI.DrawRect(rect, new Color(101f / 255, 116f / 255, 133f / 255) * 1.8f);
            else
                EditorGUI.DrawRect(rect, new Color(101f / 255, 116f / 255, 133f / 255));

            //绘制Cip颜色标识
            EditorGUI.DrawRect(new Rect(rect.x, rect.y + rect.height * 0.8f, rect.width, rect.height * 0.2f),
                color);
            ProcessEvent(window, rect, trackIndex, clipIndex);
            //绘制名称
            GUI.contentColor = Color.white;
            GUI.Label(rect, Name, "Box");

            //边框
            Handles.color = highLight[0] == trackIndex && highLight[1] == clipIndex ? Color.white : Color.black;
            Handles.DrawPolyLine(
                new Vector3(rect.x, rect.y), // 左上角
                new Vector3(rect.x + rect.width, rect.y), // 右上角
                new Vector3(rect.x + rect.width, rect.y + rect.height), // 右下角
                new Vector3(rect.x, rect.y + rect.height), // 左下角
                new Vector3(rect.x, rect.y) // 返回左上角闭合
            );
        }

        private static void ProcessEvent(AETimelineEditorWindow window, Rect rect, int trackIndex, int clipIndex)
        {
            var bodyRect = new Rect(rect.x, rect.y, rect.width * (1 - tailWdithPersent), rect.height);
            //右键按下
            if (bodyRect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 1)
            {
                EventCenter.TrigerEvent(window,
                    new ClipRightClickEvent() { TrackIndex = trackIndex, ClipIndex = clipIndex });
            }

            //左键按下
            if (bodyRect.Contains(UnityEngine.Event.current.mousePosition) &&
                UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
                UnityEngine.Event.current.button == 0)
            {
                moveEventMouseDown = true;
                EventCenter.TrigerEvent(window,
                    new ClipClickEvent() { TrackIndex = trackIndex, ClipIndex = clipIndex }
                );
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
                UnityEngine.Event.current.button == 0)
            {
                if (moveEventMouseDown)
                    EventCenter.TrigerEvent(window,
                        new ClipMoveEndEvent() { TrackIndex = trackIndex, ClipIndex = clipIndex });
                else if (resizeEventMouseDown)
                    EventCenter.TrigerEvent(window,
                        new ClipResizeEndEvent() { TrackIndex = trackIndex, ClipIndex = clipIndex });
                moveEventMouseDown = false;
                //TODO:临时这样写
                TimeLine.leftMouseDown = false;
                // Debug.Log("moveEventMouseUp");
            }

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

            //Delete
            if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown &&
                UnityEngine.Event.current.keyCode == KeyCode.Delete)
            {
                if (!needClickClip)
                {
                    keyboradEvent.Shortcut = Shortcut.Delete;
                    // Debug.Log("Delete");
                    EventCenter.TrigerEvent(window, keyboradEvent);
                    UnityEngine.Event.current.Use();
                    needClickClip = true;
                }
            }
        }
    }
}