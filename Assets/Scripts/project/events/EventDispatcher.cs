using System;
using System.Linq;
using System.Collections.Generic;

namespace MrRunner
{
    public static class EventDispatcher
    {
        private static List<EventListener> listeners = new List<EventListener>();

        public static void AddEventListener<EventType, EventTarget>(string eventName, Action<EventType> listener) where EventType : Event<EventTarget>
        {
            listeners.Add(new EventListener(eventName, listener));
        }

        public static void RemoveEventListener<EventType>(string eventName, Action<EventType> listener)
        {
            listeners.RemoveAll
            (
                delegate(EventListener eventListener)
                {
                    return eventListener.eventName == eventName && eventListener.listener.Equals(listener);
                }
            );
        }
        
        public static void DispatchEvent<EventType, TargetType>(EventType e) where EventType : Event<TargetType>
        {
            EventListener[] listeners = GetListeners<EventType>(e.Name);
            for (int i = 0; i < listeners.Length; i++)
            {
                Action<EventType> listener = listeners[i].listener as Action<EventType>;
                if (listener != null)
                {
                    listener(e);
                }
            }
        }

        private static EventListener[] GetListeners<EventType>(string eventName)
        {
            List<EventListener> result = new List<EventListener>();
            var query = from eventListener in listeners 
                        where eventListener.eventName == eventName && eventListener.listener is Action<EventType>
                        select eventListener;
            foreach(EventListener listener in query)
                result.Add(listener);
            return result.ToArray();
        }
    }

    public abstract class Event<TargetType>
    {
        public Event(string name, TargetType target)
        {
            this.name = name;
            this.target = target;
        }
        protected string name;
        public string Name { get => name; }

        protected TargetType target;
        public TargetType Target { get => target; }
    }

    struct EventListener
    {
        public EventListener(string eventName, object listener)
        {
            this.eventName = eventName;
            this.listener = listener;
        }

        public string eventName;
        public object listener;
    }
}

