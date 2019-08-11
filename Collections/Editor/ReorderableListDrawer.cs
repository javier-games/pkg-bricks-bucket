using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using Object = UnityEngine.Object;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Reorderable List Drawer.
    ///
    /// <para>
    /// Custom Property Drawer for attribute..
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityExtensions.ArrayDrawer by @garettbass.
    /// https://github.com/garettbass/UnityExtensions.ArrayDrawer
    /// </para>
    ///
    /// </summary>
    [CustomPropertyDrawer (typeof (ReorderableListAttribute))]
    public class ReorderableListDrawer : ArrayDrawer
    {

        #region Class Members

        /// <summary> Called on element selected. </summary>
        public static event ElementDelegate OnElementSelected;

        /// <summary> Selects an element. </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public delegate void ElementDelegate (
            SerializedProperty array,
            int index
        );

        /// <summary> Called on element selected. </summary>
        public static event BGColorDelegate OnBackgroundColor;

        /// <summary> Sets the Background Color. </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="backgroundColor"></param>
        public delegate void BGColorDelegate (
            SerializedProperty array,
            int index,
            ref Color backgroundColor
        );

        /// <summary> Reorderable List Map. </summary>
        private readonly ReorderableListMap
        _reorderableListMap = new ReorderableListMap ();

        /// <summary> Default Attribute. </summary>
        private static readonly ReorderableListAttribute
        _defaultAttribute = new ReorderableListAttribute ();

        /// <summary> Most Recent Reorderable List. </summary>
        private ReorderableListOfValues _mostRecentReorderableList;

        /// <summary> Most Recent Property Path. </summary>
        private string _mostRecentPropertyPath;

        /// <summary> Returns the type of an array or list. </summary>
        /// <param name="listType"></param>
        /// <returns> Type .</returns>
        private delegate Type GetArrayOrListElementTypeDelegate (Type listType);

        /// <summary> Type of EditorExtensionMethods. </summary>
        private static readonly Type
        _editorExtensionMethods = typeof (PropertyDrawer).Assembly.GetType (
            name: "UnityEditor.EditorExtensionMethods"
        );

        /// <summary> Returns the method GetArrayOrListElementType. </summary>
        private static readonly GetArrayOrListElementTypeDelegate
        _getArrayOrListElementType = (GetArrayOrListElementTypeDelegate)
        Delegate.CreateDelegate (
            type: typeof (GetArrayOrListElementTypeDelegate),
            firstArgument: null,
            method: _editorExtensionMethods.GetMethod (
                name: "GetArrayOrListElementType",
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
            )
        );

        /// <summary> Returns type of GetDrawer. </summary>
        /// <param name="type"></param>
        /// <returns> Type. </returns>
        private delegate Type GetDrawerTypeForTypeDelegate (Type type);

        /// <summary> Type of ScriptAttributeUtility. </summary>
        private static readonly Type
        _scriptAttributeUtility = typeof (PropertyDrawer).Assembly.GetType (
            name: "UnityEditor.ScriptAttributeUtility"
        );

        /// <summary> Returns the method GetDrawerTypeForType. </summary>
        private static readonly GetDrawerTypeForTypeDelegate
        _getDrawerTypeForType = (GetDrawerTypeForTypeDelegate)
        Delegate.CreateDelegate (
            type: typeof (GetDrawerTypeForTypeDelegate),
            firstArgument: null,
            method: _scriptAttributeUtility.GetMethod (
                name: "GetDrawerTypeForType",
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
            )
        );

        #endregion



        #region Accessors

        #pragma warning disable IDE1006 // Supress Naming Violation from Unity.
        public new ReorderableListAttribute attribute
        {
            get
            {
                var aux = (ReorderableListAttribute) base.attribute;
                return aux ?? _defaultAttribute;
            }
        }
        #pragma warning restore IDE1006 //  Restore Naming Violation Warnings.

        #endregion



        #region Array Drawer Overrides

        /// <summary> Defines wether the InsperctorGUI can be cached. </summary>
        /// <param name="property"></param>
        /// <returns> Wether the insperctor GUI can be cached. </returns>
        public override bool CanCacheInspectorGUI (SerializedProperty property)
        {
            return true;
        }

        /// <summary> Determinates how tall the decorator is. </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns> Height in pixels. </returns>
        public override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var reorderableListOfValues =
                GetReorderableList (
                    attribute,
                    FieldInfo,
                    property);

            Debug.Assert (
                reorderableListOfValues.serializedProperty.propertyPath ==
                property.propertyPath);

            try
            {
                return reorderableListOfValues.GetHeight (label);
            }
            catch (Exception ex)
            {
                Debug.LogException (ex);
                return 0f;
            }
        }

        /// <summary> Called on GUI to draw. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var reorderableList = GetReorderableList (
                attribute,
                FieldInfo,
                property
            );
            reorderableList._onBackgroundColor = OnBackgroundColor;
            reorderableList.onSelectCallback += OnSelectCallback;
            reorderableList.DoGUI (position);
            reorderableList.onSelectCallback -= OnSelectCallback;
            reorderableList._onBackgroundColor = null;
        }

        #endregion



        #region Class Implementation

        /// <summary> Called on select. </summary>
        /// <param name="list"></param>
        private void OnSelectCallback (ReorderableList list)
        {
            var array = list.serializedProperty;
            var index = list.index;
            if (OnElementSelected != null)
                OnElementSelected.Invoke (array, index);
        }

        /// <summary> Returns a reorderable list. </summary>
        /// <param name="listAttribute"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="property"></param>
        /// <returns> Reorderable List. </returns>
        private ReorderableListOfValues
        GetReorderableList (
            ReorderableListAttribute listAttribute,
            FieldInfo fieldInfo,
            SerializedProperty property
        )
        {
            var path = property.propertyPath;

            if (_mostRecentReorderableList != null)
                if (_mostRecentPropertyPath == path)
                {
                    _mostRecentReorderableList.serializedProperty = property;
                    return _mostRecentReorderableList;
                }

            _mostRecentReorderableList = _reorderableListMap.Find (path);

            if (_mostRecentReorderableList == null)
            {
                var reorderableList = CreateReorderableList (
                    listAttribute: listAttribute,
                    fieldInfo: fieldInfo,
                    property: property
                );

                _reorderableListMap.Add (path, reorderableList);
                _mostRecentReorderableList = reorderableList;
            }
            else
                _mostRecentReorderableList.serializedProperty = property;

            _mostRecentPropertyPath = path;

            return _mostRecentReorderableList;
        }

        /// <summary> Creates a reorderable list. </summary>
        /// <param name="listAttribute"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="property"></param>
        /// <returns> Reorderable List. </returns>
        private ReorderableListOfValues
        CreateReorderableList (
            ReorderableListAttribute listAttribute,
            FieldInfo fieldInfo,
            SerializedProperty property)
        {
            var listType = fieldInfo.FieldType;
            var elementType = _getArrayOrListElementType (listType);
            var elementIsValue =
                elementType.IsEnum ||
                elementType.IsPrimitive ||
                elementType == typeof (string) ||
                elementType == typeof (Color) ||
                elementType == typeof (LayerMask) ||
                elementType == typeof (Vector2) ||
                elementType == typeof (Vector3) ||
                elementType == typeof (Vector4) ||
                elementType == typeof (Rect) ||
                elementType == typeof (AnimationCurve) ||
                elementType == typeof (Bounds) ||
                elementType == typeof (Gradient) ||
                elementType == typeof (Quaternion) ||
                elementType == typeof (Vector2Int) ||
                elementType == typeof (Vector3Int) ||
                elementType == typeof (RectInt) ||
                elementType == typeof (BoundsInt);

            if (elementIsValue)
                return new ReorderableListOfValues (
                    attribute: listAttribute,
                    primaryProperty: property,
                    listType: listType,
                    elementType: elementType
                );

            var elementIsUnityEngineObject = typeof (Object).IsAssignableFrom (
                c: elementType
            );

            if (elementIsUnityEngineObject)
            {
                var elementsAreSubassets =
                    elementIsUnityEngineObject &&
                    listAttribute != null &&
                    listAttribute.elementsAreSubassets;

                if (elementsAreSubassets)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies ();

                    var types = assemblies.SelectMany (a => a.GetTypes ());

                    var subassetTypes = types.Where ( type =>
                        type.IsAbstract == false &&
                        type.IsGenericTypeDefinition == false &&
                        elementType.IsAssignableFrom (type)
                    ).ToArray ();

                    return new ReorderableListOfSubassets (
                        attribute: listAttribute,
                        property: property,
                        listType: listType,
                        elementType: elementType,
                        subassetTypes: subassetTypes
                    );
                }
                else
                    return new ReorderableListOfValues (
                        attribute: listAttribute,
                        primaryProperty: property,
                        listType: listType,
                        elementType: elementType
                    );
            }

            var elementPropertyDrawerType = _getDrawerTypeForType (elementType);
            if (elementPropertyDrawerType == null)
            {
                var elementIsStruct =
                    elementType.IsValueType &&
                    elementType.IsEnum == false &&
                    elementType.IsPrimitive == false;

                var elementIsClass =
                    elementType.IsClass;

                if (elementIsStruct || elementIsClass)
                    return new ReorderableListOfStructures (
                        attribute: listAttribute,
                        property: property,
                        listType: listType,
                        elementType: elementType
                    );
            }

            return new ReorderableListOfValues (
                attribute: listAttribute,
                primaryProperty: property,
                listType: listType,
                elementType: elementType
            );
        }

        #endregion



        #region Nested Classes

        /// <summary> Dictionary of lists. </summary>
        private class ReorderableListMap :
        Dictionary<string, ReorderableListOfValues>
        {
            /// <summary> Finds a List by path key. </summary>
            /// <param name="key"></param>
            /// <returns> Reorderable list. </returns>
            public ReorderableListOfValues Find (string key)
            {
                TryGetValue (key, out ReorderableListOfValues reorderableList);
                return reorderableList;
            }
        }

        /// <summary> Scope disposable. </summary>
        public struct ElementSelectionScope : IDisposable
        {
            //  Callback on selected.
            private readonly ElementDelegate _callback;

            //  Constructor.
            public ElementSelectionScope (ElementDelegate callback)
            {
                _callback = callback;
                OnElementSelected += _callback;
            }

            // Dispose callback.
            public void Dispose () => OnElementSelected -= _callback;
        }

        /// <summary> Scope disposable. </summary>
        public struct BackgroundColorScope : IDisposable
        {
            //  Callback on selected.
            private readonly BGColorDelegate _callback;

            public BackgroundColorScope (BGColorDelegate callback)
            {
                _callback = callback;
                OnBackgroundColor += _callback;
            }

            public void Dispose () => OnBackgroundColor -= _callback;
        }

        #endregion
    }
}
