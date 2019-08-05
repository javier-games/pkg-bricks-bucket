using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Collections
{
    /// <summary>
    /// 
    /// Array Drawer Adapter.
    /// 
    /// <para>
    /// Adapts an ArrayDrawe to property drawer.
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
    internal class ArrayDrawerAdapter : PropertyDrawer
    {

        #region Class Members

        //  Array drawer to adapt.
        private readonly ArrayDrawer _arrayDrawer;

        /// <summary> Type reference of ScriptAttributeUtility. </summary>
        private static readonly Type
        _scriptAttributeUtility = typeof (PropertyDrawer).Assembly.GetType (
            name: "UnityEditor.ScriptAttributeUtility"
        );

        /// <summary> Delegate to get the field info from property. </summary>
        /// <param name="property"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private delegate FieldInfo GetFieldInfoFromPropertyDelegate (
            SerializedProperty property,
            out Type type
        );

        /// <summary> Returns the GetFieldInfoFromProperty method. </summary>
        private static readonly GetFieldInfoFromPropertyDelegate
        _getFieldInfoFromProperty = (GetFieldInfoFromPropertyDelegate)
        Delegate.CreateDelegate (
            type: typeof (GetFieldInfoFromPropertyDelegate),
            firstArgument: null,
            method: _scriptAttributeUtility.GetMethod (
                name: "GetFieldInfoFromProperty",
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Static
            )
        );

        /// <summary> Attribute field of Property Drawer. </summary>
        private static readonly FieldInfo
        _attribute = typeof (PropertyDrawer).GetField (
            name: "m_Attribute",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
        );

        /// <summary> Field Info field of property Drawer. </summary>
        private static readonly FieldInfo
        _fieldInfo = typeof (PropertyDrawer).GetField (
            name: "m_FieldInfo",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
        );

        #endregion



        #region Constructor

        // Constructor
        internal ArrayDrawerAdapter (ArrayDrawer arrayDrawer)
        {
            _attribute.SetValue (this, arrayDrawer.attribute);
            _arrayDrawer = arrayDrawer;
        }

        #endregion



        #region Property Drawer Overrides

        /// <summary> Defines wether the InsperctorGUI can be cached. </summary>
        /// <param name="property"></param>
        /// <returns> Wether the InsperctorGUI can be cached. </returns>
        public sealed override bool
        CanCacheInspectorGUI (SerializedProperty property)
        {
            ResolveFieldInfo (property);
            return _arrayDrawer.CanCacheInspectorGUI (property);
        }

        /// <summary> Determinates how tall the property is. </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns> Height in pixels. </returns>
        public sealed override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            ResolveFieldInfo (property);
            return _arrayDrawer.GetPropertyHeight (property, label);
        }

        /// <summary> Called on GUI to Draw. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public sealed override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            ResolveFieldInfo (property);
            _arrayDrawer.OnGUI (position, property, label);
        }

        #endregion



        #region Class Implementation

        /// <summary> Fixes the Array Drawer reference. </summary>
        /// <param name="property"></param>
        private void ResolveFieldInfo (SerializedProperty property)
        {
            if (_arrayDrawer.FieldInfo == null)
            {
                var propertyFieldInfo = GetFieldInfo (property);
                _fieldInfo.SetValue (this, propertyFieldInfo);
                _arrayDrawer.FieldInfo = propertyFieldInfo;
            }
        }

        /// <summary> Returns the Field Info of a property. </summary>
        /// <param name="property"></param>
        /// <returns> Field Info. </returns>
        internal static FieldInfo GetFieldInfo (SerializedProperty property)
        {
            return _getFieldInfoFromProperty (
                property: property,
                type: out Type propertyType
            );
        }

        #endregion

    }
}
