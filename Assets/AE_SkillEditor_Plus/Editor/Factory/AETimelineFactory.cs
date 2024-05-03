using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using AE_SkillEditor_Plus.Editor.Driver;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using Object = UnityEngine.Object;

//创建对应资产
namespace AE_SkillEditor_Plus.Factory
{
    public static class AETimelineFactory
    {
        public static AETimelineAsset LoadTimeLineAsset(string path)
        {
            //TODO: 序列化形式
            //从path加载二进制文件
            // byte[] binaryData = File.ReadAllBytes(path);
            // //将二进制文件反序列化为AETimelineAsset
            // BinaryFormatter formatter = new BinaryFormatter();
            // using (MemoryStream stream = new MemoryStream(binaryData))
            // {
            //     AETimelineAsset timelineAsset = formatter.Deserialize(stream) as AETimelineAsset;
            //     return timelineAsset;
            // }
            // 从对应地址加载ScriptableObject 
            {
                return AssetDatabase.LoadAssetAtPath<AETimelineAsset>(path);
            }
        }

        public static void CreatTrack(AETimelineAsset asset, AETimelineEditorTick editorTick, string path,
            int trackIndex, Type type)
        {
            //TODO:修改为ScriptableObject嵌套
            // Debug.Log(type);
            // var track = ScriptableObject.CreateInstance(type) as StandardTrack;
            var track = Activator.CreateInstance(type) as StandardTrack;
            // Debug.Log("在第"+trackIndex+"创建了轨道");
            asset.Tracks.Insert(trackIndex, track);
            editorTick.AddTrack(trackIndex);
            Save(asset, path);
        }

        public static void RemoveTrack(AETimelineAsset asset, AETimelineEditorTick editorTick, string path,
            int trackIndex)
        {
            for (int i = asset.Tracks[trackIndex].Clips.Count - 1; i >= 0; i--)
            {
                RemoveClip(asset, editorTick, path, trackIndex, i);
            }

            asset.Tracks.RemoveAt(trackIndex);
            editorTick.RemoveTrack(trackIndex);
            Save(asset, path);
        }

        public static void CreateClip(AETimelineAsset asset, AETimelineEditorTick editorTick, string path,
            int trackIndex, int startIndex)
        {
            //TODO:修改为ScriptableObject嵌套
            // var clip = Activator.CreateInstance(asset.Tracks[trackIndex].GetType()
            // .GetCustomAttribute<AEBindClipAttribute>().ClipType) as StandardClip;
            var type = asset.Tracks[trackIndex].GetType().GetCustomAttribute<AEBindClipAttribute>().ClipType;
            var clip = ScriptableObject.CreateInstance(type) as StandardClip;
            clip.StartID = startIndex;
            clip.Duration = 100;
            clip.Name = clip.GetType().Name;
            AddClip(asset, editorTick, path, trackIndex, startIndex, clip);
        }

        public static void AddClip(AETimelineAsset asset, AETimelineEditorTick editorTick, string path, int trackIndex,
            int startIndex,
            StandardClip clip)
        {
            if (clip == null) return;
            //这里必须先得到length
            clip.StartID = startIndex;
            //找到左边边第一个小于他的数  找到右边第一个大于他的数 
            int left = -1;
            if (asset.Tracks[trackIndex].Clips.Count >= 1)
            {
                int leftStartID = Int32.MinValue;

                int rightStartID = Int32.MaxValue;
                int right = -1;

                for (int i = 0; i < asset.Tracks[trackIndex].Clips.Count; i++)
                {
                    if (asset.Tracks[trackIndex].Clips[i].StartID <
                        clip.StartID)
                    {
                        if (asset.Tracks[trackIndex].Clips[i].StartID > leftStartID)
                        {
                            leftStartID = asset.Tracks[trackIndex].Clips[i].StartID;
                            left = i;
                        }
                    }

                    if (asset.Tracks[trackIndex].Clips[i].StartID >
                        clip.StartID)
                    {
                        if (asset.Tracks[trackIndex].Clips[i].StartID < rightStartID)
                        {
                            rightStartID = asset.Tracks[trackIndex].Clips[i].StartID;
                            right = i;
                        }
                    }
                }

                if (left != -1 &&
                    clip.StartID <
                    asset.Tracks[trackIndex].Clips[left].StartID + asset.Tracks[trackIndex].Clips[left].Duration)
                {
                    return;
                }

                if (right != -1 &&
                    clip.StartID + clip.Duration >
                    asset.Tracks[trackIndex].Clips[right].StartID)
                {
                    clip.Duration = asset.Tracks[trackIndex].Clips[right].StartID -
                                    clip.StartID;
                }
            }

            //附加到父资产上
            AssetDatabase.AddObjectToAsset(clip, asset);
            asset.Tracks[trackIndex].Clips.Add(clip);
            int clipIndex = left == -1 ? 0 : left + 1;
            // Debug.Log(clipIndex);
            editorTick.AddBehaviour(trackIndex, clipIndex);
            Save(asset, path);
        }

        public static StandardClip CopyClip(AETimelineAsset asset, int trackIndex, int clipIndex)
        {
            //TODO: 序列化形式
            StandardClip newClip;
            // StandardClip clip, newClip;
            // {
            //     clip = asset.Tracks[trackIndex].Clips[clipIndex];
            //
            //     BinaryFormatter formatter = new BinaryFormatter();
            //     byte[] binaryData;
            //     using (MemoryStream stream = new MemoryStream())
            //     {
            //         formatter.Serialize(stream, clip);
            //         binaryData = stream.ToArray();
            //     }
            //
            //     using (MemoryStream stream = new MemoryStream(binaryData))
            //     {
            //         newClip = formatter.Deserialize(stream) as StandardClip;
            //     }
            // }
            {
                //复制资源
                // var path = AssetDatabase.GetAssetPath(asset);
                // //创建临时文件夹
                // var folderPath = Application.dataPath + "/TempAETimelineCopy";
                // if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                // //拷贝
                // var des = "Assets/TempAETimelineCopy/" + path.Split("/").Last();
                // FileUtil.CopyFileOrDirectory(path, des);
                // //加载
                // var tempAsset = AssetDatabase.LoadAssetAtPath<AETimelineAsset>(des);
                // //赋值
                // newClip = tempAsset.Tracks[trackIndex].Clips[clipIndex];
                // //销毁
                // FileUtil.DeleteFileOrDirectory("Assets/TempAETimelineCopy/");
                // var tempAsset = Object.Instantiate(asset);
                // newClip = Object.Instantiate(tempAsset.Tracks[trackIndex].Clips[clipIndex]);
            }
            {
                newClip = Object.Instantiate(asset.Tracks[trackIndex].Clips[clipIndex]);
                newClip.name = asset.Tracks[trackIndex].Clips[clipIndex].name;
            }
            return newClip;
        }

        public static void RemoveClip(AETimelineAsset asset, AETimelineEditorTick editorTick, string path,
            int trackIndex, int clipIndex)
        {
            // 从父资产中删除
            AssetDatabase.RemoveObjectFromAsset(asset.Tracks[trackIndex].Clips[clipIndex]);
            asset.Tracks[trackIndex].Clips.RemoveAt(clipIndex);
            editorTick.RemoveBehaviour(trackIndex, clipIndex);
            Save(asset, path);
        }

        public static void ResizeClip(AETimelineAsset asset, int trackIndex, int clipIndex, int target)
        {
            // Debug.Log(trackIndex+" " +clipIndex);
            if (target < 1) target = 1;
            //找到右边第一个大于他的数
            if (asset.Tracks[trackIndex].Clips.Count > 1 && target > 1)
            {
                int rightStartID = Int32.MaxValue;
                int right = -1;

                for (int i = 0; i < asset.Tracks[trackIndex].Clips.Count; i++)
                {
                    if (asset.Tracks[trackIndex].Clips[i].StartID >
                        asset.Tracks[trackIndex].Clips[clipIndex].StartID)
                    {
                        if (asset.Tracks[trackIndex].Clips[i].StartID < rightStartID)
                        {
                            rightStartID = asset.Tracks[trackIndex].Clips[i].StartID;
                            right = i;
                        }
                    }
                }

                if (right != -1 &&
                    asset.Tracks[trackIndex].Clips[clipIndex].StartID + target >
                    asset.Tracks[trackIndex].Clips[right].StartID)
                {
                    target = asset.Tracks[trackIndex].Clips[right].StartID -
                             asset.Tracks[trackIndex].Clips[clipIndex].StartID;
                }
            }

            asset.Tracks[trackIndex].Clips[clipIndex].Duration = target;
        }

        public static void MoveClip(AETimelineAsset asset, string path, int trackIndex, int clipIndex, int target)
        {
            if (target <= 0) target = 0;
            //找到左边第一个 小于他的数
            if (asset.Tracks[trackIndex].Clips.Count > 1)
            {
                int leftStartID = Int32.MinValue;
                int left = -1;

                int rightStartID = Int32.MaxValue;
                int right = -1;

                for (int i = 0; i < asset.Tracks[trackIndex].Clips.Count; i++)
                {
                    if (asset.Tracks[trackIndex].Clips[i].StartID <
                        asset.Tracks[trackIndex].Clips[clipIndex].StartID)
                    {
                        if (asset.Tracks[trackIndex].Clips[i].StartID > leftStartID)
                        {
                            left = i;
                            leftStartID = asset.Tracks[trackIndex].Clips[i].StartID;
                        }
                    }

                    if (asset.Tracks[trackIndex].Clips[i].StartID >
                        asset.Tracks[trackIndex].Clips[clipIndex].StartID)
                    {
                        if (asset.Tracks[trackIndex].Clips[i].StartID < rightStartID)
                        {
                            right = i;
                            rightStartID = asset.Tracks[trackIndex].Clips[i].StartID;
                        }
                    }
                }

                // Debug.Log("left " + leftStartID + " right " + rightStartID + "target" + target);

                if (left != -1 && target < leftStartID + asset.Tracks[trackIndex].Clips[left].Duration)
                {
                    asset.Tracks[trackIndex].Clips[clipIndex].StartID =
                        leftStartID + asset.Tracks[trackIndex].Clips[left].Duration;
                    return;
                }

                if (right != -1 && target + asset.Tracks[trackIndex].Clips[clipIndex].Duration > rightStartID)
                {
                    asset.Tracks[trackIndex].Clips[clipIndex].StartID =
                        rightStartID -  asset.Tracks[trackIndex].Clips[clipIndex].Duration;
                    return;
                }
            }

            // Debug.Log(targetStartIndex);
            asset.Tracks[trackIndex].Clips[clipIndex].StartID = target;

            // Save(asset, path);
        }

        public static void Save(AETimelineAsset asset, string path)
        {
            // Debug.Log("保存");
            //遍历asset的轨道
            //结尾最远的Clip
            int maxFar = 0;
            for (int i = 0; i < asset.Tracks.Count; i++)
            {
                var track = asset.Tracks[i];
                if (track.Clips.Count == 0) continue;
                //为clip按照startID排序
                track.Clips.Sort((StandardClip x, StandardClip y) =>
                {
                    if (x.StartID > y.StartID) return 1;
                    else return -1;
                });
                maxFar = Mathf.Max(maxFar, track.Clips.Last().StartID + track.Clips.Last().Duration);
            }

            //时间轴的总体时长
            asset.Duration = maxFar;
            // Debug.Log(maxFar);

            //重新生成
            //DO: 不对劲
            // AETimelineEditorTick.PlayAsset(asset);
            //TODO: 序列化形式
            // {
            //     //序列化timeline
            //     BinaryFormatter formatter = new BinaryFormatter();
            //     byte[] binaryData;
            //     using (MemoryStream stream = new MemoryStream())
            //     {
            //         formatter.Serialize(stream, asset);
            //         binaryData = stream.ToArray();
            //     }
            //
            //     //保存到成为二进制文件
            //     File.WriteAllBytes(path, binaryData);
            // }
            {
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssets();
            }
        }
    }
}