using UnityEngine;

namespace Kabuk
{
    public abstract class CustomEventBase<T> : MonoBehaviour where T : CustomEventBase<T>
    {
        public const string EventContainerName = "EventContainer";
        static GameObject Container
        {
            get
            {
                GameObject container = GameObject.Find(EventContainerName);
                if (container == null)
                {
                    container = new GameObject(EventContainerName);
                    container.transform.position = Vector3.zero;
                    container.transform.Reset();
                }
                return container;
            }
        }

        protected static T instance = null;
        protected static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject holder = new GameObject(typeof(T).Name + "");
                    holder.transform.position = Vector3.zero;
                    holder.transform.parent = Container.transform;
                    instance = holder.AddComponent<T>();
                }
                return instance;
            }
        }

        void OnDisabled()
        {
            instance = null;
        }
    }
}
