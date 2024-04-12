using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.UI.Controller
{
    public static class Contorller
    {
        public static void UpdateGUI(ClipEditorWindow window, Rect rect)
        {
            float itemWidth = rect.width / 5;
            float x = 0;
            for (int i = 0; i < 5; i++)
            {
                GUI.backgroundColor = new Color(60/255f, 60/255f, 60/255f,1f);
                var itemRect = new Rect(rect.x + x, rect.y, itemWidth, rect.height);
                GUI.Box(itemRect, "", "Button");
                x += itemWidth;
            }
        }
    }
}