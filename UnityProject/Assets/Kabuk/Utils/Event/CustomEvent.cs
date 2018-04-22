using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Kabuk
{

    public abstract class CustomEvent<T> : CustomEventBase<T> where T : CustomEvent<T>
    {
        public delegate IEnumerator EventHandler();

        List<EventHandler> listeners = new List<EventHandler>();
        event System.Action Listeners;

        public static void Fire()
        {
            int length = Instance.listeners.Count;
            for (int i = length - 1; i >= 0; i --)
            {
                Instance.StartCoroutine(Instance.listeners[i]());
            }
            if(Instance.Listeners != null)
            {
                Instance.Listeners();
            }
        }

        public static IEnumerator FireAndWait()
        {
            int length = Instance.listeners.Count;
            WaitAll wait = new WaitAll(Instance);
            for (int i = length - 1; i >= 0; i--)
            {
                wait.Add(Instance.listeners[i]());
            }

            if (Instance.Listeners != null)
            {
                Instance.Listeners();
            }

            yield return wait;
            
        }


        public static void RegisterCoroutine(EventHandler observer)
        {
            Instance.listeners.Add(observer);
        }

        public static void Register(Action observer)
        {
            Instance.Listeners += observer;
        }

        public static void UnregisterCoroutine(EventHandler observer)
        {
            if (instance == null)
            {
                return;
            }
            instance.listeners.Remove(observer);
        }

        public static void Unregister(System.Action observer)
        {
            if(instance == null)
            {
                return;
            }
            instance.Listeners -= observer;
        }

        public class WaitForEvent : CustomYieldInstruction
        {
            bool wait = true;
            public WaitForEvent()
            {
                Register(OnEventHappened);
            }

            public void OnEventHappened()
            {
                wait = false;
                Unregister(OnEventHappened);
            }

            public override bool keepWaiting
            {
                get
                {
                    return wait;
                }
            }
        }
    }

    public abstract class CustomEvent<T, P> : CustomEventBase<T> where T : CustomEvent<T, P>
    {
        public delegate IEnumerator EventHandlerWP(P par);

        List<EventHandlerWP> listeners = new List<EventHandlerWP>();
        event System.Action<P> Listeners;

        [SerializeField]
        protected P parameters;

        public static void Fire(P par)
        {
            int length = Instance.listeners.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                Instance.StartCoroutine(Instance.listeners[i](par));
            }
            if (Instance.Listeners != null)
            {
                Instance.Listeners(par);
            }
        }

        public static IEnumerator FireAndWait(P par)
        {
            int length = Instance.listeners.Count;
            WaitAll wait = new WaitAll(Instance);
            for (int i = length - 1; i >= 0; i--)
            {
                wait.Add(Instance.listeners[i](par));
            }
            if (Instance.Listeners != null)
            {
                Instance.Listeners(par);
            }

            yield return wait;
        }

        public static void RegisterCoroutine(EventHandlerWP observer)
        {
            Instance.listeners.Add(observer);
        }

        public static void Register(Action<P> observer)
        {
            Instance.Listeners += observer;
        }

        public static void UnregisterCoroutine(EventHandlerWP observer)
        {
            if (instance == null)
            {
                return;
            }
            instance.listeners.Remove(observer);
        }

        public static void Unregister(Action<P> observer)
        {
            if(instance == null)
            {
                return;
            }
            instance.Listeners -= observer;
        }

        public class WaitForEvent : CustomYieldInstruction
        {
            bool wait = true;
            Action<P> onEvent;
            public WaitForEvent(Action<P> onEvent = null)
            {
                Register(OnEventHappened);
                this.onEvent = onEvent;
            }

            public void OnEventHappened(P par)
            {
                wait = false;
                Unregister(OnEventHappened);
                if(onEvent != null)
                {
                    onEvent(par);
                }
            }

            public override bool keepWaiting
            {
                get
                {
                    return wait;
                }
            }
        }

    }
   
}

