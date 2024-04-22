using AE_SkillEditor_Plus.RunTime.Interface;

namespace AE_SkillEditor_Plus.RunTime.Driver
{
    public class AETimelineTick
    {
        private AETimelineAsset asset;
        private float Timer;
        public int FPS = 60;

        public void PlayAsset(AETimelineAsset asset)
        {
            this.asset = asset;
            Timer = 0;
        }

        public void Tick(float delta)
        {
            Timer += delta;
            int currentFrame = (int)(Timer * FPS);
            if (asset == null) return;
            if (currentFrame > asset.Duration) return;
            foreach (var track in asset.Tracks)
            {
                foreach (var clip in track.Clips)
                {
                    if (currentFrame > clip.StartID && currentFrame < clip.StartID + clip.Duration)
                    {
                        (track as ITrackRuntimeDriver)?.Tick(currentFrame, FPS, clip);
                    }
                }
            }
        }
    }
}