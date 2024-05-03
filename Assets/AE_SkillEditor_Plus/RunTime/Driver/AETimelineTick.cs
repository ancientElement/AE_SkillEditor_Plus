using System.Collections.Generic;
using AE_SkillEditor_Plus.RunTime.Interface;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Driver
{
    public class AETimelineTick
    {
        private List<List<AEPlayableBehaviour>> Behaviours;
        private float Timer;
        private int currentFrameID;
        private AETimelineAsset m_asset;
        private float interval;

        public void PlayAsset(AETimelineAsset asset)
        {
            // Debug.Log("VAR");
            Timer = 0;
            currentFrameID = 0;
            m_asset = asset;
            if (asset.FPS == 0)
            {
                Debug.LogError("FPS 不能为0");
                return;
            }

            interval = 1f / m_asset.FPS;
            Behaviours = new List<List<AEPlayableBehaviour>>();
            for (var trackIndex = 0; trackIndex < m_asset.Tracks.Count; trackIndex++)
            {
                var track = m_asset.Tracks[trackIndex];
                Behaviours.Add(new List<AEPlayableBehaviour>());
                for (var clipIndex = 0; clipIndex < track.Clips.Count; clipIndex++)
                {
                    var clip = track.Clips[clipIndex];
                    Behaviours[trackIndex].Add((track as IRuntimeBehaviour)?.CreateRuntimeBehaviour(clip));
                }
            }
        }

        public void Tick(float delta, GameObject context)
        {
            if (Behaviours == null) return;
            if (interval == 0)
            {
                return;
            }

            Timer += delta;
            if (Timer >= interval)
            {
                currentFrameID += 1;
                Timer = 0;
                TickPrivate(m_asset.FPS, context);
                // interval = Time.time - interval;
                // Debug.Log("Tick " + interval);
                // Debug.Log("FPS " + 1f / FPS);
                // interval = Time.time;
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
                    var behavior = Behaviours[trackIndex][clipIndex];
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