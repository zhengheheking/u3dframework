using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Client.Base
{
   
    public delegate void EventHandler(string evt, params object[] args);
    public class EventDispatcher
    {
        private SortedList<string, HashSet<EventHandler>> m_AllHandler;

        public EventDispatcher()
        {
            m_AllHandler = new SortedList<string, HashSet<EventHandler>>();
        }


        public void addEventListener(EventHandler handler, params string[] events)
        {
            foreach (string evt in events)
            {
                HashSet<EventHandler> handlerSet = null;
                if (!m_AllHandler.TryGetValue(evt, out handlerSet))
                {
                    handlerSet = new HashSet<EventHandler>();
                    m_AllHandler.Add(evt, handlerSet);
                }
                handlerSet.Add(handler);
            }
        }

        public void removeEventListener(string evt, EventHandler handler)
        {
            HashSet<EventHandler> handlerSet = null;
            if (!m_AllHandler.TryGetValue(evt, out handlerSet))
            {
                return;
            }
            handlerSet.Remove(handler);
        }

        public void ClearEventListener(string evt)
        {
            HashSet<EventHandler> handlerSet = null;
            if (!m_AllHandler.TryGetValue(evt, out handlerSet))
            {
                return;
            }
            handlerSet.Clear();
        }

        public void ClearAllEventListener()
        {
            foreach (string evt in m_AllHandler.Keys)
            {
                ClearEventListener(evt);
            }
        }

        public void dispatchEvent(string evt, params object[] args)
        {
            HashSet<EventHandler> handlerSet = null;
            if (!m_AllHandler.TryGetValue(evt, out handlerSet))
            {
                return;
            }
            foreach (EventHandler handler in handlerSet)
            {
                handler(evt, args);
            }
        }
        public bool hasEventListener(string evt)
        {
            HashSet<EventHandler> handlerSet = null;
            if (!m_AllHandler.TryGetValue(evt, out handlerSet))
            {
                return false;
            }
            return handlerSet.Count > 0;
        }
    }

}
