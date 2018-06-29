using UnityEngine;
using System;
namespace Kabuk
{
    public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static bool InstanceExists 
        {
            get 
            {
                return instance != null; 
            }
        }

        private static T instance = null;

        public static T Instance
        {
            get
            {
                TouchInstance();
                return instance;
            }
        }
        /// <summary>
        ///  Don't override
        /// </summary>
        void Awake()
        {
            if (InstanceExists && instance != this)
            {
                Destroy(gameObject);
            }else
            {
                UnitySingletonAttribute attribute = Attribute.GetCustomAttribute(typeof(T), typeof(UnitySingletonAttribute)) as UnitySingletonAttribute;
                if(!attribute.destroyOnLoad)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
				instance = GetComponent<T>();
                OnAwake();
            }
        }

        protected virtual void OnAwake()
        {
            
        }

        public static void TouchInstance()
        {
            if (!InstanceExists)
            {
                UnitySingletonAttribute attribute = Attribute.GetCustomAttribute(typeof(T), typeof(UnitySingletonAttribute)) as UnitySingletonAttribute;
                if (attribute == null)
                {
                    Debug.LogError("Cannot find UnitySingleton attribute on " + typeof(T).Name);
                    return;
                }

                for (int x = 0; x < attribute.singletonTypePriority.Length; x++)
                {
                    if (TryGenerateInstance(attribute.singletonTypePriority[x], attribute.destroyOnLoad, attribute.resourcesLoadPath, x == attribute.singletonTypePriority.Length - 1))
                        break;
                }
            }
        }


        /// <summary>
        /// Attempts to generate a singleton with the given parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="resourcesLoadPath"></param>
        /// <param name="warn"></param>
        /// <returns></returns>
        private static bool TryGenerateInstance(UnitySingletonAttribute.Type type, bool destroyOnLoad, string resourcesLoadPath, bool warn)
        {
            if (type == UnitySingletonAttribute.Type.ExistsInScene)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    if (warn)
                        Debug.LogError("Cannot find an object with a " + typeof(T).Name + " .  Please add one to the scene.");
                    return false;
                }
            }
            else if (type == UnitySingletonAttribute.Type.LoadedFromResources)
            {
                if (string.IsNullOrEmpty(resourcesLoadPath))
                {
                    if (warn)
                        Debug.LogError("UnitySingletonAttribute.resourcesLoadPath is not a valid Resources location in " + typeof(T).Name);
                    return false;
                }
                T pref = Resources.Load<T>(resourcesLoadPath);
                if (pref == null)
                {
                    if (warn)
                        Debug.LogError("Failed to load prefab with " + typeof(T).Name + " component attached to it from folder Resources/" + resourcesLoadPath + ".  Please add a prefab with the component to that location, or update the location.");
                    return false;
                }
                instance = Instantiate<T>(pref);
                if (instance == null)
                {
                    if (warn)
                        Debug.LogError("Failed to create instance of prefab " + pref + " with component " + typeof(T).Name + ".  Please check your memory constraints");
                    return false;
                }
            }
            else if (type == UnitySingletonAttribute.Type.CreateOnNewGameObject)
            {
                GameObject go = new GameObject(typeof(T).Name + " Singleton");
                if (go == null)
                {
                    if (warn)
                        Debug.LogError("Failed to create gameobject for instance of " + typeof(T).Name + ".  Please check your memory constraints.");
                    return false;
                }
                instance = go.AddComponent<T>();
                if (instance == null)
                {
                    if (warn)
                        Debug.LogError("Failed to add component of " + typeof(T).Name + "to new gameobject.  Please check your memory constraints.");
                    Destroy(go);
                    return false;
                }
            }

            if (!destroyOnLoad)
                DontDestroyOnLoad(instance.gameObject);

            return true;
        }
    }
}