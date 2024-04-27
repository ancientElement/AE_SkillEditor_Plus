using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AE_SkillEditor_Plus.RunTime;
// using AE_SkillEditor_Plus.TempAndTest.TestData;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace AE_SkillEditor_Plus.Factory
{
    public static class AETimelineAssetsCreator
    {
        //TODO: 序列化形式
        public const string SUFFIX = "asset";
        // public const string SUFFIX = "aetimeline";

        [MenuItem("Assets/Create/AETimeline编辑器/AETimelineAsset")]
        private static void CreatoFile()
        {
            AETimelineAssetsCreatorEndAction creatorEndAction =
                ScriptableObject.CreateInstance<AETimelineAssetsCreatorEndAction>();
            string name = GetName();
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(creatorEndAction.GetInstanceID(), creatorEndAction,
                name, null, null);
        }

        private static string GetName(string tempName = "New AETimelineAsset")
        {
            int i = 0;
            string name = $"{tempName}_{i}";
            string[] files = AssetDatabase.FindAssets(name);

            for (i += 1; files != null && files.Length > 0; i++)
            {
                name = $"{tempName}_{i}";
                files = AssetDatabase.FindAssets(name);
            }

            return $"{name}.{SUFFIX}";
        }
    }

    public class AETimelineAssetsCreatorEndAction : EndNameEditAction
    {
        //按回车执行Action方法
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            //TODO: 序列化形式
            // var asset = new AETimelineAsset();
            var asset = ScriptableObject.CreateInstance<AETimelineAsset>();
            // AETimelineFactory.Save(asset,pathName);
            AssetDatabase.CreateAsset(asset,pathName);
            //TODO:设置文件的图标 为内置图标light
            // Texture2D icon = EditorGUIUtility.IconContent("Light Icon").image as Texture2D;
            //刷新
            AssetDatabase.Refresh();
        }
    }
}