using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Driver
{
    public class AETimelineTick
    {
        private List<List<AEPlayableBehaviour>> Behaviors;
        private float Timer;
        private int currentFrameID;
        private AETimelineAsset m_asset;

        public void PlayAsset(AETimelineAsset asset)
        {
            // Debug.Log("VAR");
            Timer = 0;
            currentFrameID = 0;
            m_asset = asset;
            Behaviors = new List<List<AEPlayableBehaviour>>();
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                Behaviors.Add(new List<AEPlayableBehaviour>());
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    Behaviors[trackIndex].Add((track as IRuntimeBehaviour)?.CreateRuntimeBehaviour(clip));
                }
            }
        }

        public void Tick(float delta, int FPS, GameObject context)
        {
            if (Behaviors == null) return;
            Timer += delta;
            if (Timer >= 1 / FPS)
            {
                currentFrameID += 1;
                Timer = 0;
                TickPrivate(FPS, context);
            }
        }

        private void TickPrivate(int FPS, GameObject context)
        {
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
                            behavior.OnEnter(context, currentFrameID);
                        }

                        behavior.Tick(currentFrameID - clip.StartID, FPS, context);
                        // (track as ITrackEditorDriver)?.Tick((int)(currentFrameID * FPS), FPS, clip);
                        // Debug.Log(clip.Name + "Playing" + currentFrameID);
                    }
                    else if ((currentFrameID < clip.StartID || currentFrameID >= clip.StartID + clip.Duration) &&
                             behavior.State == AEPlayableStateEnum.Running)
                    {
                        behavior.OnExit(context, currentFrameID);
                    }
                }
            }
        }
    }
}