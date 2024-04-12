using System.Collections.Generic;
using AE_SkillEditor_Plus.UI.Data;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    //轨道体
    public static class TrackBodyStyle
    {
        public static void UpdateUI(ClipEditorWindow window,Rect rect,TrackStyleData data,int trackIndex)
        {
            //绘制背景
            GUI.backgroundColor = Color.gray;
            GUI.Box(rect,"","AC BoldHeader");
            //划分
            for (int i = 0; i < data.Clips.Count; i++)
            {
                var clipData = data.Clips[i];
                //为Clip划分rect
                var clipRect = new Rect(
                    rect.x + clipData.StartID * data.WidthPreFrame,
                    rect.y,
                    (clipData.EndID - clipData.StartID) * data.WidthPreFrame,
                    rect.height);
                TrackClipStyle.UpdateUI(window,clipRect,clipData,trackIndex,i);
            }
        }
    }
}