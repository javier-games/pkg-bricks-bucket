using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Assets Utils.
    ///
    /// <para>
    /// Usefull tools for editing assets.
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

            //  Determinating wether the asset is a prefab.
            if (PrefabUtility.IsPartOfPrefabAsset (asset))
                path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot (
                    instanceComponentOrGameObject: asset
                );

            #else
            
            //  Determinating wether the asset is a prefab.
            var prefabRoot = PrefabUtility.GetPrefabObject(asset);
            if (prefabRoot != null)
                path = AssetDatabase.GetAssetPath(prefabRoot);

            #endif

            if (!string.IsNullOrEmpty (path))
                return path;

            //  Determinating if the game object is on scene.
            GameObject gameObject = null;
            if (asset is GameObject)
                gameObject = (GameObject) asset;
            else if (asset is Component)
                gameObject = ((Component) asset).gameObject;

            if (gameObject != null)
                return gameObject.scene.path;

            return null;
        }

        /// <summary> Add a sub asset to this object. </summary>
        /// <param name="asset"></param>
        /// <param name="subasset"></param>
        public static void AddSubasset (this Object asset, Object subasset)
        {
            var assetPath = asset.GetAssetPath ();
            Debug.Assert (assetPath != null);
            AssetDatabase.AddObjectToAsset (subasset, assetPath);
            AssetDatabase.ImportAsset (assetPath);
        }

        /// <summary> Try to destroy this inmediately. </summary>
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
                DebugEditor.LogException (e);
            }
        }

        /// <summary> Destroy unreferenced sub assets. </summary>
        /// <param name="asset"></param>
        public static void DestroyUnreferencedSubassets
        (this Object asset, IEnumerable<Object> candidates)
        {
            var path = AssetDatabase.GetAssetPath (asset);

            if (string.IsNullOrEmpty(path))
                return;

            var mainAsset = AssetDatabase.LoadMainAssetAtPath (path);
            var allAssets = AssetDatabase.LoadAllAssetsAtPath (path);
            var referencedAssets = new HashSet<Object> ();

            //  Add Referenced subassets
            AddReferencedSubassets (referencedAssets, allAssets, mainAsset);

            //  Destroy unreferenced assets.
            var unreferencedSubassets = candidates.Except (referencedAssets);
            foreach (var unreferencedAsset in unreferencedSubassets)
                unreferencedAsset.TryDestroyImmediate ();
            AssetDatabase.ImportAsset (path);
        }

        #endregion



        #region Subassets Methods

        /// <summary> Adds referenced subassets. </summary>
        /// <param name="referencedAssets"></param>
        /// <param name="allAssets"></param>
        /// <param name="asset"></param>
        public static void AddReferencedSubassets
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
                AddReferencedSubassets (referencedAssets,allAssets,child);
        }

        /// <summary> Adds referenced subassets. </summary>
        /// <param name="referencedAssets"></param>
        /// <param name="allAssets"></param>
        /// <param name="property"></param>
        public static void AddReferencedSubassets (
            HashSet<Object> referencedAssets,
            Object[] allAssets,
            SerializedProperty property
        ) {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
                AddReferencedSubassets (
                    referencedAssets: referencedAssets,
                    allAssets: allAssets,
                    asset: property.objectReferenceValue
                );
            else
            {
                var children = property.EnumerateChildProperties ();
                foreach (var child in children)
                    AddReferencedSubassets (
                        referencedAssets: referencedAssets,
                        allAssets: allAssets,
                        asset: property.objectReferenceValue
                    );
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
                DebugEditor.LogError (e);
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
                directory = StringUtils.Concat (directory, pathSections[i]);
                if (!Directory.Exists (directory))
                    Directory.CreateDirectory (directory);
                directory = StringUtils.Concat (directory, "/");
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
