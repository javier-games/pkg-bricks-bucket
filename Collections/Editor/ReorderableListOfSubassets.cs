using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using Object = UnityEngine.Object;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Reorderable List of Structures.
    ///
    /// <para>
    /// Implementation of a reorderable lis of structures to draw subassets.
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
    internal class ReorderableListOfSubassets : ReorderableListOfStructures
    {

        #region Class Members

        /// <summary> Flag to ise full sub asset type names. </summary>
        private readonly bool _useFullSubassetTypeNames;

        /// <summary> Cache for serialized objects. </summary>
        private readonly SerializedObjectCache
        _serializedObjectCache = new SerializedObjectCache ();

        /// <summary> Array to store subasset types. </summary>
        private readonly Type[] _subassetTypes;

        #endregion



        #region Accessors

        /// <summary> Wether has single subasset type. </summary>
        public bool HasSingleSubassetType
        {
            get { return _subassetTypes.Length == 1; }
        }

        /// <summary> Wether has multipler subasset type. </summary>
        public bool HasMultipleSubassetTypes
        {
            get { return _subassetTypes.Length > 1; }
        }

        /// <summary> Wether header is showing. </summary>
        public override bool ShowElementHeader
        {
            get { return base.ShowElementHeader || HasMultipleSubassetTypes; }
        }

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of this. </summary>
        /// <param name="attribute"></param>
        /// <param name="property"></param>
        /// <param name="listType"></param>
        /// <param name="elementType"></param>
        /// <param name="subassetTypes"></param>
        public ReorderableListOfSubassets (
            ReorderableListAttribute attribute,
            SerializedProperty property,
            Type listType,
            Type elementType,
            Type[] subassetTypes)
        : base (attribute, property, listType, elementType)
        {
            _subassetTypes = subassetTypes;

            _useFullSubassetTypeNames = SubassetTypeNamesAreAmbiguous ();

            onCanAddCallback = OnCanAddCallback;

            if (HasSingleSubassetType)
                onAddCallback = OnAddCallback;

            else if (HasMultipleSubassetTypes)
                onAddDropdownCallback = OnAddDropdownCallback;
        }

        #endregion



        #region Reorderable List of Structures Overrides

        /// <summary> Does the GUI. </summary>
        /// <param name="position"></param>
        public override void DoGUI (Rect position)
        {
            base.DoGUI (position);
            EvictObsoleteSerializedObjectsFromCache ();
        }

        /// <summary> Return element height. </summary>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <returns> Height. </returns>
        protected override float
        GetElementHeight (SerializedProperty element, int elementIndex)
        {
            var subasset = element.objectReferenceValue;
            if (subasset == null)
                return EditorGUIUtility.singleLineHeight;

            var serializedObject = GetSerializedObjectFromCache (subasset);
            var properties = serializedObject.EnumerateChildProperties ();
            return GetElementHeight (properties);
        }

        /// <summary> Draws an element. </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        protected override void DrawElement (
            Rect position,
            SerializedProperty element,
            int elementIndex,
            bool isActive
        ) {
            var subasset = element.objectReferenceValue;
            if (subasset == null)
                return;

            var serializedObject = GetSerializedObjectFromCache (subasset);
            serializedObject.Update ();
            var properties = serializedObject.EnumerateChildProperties ();
            DrawElement (position, properties, elementIndex, isActive);
            serializedObject.ApplyModifiedProperties ();
        }

        /// <summary> Populates Context menu. </summary>
        /// <param name="menu"></param>
        /// <param name="elementIndex"></param>
        protected override void
        PopulateElementContextMenu (GenericMenu menu, int elementIndex)
        {
            foreach (var mutableElementType in _subassetTypes)
            {
                var elementType = mutableElementType;

                var elementTypeName = _useFullSubassetTypeNames ?
                    elementType.FullName :
                    elementType.Name;

                var insertAbove = "Insert Above/" + elementTypeName;
                var insertBelow = "Insert Below/" + elementTypeName;

                menu.AddItem (
                    content: new GUIContent (insertAbove),
                    on: false,
                    func: () => OnNextGUIFrame (
                        () => InsertSubasset (elementType, elementIndex)
                    )
                );

                menu.AddItem (
                    content: new GUIContent (insertBelow),
                    on: false,
                    func: () => OnNextGUIFrame (
                        () => InsertSubasset (elementType, elementIndex + 1)
                    )
                );
            }

            menu.AddSeparator (string.Empty);

            menu.AddItem (
                content: new GUIContent ("Remove"),
                on: false,
                func: () => OnNextGUIFrame (() => DeleteElement (elementIndex))
            );
        }

        /// <summary> Delets an element from list. </summary>
        /// <param name="elementIndex"></param>
        protected override void DeleteElement (int elementIndex)
        {
            if (elementIndex < 0)
                return;

            var array = serializedProperty;
            if (elementIndex < array.arraySize)
            {
                var serializedObject = array.serializedObject;
                var element = array.GetArrayElementAtIndex (elementIndex);
                var oldSubassets = element.FindReferencedSubassets ();

                element.objectReferenceValue = null;
                array.DeleteArrayElementAtIndex (elementIndex);

                if (oldSubassets.Any ())
                {
                    serializedObject.ApplyModifiedPropertiesWithoutUndo ();
                    serializedObject.DestroyUnreferencedSubassets (
                        candidates: oldSubassets
                    );
                }
                else
                    serializedObject.ApplyModifiedProperties ();

                var length = array.arraySize;
                if (index > length - 1)
                    index = length - 1;
            }
        }

        #endregion



        #region Class Implementation

        #pragma warning disable IDE0060 // Remove unused parameter warning.
        /// <summary> Draws element's header. </summary>
        /// <param name="position"></param>
        /// <param name="subasset"></param>
        /// <param name="isActive"></param>
        private void
        DrawElementHeader (Rect position, Object subasset, bool isActive)
        #pragma warning restore IDE0060 // Remove unused parameter warning.
        {
            var subassetType = subasset != null ?
                subasset.GetType () :
                typeof (Object);

            position.height = headerHeight;

            var titleContent = base._titleContent;
            titleContent.text = ObjectNames.NicifyVariableName (subasset.name);
            titleContent.image = EditorGUIUtility.ObjectContent (
                obj: subasset,
                type: subassetType
            ) .image;

            var titleStyle = EditorStyles.boldLabel;

            var titleWidth = titleStyle.CalcSize (titleContent).x;

            var scriptRect = position;
            scriptRect.yMin -= 1;
            scriptRect.yMax -= 1;
            scriptRect.width = titleWidth + 16;

            using (ColorAlphaScope (0))
            {
                EditorGUI.BeginDisabledGroup (disabled: true);
                EditorGUI.ObjectField (
                    position: scriptRect,
                    obj: subasset,
                    objType: subassetType,
                    allowSceneObjects: false);
                EditorGUI.EndDisabledGroup ();
            }

            if (IsRepaint ())
            {
                var fillRect = position;
                fillRect.xMin -= draggable ? 18 : 4;
                fillRect.xMax += 4;
                fillRect.y -= 2;

                var fillStyle = _headerBackgroundStyle;

                using (ColorAlphaScope (0.5f))
                {
                    fillStyle.Draw (fillRect, false, false, false, false);
                }

                var titleRect = position;
                titleRect.xMin -= 4;
                titleRect.yMin -= 2;
                titleRect.yMax += 1;
                titleRect.width = titleWidth;

                titleStyle.Draw (
                    position: titleRect,
                    content: titleContent,
                    isHover: false,
                    isActive: false,
                    on: false,
                    hasKeyboardFocus: false
                );
            }
        }

        #region Callbacks

        /// <summary> Called on can add. </summary>
        /// <param name="reorderableList"></param>
        /// <returns> Wether can add. </returns>
        private bool OnCanAddCallback (ReorderableList reorderableList) =>
            _subassetTypes.Length > 0;

        /// <summary> Called on Add </summary>
        /// <param name="reorderableList"></param>
        private void OnAddCallback (ReorderableList reorderableList)
        {
            serializedProperty.isExpanded = true;
            AddSubasset (_subassetTypes[0]);
        }

        /// <summary> Called on dropdown. </summary>
        /// <param name="position"></param>
        /// <param name="reorederableList"></param>
        private void
        OnAddDropdownCallback (Rect position, ReorderableList reorederableList)
        {
            serializedProperty.isExpanded = true;
            var menu = new GenericMenu ();

            foreach (var mutableElementType in _subassetTypes)
            {
                var elementType = mutableElementType;
                var elementTypeName = _useFullSubassetTypeNames ?
                    elementType.FullName :
                    elementType.Name;

                var content = new GUIContent ();
                content.text = ObjectNames.NicifyVariableName (elementTypeName);

                menu.AddItem (
                    content: content,
                    on: false,
                    func: () => AddSubasset (elementType)
                );
            }

            position.x -= 2;
            position.y += 1;
            menu.DropDown (position);
        }

        #endregion

        /// <summary> Adds a subasset. </summary>
        /// <param name="subassetType"></param>
        private void AddSubasset (Type subassetType)
        {
            var array = serializedProperty;
            var elementIndex = array.arraySize;

            InsertSubasset (subassetType, elementIndex);
        }

        /// <summary> Inserts a subasset. </summary>
        /// <param name="subassetType"></param>
        /// <param name="elementIndex"></param>
        private void InsertSubasset (Type subassetType, int elementIndex)
        {
            var array = serializedProperty;

            array.InsertArrayElementAtIndex (elementIndex);
            index = elementIndex;
            GUI.changed = true;

            var subasset = default (Object);

            if (typeof (ScriptableObject).IsAssignableFrom (subassetType))
                subasset = ScriptableObject.CreateInstance (subassetType);
            else if (typeof (Object).IsAssignableFrom (subassetType))
                subasset = (Object) Activator.CreateInstance (subassetType);

            if (subasset == null)
            {
                DebugEditor.LogWarningFormat (
                    context: null,
                    format: "Failed to create subasset of type {0}",
                    data: subassetType.FullName
                );

                return;
            }

            subasset.name = subassetType.Name;

            var serializedObject = array.serializedObject;
            serializedObject.targetObject.AddSubasset (subasset);

            var element = array.GetArrayElementAtIndex (elementIndex);
            var oldSubassets = element.FindReferencedSubassets ();
            element.objectReferenceValue = subasset;

            if (oldSubassets.Any ())
            {
                serializedObject.ApplyModifiedPropertiesWithoutUndo ();
                serializedObject.DestroyUnreferencedSubassets (oldSubassets);
            }
            else
                serializedObject.ApplyModifiedProperties ();
        }

        /// <summary> Wether Subassets Type Names are Ambiguous. </summary>
        /// <returns> Wether Subassets Type Names are Ambiguous. </returns>
        private bool SubassetTypeNamesAreAmbiguous ()
        {
            var elementTypeNames = _subassetTypes.Select (t => t.Name);
            var elementTypeNamesAreAmbiguous =
                elementTypeNames.Count () >
                elementTypeNames.Distinct ().Count ();
            return elementTypeNamesAreAmbiguous;
        }

        /// <summary> Returns serialized object from cache. </summary>
        /// <param name="object"></param>
        /// <returns> Serialized object from cache. </returns>
        private SerializedObject GetSerializedObjectFromCache (Object @object)
        {
            var cache = _serializedObjectCache;
            if (!cache.TryGetValue (
                key: @object,
                value: out SerializedObject serializedObject
            )) {
                serializedObject = new SerializedObject (@object);
                cache.Add (@object, serializedObject);
            }
            return serializedObject;
        }

        /// <summary> Evicts Obsolete SerializedObjects from cache. </summary>
        private void EvictObsoleteSerializedObjectsFromCache ()
        {
            var cache = _serializedObjectCache;
            var destroyedObjects = cache.Keys.Where (key => key == null);
            if (destroyedObjects.Any ())
                foreach (var @object in destroyedObjects.ToArray ())
                    cache.Remove (@object);
        }

        #endregion



        #region Nested Classes

        /// <summary> Dictionary of serialized objects. </summary>
        class SerializedObjectCache : Dictionary<Object, SerializedObject> { }

        #endregion


    }
}
