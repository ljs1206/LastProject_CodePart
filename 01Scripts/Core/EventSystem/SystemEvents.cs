using UnityEngine;

namespace LJS.EventSystem
{
    public static class SystemEvents
    {
        public static readonly FadeScreenEvent FadeScreenEvent = new FadeScreenEvent(); 
        public static readonly FadeComplete FadeComplete = new FadeComplete();
        public static readonly SaveGameEvent SaveGameEvent = new SaveGameEvent();
        public static readonly LoadGameEvent LoadGameEvent = new LoadGameEvent();
    }

    public class FadeScreenEvent : GameEvent
    {
        public bool isFadeIn;
    }
    public class FadeComplete : GameEvent
    {}

    public class SaveGameEvent : GameEvent
    {
        public bool isSaveToFile;
    }

    public class LoadGameEvent : GameEvent
    {
        public bool isLoadFromFile;
    }
}
