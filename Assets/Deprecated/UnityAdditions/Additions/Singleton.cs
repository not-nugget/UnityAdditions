using UnityEngine;

namespace UnityAdditions.Singleton
{
    /// <summary>
    /// Advanced singleton pattern that offers flexibility as well as safety when creating and accessing singleons. Thread safe
    /// </summary>
    /// <typeparam name="T">MonoBehavior object to specify as a singleton object</typeparam>
    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        [SerializeField, Tooltip("Should this singleton instance persist between scene transitions?")]
        private bool persistOnSceneTransition = true;
        private static readonly object Lock = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                    return null;
                }

                lock (Lock)
                {
                    if (instance != null) return instance;

                    var instances = FindObjectsOfType<T>();
                    var count = instances.Length;
                    if (count > 0)
                    {
                        if (count == 1) return instance = instances[0];

                        Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                        for (var i = 1; i < instances.Length; i++) Destroy(instances[i].gameObject);

                        return instance = instances[0];
                    }

                    Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                    return instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
                }
            }
        }

        private void Awake()
        {
            if (persistOnSceneTransition) DontDestroyOnLoad(gameObject);
            OnAwake();
        }

        protected virtual void OnAwake() { }
    }

    /// <summary>
    /// Singleton sub-class used to detect when application has quit. When creating a singleton, extend <c>Singleton&lt;T&gt;</c> instead
    /// </summary>
    public abstract class Singleton : MonoBehaviour
    {
        public static bool Quitting { get; private set; }
        
        private void OnApplicationQuit()
        {
            Quitting = true;
        }
    }
}