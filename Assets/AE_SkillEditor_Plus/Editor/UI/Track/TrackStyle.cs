using AE_SkillEditor_Plus.UI.Data;
using UnityEngine;

namespace AE_SkillEditor_Plus.UI
{
    public class TrackStyle
    {
        public int TrackID;

        public void UpdateUI(ClipEditorWindow window,Rect rect, float headWidth,TrackStyleData data,int trackIndex)
        {
            //划分
            var headRect = new Rect(rect.x, rect.y, headWidth,
                rect.height);
            TrackHeadStyle.UpdateUI(headRect, data,trackIndex);
            //划分
            var bodyRect = new Rect(rect.x + headWidth,
                rect.y,
                rect.width - headWidth,
                rect.height);
            TrackBodyStyle.UpdateUI(window,bodyRect, data,trackIndex);
        }
    }
}