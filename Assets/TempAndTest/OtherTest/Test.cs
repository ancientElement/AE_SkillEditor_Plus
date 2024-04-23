using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace  AE_SkillEditor_Plus.TempAndTest
{
    public class Test
    {
        [MenuItem("Test/写入子类 测试SO中父子类")]
        public static void TestFunWrite()
        {
            var test = ScriptableObject.CreateInstance<TestOS>();
            test.Fathers = new List<Father>()
            {
                new Son()
                {
                    num = 10,
                    str = "hello"
                }
            };
            string path = "Assets/Temp/TestOS.asset"; // 指定保存路径
            AssetDatabase.CreateAsset(test, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("TestOS created at " + path);
        }

        [MenuItem("Test/读取 测试SO中父子类")]
        public static void TestFunRead()
        {
            string path = "Assets/Temp/TestOS.asset"; // 指定保存路径
            var test = AssetDatabase.LoadAssetAtPath<TestOS>(path);
            if (test != null)
            {
                Debug.Log("TestOS loaded from " + path);
                // 在这里可以使用加载的TestOS对象
                Debug.Log("num " + (test.Fathers[0] as Son).num + " str " + (test.Fathers[0] as Son).str);
            }
            else
            {
                Debug.LogError("Failed to load TestOS from " + path);
            }
        }
    }
}