using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.TempAndTest.TestData;
using UnityEditor;
using UnityEngine;

namespace TempAndTest.TestInspector
{
    public class TestSOInspector : ScriptableObject
    {
        [SerializeReference] public StandardClip Clip;

        public static TestSOInspector _instance;

        public static TestSOInspector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = CreateInstance<TestSOInspector>();
                    // _instance.Clip = new TestClipData();
                }

                return _instance;
            }
        }

        [MenuItem("Test/测试面板")]
        public static void TestFun()
        {
            Selection.activeObject = Instance;
        }
    }
}