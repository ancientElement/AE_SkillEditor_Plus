﻿using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using AE_SkillEditor_Plus.RunTime.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace AE_SkillEditor_Plus.Editor.Driver
{
    internal static class AETimelineEditorTick
    {
        private static List<List<AEPlayableBehavior>> Behaviors;
        private static AETimelineAsset m_asset;

        public static void PlayAsset(AETimelineAsset asset)
        {
            Debug.Log("VAR");
            m_asset = asset;
            Behaviors = new List<List<AEPlayableBehavior>>();
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                Behaviors.Add(new List<AEPlayableBehavior>());
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    Behaviors[trackIndex].Add((track as IEditorBehaviour)?.CreateEditorBehaviour(clip));
                }
            }
        }

        public static void Tick(int currentFrameID, int FPS)
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
                            behavior.OnEnter();
                        }

                        behavior.Tick(currentFrameID - clip.StartID, FPS);
                        // (track as ITrackEditorDriver)?.Tick((int)(currentFrameID * FPS), FPS, clip);
                        // Debug.Log(clip.Name + "Playing" + currentFrameID);
                    }
                    else if ((currentFrameID < clip.StartID || currentFrameID >= clip.StartID + clip.Duration) &&
                             behavior.State == AEPlayableStateEnum.Running)
                    {
                        behavior.OnExit();
                    }
                }
            }
        }
    }
}