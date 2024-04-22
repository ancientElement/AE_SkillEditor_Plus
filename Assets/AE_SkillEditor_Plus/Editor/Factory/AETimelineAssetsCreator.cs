using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.TempAndTest.TestData;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace AE_SkillEditor_Plus.Factory
{
    public static class AETimelineAssetsCreator
    {
        public const string SUFFIX = "aetimeline";

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
            // Debug.Log(pathName + " " + resourceFile);
            var asset = new AETimelineAsset();
            //序列化新的timeline
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] binaryData;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, asset);
                binaryData = stream.ToArray();
            }

            //保存到成为二进制文件
            File.WriteAllBytes(pathName, binaryData);
            //TODO:设置文件的图标 为内置图标light
            // Texture2D icon = EditorGUIUtility.IconContent("Light Icon").image as Texture2D;
            //刷新
            AssetDatabase.Refresh();
        }
    }
}