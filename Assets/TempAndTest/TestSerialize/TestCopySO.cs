using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AE_SkillEditor_Plus.TempAndTest;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;


public class TestCopySO : MonoBehaviour
{
    [MenuItem("Test/测试拷贝SO")]
    public static void TestFUn()
    {
        //复制资源
        var path = "Assets/TempAndTest/TestSerialize/New Test OS.asset";
        //创建临时文件夹
        var folderPath = Application.dataPath + "/TempAETimelineCopy";
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        //拷贝
        var dest = "Assets/TempAETimelineCopy/" + path.Split("/").Last();
        FileUtil.CopyFileOrDirectory(path, dest);
        // //加载
        var tempAsset = AssetDatabase.LoadAssetAtPath<TestOS>(path);
        var destAsset = AssetDatabase.LoadAssetAtPath<TestOS>(dest);
        // //赋值
        // newClip = tempAsset.Tracks[trackIndex].Clips[clipIndex];
        Debug.LogWarning(tempAsset==destAsset);
        //销毁
        var files = Directory.GetFiles(folderPath);
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }
        AssetDatabase.Refresh();
        Directory.Delete(folderPath);
    }
}