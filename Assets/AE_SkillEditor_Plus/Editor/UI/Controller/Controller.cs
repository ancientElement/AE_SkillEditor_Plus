using System;
using System.Collections.Generic;
using System.Linq;
using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.AEUIEvent;
using AE_SkillEditor_Plus.Factory;
using AE_SkillEditor_Plus.RunTime;
using OpenCover.Framework.Model;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor.Timeline;

namespace AE_SkillEditor_Plus.Editor.UI.Controller
{
    public static class Controller
    {
        private static ControllerEvent controllerEvent;

        static Controller()
        {
            controllerEvent = new ControllerEvent();
        }

        public static void UpdateGUI(AETimelineEditorWindow window, Rect rect)
        {
            rect = new Rect(rect.x + 10f, rect.y, rect.width - 20f, rect.height);
            float topHeight = rect.height * 0.5f;
            //绘制帧率
            float FPSWidth = rect.width / 4;
            var FPSRect = new Rect(rect.x, rect.y, FPSWidth, topHeight);
            if (GUI.Button(FPSRect, window.FPS + "帧", EditorStyles.popup))
            {
                GenericMenu menu = new GenericMenu();
                // 添加菜单项
                //拿到所有StandardTrack的子类型
                menu.AddItem(new GUIContent("30"), window.FPS == 30, () => window.FPS = 30);
                menu.AddItem(new GUIContent("60"), window.FPS == 60, () => window.FPS = 60);
                menu.AddItem(new GUIContent("120"), window.FPS == 120, () => window.FPS = 120);
                // 显示菜单
                menu.ShowAsContext();
            }

            float rectX = rect.x + FPSWidth;

            //绘制ObjectFIled
            float contextObjectWidth = rect.width / 4;
            var contextObject = new Rect(rectX, rect.y, contextObjectWidth, topHeight);
            //绘制一个灰色的box lable为 点击选择文件
            {
                window.Context =
                    EditorGUI.ObjectField(contextObject, window.Context, typeof(GameObject), true) as GameObject;
            }
            rectX += contextObjectWidth;

            //TODO: 序列化形式
            //绘制ObjectFIled
            var objectFiledRect = new Rect(rectX, rect.y, rect.width - FPSWidth - contextObjectWidth, topHeight);
            //绘制一个灰色的box lable为 点击选择文件
            {
                var tempObj = EditorGUI.ObjectField(objectFiledRect, window.Asset, typeof(AETimelineAsset), false);
                if (tempObj != window.Asset)
                {
                    window.AssetPath = AssetDatabase.GetAssetPath(tempObj);
                }
            }

            // {
            //     if (GUI.Button(objectFiledRect, window.AssetPath.Split("/").Last().Split(".")[0], EditorStyles.popup))
            //     {
            //         //搜索文件
            //         var res = AssetDatabase.FindAssets("glob:\"Assets/**/*.aetimeline\"",
            //             new string[] { "Assets" });
            //         var paths = new List<string>();
            //         foreach (var VAR in res)
            //         {
            //             var path = AssetDatabase.GUIDToAssetPath(VAR);
            //             paths.Add(path);
            //             // Debug.Log(path);
            //         }
            //
            //         //弹出窗口
            //         var searchWindow = ScriptableObject.CreateInstance<AETimelineSearchWindow>();
            //         searchWindow.paths = paths;
            //         //回调点击文件
            //         searchWindow.Callbakc = s => window.AssetPath = s;
            //         //打开窗口
            //         SearchWindow.Open(
            //             new SearchWindowContext(GUIUtility.GUIToScreenPoint(UnityEngine.Event.current.mousePosition)),
            //             searchWindow
            //         );
            //     }
            // }

            //绘制控件
            float itemWidth = rect.width / 5;
            float x = 0;
            for (int i = 0; i < 5; i++)
            {
                var itemRect = new Rect(rect.x + x, rect.y + topHeight, itemWidth,
                    rect.height - topHeight);
                GUI.backgroundColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1f);
                switch (i)
                {
                    case 0:
                        ToMostBegin(window, itemRect);
                        break;
                    case 1:
                        ToPreFrame(window, itemRect);
                        break;
                    case 2:
                        Play(window, itemRect);
                        break;
                    case 3:
                        ToNextFrame(window, itemRect);
                        break;
                    case 4:
                        ToMostEnd(window, itemRect);
                        break;
                }

                x += itemWidth;
            }
        }

        private static void ToMostBegin(AETimelineEditorWindow window, Rect rect)
        {
            GUIContent gotoBeginingContent =
                L10n.IconContent("Animation.FirstKey", "Go to the beginning of the timeline");
            if (GUI.Button(rect, gotoBeginingContent))
            {
                controllerEvent.ControllerType = ControllerType.ToMostBegin;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToPreFrame(AETimelineEditorWindow window, Rect rect)
        {
            GUIContent previousFrameContent = L10n.IconContent("Animation.PrevKey", "Go to the previous frame");
            if (GUI.Button(rect, previousFrameContent))
            {
                controllerEvent.ControllerType = ControllerType.ToPre;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void Play(AETimelineEditorWindow window, Rect rect)
        {
            GUIContent playContent = L10n.IconContent("Animation.Play", "Play the timeline (Space)");
            if (GUI.Button(rect, playContent, "Button"))
            {
                controllerEvent.ControllerType = ControllerType.Play;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToNextFrame(AETimelineEditorWindow window, Rect rect)
        {
            GUIContent nextFrameContent = L10n.IconContent("Animation.NextKey", "Go to the next frame");
            if (GUI.Button(rect, nextFrameContent))
            {
                controllerEvent.ControllerType = ControllerType.ToNext;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToMostEnd(AETimelineEditorWindow window, Rect rect)
        {
            GUIContent gotoEndContent =
                L10n.IconContent("Animation.LastKey", "Go to the end of the timeline");
            if (GUI.Button(rect, gotoEndContent))
            {
                controllerEvent.ControllerType = ControllerType.ToMostEnd;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }
    }
}