using AE_SkillEditor_Plus.UI.Data;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    //轨道头
    public static class TrackHeadStyle
    {
        public static void UpdateUI(Rect rect, TrackStyleData data,int trackIndex)
        {
            // Debug.Log(data.Name + rect);
            GUI.backgroundColor = data.Color;
            GUI.Box(rect, data.Name,"AC BoldHeader");
        }
    }
}