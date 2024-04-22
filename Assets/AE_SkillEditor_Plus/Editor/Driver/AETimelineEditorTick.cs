using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.Driver
{
    public enum AEPlayableStateEnum
    {
        Exit,
        Running
    }

    public class AEPlayableBehavior
    {
        public AEPlayableStateEnum State { get; protected set; }

        public AEPlayableBehavior(StandardClip clip)
        {
        }

        public void OnEnter()
        {
            State = AEPlayableStateEnum.Running;
        }

        public void Tick()
        {
        }

        public void OnExit()
        {
            State = AEPlayableStateEnum.Exit;
        }
    }

    public static class AETimelineEditorTick
    {
        private static Dictionary<int, Dictionary<int, AEPlayableBehavior>> PlayableTree;
        private static AETimelineAsset m_asset;

        public static void PlayAsset(AETimelineAsset asset)
        {
            m_asset = asset;
            PlayableTree = new Dictionary<int, Dictionary<int, AEPlayableBehavior>>();
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                PlayableTree.Add(trackIndex, new Dictionary<int, AEPlayableBehavior>());
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    PlayableTree[trackIndex].Add(clipIndex, new AEPlayableBehavior(clip));
                }
            }
        }

        public static void Tick(int currentFrameID, int FPS)
        {
            if (PlayableTree == null) return;
            if (currentFrameID > m_asset.Duration) return;
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    var behavior = PlayableTree[trackIndex][clipIndex];
                    if (currentFrameID == clip.StartID && behavior.State == AEPlayableStateEnum.Exit)
                    {
                        Debug.Log(clip.Name + "OnEnter");
                    }
                    else if (currentFrameID > clip.StartID && currentFrameID < clip.StartID + clip.Duration)
                    {
                        (track as ITrackEditorDriver)?.Tick((int)(currentFrameID * FPS), FPS, clip);
                        // Debug.Log(clip.Name + "Playing");
                    }
                    else
                    {
                        Debug.Log(clip.Name + "OnExit");
                    }
                }
            }
        }
    }
}