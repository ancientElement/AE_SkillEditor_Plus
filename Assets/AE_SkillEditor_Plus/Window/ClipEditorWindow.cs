using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.Event;
using AE_SkillEditor_Plus.UI;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEngine;
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

        //测试数据
        private List<TrackStyleData> trackStyleData;

        #endregion

        private int TrackHeight = 50; //轨道高度
        private int WidthPreFrame = 1; //每帧多宽
        private int HeadWidth = 100; //轨道头部宽度
        private int IntervalHeight = 10; //轨道间隔
        private int ControllerHeight = 30; //控件高度

        //初始化
        private void CreateGUI()
        {
            EventCenter.AddEventListener(this, ProcessEvent);

            //测试数据
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
            }
        }

        //Clip移动事件
        private void ClipMove(MoveEvent move)
        {
            // Debug.Log(move.MouseStart + "--" + move.MouseCurrent + "--" + move.TrackIndex + "--" + move.ClipIndex);
            trackStyleData[move.TrackIndex].Clips[move.ClipIndex].StartID +=
                (int)move.MouseDeltaX/ WidthPreFrame;
            trackStyleData[move.TrackIndex].Clips[move.ClipIndex].EndID +=
                (int)move.MouseDeltaX / WidthPreFrame;
            Repaint();
        }
    }
}