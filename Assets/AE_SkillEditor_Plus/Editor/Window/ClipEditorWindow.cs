using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.Event;
using AE_SkillEditor_Plus.UI;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using EventType = AE_SkillEditor_Plus.Event.EventType;


namespace AE_SkillEditor_Plus
{
    public class ClipEditorWindow : EditorWindow
    {
        #region Static

        public static string Title = "AEClip编辑器";

        //打开窗口
        [MenuItem("Tools/AEClip编辑器")]
        public static void OpenWindiw()
        {
            var window = CreateWindow<ClipEditorWindow>(Title);
        }

        #endregion

        #region Test

        //TODO:测试数据
        private List<TrackStyleData> trackStyleData;

        #endregion

        private int TrackHeight = 30; //轨道高度
        private int WidthPreFrame = 1; //每帧多宽
        private int HeadWidth = 100; //轨道头部宽度
        private int IntervalHeight = 10; //轨道间隔
        private int ControllerHeight = 30; //控件高度

        private int mouseCurrentFrameID //鼠标位置转化为FrameID
            => (int)(UnityEngine.Event.current.mousePosition.x - HeadWidth) / WidthPreFrame;

        private object tempObject;

        //初始化
        private void OnEnable()
        {
            EventCenter.AddEventListener(this, ProcessEvent);

            //TODO:测试数据
            trackStyleData = new List<TrackStyleData>()
            {
                new TrackStyleData()
                {
                    Name = "测试Track1",
                    WidthPreFrame = this.WidthPreFrame,
                    Color = Color.green,
                    Clips = new List<ClipStyleData>()
                    {
                        new ClipStyleData()
                            { Color = Color.red, StartID = 600, EndID = 800, Name = "测试Clip1" },
                        new ClipStyleData()
                            { Color = Color.red, StartID = 150, EndID = 250, Name = "测试Clip2" }
                    }
                },
                new TrackStyleData()
                {
                    Name = "测试Track2",
                    WidthPreFrame = this.WidthPreFrame,
                    Color = Color.green,
                    Clips = new List<ClipStyleData>()
                    {
                        new ClipStyleData()
                            { Color = Color.red, StartID = 550, EndID = 660, Name = "测试Clip1" },
                        new ClipStyleData()
                            { Color = Color.red, StartID = 110, EndID = 450, Name = "测试Clip2" }
                    }
                },
                new TrackStyleData()
                {
                    Name = "测试Track3",
                    WidthPreFrame = this.WidthPreFrame,
                    Color = Color.green,
                    Clips = new List<ClipStyleData>()
                    {
                        new ClipStyleData()
                            { Color = Color.red, StartID = 200, EndID = 400, Name = "测试Clip1" },
                        new ClipStyleData()
                            { Color = Color.red, StartID = 600, EndID = 700, Name = "测试Clip2" }
                    }
                }
            };
        }

        //销毁
        private void OnDestroy()
        {
            EventCenter.RemoveEventListener(this, ProcessEvent);
        }

        //更新UI
        private void OnGUI()
        {
            //遍历轨道
            int height = 0;
            TrackControllerStyle.UpdateUI(new Rect(0, height, position.width, ControllerHeight));
            height += ControllerHeight;
            for (var index = 0; index < trackStyleData.Count; index++)
            {
                var trackData = trackStyleData[index];
                var track = new TrackStyle();
                //为轨道划分Rect
                var rect = new Rect(0, height, position.width, TrackHeight);
                track.UpdateUI(this, rect, HeadWidth, trackData, index);
                height += TrackHeight + IntervalHeight;
            }
        }

        //处理事件
        private void ProcessEvent(BaseEvent baseEvent)
        {
            switch (baseEvent.EventType)
            {
                //Clip移动事件
                case EventType.Move:
                {
                    ClipMove((MoveEvent)baseEvent);
                    break;
                }
                case EventType.Resize:
                {
                    ClipResize((ResizeEvent)baseEvent);
                    break;
                }
                case EventType.Keyborad:
                {
                    ProcessKeyborad((KeyboradEvent)baseEvent);
                    break;
                }
            }
        }

        //Clip大小改变
        private void ClipResize(ResizeEvent resize)
        {
            trackStyleData[resize.TrackIndex].Clips[resize.ClipIndex].EndID =
                (int)(mouseCurrentFrameID - (resize.OffsetMouseX / WidthPreFrame));
            Repaint();
        }

        //Clip移动事件
        private void ClipMove(MoveEvent move)
        {
            int originStart = trackStyleData[move.TrackIndex].Clips[move.ClipIndex].StartID;
            trackStyleData[move.TrackIndex].Clips[move.ClipIndex].StartID =
                (int)(mouseCurrentFrameID - (move.OffsetMouseX / WidthPreFrame));
            trackStyleData[move.TrackIndex].Clips[move.ClipIndex].EndID =
                trackStyleData[move.TrackIndex].Clips[move.ClipIndex].StartID +
                trackStyleData[move.TrackIndex].Clips[move.ClipIndex].EndID - originStart;
            Repaint();
        }

        //处理按键事件
        public void ProcessKeyborad(KeyboradEvent keyborad)
        {
            Debug.Log(keyborad.Shortcut + "--" + keyborad.TrackIndex + "--" + keyborad.ClipIndex);
            switch (keyborad.Shortcut)
            {
                case Shortcut.CtrlC:
                    tempObject = new ClipStyleData()
                    {
                        StartID  = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].StartID,
                        EndID  = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].EndID,
                        Name = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].Name,
                        Color = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].Color
                    };
                    break;
                case Shortcut.CtrlV:
                    AddClip(trackStyleData,keyborad.TrackIndex,tempObject);
                    var newTemp = new ClipStyleData()
                    {
                        StartID = (tempObject as ClipStyleData).StartID,
                        EndID  = (tempObject as ClipStyleData).EndID,
                        Name = (tempObject as ClipStyleData).Name,
                        Color = (tempObject as ClipStyleData).Color
                    };
                    tempObject = newTemp;
                    break;
                case Shortcut.CtrlX:
                    tempObject = new ClipStyleData()
                    {
                        StartID  = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].StartID,
                        EndID  = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].EndID,
                        Name = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].Name,
                        Color = trackStyleData[keyborad.TrackIndex].Clips[keyborad.ClipIndex].Color
                    };
                    RemoveClip(trackStyleData, keyborad.TrackIndex, keyborad.ClipIndex);
                    break;
            }
        }

        //TODO:测试数据
        private void AddClip(List<TrackStyleData> data, int trackIndex,object clip)
        {
            var clipData = (clip as ClipStyleData);
            //这里必须先得到length
            int length = clipData.EndID - clipData.StartID;
            clipData.StartID = mouseCurrentFrameID;
            clipData.EndID = mouseCurrentFrameID + length;
            data[trackIndex].Clips.Add(clipData);
            Repaint();
        }

        //TODO:测试数据
        private void RemoveClip(List<TrackStyleData> data, int trackIndex, int clipIndex)
        {
            data[trackIndex].Clips.RemoveAt(clipIndex);
        }
    }
}