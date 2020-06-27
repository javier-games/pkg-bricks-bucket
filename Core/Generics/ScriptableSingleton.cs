using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable VirtualMemberNeverOverridden.Global
namespace BricksBucket.Generics
{
    /// <summary>
    /// 
    /// ScriptableSingleton.
    /// 
    /// <para>
    /// Generic singleton that inherits from scriptable object.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2018 </para>
    /// 
    /// </summary>
    /// <typeparam name="T">Heiress Class Type.</typeparam>
    public abstract class ScriptableSingleton<T> : ScriptableObject
    where T : ScriptableSingleton<T>
    {
        
        #region Constants
        
        /// <summary>
        /// Defines the default _folderPath in project folder.
        /// </summary>
        private const string DefaultFolderPath = "Assets/Resources";
        
        /// <summary>
        /// Defines the default extension for the scriptable object.
        /// </summary>
        private const string DefaultExtension = ".asset";

        #endregion
        
        
        #region Fields
        
        /// <summary>
        /// Scriptable Instance.
        /// </summary>
        private static T _instance;
        
        #endregion
        
        
        #region Properties
        
        /// <summary>
        /// Name of the folder.
        /// </summary>
        public virtual string FileName => typeof (T).Name;

        /// <summary>
        /// Extension of the scriptable object asset.
        /// </summary>
        public virtual string FileExtension => DefaultExtension;

        /// <summary>
        /// Path of the file's folder.
        /// </summary>
        public virtual string FolderPath => DefaultFolderPath;
        
        /// <summary>
        /// Complete path of the file.
        /// </summary>
        public string Path => string.Concat (
            FolderPath, "/", FileName, FileExtension
        );

        /// <summary>
        /// Indicates whether an instance exists or not.
        /// </summary>
        public static bool InstanceExist => _instance != null;

        /// <summary>
        /// Returns the scriptable instance.
        /// </summary>
        // ReSharper disable once MemberCanBeProtected.Global
        public static T Instance
        {
            get
            {
                //  Returns the current instance if it exits.
                if (InstanceExist) return _instance;

                //  Try to find an instance on Resources Folder.
                var instances = Resources.LoadAll ("", typeof (T));
                if (instances != null && instances.Length > 0)
                {
                    if (instances.Length > 1)
                        Debug.LogWarning (
                            "There are more than one" + typeof (T));
                    _instance = instances[0] as T;
                }
                if (InstanceExist) return _instance;

                // Creates a new asset in Resources folder.
                _instance = ScriptableObject.CreateInstance<T> ();
                
#if UNITY_EDITOR
                var path = string.IsNullOrWhiteSpace (_instance.Path)
                    ? DefaultFolderPath
                    : _instance.Path;
                if (!AssetDatabase.IsValidFolder (path))
                {
                    var segments = path.Split ('/');
                    var currentPath = segments[0];
                    for (int i = 1; i < segments.Length; i++)
                    {
                        var wantedPath = currentPath + "/" + segments[i];
                        if (AssetDatabase.IsValidFolder (wantedPath))
                        {
                            currentPath = wantedPath;
                            continue;
                        }

                        string guid = AssetDatabase.CreateFolder(
                            parentFolder: currentPath,
                            newFolderName: segments[i]
                        );
                        currentPath = AssetDatabase.GUIDToAssetPath(guid);
                    }

                    path = currentPath;
                }
                AssetDatabase.CreateAsset (_instance, path);
#endif
                return _instance;
            }
        }
        
        #endregion
    }
}
