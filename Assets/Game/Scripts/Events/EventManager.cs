using System;
using System.Collections.Generic;
using UnityEngine;

namespace TogoEvents
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<EventsType, Action<Dictionary<MsgType, object>>> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();

                        //  Sets this to not be destroyed when reloading scene
                        DontDestroyOnLoad(eventManager);
                    }
                }
                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<EventsType, Action<Dictionary<MsgType, object>>>();
            }
        }

        public static void StartListening(EventsType eventName, Action<Dictionary<MsgType, object>> listener)
        {
            Action<Dictionary<MsgType, object>> thisEvent;

            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(EventsType eventName, Action<Dictionary<MsgType, object>> listener)
        {
            if (eventManager == null) return;
            Action<Dictionary<MsgType, object>> thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        // example
        // EventManager.TriggerEvent(EventsCol.AddCoints,
        //    new Dictionary<MsgType, object> { { MsgColl.Amount, 1 } });
        public static void TriggerEvent(EventsType eventName, Dictionary<MsgType, object> message)
        {
            Action<Dictionary<MsgType, object>> thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }

        // example
        // TogoEvents.EventManager.TriggerEvent(EventsCol.PlaySound, MsgColl.BtnSound);
        public static void TriggerEvent(EventsType eventName, MsgType msgType, object obj = null)
        {
            TogoEvents.EventManager.TriggerEvent(eventName,
                new Dictionary<MsgType, object> { { msgType, obj } });            
        }        
    }
}