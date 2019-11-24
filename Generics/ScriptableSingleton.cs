using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BricksBucket.Generics
{

    /// <summary>
    /// ScriptableSingleton.
    /// 
    /// <para>
    /// Generic singleton that inherits from scriptable object.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2018 </para>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScriptableSingleton <T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        /// <summary>
        /// Scriptable Instance.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Returns the scriptable instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                //  Returns the current instance if it exits.
                if(InstanceExist)
                    return _instance;
                
                //  Try to find an instance on Resources Folder.
                var instances = Resources.LoadAll("",typeof(T));
                if(instances != null && instances.Length > 0)
                {
                    if(instances.Length > 1)
                        Debug.LogWarning("There are more than one" + typeof(T));
                    _instance = instances[0] as T;
                }

                #if UNITY_EDITOR
                // Creates a new asset in Resources folder.
                if (!InstanceExist)
                {
                    _instance = ScriptableObject.CreateInstance<T>();
                    AssetDatabase.CreateAsset(
                        _instance,
                        StringUtils.Concat(
                            "Assets/Resources/",typeof(T).Name,".asset"
                        )
                    );
                }
                #endif

                return _instance;
            }
        }

        /// <summary>
        /// Indicates whether an instance exists or not.
        /// </summary>
        public static bool InstanceExist
        {
            get { return _instance != null; }
        }
    }
}
