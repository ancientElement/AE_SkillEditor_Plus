using System;
using System.Collections.Generic;

namespace AE_SkillEditor_Plus.Event
{
    public static class EventCenter
    {
        private static Dictionary<ClipEditorWindow, Action<BaseEvent>> events;

        static EventCenter()
        {
            events = new Dictionary<ClipEditorWindow, Action<BaseEvent>>();
        }

        public static void AddEventListener(ClipEditorWindow window, Action<BaseEvent> callback)
        {
            if (events.ContainsKey(window)) events[window] = callback;
            else events.Add(window, callback);
        }

        public static void RemoveEventListener(ClipEditorWindow window, Action<BaseEvent> callback)
        {
            if (events.ContainsKey(window)) events.Remove(window);
        }

        public static void TrigerEvent(ClipEditorWindow window, BaseEvent baseEvent)
        {
            events[window]?.Invoke(baseEvent);
        }
    }
}