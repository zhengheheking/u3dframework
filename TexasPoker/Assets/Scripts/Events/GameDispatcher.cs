namespace Events
{
    using Client.Base;
    public class GameDispatcher : Singleton<GameDispatcher>
    {
        private EventDispatcher eventDispatcher;

        public GameDispatcher()
        {
            eventDispatcher = new EventDispatcher();
        }

        public void addEventListener(EventHandler handler, params string[] events)
        {
            eventDispatcher.addEventListener(handler, events);
        }

        public void removeEventListener(string evt, EventHandler handler)
        {
            eventDispatcher.removeEventListener(evt, handler);
        }

        public void ClearEventListener(string evt)
        {
            eventDispatcher.ClearEventListener(evt);
        }

        public void ClearAllEventListener()
        {
            eventDispatcher.ClearAllEventListener();
        }

        public void dispatchEvent(string evt, params object[] args)
        {
            eventDispatcher.dispatchEvent(evt, args);
        }

        public bool hasEventListener(string evt)
        {
            return eventDispatcher.hasEventListener(evt);
        }
    }
}