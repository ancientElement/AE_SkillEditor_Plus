using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.Driver
{
    public class AETimelineEditorTick
    {
        private List<List<AEPlayableBehaviour>> Behaviors;
        private AETimelineAsset m_asset;

        public void PlayAsset(AETimelineAsset asset)
        {
            // Debug.Log("VAR");
            m_asset = asset;
            Behaviors = new List<List<AEPlayableBehaviour>>();
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                Behaviors.Add(new List<AEPlayableBehaviour>());
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    Behaviors[trackIndex].Add((track as IEditorBehaviour)?.CreateEditorBehaviour(clip));
                }
            }
        }

        public void Tick(int currentFrameID, int FPS, GameObject context)
        {
            if (Behaviors == null) return;
            if (currentFrameID > m_asset.Duration) return;
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    var behavior = Behaviors[trackIndex][clipIndex];
                    if (behavior == null) break;
                    if (currentFrameID >= clip.StartID && currentFrameID < clip.StartID + clip.Duration)
                    {
                        if (behavior.State == AEPlayableStateEnum.Exit)
                        {
                            // Debug.Log(clip.Name + "OnEnter");
                            behavior.OnEnter(context, FPS, currentFrameID - clip.StartID);
                        }

                        if (behavior.State == AEPlayableStateEnum.Running)
                            behavior.Tick(currentFrameID - clip.StartID, FPS, context);
                        // (track as ITrackEditorDriver)?.Tick((int)(currentFrameID * FPS), FPS, clip);
                        // Debug.Log(clip.Name + "Playing" + currentFrameID);
                    }
                    else if ((currentFrameID < clip.StartID || currentFrameID >= clip.StartID + clip.Duration) &&
                             behavior.State == AEPlayableStateEnum.Running)
                    {
                        behavior.OnExit(context, FPS, currentFrameID - clip.StartID);
                    }
                }
            }
        }

        public void AddBehaviour(int trackIndex, int clipIndex)
        {
            var track = m_asset.Tracks[trackIndex];
            var clip = track.Clips[clipIndex];
            Behaviors[trackIndex].Insert(clipIndex, (track as IEditorBehaviour)?.CreateEditorBehaviour(clip));
        }

        public void RemoveBehaviour(int trackIndex, int clipIndex)
        {
            Behaviors[trackIndex].RemoveAt(clipIndex);
        }

        public void AddTrack(int trackIndex)
        {
            Behaviors.Insert(trackIndex, new List<AEPlayableBehaviour>());
        }

        public void RemoveTrack(int trackIndex)
        {
            Behaviors.RemoveAt(trackIndex);
        }
    }
}