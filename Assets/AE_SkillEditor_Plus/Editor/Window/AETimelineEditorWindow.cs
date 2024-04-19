using System;
using System.Collections.Generic;
using System.Reflection;
using AE_SkillEditor_Plus.Editor.UI.Controller;
using AE_SkillEditor_Plus.Event;
using AE_SkillEditor_Plus.Factory;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Attribute;
using AE_SkillEditor_Plus.UI;
using AE_SkillEditor_Plus.UI.Data;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;
using EventType = AE_SkillEditor_Plus.Event.EventType;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

namespace AE_SkillEditor_Plus.Editor.Window
{
    public class AETimelineEditorWindow : EditorWindow
    {
        #region Static

        public static string Title = "AETimeline编辑器";

        //打开窗口
        [MenuItem("Tools/AETimeline编辑器")]
        public static void OpenWindiw()
        {
            var window = CreateWindow<AETimelineEditorWindow>(Title);
        }

        #endregion

        private int TrackHeight = 30; //轨道高度
        private float widthPreFrame = 1f;

        private float WidthPreFrame
        {
            get { return widthPreFrame; }
            set
            {
                if ((value) <= 0.005f) widthPreFrame = 0.005f;
                else if (value >= 194f) widthPreFrame = 194f;
                else widthPreFrame = value;
            }
        } //每帧多宽

        private int HeadWidth = 250; //轨道头部宽度
        private int IntervalHeight = 10; //轨道间隔
        private int ControllerHeight = 38; //控件高度
        private int currentFrameID;

        private int CurrentFrameID
        {
            get { return currentFrameID; }
            set
            {
                if (value < 0) return;
                if (currentFrameID == value) return;
                currentFrameID = value;
                CurrentFrameIDChange();
            }
        } //当前帧

        private StandardClip tempObject; //临时的一个变量 保存CtrlC的数据

        public int MouseCurrentFrameID //鼠标位置转化为FrameID
            => (int)((UnityEngine.Event.current.mousePosition.x - HeadWidth) / WidthPreFrame);

        private int[] HighLight = { -1, -1 }; //选中的Clip

        private bool IsPlaying; //是否在播放
        private int FPS = 60; //帧率
        private float OneFrameTimer; //一帧时长的计时器

        private AETimelineAsset Asset;
        private string assetPath = "";

        public string AssetPath
        {
            get { return assetPath; }
            set
            {
                if (value == "") return;
                assetPath = value;
                Asset = AETimelineFactory.LoadTimeLineAsset(assetPath);
            }
        }

        private Vector2 ScrollPosHead;
        private Vector2 ScrollPosBody;
        private Vector2 ScrollPosTimeline;

        //初始化
        private void OnEnable()
        {
            EventCenter.AddEventListener(this, ProcessEvent);
            if (AssetPath != "") Asset = AETimelineFactory.LoadTimeLineAsset(AssetPath);
        }

        //销毁
        private void OnDestroy()
        {
            EventCenter.RemoveEventListener(this, ProcessEvent);
        }

        //获取UI参数
        private List<TrackStyleData> GetTrackStyleFromData()
        {
            if (Asset == null) return new List<TrackStyleData>();
            var trackStyles = new List<TrackStyleData>();

            for (var i = 0; i < Asset.Tracks.Count; i++)
            {
                var trackData = Asset.Tracks[i];
                var trackStyle = new TrackStyleData();
                trackStyle.Clips = new List<ClipStyleData>();
                string name = trackData.GetType().GetCustomAttribute<AETrackNameAttribute>().Name;
                trackStyle.Name = name;
                var colorAttribute = trackData.GetType().GetCustomAttribute<AETrackColorAttribute>();
                trackStyle.Color = colorAttribute == null
                    ? new Color(251f / 255, 0f / 255, 253f / 255)
                    : colorAttribute.Color;
                for (var j = 0; j < trackData.Clips.Count; j++)
                {
                    var clip = trackData.Clips[j];
                    trackStyle.Clips.Add(new ClipStyleData()
                    {
                        Name = clip.Name,
                        StartID = clip.StartID,
                        EndID = clip.StartID + clip.Duration,
                    });
                }

                trackStyles.Add(trackStyle);
            }

            return trackStyles;
        }

        //更新UI
        private void OnGUI()
        {
            var styleData = GetTrackStyleFromData();
            //划分左右
            var leftRect = new Rect(0, 0, HeadWidth, position.height);
            var rightRect = new Rect(HeadWidth, 0, position.width - HeadWidth, position.height);
            //划分控件与轨道头
            var controllerRect = new Rect(leftRect.x, leftRect.y, leftRect.width, ControllerHeight);
            var trackHeadRect = new Rect(leftRect.x, 10 + leftRect.y + ControllerHeight, leftRect.width,
                leftRect.height - ControllerHeight-10);
            //划分时间轴与轨道体
            var timelineRect = new Rect(rightRect.x, rightRect.y, rightRect.width, ControllerHeight);
            var trackbodyRect = new Rect(rightRect.x, 10 + rightRect.y + ControllerHeight, rightRect.width,
                rightRect.height - ControllerHeight - 10);
            // EditorGUI.DrawRect(controllerRect,
            //     new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            // EditorGUI.DrawRect(trackHeadRect,
            //     new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            // EditorGUI.DrawRect(timelineRect,
            //     new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            // EditorGUI.DrawRect(trackbodyRect,
            //     new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));


            //轨道头
            ScrollPosHead = GUI.BeginScrollView(trackHeadRect,
                ScrollPosHead, new Rect(trackHeadRect.x, trackHeadRect.y, trackHeadRect.width, 1000f));
            ScrollPosBody.y = ScrollPosHead.y;
            float height = trackHeadRect.y;
            for (var index = 0; index < styleData.Count; index++)
            {
                var trackData = styleData[index];
                var track = new TrackStyle();
                //为轨道划分Rect
                var rectHead = new Rect(trackHeadRect.x, height, trackHeadRect.width, TrackHeight);
                TrackHeadStyle.UpdateUI(this, rectHead, trackData, index);
                height += TrackHeight + IntervalHeight;
            }

            GUI.EndScrollView();

            //轨道体
            ScrollPosBody = GUI.BeginScrollView(trackbodyRect,
                ScrollPosBody, new Rect(trackbodyRect.x, trackbodyRect.y, 10000f, 1000f));
            ScrollPosTimeline.x = ScrollPosBody.x;
            Debug.Log(ScrollPosBody);
            height = trackbodyRect.y;
            for (var index = 0; index < styleData.Count; index++)
            {
                var trackData = styleData[index];
                var track = new TrackStyle();
                //为轨道划分Rect
                var rectBody = new Rect(trackbodyRect.x, height, trackbodyRect.width, TrackHeight);
                TrackBodyStyle.UpdateUI(this, rectBody, HighLight, widthPreFrame, trackData, index);
                height += TrackHeight + IntervalHeight;
            }

            GUI.EndScrollView();

            //控件
            Controller.UpdateGUI(this, controllerRect);
            //timeline
            TimeLine.UpdateGUI(this, CurrentFrameID, WidthPreFrame, timelineRect);

            // //事件添加轨道
            // var leftReft = new Rect(0, ControllerHeight, HeadWidth, position.height);
            // if (leftReft.Contains(UnityEngine.Event.current.mousePosition) &&
            //     UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown &&
            //     UnityEngine.Event.current.button == 1)
            // {
            //     GenericMenu menu = new GenericMenu();
            //     // 添加菜单项
            //     //拿到所有StandardTrack的子类型
            //     Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //     //遍历子类型得到菜单
            //     foreach (Assembly assembly in assemblies)
            //     {
            //         foreach (Type type in assembly.GetTypes())
            //         {
            //             if (type.IsSubclassOf(typeof(StandardTrack)))
            //             {
            //                 var obj = Activator.CreateInstance(type) as StandardTrack;
            //                 menu.AddItem(new GUIContent("添加轨道/" + type.Name), false,
            //                     () => { AETimelineFactory.CreatTrack(Asset, AssetPath, Asset.Tracks.Count, obj); });
            //             }
            //         }
            //     }
            //
            //     // 显示菜单
            //     menu.ShowAsContext();
            // }
            //
            // //遍历轨道
            // int height = 0;
            // height += ControllerHeight;
            // height += 10;
            // for (var index = 0; index < styleData.Count; index++)
            // {
            //     var trackData = styleData[index];
            //     var track = new TrackStyle();
            //     //为轨道划分Rect
            //     var rect = new Rect(0, height, position.width, TrackHeight);
            //     // track.UpdateUI(this, rect, HighLight, WidthPreFrame, HeadWidth, trackData, index);
            //     height += TrackHeight + IntervalHeight;
            // }
            //
            // //划分控件 Timeline
            // var controllerRect = new Rect(0, 0, HeadWidth - 10f, ControllerHeight);
            // var timelineRect = new Rect(HeadWidth, 2.5f, position.width - HeadWidth, ControllerHeight - 2.5f);
            // //绘制Timeline
            // TimeLine.UpdateGUI(this, CurrentFrameID, WidthPreFrame, timelineRect);
            // //绘制控件
            // Controller.UpdateGUI(this, controllerRect);
        }

        private void Update()
        {
            if (!IsPlaying) return;
            OneFrameTimer -= 1f / FPS;
            if (OneFrameTimer <= 0)
            {
                OneFrameTimer = 1f / FPS;
                Tick(CurrentFrameID);
                CurrentFrameID += 1;
            }
        }

        private void Tick(int frameID)
        {
            // Debug.Log(currentFrameID);
        }

        #region 事件处理

        //处理事件
        private void ProcessEvent(BaseEvent baseEvent)
        {
            switch (baseEvent.EventType)
            {
                //Clip事件
                case EventType.ClipMove:
                {
                    ClipMove((ClipMoveEvent)baseEvent);
                    break;
                }
                case EventType.ClipResize:
                {
                    ClipResize((ClipResizeEvent)baseEvent);
                    break;
                }
                case EventType.ClipKeyborad:
                {
                    ProcessKeyborad((KeyboradEvent)baseEvent);
                    break;
                }
                case EventType.ClipClick:
                    ClipClick((ClipClickEvent)baseEvent);
                    break;
                case EventType.ClipRightClick:
                    ClipRightClick((ClipRightClickEvent)baseEvent);
                    break;
                case EventType.ClipResizeEnd:
                case EventType.ClipMoveEnd:
                    AETimelineFactory.Save(Asset, AssetPath);
                    break;
                //控件事件
                case EventType.Controller:
                {
                    ProcessController((ControllerEvent)baseEvent);
                    break;
                }
                //Timeline
                case EventType.TimelineScale:
                {
                    ProcessTimelineScale((TimelineScaleEvent)baseEvent);
                    break;
                }
                case EventType.TimelineDrag:
                {
                    ProcessTimelineDrag((TimelineDragEvent)baseEvent);
                    break;
                }
                //右键轨道头部
                case EventType.HeadRightClick:
                    ProcessHeadRightClick((HeadRightClickEvent)baseEvent);
                    break;
                //右键轨道体
                case EventType.BodyRightClick:
                    ProcessBodyRightClick((BodyRightClick)baseEvent);
                    break;
            }
        }

        //当前帧改变事件
        private void CurrentFrameIDChange()
        {
            Tick(CurrentFrameID);
            Repaint();
        }

        //在时间轴上拖动
        private void ProcessTimelineDrag(TimelineDragEvent dragEvent)
        {
            CurrentFrameID = MouseCurrentFrameID;
        }

        //时轴缩放
        private void ProcessTimelineScale(TimelineScaleEvent scaleEvent)
        {
            // Debug.Log(scaleEvent.EventType + "  " + UnityEngine.Event.current.delta.y);
            WidthPreFrame -= Mathf.Sign(UnityEngine.Event.current.delta.y) * WidthPreFrame * 0.2f;
            // Debug.Log(WidthPreFrame);
            //仅仅是+=1的话没有卡死
            Repaint();
        }

        //控件事件
        private void ProcessController(ControllerEvent controller)
        {
            switch (controller.ControllerType)
            {
                case ControllerType.ToMostBegin:
                    CurrentFrameID = 0;
                    break;
                case ControllerType.ToPre:
                    CurrentFrameID -= 1;
                    break;
                case ControllerType.Play:
                    IsPlaying = !IsPlaying;
                    break;
                case ControllerType.ToNext:
                    CurrentFrameID += 1;
                    break;
                case ControllerType.ToMostEnd:
                    //TODO:未完成
                    CurrentFrameID += 1;
                    break;
            }
        }

        //Clip选中
        private void ClipClick(ClipClickEvent click)
        {
            // Debug.Log("选中 " + click.TrackIndex + " " + click.ClipIndex);
            HighLight[0] = click.TrackIndex;
            HighLight[1] = click.ClipIndex;
            Repaint();
        }

        //Clip右键
        private void ClipRightClick(ClipRightClickEvent click)
        {
            Debug.Log("Clip右键 " + click.TrackIndex + " " + click.ClipIndex);
        }

        //Clip大小改变
        private void ClipResize(ClipResizeEvent clipResize)
        {
            AETimelineFactory.ResizeClip(Asset, clipResize.TrackIndex, clipResize.ClipIndex, (int)(MouseCurrentFrameID -
                (clipResize.OffsetMouseX / WidthPreFrame) -
                Asset.Tracks[clipResize.TrackIndex].Clips[clipResize.ClipIndex].StartID));
            Repaint();
        }

        //Clip移动事件
        private void ClipMove(ClipMoveEvent clipMove)
        {
            AETimelineFactory.MoveClip(Asset, AssetPath, clipMove.TrackIndex, clipMove.ClipIndex,
                (int)(MouseCurrentFrameID - (clipMove.OffsetMouseX / WidthPreFrame)));
            Repaint();
        }

        //处理按键事件
        public void ProcessKeyborad(KeyboradEvent keyborad)
        {
            // Debug.Log(keyborad.Shortcut + "--" + keyborad.TrackIndex + "--" + keyborad.ClipIndex);
            switch (keyborad.Shortcut)
            {
                case Shortcut.CtrlC:
                    tempObject = AETimelineFactory.CopyClip(Asset, keyborad.TrackIndex, keyborad.ClipIndex);
                    break;
                case Shortcut.CtrlV:
                    var temp = AETimelineFactory.CopyClip(Asset, tempObject);
                    AETimelineFactory.AddClip(Asset, AssetPath, keyborad.TrackIndex, MouseCurrentFrameID, temp);
                    break;
                case Shortcut.CtrlX:
                    tempObject = AETimelineFactory.CopyClip(Asset, keyborad.TrackIndex, keyborad.ClipIndex);
                    AETimelineFactory.RemoveClip(Asset, AssetPath, keyborad.TrackIndex, keyborad.ClipIndex);
                    HighLight[0] = -1;
                    HighLight[1] = -1;
                    break;
                case Shortcut.Delete:
                    AETimelineFactory.RemoveClip(Asset, AssetPath, keyborad.TrackIndex, keyborad.ClipIndex);
                    HighLight[0] = -1;
                    HighLight[1] = -1;
                    break;
            }

            Repaint();
        }

        //右键轨道头部
        private void ProcessHeadRightClick(HeadRightClickEvent click)
        {
            GenericMenu menu = new GenericMenu();
            // 添加菜单项
            //拿到所有StandardTrack的子类型
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //遍历子类型得到菜单
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(StandardTrack)))
                    {
                        var obj = Activator.CreateInstance(type) as StandardTrack;
                        menu.AddItem(new GUIContent("添加轨道/" + type.Name), false,
                            () => { AETimelineFactory.CreatTrack(Asset, AssetPath, click.TrackIndex, obj); });
                    }
                }
            }

            menu.AddItem(new GUIContent("删除轨道"), false,
                () =>
                {
                    AETimelineFactory.RemoveTrack(Asset, AssetPath, click.TrackIndex);
                    HighLight[0] = -1;
                    HighLight[1] = -1;
                });
            // 显示菜单
            menu.ShowAsContext();
        }

        //右键轨道体
        private void ProcessBodyRightClick(BodyRightClick click)
        {
            GenericMenu menu = new GenericMenu();
            // 添加菜单项
            menu.AddItem(new GUIContent("添加Clip"), false,
                () => { AETimelineFactory.AddClip(Asset, AssetPath, click.TrackIndex, click.MouseFrameID); });
            // 显示菜单
            menu.ShowAsContext();
        }

        #endregion
    }
}