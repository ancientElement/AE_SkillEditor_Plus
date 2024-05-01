using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TempAndTest.OtherTest
{
    public static class 测试Insert
    {
        [MenuItem("Test/测试Insert")]
        public static void TestFun()
        {
            var testList = new List<int>() { 0, 1, 2, 3 };
            testList.Insert(4, 10);
            foreach (var item in testList)
            {
                Debug.Log(item);
            }
        }
    }
}