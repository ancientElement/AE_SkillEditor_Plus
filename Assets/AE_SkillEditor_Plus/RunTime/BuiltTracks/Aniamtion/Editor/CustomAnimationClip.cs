using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.BuiltTracks;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.BuiltTracks
{
    public static class CustomAnimationClip
    {
        public static void UpdateUI(AETimelineEditorWindow window, Rect rect, int[] hightLight, Color color,
            string clipName, float widthPerFrame, int trackIndex, int clipIndex)
        {
            // Debug.Log("额外的Clip样式");
            var clip = window.Asset.Tracks[trackIndex].Clips[clipIndex];
            var animationClip = (clip as AEAnimationClip).AnimationClip;
            if (animationClip != null)
            {
                float animationDuration = animationClip.length * window.FPS;
                float x = rect.x + animationDuration * window.WidthPreFrame;
                if (animationDuration == clip.Duration)
                {
                    EditorGUI.DrawRect(new Rect(x - 5, rect.y, 5, rect.height), Color.green);
                }
                else if (x < rect.x + rect.width)
                {
                    var nameRect = new Rect(rect.x+5f, rect.y+5f, x - rect.x-5f, rect.height - 12f);
                    EditorGUI.DrawRect(nameRect, new Color(83f / 255, 93f / 255, 105f / 255));
                    EditorGUI.LabelField(nameRect, clip.Name);

                    EditorGUI.DrawRect(new Rect(x - 5, rect.y, 5, rect.height), Color.red);
                    GUI.contentColor = Color.white;

                    var loopOrHoldRect = new Rect(x, rect.y+5f, rect.width - (x - rect.x) -5f, rect.height - 12f);
                    EditorGUI.DrawRect(loopOrHoldRect, Color.grey);
                    EditorGUI.LabelField(loopOrHoldRect, "LoopOrHold");
                }
            }
        }
    }
}