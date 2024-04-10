using AE_SkillEditor_Plus.UI.Data;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    //clip
    public static class TrackClipStyle
    {
        public static void UpdateUI(Rect rect,ClipStyleData data)
        {
            // Debug.Log(clipStyleData.Color + "--" + clipStyleData.Name + "--" + clipStyleData.StartID + "--" +
            //           clipStyleData.EndID);
            GUI.backgroundColor = data.Color;
            GUI.Box(rect, data.Name,"AC BoldHeader");
        }
    }
}