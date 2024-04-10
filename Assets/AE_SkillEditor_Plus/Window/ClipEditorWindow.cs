using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.UI;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEngine;

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

        private int TrackHeight = 50; //轨道高度
        private int WidthPreFrame = 1; //每帧多宽
        private int HeadWidth = 100; //轨道头部宽度
        private int IntervalHeight = 10; //轨道间隔
        private int ControllerHeight = 30; //控件高度

        //更新UI
        private void OnGUI()
        {
            //测试数据
            var trackDatas = new List<TrackStyleData>()
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
            
            //遍历轨道
            int height = 0;
            TrackControllerStyle.UpdateUI(new Rect(0, height, position.width, ControllerHeight));
            height += ControllerHeight;
            foreach (var trackData in trackDatas)
            {
                var track = new TrackStyle();
                //为轨道划分Rect
                var rect = new Rect(0, height, position.width, TrackHeight);
                track.UpdateUI(rect, HeadWidth,trackData);
                height += TrackHeight+IntervalHeight;
            }
        }
    }
}