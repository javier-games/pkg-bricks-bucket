#if UNITY_EDITOR

using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BricksBucket.Editor
{
    /// <summary>
    ///
    /// Assets Utils.
    ///
    /// <para>
    /// Useful tools for editing assets.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public static class AssetsUtils
    {

        #region Extensions

        /// <summary> Returns the path of the asset in project. </summary>
        /// <param name="asset"></param>
        /// <returns> Path. </returns>
        public static string GetAssetPath (this Object asset)
        {
            var path = AssetDatabase.GetAssetPath (asset);
            if (!string.IsNullOrEmpty (path))
                return path;

            #if UNITY_2018_3_OR_NEWER

            //  Determinate whether the asset is a prefab.
            if (PrefabUtility.IsPartOfPrefabAsset (asset))
                path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot (
                    instanceComponentOrGameObject: asset
                );

            #else

            //  Determinate whether the asset is a prefab.
            var prefabRoot = PrefabUtility.GetPrefabObject(asset);
            if (prefabRoot != null)
                path = AssetDatabase.GetAssetPath(prefabRoot);

            #endif

            if (!string.IsNullOrEmpty (path))
                return path;

            //  Determinate if the game object is on scene.
            GameObject gameObject = null;
            switch (asset) {
                case GameObject o:
                    gameObject = o;
                    break;
                case Component component:
                    gameObject = component.gameObject;
                    break;
            }

            return gameObject != null ? gameObject.scene.path : null;
        }

        /// <summary> Adds a sub asset to this object. </summary>
        /// <param name="asset"></param>
        /// <param name="subAsset"></param>
        public static void AddSubAsset (this Object asset, Object subAsset)
        {
            var assetPath = asset.GetAssetPath ();
            Debug.Assert (assetPath != null);
            AssetDatabase.AddObjectToAsset (subAsset, assetPath);
            AssetDatabase.ImportAsset (assetPath);
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh ();
        }

        /// <summary> Try to destroy this immediately. </summary>
        /// <param name="asset"></param>
        public static void TryDestroyImmediate (this Object asset)
        {
            try
            {
                if (asset != null)
                    Object.DestroyImmediate (asset, true);
            }
            catch (System.Exception e)
            {
                Debug.Log (e);
                //DebugEditor.LogException (e);
            }
        }

        /// <summary> Destroy unreferenced sub assets. </summary>
        /// <param name="asset"></param>
        /// <param name="candidates"></param>
        public static void DestroyUnreferencedSubAssets
        (this Object asset, IEnumerable<Object> candidates)
        {
            var path = AssetDatabase.GetAssetPath (asset);

            if (string.IsNullOrEmpty(path))
                return;

            var mainAsset = AssetDatabase.LoadMainAssetAtPath (path);
            var allAssets = AssetDatabase.LoadAllAssetsAtPath (path);
            var referencedAssets = new HashSet<Object> ();

            //  Add Referenced sub assets
            AddReferencedSubAssets (referencedAssets, allAssets, mainAsset);

            //  Destroy unreferenced assets.
            var unreferencedSubAssets = candidates.Except (referencedAssets);
            foreach (var unreferencedAsset in unreferencedSubAssets)
                unreferencedAsset.TryDestroyImmediate ();
            AssetDatabase.ImportAsset (path);
        }

        #endregion



        #region Subassets Methods

        /// <summary> Adds referenced sub assets. </summary>
        /// <param name="referencedAssets"></param>
        /// <param name="allAssets"></param>
        /// <param name="asset"></param>
        public static void AddReferencedSubAssets
        (HashSet<Object> referencedAssets, Object[] allAssets, Object asset)
        {
            if (asset == null)
                return;

            if (!allAssets.Contains (asset))
                return;

            if (!referencedAssets.Add (asset))
                return;

            IEnumerable<SerializedProperty> children;
            using (var serializedObject = new SerializedObject(asset))
                children = serializedObject.EnumerateChildProperties();
            foreach (var child in children)
                AddReferencedSubAssets (referencedAssets,allAssets,child);
        }

        /// <summary> Adds referenced sub assets. </summary>
        /// <param name="referencedAssets"></param>
        /// <param name="allAssets"></param>
        /// <param name="property"></param>
        public static void AddReferencedSubAssets (
            HashSet<Object> referencedAssets,
            Object[] allAssets,
            SerializedProperty property
        ) {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
                AddReferencedSubAssets (
                    referencedAssets: referencedAssets,
                    allAssets: allAssets,
                    asset: property.objectReferenceValue
                );
            else
            {
                var children = property.EnumerateChildProperties ();
                var serializedProperties = children.ToList ();
                for (int i = 0; i < serializedProperties.Count(); i++)
                {
                    AddReferencedSubAssets (
                        referencedAssets: referencedAssets,
                        allAssets: allAssets,
                        asset: property.objectReferenceValue
                    );
                }
                    
            }
        }

        #endregion



        #region Scriptable Assets

        /// <summary> Returns an scriptable asset instance of T. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T
        GetScriptable<T> (string path) where T : ScriptableObject
        {
            var settings = !File.Exists (path) ?
                CreateScriptable<T> (path) :
                LoadScriptable<T> (path);

            if (settings == null)
            {
                DeleteFile (path);
                settings = CreateScriptable<T> (path);
            }

            settings.hideFlags = HideFlags.HideAndDontSave;
            return settings;
        }

        /// <summary> Loads an scriptable object. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T
        LoadScriptable<T> (string path) where T : ScriptableObject
        {
            T settingsInstance;
            try
            {
                settingsInstance = AssetDatabase.LoadAssetAtPath<T> (path);
            }
            catch (System.Exception e)
            {
                Debug.Log (e);
                //DebugEditor.LogError (e);
                settingsInstance = null;
            }

            return settingsInstance;
        }

        /// <summary> Creates a new scriptable asset file. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T
        CreateScriptable<T> (string path) where T : ScriptableObject
        {
            T instance = ScriptableObject.CreateInstance<T> ();
            SaveScriptable (instance, path);

            return instance;
        }

        /// <summary> Deletes an asset at the specific path. </summary>
        /// <param name="path"></param>
        public static void DeleteFile (string path)
        {
            if (!File.Exists (path))
                return;

            var attributes = File.GetAttributes (path);
            var bitwise = attributes & FileAttributes.ReadOnly;
            if (bitwise == FileAttributes.ReadOnly)
                File.SetAttributes (
                    path: path,
                    fileAttributes: attributes & ~FileAttributes.ReadOnly
                );

            File.Delete (path);
        }

        /// <summary> Saves the scriptable instance. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="path"></param>
        public static void
        SaveScriptable<T> (T instance, string path) where T : ScriptableObject
        {
            //  Creating or finding directories.
            char[] separator = { '/' };
            string[] pathSections = path.Split (separator);
            string directory = string.Empty;
            for (int i = 0; i < pathSections.Length - 1; i++)
            {
                directory = string.Concat (directory, pathSections[i]);
                if (!Directory.Exists (directory))
                    Directory.CreateDirectory (directory);
                directory = string.Concat (directory, "/");
            }

            //  Creation of scriptable asset.
            try
            {
                AssetDatabase.CreateAsset (instance, path);
                AssetDatabase.SaveAssets ();
            }
            finally
            {
                AssetDatabase.Refresh ();
            }

        }

        #endregion


    }
}

#endif