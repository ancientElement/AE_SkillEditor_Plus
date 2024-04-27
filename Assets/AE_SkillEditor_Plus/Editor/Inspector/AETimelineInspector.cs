using System;
using AE_SkillEditor_Plus.Editor.Window;
using AE_SkillEditor_Plus.Factory;
using AE_SkillEditor_Plus.RunTime;
// using AE_SkillEditor_Plus.TempAndTest.TestData;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.Inspector
{
    /// <summary>
    /// 序列化面板
    /// </summary>
    public class AETimelineInspector //: ScriptableObject
    {
        // [SerializeReference] public StandardClip Clip;
        // public static AETimelineEditorWindow Window;
        // public static AETimelineInspector _instance;

        // public static AETimelineInspector Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             _instance = CreateInstance<AETimelineInspector>();
        //         }
        //
        //         return _instance;
        //     }
        // }

        public static void ShowInspector(AETimelineEditorWindow window, StandardClip clip)
        {
            // Window = window;
            // Instance.Clip = clip;
            // Selection.activeObject = Instance;
            Selection.activeObject = clip;
        }

        public void OnValidate()
        {
            //if (Window != null) AETimelineFactory.Save(Window.Asset, Window.AssetPath);
        }
    }
}