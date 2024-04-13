using AE_SkillEditor_Plus.Event;
using UnityEditor;
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

        public static void UpdateGUI(ClipEditorWindow window, Rect rect)
        {
            float itemWidth = rect.width / 5;
            float x = 0;
            for (int i = 0; i < 5; i++)
            {
                var itemRect = new Rect(rect.x + x, rect.y, itemWidth, rect.height);
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

        private static void ToMostBegin(ClipEditorWindow window, Rect rect)
        {
            GUIContent gotoBeginingContent =
                L10n.IconContent("Animation.FirstKey", "Go to the beginning of the timeline");
            if (GUI.Button(rect, gotoBeginingContent))
            {
                controllerEvent.ControllerType = ControllerType.ToMostBegin;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToPreFrame(ClipEditorWindow window, Rect rect)
        {
            GUIContent previousFrameContent = L10n.IconContent("Animation.PrevKey", "Go to the previous frame");
            if (GUI.Button(rect, previousFrameContent))
            {
                controllerEvent.ControllerType = ControllerType.ToPre;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void Play(ClipEditorWindow window, Rect rect)
        {
            GUIContent playContent = L10n.IconContent("Animation.Play", "Play the timeline (Space)");
            if (GUI.Button(rect, playContent, "Button"))
            {
                controllerEvent.ControllerType = ControllerType.Play;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToNextFrame(ClipEditorWindow window, Rect rect)
        {
            GUIContent nextFrameContent = L10n.IconContent("Animation.NextKey", "Go to the next frame");
            if (GUI.Button(rect, nextFrameContent))
            {
                controllerEvent.ControllerType = ControllerType.ToNext;
                EventCenter.TrigerEvent(window, controllerEvent);
            }
        }

        private static void ToMostEnd(ClipEditorWindow window, Rect rect)
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