namespace LJS.EventSystem
{
    public static class UIEvents
    {
        public static readonly OpenMenuEvent OpenMenu = new OpenMenuEvent();
    }

    public class OpenMenuEvent : GameEvent
    { }
}
