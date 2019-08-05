using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    /// 
    /// Array Drawer.
    /// 
    /// <para>
    /// Implementation of ArrayDrawerBase.
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
    public class ArrayDrawer : ArrayDrawerBase
    {

        #region Class Members

        /// <summary> Field Info of the array drawer. </summary>
        public FieldInfo FieldInfo { get; internal set; }

        /// <summary> Return wether the property is the default. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        private delegate bool DefaultPropertyFieldDelegate (
            Rect position,
            SerializedProperty property,
            GUIContent label
        );

        /// <summary> Return wether the has visible child fields. </summary>
        private delegate bool HasVisibleChildFieldsDelegate (
            SerializedProperty property
        );

        /// <summary> Get DefaultPropertyField method. </summary>
        private static readonly DefaultPropertyFieldDelegate
        defaultPropertyFieldDelegate = (DefaultPropertyFieldDelegate)
        Delegate.CreateDelegate (
            type: typeof (DefaultPropertyFieldDelegate),
            firstArgument: null,
            method: typeof (EditorGUI).GetMethod (
                name: "DefaultPropertyField",
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
            )
        );

        /// <summary> Get HasVisibleChildFields method. </summary>
        private static readonly HasVisibleChildFieldsDelegate
        s_HasVisibleChildFields = (HasVisibleChildFieldsDelegate)
        Delegate.CreateDelegate (
            type: typeof (HasVisibleChildFieldsDelegate),
            firstArgument: null,
            method: typeof (EditorGUI).GetMethod (
                name: "HasVisibleChildFields",
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
            )
        );

        #endregion



        #region Array Drawer Base Override Method.

        #pragma warning disable IDE0060

        private new void CanCacheInspectorGUI () { }

        private new void GetHeight () { }

        private new void OnGUI (Rect position) { }

        #pragma warning restore IDE0060

        #endregion



        #region Public Virtual Methods 

        /// <summary> Defines wether the InsperctorGUI can be cached. </summary>
        /// <param name="property"></param>
        /// <returns> Wether the InsperctorGUI can be cached.</returns>
        public virtual bool
        CanCacheInspectorGUI (SerializedProperty property) => true;

        /// <summary> Determinates how tall the decorator is. </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns> Height in pixels. </returns>
        public virtual float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            //  Determinating the height by its childs.
            if (property.isExpanded && HasVisibleChildFields (property))
            {
                var spacing = EditorGUIUtility.standardVerticalSpacing;
                foreach (var child in EnumerateChildProperties (property))
                {
                    height += spacing;
                    height += EditorGUI.GetPropertyHeight (
                        child,
                        includeChildren: true
                    );
                }
            }
            return height;
        }

        /// <summary> Called on GUI to draw. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public virtual void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            // EditorGUI.DrawRect(position, Color.yellow);

            position.height = EditorGUIUtility.singleLineHeight;
            DefaultPropertyField (position, property, label);

            //  Drawing childs.
            if (property.isExpanded && HasVisibleChildFields (property))
            {
                var spacing = EditorGUIUtility.standardVerticalSpacing;

                //  Use an ident to draw.
                using (IndentLevelScope ())
                {
                    foreach (var child in EnumerateChildProperties (property))
                    {
                        position.y += spacing;
                        position.y += position.height;
                        position.height = EditorGUI .GetPropertyHeight (
                            property: child,
                            includeChildren: true
                        );

                        EditorGUI.PropertyField (
                            position: position,
                            property: child,
                            includeChildren: true
                        );

                    }
                }
            }
        }

        #endregion



        #region Class Implementation

        /// <summary> Enumerates Child Properties. </summary>
        /// <param name="parentProperty"></param>
        /// <returns> IEnumerable. </returns>
        protected static IEnumerable<SerializedProperty>
        EnumerateChildProperties (SerializedProperty parentProperty)
        {
            return parentProperty.EnumerateChildProperties ();
        }

        /// <summary> Enumerate Element Properties. </summary>
        /// <param name="arrayProperty"></param>
        /// <returns></returns>
        protected static IEnumerable<SerializedProperty>
        EnumerateElementProperties (SerializedProperty arrayProperty)
        {
            return EnumerateChildProperties (arrayProperty).Skip (1);
        }

        /// <summary> Return wether the property is the default. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns> Wether the property is the default. </returns>
        protected static bool DefaultPropertyField
        (Rect position, SerializedProperty property, GUIContent label)
        {
            return defaultPropertyFieldDelegate (position, property, label);
        }

        /// <summary> Return wether the has visible child fields. </summary>
        /// <param name="property"></param>
        /// <returns> Wether the has visible child fields. </returns>
        protected static bool
        HasVisibleChildFields (SerializedProperty property)
        {
            return s_HasVisibleChildFields (property);
        }

        /// <summary> Indent Level Scope. </summary>
        /// <param name="indent"></param>
        protected static IDisposable IndentLevelScope (int indent = 1)
        {
            EditorGUI.indentLevel += indent;
            return new Deferred (() => EditorGUI.indentLevel -= indent);
        }

        #endregion



        #region Nested Classes

        /// <summary>
        /// Structure to release allocated action.
        /// </summary>
        private struct Deferred : IDisposable
        {
            private readonly Action _onDispose;

            //  Constructor.
            public Deferred (Action onDispose) => _onDispose = onDispose;

            //  Dispose action.
            public void Dispose () => _onDispose?.Invoke ();
        }

        #endregion

    }
}
