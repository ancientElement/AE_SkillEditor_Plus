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

//创建对应资产
namespace AE_SkillEditor_Plus.Factory
{
    public static class AETimelineFactory
    {
        public static AETimelineAsset LoadTimeLineAsset(string path)
        {
            //从path加载二进制文件
            byte[] binaryData = File.ReadAllBytes(path);
            //将二进制文件反序列化为AETimelineAsset
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(binaryData))
            {
                AETimelineAsset timelineAsset = formatter.Deserialize(stream) as AETimelineAsset;
                return timelineAsset;
            }
        }

        public static void CreatTrack(AETimelineAsset asset, string path, int trackIndex, StandardTrack track)
        {
            // Debug.Log("在第"+trackIndex+"创建了轨道");
            asset.Tracks.Insert(trackIndex, track);
            Save(asset, path);
        }

        public static void RemoveTrack(AETimelineAsset asset, string path, int trackIndex)
        {
            asset.Tracks.RemoveAt(trackIndex);
            Save(asset, path);
        }

        public static void AddClip(AETimelineAsset asset, string path, int trackIndex, int startIndex)
        {
            var clip = Activator.CreateInstance(asset.Tracks[trackIndex].GetType()
                .GetCustomAttribute<AEBindClipAttribute>().ClipType) as StandardClip;
            clip.StartID = startIndex;
            clip.Duration = 100;
            clip.Name = clip.GetType().Name;
            AddClip(asset, path, trackIndex, startIndex, clip);
        }

        public static void AddClip(AETimelineAsset asset, string path, int trackIndex, int startIndex,
            StandardClip clip)
        {
            if (clip == null) return;
            //这里必须先得到length
            clip.StartID = startIndex;
            //找到右边第一个大于他的数
            if (asset.Tracks[trackIndex].Clips.Count >= 1)
            {
                int leftStartID = Int32.MinValue;
                int left = -1;

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

            asset.Tracks[trackIndex].Clips.Add(clip);
            Save(asset, path);
        }

        public static StandardClip CopyClip(AETimelineAsset asset, int trackIndex, int clipIndex)
        {
            var clip = asset.Tracks[trackIndex].Clips[clipIndex];
            return CopyClip(asset, clip);
        }

        public static StandardClip CopyClip(AETimelineAsset asset, StandardClip clip)
        {
            StandardClip newTrack;
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] binaryData;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, clip);
                binaryData = stream.ToArray();
            }

            using (MemoryStream stream = new MemoryStream(binaryData))
            {
                newTrack = formatter.Deserialize(stream) as StandardClip;
            }

            return newTrack;
        }

        public static void RemoveClip(AETimelineAsset asset, string path, int trackIndex, int clipIndex)
        {
            asset.Tracks[trackIndex].Clips.RemoveAt(clipIndex);
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
                    return;
                }

                if (right != -1 && target + asset.Tracks[trackIndex].Clips[clipIndex].Duration > rightStartID)
                {
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
                if(track.Clips.Count == 0) continue;
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
            AETimelineEditorTick.PlayAsset(asset);
            
            //序列化timeline
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] binaryData;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, asset);
                binaryData = stream.ToArray();
            }

            //保存到成为二进制文件
            File.WriteAllBytes(path, binaryData);
        }
    }
}