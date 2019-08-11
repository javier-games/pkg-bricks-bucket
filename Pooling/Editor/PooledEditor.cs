using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


namespace BricksBucket.Pooling
{
    /// <summary>
    ///
    /// Pooled Editor.
    ///
    /// <para>
    /// Custom editor for pooled component. This class shows just the
    /// necessary attributes that the user must manipulate and automatically
    /// assign the source attribute.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor (typeof (Pooled), false)]
    public class PooledEditor : Editor
    {
        protected SerializedProperty _sourceProperty;           //  Helps to
        protected SerializedProperty _amountProperty;           //  display
        protected SerializedProperty _typeProperty;             //  properties.
        protected SerializedProperty _stopCoroutinesProperty;   //

        /// <summary> Called on Enable </summary>
        protected virtual void OnEnable ()
        {
            //  Initializing serialized properties.
            _sourceProperty = serializedObject.FindProperty ("_source");
            _amountProperty = serializedObject.FindProperty ("_amount");
            _typeProperty = serializedObject.FindProperty ("_type");
            _stopCoroutinesProperty = serializedObject.FindProperty (
                propertyPath: "_stopCoroutines"
            );
        }


        /// <summary> Called to return the Height of a property. </summary>
        /// <returns> Height to draw property.</returns>
        public override void OnInspectorGUI ()
        {

            serializedObject.Update ();
            if (!EditorApplication.isPlaying)
            {
                Pooled current = (Pooled) target;

                //  Determining if the current object belongs to the scene (is a
                //  prefab instance) or is a prefab Asset.

                List<Pooled> pooledObjects =
                    FindObjectsOfType<Pooled> ().ToList ();
                bool isInstance = pooledObjects.Contains (current);

                //  TODO: There must be a way to not use FindObject Method.
                //  By the record, the PrefabUtility.IsSomething methods
                //  does not work.

                Pooled source = null;
                if (isInstance)
                {
                    switch (PrefabUtility.GetPrefabAssetType (current))
                    {

                        //  Change the source reference for its original source.
                        case PrefabAssetType.Regular:
                        case PrefabAssetType.Variant:
                        case PrefabAssetType.Model:
                        GameObject sourceGO = GetPrefabSource (
                            instance: current.gameObject
                        );
                        source = sourceGO.GetComponent<Pooled> ();
                        current.SetSource (source);
                        break;

                        //  Else it does not have source.
                        case PrefabAssetType.NotAPrefab:
                        case PrefabAssetType.MissingAsset:
                        source = null;
                        break;
                    }
                }
                else
                    source = null;

                _sourceProperty.objectReferenceValue = source;
            }

            //  Show the rest of the attributes on inspector.
            EditorGUILayout.PropertyField (_amountProperty);
            EditorGUILayout.PropertyField (_typeProperty);
            EditorGUILayout.PropertyField (_stopCoroutinesProperty);
            serializedObject.ApplyModifiedProperties ();

        }

        /// <summary>
        /// This method returns the prefab source until the most prefab
        /// instance root that means that will look trough nested prefabs
        /// until find the origin of this instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private GameObject GetPrefabSource (GameObject instance)
        {
            GameObject source = PrefabUtility.GetCorrespondingObjectFromSource (
                componentOrGameObject: instance
            );
            if (source == null)
                return instance;

            if (instance == PrefabUtility.GetOutermostPrefabInstanceRoot (
                componentOrGameObject: instance
            ))
                return source;
            return GetPrefabSource (source);
        }
    }
}
