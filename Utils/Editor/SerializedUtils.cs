#if UNITY_EDITOR

using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

using Object = UnityEngine.Object;

namespace BricksBucket.Editor
{
    /// <summary>
    ///
    /// Serialized Utils.
    ///
    /// <para>
    /// Usefull tools for serialized properties.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public static class SerializedUtils
    {

        #region Extensions

        /// <summary> Enumerates Child Properties. </summary>
        /// <param name="serializedObject"></param>
        /// <returns> Simple iteration over a collection. </returns>
        public static IEnumerable<SerializedProperty>
        EnumerateChildProperties (this SerializedObject serializedObject)
        {
            var iterator = serializedObject.GetIterator ();
            if (iterator.NextVisible (enterChildren: true))
                while (iterator.NextVisible (enterChildren: false))
                    yield return iterator;
        }

        /// <summary> Enumerates Child Properties. </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable<SerializedProperty>
        EnumerateChildProperties (this SerializedProperty property)
        {
            var iterator = property.Copy ();
            var end = iterator.GetEndProperty ();
            if (iterator.NextVisible (enterChildren: true))
                do
                {
                    if (SerializedProperty.EqualContents (iterator, end))
                        yield break;

                    yield return iterator;
                }
                while (iterator.NextVisible (enterChildren: false));
        }

        /// <summary> Find Parent Property. </summary>
        /// <param name="property"></param>
        /// <returns> Serialized Property. </returns>
        public static SerializedProperty
        FindParentProperty (this SerializedProperty property)
        {
            var serializedObject = property.serializedObject;
            var propertyPath = property.propertyPath;
            var propertyKeys = ParsePropertyPath (propertyPath).ToArray ();
            var propertyKeyCount = propertyKeys.Length;

            // Parent is serialized object.
            if (propertyKeyCount == 1)
                return null;

            var lastPropertyKey = propertyKeys[propertyKeyCount - 1];
            // Parent is an array, drop [Array,data,N] from path.
            if (lastPropertyKey is int)
            {
                var parentKeys = propertyKeys.Take (propertyKeyCount - 3);
                var parentPath = JoinPropertyPath (parentKeys);
                return serializedObject.FindProperty (parentPath);
            }
            else
            // Parent is a structure, drop [name] from path.
            {
                var parentKeys = propertyKeys.Take (propertyKeyCount - 1);
                var parentPath = JoinPropertyPath (parentKeys);
                return serializedObject.FindProperty (parentPath);
            }
        }

        /// <summary> Gets the object of a serialized property. </summary>
        /// <param name="property"></param>
        /// <returns> The object. </returns>
        public static object GetObject (this SerializedProperty property)
        {
            var obj = property.serializedObject.targetObject;
            var path = ParseValuePath (property);
            return FindObject (obj, path);
        }

        /// <summary> Sets the object of a serialized property. </summary>
        /// <param name="property"></param>
        /// <returns> The object. </returns>
        public static bool SetObjectValue (
            this SerializedProperty property,
            Type type,
            object value
        ) {
            var obj = property.serializedObject.targetObject;
            var path = ParseValuePath (property);
            return SetObjectValue (type, obj, path, value);
        }

        /// <summary> Returns the type of the objec. </summary>
        /// <param name="property"> Property to look for. </param>
        /// <returns> Type of object. </returns>
        public static Type GetObjectType (this SerializedProperty property)
        {
            object obj = property.GetObject ();

            if (obj != null)
                return obj.GetType ();
            return null;
        }

        /// <summary> Compares the type of a property. </summary>
        /// <param name="property"> Main Property. </param>
        /// <param name="typeToCompare"> Type to compare. </param>
        /// <returns> Wether the property is the type to compare. </returns>
        public static bool
        CompareType (this SerializedProperty property, Type typeToCompare)
        {
            Type propertyType = property.GetObjectType();
            return propertyType == typeToCompare;
        }

        /// <summary> Returns wether this is array or list. </summary>
        /// <param name="property"></param>
        /// <returns> Wether this is array or list or not. </returns>
        public static bool IsArrayOrList (this SerializedProperty property)
        {
            return (
                property.propertyType == SerializedPropertyType.Generic &&
                property.isArray == true
            );
        }

        /// <summary> Returns wether this is an structure. </summary>
        /// <param name="property"></param>
        /// <returns> Wether this is an structure or not. </returns>
        public static bool IsStructure (this SerializedProperty property)
        {
            return (
                property.propertyType == SerializedPropertyType.Generic &&
                property.isArray == false &&
                property.hasChildren == true
            );
        }

        /// <summary> Finds References SubAssets. </summary>
        /// <param name="property"></param>
        /// <returns> HashSet of subassets. </returns>
        public static HashSet<Object>
        FindReferencedSubassets (this SerializedProperty property)
        {
            var propertyAsset = property.serializedObject.targetObject;
            var assetPath = AssetDatabase.GetAssetPath (propertyAsset);
            var allSubassets = AssetDatabase.LoadAllAssetsAtPath (assetPath);
            var referencedSubassets = new HashSet<Object> ();
            AssetsUtils.AddReferencedSubAssets (
                referencedAssets: referencedSubassets,
                allAssets: allSubassets,
                property: property
            );
            return referencedSubassets;
        }

        /// <summary> Destroys Unreferenced Subassets. </summary>
        /// <param name="serializedObject"></param>
        /// <param name="candidates"></param>
        public static void
        DestroyUnreferencedSubassets
        (this SerializedObject serializedObject, IEnumerable<Object> candidates)
        {
            var targetObject = serializedObject.targetObject;
            targetObject.DestroyUnreferencedSubAssets (candidates);
        }

        /// <summary> Returns wether has referenced subassets. </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool
        HasReferenceSubassets (this SerializedProperty property) =>
            FindReferencedSubassets (property).Any ();

        /// <summary> Resizes the array. </summary>
        /// <param name="property"></param>
        /// <param name="arraySize"></param>
        public static void
        ResizeArray (this SerializedProperty property, int arraySize)
        {
            while (property.arraySize < arraySize)
                property.InsertArrayElementAtIndex (property.arraySize);
            while (property.arraySize > arraySize)
                property.DeleteArrayElementAtIndex (property.arraySize - 1);
        }

        /// <summary> Replaces the property array for new values. </summary>
        /// <param name="property"> Property with elements to replace. </param>
        /// <param name="newElements"> New elements. </param>
        public static void
        ReplaceArray (this SerializedProperty property, Object[] newElements)
        {
            property.arraySize = 0;
            property.serializedObject.ApplyModifiedProperties ();
            property.arraySize = newElements.Length;
            for (var i = 0; i < newElements.Length; i++)
            {
                var subproperty = property.GetArrayElementAtIndex (i);
                subproperty.objectReferenceValue = newElements[i];
            }

            property.serializedObject.ApplyModifiedProperties ();
        }

        /// <summary> Returns the value path. </summary>
        /// <param name="property"> Path value. </param>
        public static string GetValuePath (this SerializedProperty property) =>
            property.propertyPath.Replace (".Array.data[", "[");

        /// <summary> Returns string value. </summary>
        /// <param name="property"> Property to parse. </param>
        /// <returns> String Value. </returns>
        public static string ToStringValue (this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                return property.stringValue;

                case SerializedPropertyType.Character:
                case SerializedPropertyType.Integer:
                if (property.type == "char")
                    return Convert.ToChar (property.intValue).ToString ();
                return property.intValue.ToString ();

                case SerializedPropertyType.ObjectReference:
                return property.objectReferenceValue != null ?
                    property.objectReferenceValue.ToString () : "null";

                case SerializedPropertyType.Boolean:
                return property.boolValue.ToString ();

                case SerializedPropertyType.Enum:
                return property.enumNames[property.enumValueIndex];

                default:
                return string.Empty;
            }
        }

        #endregion



        #region Implementation

        /// <summary> Parse the property path. </summary>
        /// <param name="propertyPath"></param>
        private static IEnumerable<object>
        ParsePropertyPath (string propertyPath)
        {
            return ParseValuePath (propertyPath);
        }

        /// <summary> Parse the property path. </summary>
        /// <param name="property"></param>
        private static IEnumerable<object>
        ParsePropertyPath (SerializedProperty property)
        {
            return ParsePropertyPath (property.propertyPath);
        }

        /// <summary> Parse the value path. </summary>
        /// <param name="property"></param>
        private static IEnumerable<object>
        ParseValuePath (SerializedProperty property)
        {
            return ParseValuePath (property.GetValuePath());
        }


        /// <summary> Joins Value Path. </summary>
        /// <param name="keys"></param>
        private static string JoinValuePath (IEnumerable<object> keys)
        {
            var builder = new StringBuilder ();
            foreach (var key in keys)
            {
                if (key is string)
                {
                    if (builder.Length > 0)
                        builder.Append ('.');
                    builder.Append ((string) key);
                    continue;
                }
                if (key is int)
                {
                    builder.Append ('[');
                    builder.Append ((int) key);
                    builder.Append (']');
                    continue;
                }
                throw new Exception (string.Format (
                    "invalid key: {0}", key
                ));
            }
            return builder.ToString ();
        }

        /// <summary> Joins Property Path. </summary>
        /// <param name="keys"></param>
        private static string JoinPropertyPath (IEnumerable<object> keys)
        {
            return JoinValuePath (keys);
        }
        
        /// <summary>
        /// Parse value path.
        /// </summary>
        /// <param name="path">Path to parse.</param>
        private static IEnumerable<object> ParseValuePath (string path)
        {
            var keys = path.Split ('.');
            foreach (var key in keys)
            {
                //  For element identifier.
                if (key.IsElementIdentifier ())
                {
                    var subKeys = key.Split ('[', ']');
                    yield return subKeys[0];
                    foreach (var subKey in subKeys.Skip (1))
                    {
                        if (string.IsNullOrEmpty (subKey))
                            continue;

                        int index = int.Parse (subKey);
                        yield return index;
                    }

                    //  Continue the key iteration.
                    continue;
                }

                //  For element index.
                if (key.IsElementIndex ())
                {
                    var subKeys = key.Split ('[', ']');
                    foreach (var subKey in subKeys)
                    {
                        if (string.IsNullOrEmpty (subKey))
                            continue;

                        int index = int.Parse (subKey);
                        yield return index;
                    }

                    //  Continue the key iteration.
                    continue;
                }

                //  For member identifier.
                if (key.IsMemberIdentifier ())
                {
                    yield return key;

                    //  Continue the key iteration.
                    continue;
                }

                //  Else Exception.
                throw new System.Exception (
                    StringUtils.ConcatFormat ( "Invalid path: {0}", path)
                );
            }
        }

        #endregion



        #region Reflection

        /// <summary> Wether the type is a bool. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a bool. </returns>
        public static bool IsBool (this Type type) =>
            type == typeof (bool);

        /// <summary> Wether the type is a sbyte. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a sbyte. </returns>
        public static bool IsSignedByte (this Type type) =>
            type == typeof (sbyte);

        /// <summary> Wether the type is a short. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an short. </returns>
        public static bool IsShort (this Type type) =>
            type == typeof (short);

        /// <summary> Wether the type is an int. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an int. </returns>
        public static bool IsInt (this Type type) =>
            type == typeof (int);

        /// <summary> Wether the type is a float. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a float. </returns>
        public static bool IsFloat (this Type type) =>
            type == typeof (float);

        /// <summary> Wether the type is a long. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an long. </returns>
        public static bool IsLong (this Type type) =>
            type == typeof (long);

        /// <summary> Wether the type is a decimal. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an decimal. </returns>
        public static bool IsDecimal (this Type type) =>
            type == typeof (decimal);

        /// <summary> Wether the type is a double. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a double. </returns>
        public static bool IsDouble (this Type type) =>
            type == typeof (double);

        /// <summary> Wether the type is a byte. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a byte. </returns>
        public static bool IsByte (this Type type) =>
            type == typeof (byte);

        /// <summary> Wether the type is an ushort. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an ushort. </returns>
        public static bool IsUnsignedShort (this Type type) =>
            type == typeof (ushort);

        /// <summary> Wether the type is a uint. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an uint. </returns>
        public static bool IsUnsignedInt (this Type type) =>
            type == typeof (uint);

        /// <summary> Wether the type is an ulong. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is an ulong. </returns>
        public static bool IsUnsignedLong (this Type type) =>
            type == typeof (ulong);

        /// <summary> Wether the type is a string. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a string. </returns>
        public static bool IsString (this Type type) =>
            type == typeof (string);

        /// <summary> Wether the type is a char. </summary>
        /// <param name="type"> Type to evaluate. </param>
        /// <returns> Wether the type is a char. </returns>
        public static bool IsChar (this Type type) =>
            type == typeof (char);

        /// <summary> Get all fields with attribute of type T. </summary>
        /// <typeparam name="T"> Type of attributes. </typeparam>
        /// <returns> Collection of all attributes. </returns>
        public static ComponentFieldInfo[]
        GetFieldsWithAttribute<T> () where T : Attribute
        {
            var components = GetAllComponentsInScenes<MonoBehaviour> ();

            var fields = new List<ComponentFieldInfo> ();

            foreach (var component in components)
            {
                if (component == null)
                    continue;

                Type type = component.GetType ();
                FieldInfo[] fieldInfos = type.GetFields (
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance
                );

                var attributes = fieldInfos.Where (
                    field => field.IsDefined (typeof (T), false)
                );

                foreach (var attribute in attributes)
                    fields.Add (new ComponentFieldInfo (attribute, component));
            }

            return fields.ToArray ();
        }

        /// <summary> Get all methods with attribute of type T. </summary>
        /// <typeparam name="T"> Type of attributes. </typeparam>
        /// <returns> Collection of all attributes. </returns>
        public static ComponentMethodInfo[]
        GetMethodsWithAttribute<T> () where T : Attribute
        {
            var components = GetAllComponentsInScenes<MonoBehaviour> ();

            var methods = new List<ComponentMethodInfo> ();

            foreach (var component in components)
            {
                if (component == null)
                    continue;

                Type type = component.GetType ();
                MethodInfo[] methodInfos = type.GetMethods (
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic
                );

                var attributes = methodInfos.Where (
                    field => field.IsDefined (typeof (T), false)
                );

                foreach (var attribute in attributes)
                    methods.Add (new ComponentMethodInfo (attribute, component));
            }

            return methods.ToArray ();
        }

        /// <summary>
        /// Get all Components on loaded scenes, even all those
        /// that has been disabled.
        /// </summary>
        /// <typeparam name="T"> Type of components. </typeparam>
        /// <returns> Array of components. </returns>
        public static T[] GetAllComponentsInScenes <T>()
        {
            var components = new List<T> ();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt (i);
                var roots = scene.GetRootGameObjects ();
                foreach (var root in roots)
                {
                    var childrenComponents = root.GetComponentsInChildren<T> (
                        includeInactive: true
                    );
                    foreach (var component in childrenComponents)
                        components.Add (component);
                }
            }

            return components.ToArray ();
        }

        /// <summary> Finds the field info. </summary>
        /// <param name="type"></param>
        /// <param name="field"></param>
        /// <returns> Field Info. </returns>
        private static FieldInfo FindFieldInfo (this Type type, string field)
        {
            const BindingFlags bindingFlags =
                BindingFlags.FlattenHierarchy |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic;
            var fieldInfo = type.GetField (field, bindingFlags);
            if (fieldInfo != null)
                return fieldInfo;

            var baseType = type.BaseType;
            if (baseType == null)
                return null;

            return FindFieldInfo (baseType, field);
        }

        /// <summary> Finds an object in path. </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns> The object. </returns>
        public static object
        FindObject (object obj, IEnumerable<object> path)
        {
            foreach (var key in path)
            {
                if (key is string)
                {
                    var objType = obj.GetType ();
                    var field = (string) key;
                    var fieldInfo = objType.FindFieldInfo (field);
                    if (fieldInfo == null)
                        throw FieldNotFoundException (objType, field);
                    obj = fieldInfo.GetValue (obj);
                    continue;
                }
                if (key is int elementIndex)
                {
                    var array = (IList) obj;
                    obj = array[elementIndex];
                    continue;
                }
            }
            return obj;
        }

        /// <summary> Sets the object value in the gived path. </summary>
        /// <param name="type"> Type of the object to assign. </param>
        /// <param name="obj"> Object with field to set. </param>
        /// <param name="path"> Complete Path where to find field. </param>
        /// <param name="value"> New value. </param>
        public static bool SetObjectValue (
            Type type,
            object obj,
            IEnumerable<object> path,
            object value
        ) {
            bool success = true;
            foreach (var key in path)
            {
                if (key is string)
                {
                    var objType = obj.GetType ();
                    var field = (string) key;
                    var fieldInfo = objType.FindFieldInfo (field);
                    if (fieldInfo == null)
                    {
                        success = false;
                        continue;
                    }
                    try
                    {
                        fieldInfo.SetValue (
                            obj: obj,
                            value: Convert.ChangeType (value, type));
                    }
                    catch { success = false; }
                    continue;
                }
            }
            return success;
        }

        #endregion



        #region Exceptions

        /// <summary> Unsupported Value Type. </summary>
        /// <param name="property"></param>
        /// <returns> Exception. </returns>
        public static Exception
        UnsupportedValueType (SerializedProperty property)
        {
            var serializedObject = property.serializedObject;
            var targetObject = serializedObject.targetObject;
            var targetType = targetObject.GetType ();
            var targetTypeName = targetType.Name;
            var valueTypeName = property.propertyType.ToString ();
            var propertyPath = property.propertyPath;
            return new Exception ( StringUtils.ConcatFormat (
                "Unsupported value type {0} for {1}.{2}",
                valueTypeName, targetTypeName, propertyPath
            ));
        }

        /// <summary> Unsupported Value Type. </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns> Exception. </returns>
        public static Exception
        UnsupportedValue (SerializedProperty property, object value)
        {
            var serializedObject = property.serializedObject;
            var targetObject = serializedObject.targetObject;
            var targetType = targetObject.GetType ();
            var targetTypeName = targetType.Name;
            var propertyPath = property.propertyPath;
            return new Exception ( StringUtils.ConcatFormat (
                "Unsupported value {0} for {1}.{2}",
                value, targetTypeName, propertyPath
            ));
        }

        /// <summary> Unsupported Value Type. </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        /// <returns> Exception. </returns>
        public static Exception
        UnsupportedValue
        (SerializedProperty property, object value, string expected)
        {
            var serializedObject = property.serializedObject;
            var targetObject = serializedObject.targetObject;
            var targetType = targetObject.GetType ();
            var targetTypeName = targetType.Name;
            var propertyPath = property.propertyPath;

            if (value == null)
                value = "null";
            else
                value = string.Format ("'{0}'", value);
            return new Exception ( StringUtils.ConcatFormat (
                "Unsupported value {0} for {1}.{2}, expected {3}",
                value, targetTypeName, propertyPath, expected
            ));
        }

        /// <summary> Exception when a field is not found. </summary>
        /// <param name="type"></param>
        /// <param name="field"></param>
        /// <returns> Exception. </returns>
        public static Exception
        FieldNotFoundException (Type type, string field)
        {
            return new KeyNotFoundException ( StringUtils.ConcatFormat (
                "Property {0}.{1} not found", type, field)
            );
        }

        #endregion
    }



    /// <summary>
    ///
    /// Component and FieldInfo.
    ///
    /// <para>
    /// Structure to store Field Info and Component reference.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public struct ComponentFieldInfo
    {
        /// <summary> Info of the property. </summary>
        public readonly FieldInfo field;

        /// <summary> Reference to the component. </summary>
        public readonly Component component;

        /// <summary> Creates a new instance of this structure. </summary>
        /// <param name="field"> Info of the property. </param>
        /// <param name="component"> Component </param>
        public ComponentFieldInfo (FieldInfo field, Component component)
        {
            this.field = field;
            this.component = component;
        }
    }

    /// <summary>
    ///
    /// Component and FieldInfo.
    ///
    /// <para>
    /// Structure to store Method Info and Component reference.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    public struct ComponentMethodInfo
    {
        /// <summary> Info of the property. </summary>
        public readonly MethodInfo method;

        /// <summary> Reference to the component. </summary>
        public readonly Component component;

        /// <summary> Creates a new instance of this structure. </summary>
        /// <param name="method"> Info of the property. </param>
        /// <param name="component"> Component </param>
        public ComponentMethodInfo (MethodInfo method, Component component)
        {
            this.method = method;
            this.component = component;
        }
    }
}

#endif