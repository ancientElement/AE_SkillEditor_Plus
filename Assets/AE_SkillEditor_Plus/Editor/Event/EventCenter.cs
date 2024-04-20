using System;
using System.Collections.Generic;
using AE_SkillEditor_Plus.Editor.Window;

namespace AE_SkillEditor_Plus.AEUIEvent
{
    public static class EventCenter
    {
        private static Dictionary<AETimelineEditorWindow, Action<BaseEvent>> events;

        static EventCenter()
        {
            events = new Dictionary<AETimelineEditorWindow, Action<BaseEvent>>();
        }

        public static void AddEventListener(AETimelineEditorWindow window, Action<BaseEvent> callback)
        {
            if (events.ContainsKey(window)) events[window] = callback;
            else events.Add(window, callback);
        }

        public static void RemoveEventListener(AETimelineEditorWindow window, Action<BaseEvent> callback)
        {
            if (events.ContainsKey(window)) events.Remove(window);
        }

        public static void TrigerEvent(AETimelineEditorWindow window, BaseEvent baseEvent)
        {
            events[window]?.Invoke(baseEvent);
        }
    }
}