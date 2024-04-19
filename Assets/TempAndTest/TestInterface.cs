using System;

namespace AE_SkillEditor_Plus.TempAndTest
{
    public interface TestInterface
    {
        public Action<int> EditorTick { get; }
        public Action<int> RuntimeTick { get; }
    }

    public static class Dirver
    {
        public static void Tick(int cur)
        {
        }
    }

    public class TestTrack : TestInterface
    {
        public Action<int> EditorTick => Dirver.Tick;
        public Action<int> RuntimeTick => Dirver.Tick;
    }
}