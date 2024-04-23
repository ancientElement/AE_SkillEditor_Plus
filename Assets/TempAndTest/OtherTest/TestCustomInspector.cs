using AE_SkillEditor_Plus.TempAndTest;
using UnityEditor;
using UnityEngine;

namespace TempAndTest.OtherTest
{
    // [CustomEditor(typeof(TestInspector))]
    public class TestCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            var fatherProperty = serializedObject.FindProperty("Father");
            // EditorGUILayout.PropertyField(fatherProperty);
            // var sobj =  fatherProperty.serializedObject;
            // var obj = sobj.targetObject;
            // Debug.Log(fatherProperty.type);
            // if (fatherProperty.objectReferenceValue != null)
            // {
                // var fatherType = fatherProperty.objectReferenceValue.GetType();
                // if (fatherType == typeof(Son))
                // {
                    // var numProperty = fatherProperty.FindPropertyRelative("num");
                    // var strProperty = fatherProperty.FindPropertyRelative("str");
                    // EditorGUILayout.PropertyField(numProperty);
                    // EditorGUILayout.PropertyField(strProperty);
                // }
                // else
                // {
                //     EditorGUILayout.LabelField("Unsupported Type", EditorStyles.boldLabel);
                // }
            // }

            serializedObject.ApplyModifiedProperties();
        }
    }
}