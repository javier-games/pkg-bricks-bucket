using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace BricksBucket.Collections
{
    using BGColorDelegate = ReorderableListDrawer.BGColorDelegate;

    /// <summary>
    /// 
    /// Reorderable List of Values.
    /// 
    /// <para>
    /// Reorderable List implementation for general values.
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
    internal class ReorderableListOfValues : ReorderableList
    {

        #region Class Members

        private const float IndentPerLevel = 15;    //  Indent Size.
        private const float BorderHeight = 0;       //  Height of the border.

        /// <summary> Field info of "m_NonDragTargetIndices". </summary>
        private static readonly FieldInfo
        _nonDragTargetIndicesField = typeof (ReorderableList) .GetField (
            name: "m_NonDragTargetIndices",
            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic
        );

        /// <summary> Reference to EndEditingActiveTextField method. </summary>
        private static readonly EndEditingActiveTextFieldDelegate
        _endEditingActiveTextField = (EndEditingActiveTextFieldDelegate)
        Delegate.CreateDelegate (
            type: typeof (EndEditingActiveTextFieldDelegate),
            firstArgument: null,
            method: typeof (EditorGUI).GetMethod (
                name: "EndEditingActiveTextField",
                bindingAttr: BindingFlags.NonPublic |
                BindingFlags.Public | BindingFlags.Static
            )
        );

        /// <summary> Return adefault. </summary>
        protected static readonly new Defaults
        defaultBehaviours = new Defaults ();

        #region GUI Parameters
        protected static readonly GUIStyle
        _eyeDropperHorizontalLine = "EyeDropperHorizontalLine";
        protected static readonly GUIStyle
        _contextMenuButtonStyle = "Icon.TrackOptions";
        protected static readonly GUIStyle
        _elementBackgroundStyle = "CN EntryBackEven";
        protected static readonly GUIContent
        _cutLabel = new GUIContent ("Cut");
        protected static readonly GUIContent
        _copyLabel = new GUIContent ("Copy");
        protected static readonly GUIContent
        _pasteLabel = new GUIContent ("Paste");
        #endregion

        /// <summary> Wether or not ends editing active text. </summary>
        private delegate void EndEditingActiveTextFieldDelegate ();

        private int _dragIndex;
        private Action _onNextGUIFrame;
        protected GUIContent _label = new GUIContent ();

        protected string _elementHeaderFormat;
        protected string _singularListHeaderFormat;
        protected string _pluralListHeaderFormat;
        protected Color _backgroundColor;

        internal BGColorDelegate _onBackgroundColor;

        protected readonly List<float> _elementHeights = new List<float> ();
        protected readonly Type _listType;
        protected readonly Type _elementType;
        protected readonly bool _showFooterButtons;
        protected readonly SerializedProperty[] serializedProperties;
        protected readonly ParallelListLayout parallelListLayout;
        protected readonly GUIContent _titleContent = new GUIContent ();

        #endregion



        #region Accessors

        /// <summary> Date time of last rendered. </summary>
        public DateTime LastRendered { get; private set; } = DateTime.MaxValue;

        /// <summary> Element indent drawed. </summary>
        protected virtual float DrawElementIndent { get { return 0; } }

        /// <summary> Wether or not has element header format. </summary>
        public bool HasElementHeaderFormat
        {
            get { return _elementHeaderFormat != null; }
        }

        /// <summary> Wether shows an element header. </summary>
        public virtual bool ShowElementHeader
        {
            get { return false; }
        }

        /// <summary> Deafult Icon Plus. </summary>
        protected GUIContent IconToolbarPlus
        {
            get { return defaultBehaviours.iconToolbarPlus; }
        }

        /// <summary> Deafult Icon Plus More. </summary>
        protected GUIContent IconToolbarPlusMore
        {
            get { return defaultBehaviours.iconToolbarPlusMore; }
        }

        /// <summary> Deafult Icon Minus. </summary>
        protected GUIContent IconToolbarMinus
        {
            get { return defaultBehaviours.iconToolbarMinus; }
        }

        /// <summary> Deafult Pre Button. </summary>
        protected GUIStyle PreButton
        {
            get { return defaultBehaviours.preButton; }
        }

        /// <summary> Returns Non drag target indeces. </summary>
        private List<int> NonDragTargetIndices
        {
            get
            {
                return (List<int>) _nonDragTargetIndicesField .GetValue (this);
            }
        }

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of this. </summary>
        /// <param name="attribute"></param>
        /// <param name="primaryProperty"></param>
        /// <param name="listType"></param>
        /// <param name="elementType"></param>
        public ReorderableListOfValues (
            ReorderableListAttribute attribute,
            SerializedProperty primaryProperty,
            Type listType,
            Type elementType
        ) : base (
            serializedObject: primaryProperty.serializedObject,
            elements: primaryProperty.Copy (),
            draggable: !attribute.disableDragging,
            displayHeader: true,
            displayAddButton: !attribute.disableAdding,
            displayRemoveButton: !attribute.disableRemoving)
        {
            _listType = listType;
            _elementType = elementType;
            _elementHeaderFormat = attribute.elementHeaderFormat;
            _showFooterButtons =
                (displayAdd || displayRemove) && !attribute.hideFooterButtons;
            _singularListHeaderFormat =
                attribute.SingularListHeaderFormat ?? "{0} ({1})";
            _pluralListHeaderFormat =
                attribute.PluralListHeaderFormat ?? "{0} ({1})";
            _backgroundColor = new Color (
                r: attribute.r,
                g: attribute.g,
                b: attribute.b
            );
            serializedProperties = AcquireSerializedProperties (
                primaryProperty: serializedProperty,
                parallelListNames: attribute.parallelListNames
            );
            parallelListLayout = attribute.parallelListLayout;

            headerHeight -= 2;
            drawHeaderCallback = DrawHeaderCallback;
            drawFooterCallback = DrawFooterCallback;
            elementHeightCallback = ElementHeightCallback;
            drawElementCallback = DrawElementCallback;
            drawElementBackgroundCallback = DrawElementBackgroundCallback;

            onAddCallback = OnAddCallback;
            onCanRemoveCallback = OnCanRemoveCallback;
            onRemoveCallback = OnRemoveCallback;

            onSelectCallback = OnSelectCallback;
            onReorderCallback = OnReorderCallback;

            #if UNITY_2018_1_OR_NEWER
            drawNoneElementCallback = DrawEmptyElementCallback;
            #endif
        }

        #endregion


        #region Reorderable Implementation 

        /// <summary> Acquire Serialized Properties. </summary>
        /// <param name="primaryProperty"></param>
        /// <param name="parallelListNames"></param>
        /// <returns></returns>
        private static SerializedProperty[] AcquireSerializedProperties (
            SerializedProperty primaryProperty,
            string[] parallelListNames
        )
        {
            if (parallelListNames == null || parallelListNames.Length == 0)
                return new[] { primaryProperty };

            var serializedObject = primaryProperty.serializedObject;

            var serializedProperties =
                new List<SerializedProperty> (1 + parallelListNames.Length)
            {
                primaryProperty
            };

            var primaryArraySize = primaryProperty.arraySize;

            var primaryPropertyPath = primaryProperty.propertyPath;
            var lastDotIndex = primaryPropertyPath.LastIndexOf ('.');
            var parallelPropertyPrefix =
                primaryPropertyPath.Substring (0, lastDotIndex + 1);

            foreach (var parallelListName in parallelListNames)
            {
                var parallelPropertyPath =
                    parallelPropertyPrefix +
                    parallelListName;
                var parallelProperty =
                    serializedObject
                    .FindProperty (parallelPropertyPath);
                if (parallelProperty != null && parallelProperty.isArray)
                {
                    parallelProperty.ResizeArray (primaryArraySize);
                    serializedProperties.Add (parallelProperty);
                }
            }
            return serializedProperties.ToArray ();
        }

        /// <summary> Returns the Height </summary>
        /// <param name="label"></param>
        /// <returns> Height. </returns>
        public float GetHeight (GUIContent label)
        {
            LastRendered = DateTime.Now;
            UpdateLabel (label);
            UpdateElementHeights ();
            var height = GetHeight ();

            if (!_showFooterButtons)
                height -= 14; // no add/remove buttons in footer

            if (!serializedProperty.isExpanded)
            {
                var elementCount = _elementHeights.Count;
                if (elementCount == 0)
                    height -= 21; // no empty element
            }

            return height;
        }

        /// <summary> Does the GUI. </summary>
        /// <param name="position"></param>
        public virtual void DoGUI (Rect position)
        {
            if (_onNextGUIFrame != null)
                _onNextGUIFrame.Invoke ();

            _onNextGUIFrame = null;

            if (!displayAdd && !displayRemove && !draggable)
                index = -1;

            position.xMin += EditorGUI.indentLevel * IndentPerLevel;

            using (IndentLevelScope (-EditorGUI.indentLevel))
            {
                if (serializedProperty.isExpanded)
                    DoList (position);
                else
                {
                    index = -1;
                    DoCollapsedListBackground (position);
                }
                DrawHeader (position);
            }
        }

        /// <summary> Coes Collapsed List Background. </summary>
        /// <param name="position"></param>
        private void DoCollapsedListBackground (Rect position)
        {
            var headerRect = position;
            headerRect.height = headerHeight;

            var listRect = position;
            listRect.y += headerHeight;
            listRect.height = 7;

            var footerRect = position;
            footerRect.y += headerHeight + listRect.height;
            footerRect.height = footerHeight;

            if (showDefaultBackground && IsRepaint ())
            {
                defaultBehaviours.DrawHeaderBackground (headerRect);
                defaultBehaviours
                .boxBackground
                .Draw (listRect, false, false, false, false);
            }
            DrawFooterCallback (footerRect);
        }

        #endregion



        #region Element Manipulation

        /// <summary> Copies an element. </summary>
        /// <param name="elementIndex"></param>
        private void CopyElement (int elementIndex)
        {
            if (elementIndex < 0)
                return;

            var arrayIndex = 0;
            var arrayCount = serializedProperties.Length;
            var clipboardContent = new ClipboardContent (arrayCount);
            foreach (var array in serializedProperties)
            {
                var arrayObj = (IList) array.GetObject ();
                var elementObj = arrayObj[elementIndex];
                var arrayElementType = elementObj.GetType ();
                var elementJson = JsonUtility.ToJson (elementObj);
                var clipboardElement = new ClipboardElement
                {
                    type = arrayElementType.FullName,
                    json = elementJson
                };
                clipboardContent.elements[arrayIndex] = clipboardElement;
                arrayIndex += 1;
            }
            EditorGUIUtility.systemCopyBuffer = clipboardContent.Serialize ();
        }

        /// <summary> Cuts an element. </summary>
        /// <param name="elementIndex"></param>
        private void CutElement (int elementIndex)
        {
            if (elementIndex < 0)
                return;

            CopyElement (elementIndex);
            DeleteElement (elementIndex);
        }

        /// <summary> Wether can paste clipboard content. </summary>
        /// <param name="clipboardContent"></param>
        /// <returns> Wether can paste clipboard content. </returns>
        private bool CanPaste (ClipboardContent clipboardContent)
        {
            if (clipboardContent == null)
                return false;

            var arrayIndex = 0;
            foreach (var array in serializedProperties)
            {
                var arrayObj = (IList) array.GetObject ();
                var arrayType = arrayObj.GetType ();
                var elementTypeAux = arrayType.IsArray ?
                    arrayType.GetElementType () :
                    arrayType.GetGenericArguments ()[0];

                var clipboardElement = clipboardContent.elements[arrayIndex++];
                if (clipboardElement.type != elementTypeAux.FullName)
                    return false;
            }
            return true;
        }

        /// <summary> Pastes a new element. </summary>
        /// <param name="elementIndex"></param>
        /// <param name="clipboardContent"></param>
        private void
        PasteElement (int elementIndex, ClipboardContent clipboardContent)
        {
            if (elementIndex < 0)
                return;

            var clipboardElements = clipboardContent.elements;
            if (clipboardElements.Length == 0)
                return;

            var arrayIndex = 0;
            var serializedPropertyAux = serializedProperty;
            var serializedObject = serializedPropertyAux.serializedObject;
            var targetObject = serializedObject.targetObject;
            Undo.RecordObject (
                targetObject,
                string.Format ("Paste {0}", clipboardElements[0].type)
            );
            foreach (var array in serializedProperties)
            {
                if (elementIndex >= array.arraySize)
                    array.arraySize = elementIndex + 1;

                var clipboardElement = clipboardContent.elements[arrayIndex++];
                var arrayObj = (IList) array.GetObject ();
                var elementObj = arrayObj[elementIndex];
                var elementJson = clipboardElement.json;
                JsonUtility.FromJsonOverwrite (elementJson, elementObj);
            }
            serializedObject.Update ();
            GUI.changed = true;
        }

        /// <summary> Inserts an element at specified index. </summary>
        /// <param name="elementIndex"></param>
        protected virtual void InsertElement (int elementIndex)
        {
            if (elementIndex < 0)
                return;

            var serializedPropertyAux = serializedProperty;
            var serializedObject = serializedPropertyAux.serializedObject;
            foreach (var array in serializedProperties)
            {
                array.InsertArrayElementAtIndex (elementIndex);
            }
            serializedObject.ApplyModifiedProperties ();
            index = elementIndex;
            GUI.changed = true;
        }

        /// <summary> Deletes an Element at specified index. </summary>
        /// <param name="elementIndex"></param>
        protected virtual void DeleteElement (int elementIndex)
        {
            if (elementIndex < 0)
                return;

            var serializedPropertyAux = serializedProperty;
            var serializedObject = serializedPropertyAux.serializedObject;
            if (elementIndex < serializedPropertyAux.arraySize)
            {
                foreach (var array in serializedProperties)
                {
                    var element = array.GetArrayElementAtIndex (elementIndex);
                    var oldSubassets = element.FindReferencedSubassets ();
                    array.DeleteArrayElementAtIndex (elementIndex);
                    if (oldSubassets.Any ())
                    {
                        serializedObject.ApplyModifiedPropertiesWithoutUndo ();
                        serializedObject.DestroyUnreferencedSubassets (oldSubassets);
                    }
                    else
                    {
                        serializedObject.ApplyModifiedProperties ();
                    }
                }

                var length = serializedPropertyAux.arraySize;
                if (index > length - 1)
                    index = length - 1;
            }
            GUI.changed = true;
        }

        #endregion



        #region Drawing

        /// <summary> Returns the element Height. </summary>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <returns></returns>
        protected virtual float
        GetElementHeight (SerializedProperty element, int elementIndex) =>
            GetPropertyHeight (element, GUIContent.none);


        /// <summary> Draws an elements. </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        protected virtual void DrawElement (
            Rect position,
            SerializedProperty element,
            int elementIndex,
            bool isActive
        ) => PropertyField (position, element, GUIContent.none);

        /// <summary> Draws the element Background. </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        /// <param name="isFocused"></param>
        private void DrawElementBackground (
            Rect position,
            SerializedProperty element,
            int elementIndex,
            bool isActive,
            bool isFocused)
        {
            if (isActive)
            {
                var isProSkin = EditorGUIUtility.isProSkin;
                position.xMax += isProSkin ? 1 : 0;
                position.yMin -= isProSkin ? 0 : 1;
                position.yMax += isProSkin ? 2 : 1;
            }

            defaultBehaviours.DrawElementBackground (
                rect: position,
                index: elementIndex,
                selected: isActive,
                focused: isFocused,
                draggable: true
            );

            if (IsRepaint () && element != null)
            {
                var fillStyle = _elementBackgroundStyle;
                var fillRect = position;
                fillRect.xMin += 2;
                fillRect.xMax -= 2;
                fillRect.yMin += 1;
                fillRect.yMax -= 1;

                var backgroundColor = (_backgroundColor == Color.black) ?
                GUI.backgroundColor :
                _backgroundColor;

                if (_onBackgroundColor != null)
                    _onBackgroundColor.Invoke (
                        array: serializedProperty,
                        index: elementIndex,
                        backgroundColor: ref _backgroundColor
                    );

                using (BackgroundColorScope (backgroundColor))
                using (ColorAlphaScope (isActive ? 0.5f : 1))
                {
                    fillStyle.Draw (fillRect, false, false, false, false);
                }
            }
        }

        /// <summary> On Nex GUI Frame. </summary>
        /// <param name="action"></param>
        protected void OnNextGUIFrame(Action action) =>
            _onNextGUIFrame += action;


        /// <summary>  </summary>
        /// <param name="menu"></param>
        /// <param name="elementIndex"></param>
        protected virtual void
        PopulateElementContextMenu (GenericMenu menu, int elementIndex)
        {
            var serializedPropertyAux = serializedProperty;
            var serializedObject = serializedPropertyAux.serializedObject;

            menu.AddItem (_cutLabel, false, () =>
                   OnNextGUIFrame (() => CutElement (elementIndex)));
            menu.AddItem (_copyLabel, false, () =>
                   CopyElement (elementIndex));

            var content = ClipboardContent.Deserialize (
                EditorGUIUtility.systemCopyBuffer
            );

            var canPaste = CanPaste (content);

            if (canPaste) menu.AddItem (_pasteLabel, false, () =>
                   OnNextGUIFrame (() => PasteElement (elementIndex, content)));
            else menu.AddDisabledItem (_pasteLabel);

            if (displayAdd)
            {
                menu.AddSeparator (string.Empty);
                menu.AddItem (new GUIContent ("Insert Above"), false, () =>
                      OnNextGUIFrame (() => InsertElement (elementIndex)));
                menu.AddItem (new GUIContent ("Insert Below"), false, () =>
                      OnNextGUIFrame (() => InsertElement (elementIndex + 1)));
            }

            if (displayAdd && displayRemove)
                menu.AddSeparator (string.Empty);

            if (displayRemove)
                menu.AddItem (new GUIContent ("Remove"), false, () =>
                      OnNextGUIFrame (() => DeleteElement (elementIndex)));
        }

        /// <summary> Returns the Height of a property. </summary>
        /// <param name="property"></param>
        /// <returns> Height. </returns>
        protected float GetPropertyHeight (SerializedProperty property) =>
            EditorGUI.GetPropertyHeight (
                property: property,
                includeChildren: true
            );

        /// <summary> Returns the Height of a property. </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns> Height. </returns>
        protected float
        GetPropertyHeight (SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight (
                property: property,
                label: label,
                includeChildren: true
            );

        /// <summary> Draws a property field. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        protected void
        PropertyField (Rect position, SerializedProperty property) =>
            EditorGUI.PropertyField (
                position: position,
                property: property,
                includeChildren: true
            );

        /// <summary> Draws a property field. </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        protected void PropertyField (
            Rect position,
            SerializedProperty property,
            GUIContent label
        ) => EditorGUI.PropertyField (
                position: position,
                property: property,
                label: label,
                includeChildren: true
            );

        /// <summary> Draws an horizontal line. </summary>
        /// <param name="position"></param>
        protected static void DrawHorizontalLine (Rect position)
        {
            if (IsRepaint ())
            {
                var style = _eyeDropperHorizontalLine;
                position.height = 1;
                var color = GUI.color;
                GUI.color = new Color (1, 1, 1, 0.75f);
                style.Draw (position, false, false, false, false);
                GUI.color = color;
            }
        }

        /// <summary> Wether is repaint. </summary>
        /// <returns> Wether is repaint. </returns>
        protected static bool IsRepaint ()
        {
            var @event = Event.current;
            return @event != null && @event.type == EventType.Repaint;
        }

        /// <summary> Draws the header. </summary>
        /// <param name="position"></param>
        private void DrawHeader (Rect position)
        {
            defaultBehaviours.DrawHeaderBackground (position);
            position.xMin += 16;
            position.y += 1;
            position.height = EditorGUIUtility.singleLineHeight;

            var foldoutRect = position;
            foldoutRect.width -= 50;
            var property = serializedProperty;
            var wasExpanded = property.isExpanded;
            var isExpanded = EditorGUI.Foldout (foldoutRect, wasExpanded, _label);
            if (isExpanded != wasExpanded)
                property.isExpanded = isExpanded;

            DrawHeaderButtons (position);
        }

        /// <summary> Draws headers button. </summary>
        /// <param name="position"></param>
        private void DrawHeaderButtons (Rect position)
        {
            position.yMin += 3;
            float rightEdge = position.xMax;
            float leftEdge = rightEdge - 8f;
            if (displayAdd)
                leftEdge -= 25;
            if (displayRemove)
                leftEdge -= 25;
            position = new Rect (leftEdge, position.y, rightEdge - leftEdge, position.height);
            Rect addRect = new Rect (leftEdge + 4, position.y - 3, 25, 13);
            Rect removeRect = new Rect (rightEdge - 29, position.y - 3, 25, 13);
            if (displayAdd)
                DrawAddButton (addRect);
            if (displayRemove)
                DrawRemoveButton (removeRect);
        }

        /// <summary> Draws add button. </summary>
        /// <param name="position"></param>
        private void DrawAddButton (Rect position)
        {
            var canAdd = onCanAddCallback == null || onCanAddCallback (this);
            var disabled = !canAdd;
            using (new EditorGUI.DisabledScope (disabled))
            {
                var style = PreButton;
                var content =
                    onAddDropdownCallback != null
                    ? IconToolbarPlusMore
                    : IconToolbarPlus;
                if (GUI.Button (position, content, style))
                {
                    if (onAddDropdownCallback != null)
                        onAddDropdownCallback (position, this);
                    else onAddCallback?.Invoke (this);
                    if (onChangedCallback != null)
                        onChangedCallback.Invoke (this);
                }
            }
        }

        /// <summary> Draws remove button. </summary>
        /// <param name="position"></param>
        private void DrawRemoveButton (Rect position)
        {
            var disabled = index < 0 || index > count;
            if (disabled == false)
            {
                var canRemove = onCanRemoveCallback == null || onCanRemoveCallback (this);
                disabled |= !canRemove;
            }
            using (new EditorGUI.DisabledScope (disabled))
            {
                var style = PreButton;
                var content = IconToolbarMinus;
                if (GUI.Button (position, content, style))
                {
                    if (onRemoveCallback != null)
                        onRemoveCallback.Invoke (this);
                    if (onChangedCallback != null)
                        onChangedCallback.Invoke (this);
                }
            }
        }

        /// <summary> Updates Label. </summary>
        /// <param name="label"></param>
        private void UpdateLabel (GUIContent label)
        {
            _label.image = label.image;

            var tooltip = label.tooltip;
            if (string.IsNullOrEmpty (tooltip))
            {
                tooltip = serializedProperty.tooltip;
            }
            _label.tooltip = tooltip;

            var arraySize = serializedProperty.arraySize;

            var listHeaderFormat =
                (arraySize != 1)
                ? _pluralListHeaderFormat
                : _singularListHeaderFormat;

            var text = label.text ?? string.Empty;
            text = string.Format (listHeaderFormat, text, arraySize).Trim ();
            _label.text = text;
        }

        /// <summary> Updates element heights. </summary>
        private void UpdateElementHeights ()
        {
            var primaryProperty = serializedProperty;
            var elementCount = primaryProperty.arraySize;
            _elementHeights.Clear ();
            _elementHeights.Capacity = elementCount;
            for (int i = 0; i < elementCount; ++i)
                _elementHeights.Add (0f);

            if (primaryProperty.isExpanded)
            {
                var spacing = EditorGUIUtility.standardVerticalSpacing;
                var arrayCount = 0;
                foreach (var array in serializedProperties)
                {
                    for (int i = 0; i < elementCount; ++i)
                    {
                        var element = array.GetArrayElementAtIndex (i);
                        var elementHeightAux = GetElementHeight (element, i);
                        if (arrayCount > 0)
                            elementHeightAux += spacing;
                        switch (parallelListLayout)
                        {
                            case ParallelListLayout.Rows:
                            _elementHeights[i] += elementHeightAux;
                            break;

                            case ParallelListLayout.Columns:
                            _elementHeights[i] = Mathf.Max (
                                    _elementHeights[i],
                                    elementHeightAux)
                            ;
                            break;
                        }
                    }
                    arrayCount += 1;
                }

                for (int i = 0; i < elementCount; ++i)
                {
                    var elementHeightAux = _elementHeights[i];
                    _elementHeights[i] = AddElementPadding (elementHeightAux);
                }
            }
        }

        /// <summary> Draws Rows. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        private void DrawElementRows (Rect position, int elementIndex, bool isActive)
        {
            var spacing = EditorGUIUtility.standardVerticalSpacing;
            var loopCounter = 0;

            foreach (var array in serializedProperties)
            {
                if (loopCounter++ > 0)
                    position.y += spacing;

                var element = array.GetArrayElementAtIndex (elementIndex);
                position.height = GetElementHeight (element, elementIndex);
                DrawElement (position, element, elementIndex, isActive);
                position.y += position.height;
            }
        }

        /// <summary> Draws element columns. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        private void DrawElementColumns (Rect position, int elementIndex, bool isActive)
        {
            const float columnSpacing = 5;
            var lastColumnXMax = position.xMax;
            var columnCount = serializedProperties.Length;
            var columnSpaceCount = columnCount - 1;
            var columnSpaceWidth = columnSpacing * columnSpaceCount;
            var columnWidth = (position.width - columnSpaceWidth) / columnCount;
            columnWidth = Mathf.Floor (columnWidth);
            position.width = columnWidth;
            var loopCounter = 0;
            foreach (var array in serializedProperties)
            {
                if (loopCounter++ > 0)
                    position.x += columnSpacing + columnWidth;

                if (loopCounter == columnCount)
                    position.xMax = lastColumnXMax;

                var element = array.GetArrayElementAtIndex (elementIndex);
                position.height = GetElementHeight (element, elementIndex);
                DrawElement (position, element, elementIndex, isActive);
            }
        }

        /// <summary> Handles events. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        private void HandleElementEvents (Rect position, int elementIndex)
        {
            var @event = Event.current;
            if (@event == null)
                return;

            var handleRect = position;
            var menuRect = Rect.zero;
            if (ShowElementHeader)
            {
                handleRect.width += 1;
                menuRect = position;
                menuRect.xMin = menuRect.xMax - 16;
            }
            else
            {
                handleRect.width = 19;
            }

            var isLeftMouseInMenuRect =
                @event.button == 0 &&
                menuRect.Contains (@event.mousePosition);

            var isRightMouseInHandleRect =
                @event.button == 1 &&
                handleRect.Contains (@event.mousePosition);

            var isMouseInRect =
                isLeftMouseInMenuRect ||
                isRightMouseInHandleRect;

            var isActiveElementIndex = index == elementIndex;

            switch (@event.type)
            {
                case EventType.MouseDown:
                if (isMouseInRect)
                {
                    _endEditingActiveTextField ();
                    index = elementIndex;
                    return;
                }
                break;

                case EventType.MouseUp:
                if (isMouseInRect && isActiveElementIndex)
                {
                    DoElementContextMenu (handleRect, elementIndex);
                    return;
                }
                break;
            }
        }

        /// <summary> Does element context menu. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        private void DoElementContextMenu (Rect position, int elementIndex)
        {
            position.x += 1;
            position.height = elementHeight - 1;

            var menu = new GenericMenu ();

            PopulateElementContextMenu (menu, elementIndex);

            if (menu.GetItemCount () > 0)
                menu.DropDown (position);
        }

        /// <summary> Adds padding to the elment.s </summary>
        /// <param name="elementHeight"></param>
        /// <returns></returns>
        private static float AddElementPadding (float elementHeight)
        {
            var verticalSpacing = EditorGUIUtility.standardVerticalSpacing;
            return BorderHeight
                + verticalSpacing
                + elementHeight
                + verticalSpacing
                + 1;
        }

        /// <summary> Removes padding. </summary>
        /// <param name="position"></param>
        private static void RemoveElementPadding (ref Rect position)
        {
            var verticalSpacing = EditorGUIUtility.standardVerticalSpacing;
            position.yMin += BorderHeight;
            position.yMin += verticalSpacing;
            position.yMax -= verticalSpacing;
            position.yMax -= 1;
        }

        #endregion



        #region Deferred Scopes

        /// <summary> Deferred Scopes for background.. </summary>
        /// <param name="newColor"></param>
        /// <returns> Deferred </returns>
        protected static Deferred BackgroundColorScope (Color newColor)
        {
            var oldColor = GUI.backgroundColor;
            GUI.backgroundColor = newColor;
            return new Deferred (() => GUI.backgroundColor = oldColor);
        }

        /// <summary> Deferret color scope. </summary>
        /// <param name="newColor"></param>
        /// <returns> Deferred </returns>
        protected static Deferred ColorScope (Color newColor)
        {
            var oldColor = GUI.color;
            GUI.color = newColor;
            return new Deferred (() => GUI.color = oldColor);
        }

        /// <summary> Deferred Color Alpha Scope. </summary>
        /// <param name="a"></param>
        /// <returns> Deferred </returns>
        protected static Deferred ColorAlphaScope (float a)
        {
            var oldColor = GUI.color;
            GUI.color = new Color (1, 1, 1, a);
            return new Deferred (() => GUI.color = oldColor);
        }

        /// <summary> Deferred Indent. </summary>
        /// <param name="indent"></param>
        /// <returns> Deferred </returns>
        protected static Deferred IndentLevelScope (int indent = 1)
        {
            EditorGUI.indentLevel += indent;
            return new Deferred (() => EditorGUI.indentLevel -= indent);
        }

        /// <summary> Returns a disposable element. </summary>
        /// <param name="newLabelWidth"></param>
        /// <returns></returns>
        protected IDisposable LabelWidthScope (float newLabelWidth)
        {
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = (int) newLabelWidth;
            return new Deferred (
                () => EditorGUIUtility.labelWidth = oldLabelWidth
            );
        }

        #endregion



        #region Callbacks

        /// <summary> Called on select. </summary>
        /// <param name="reorderableList"></param>
        private void OnSelectCallback (ReorderableList reorderableList) =>
            _dragIndex = reorderableList.index;

        /// <summary> Called on reording. </summary>
        /// <param name="reorderableList"></param>
        private void OnReorderCallback (ReorderableList reorderableList)
        {
            var dragIndex = _dragIndex;
            if (dragIndex < 0)
                return;

            var dropIndex = reorderableList.index;
            if (dropIndex < 0)
                return;

            try
            {
                for (int i = 1; i < serializedProperties.Length; ++i)
                {
                    var array = serializedProperties[i];
                    array.MoveArrayElement (dragIndex, dropIndex);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException (ex);
            }
        }

        /// <summary> Called on Add. </summary>
        /// <param name="reorderableList"></param>
        private void OnAddCallback (ReorderableList reorderableList)
        {
            serializedProperty.isExpanded = true;
            InsertElement (serializedProperty.arraySize);
        }

        /// <summary> Called on can remove. </summary>
        /// <param name="reorderableList"></param>
        /// <returns> Wether can remove. </returns>
        private bool OnCanRemoveCallback (ReorderableList reorderableList) =>
            serializedProperty.isExpanded;

        /// <summary> Called on Remove. </summary>
        /// <param name="reorderableList"></param>
        private void OnRemoveCallback (ReorderableList reorderableList) =>
            DeleteElement (index);

        /// <summary> Called on element height change. </summary>
        /// <param name="elementIndex"></param>
        /// <returns> Height. </returns>
        private float ElementHeightCallback (int elementIndex) =>
            _elementHeights[elementIndex];

        /// <summary> Called on draw element. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        /// <param name="isFocused"></param>
        private void DrawElementCallback
        (Rect position, int elementIndex, bool isActive, bool isFocused)
        {
            var primaryProperty = serializedProperty;
            if (primaryProperty.isExpanded)
            {
                RemoveElementPadding (ref position);
                position.xMin += DrawElementIndent;
                switch (parallelListLayout)
                {
                    case ParallelListLayout.Rows:
                    DrawElementRows (position, elementIndex, isActive);
                    break;
                    case ParallelListLayout.Columns:
                    DrawElementColumns (position, elementIndex, isActive);
                    break;
                }
            }
        }

        /// <summary> Called on draw empty element. </summary>
        /// <param name="position"></param>
        private void DrawEmptyElementCallback (Rect position)
        {
            position.y += 2;
            EditorGUI.BeginDisabledGroup (disabled: true);
            EditorGUI.LabelField (position, "List is Empty");
            EditorGUI.EndDisabledGroup ();
        }

        /// <summary> Called on draw background. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        /// <param name="isFocused"></param>
        private void DrawElementBackgroundCallback
        (Rect position, int elementIndex, bool isActive, bool isFocused)
        {
            var array = this.serializedProperty;
            if (array.isExpanded == false)
                return;

            var length = array.arraySize;
            var element = default (SerializedProperty);

            var activeIndex = base.index;
            if (activeIndex == elementIndex && isActive == false)
            {
                // HACK: ReorderableList invokes this callback with the
                // wrong elementIndex.
                var nonDragTargetIndices = NonDragTargetIndices;
                if (nonDragTargetIndices != null)
                {
                    elementIndex = nonDragTargetIndices[elementIndex];
                }
            }

            if (elementIndex >= 0 && elementIndex < length)
            {
                // HACK: ReorderableList invokes this callback with the
                // wrong height.
                position.height = ElementHeightCallback (elementIndex);
                element = array.GetArrayElementAtIndex (elementIndex);
            }

            DrawElementBackground (
                position,
                element,
                elementIndex,
                isActive,
                isFocused
            );

            if (element != null)
            {
                HandleElementEvents (position, elementIndex);
            }

            {
                var upperEdge = position;
                upperEdge.xMin += 2;
                upperEdge.xMax -= 2;
                upperEdge.y -= 1;
                DrawHorizontalLine (upperEdge);
            }

            {
                var lowerEdge = position;
                lowerEdge.xMin += 2;
                lowerEdge.xMax -= 2;
                lowerEdge.y += lowerEdge.height;
                lowerEdge.y -= 1;
                DrawHorizontalLine (lowerEdge);
            }
        }

        /// <summary> Called on draw header. </summary>
        /// <param name="position"></param>
        private void DrawHeaderCallback (Rect position)
        {
            // DoGUI draws the header content after the list is drawn
        }

        /// <summary> Called on draw footer. </summary>
        /// <param name="position"></param>
        private void DrawFooterCallback (Rect position)
        {
            if (_showFooterButtons)
                defaultBehaviours.DrawFooter (position, this);

            position.xMin += 2;
            position.xMax -= 2;
            position.y -= 6;
            DrawHorizontalLine (position);
        }

        #endregion



        #region Nested Classes

        /// <summary> Strcut for disposable actions. </summary>
        protected struct Deferred : IDisposable
        {
            //  Disposable action.
            private readonly Action _onDispose;

            /// <summary> Constructor. </summary>
            /// <param name="onDispose"></param>
            public Deferred (Action onDispose) => _onDispose = onDispose;

            /// <summary> Dispose the action. </summary>
            public void Dispose () => _onDispose?.Invoke ();
        }

        /// <summary> Class to clipboard content. </summary>
        [Serializable]
        private class ClipboardContent
        {
            /// <summary> Array of elements on clipboard. </summary>
            public ClipboardElement[] elements;

            /// <summary> Constructor. </summary>
            /// <param name="elementCount"></param>
            public ClipboardContent (int elementCount) =>
                elements = new ClipboardElement[elementCount];

            /// <summary> Deserealize a clipboard. </summary>
            /// <param name="s"></param>
            /// <returns> Clipboard content. </returns>
            public static ClipboardContent Deserialize (string s)
            {
                try
                {
                    return JsonUtility.FromJson<ClipboardContent> (s);
                }
                catch
                {
                    return null;
                }
            }

            /// <summary> Serialize content. </summary>
            /// <returns> Text. </returns>
            public string Serialize ()
            {
                return JsonUtility.ToJson (this);
            }
        }

        /// <summary> Serialized content. </summary>
        [Serializable]
        private struct ClipboardElement
        {
            public string type;
            public string json;
        }

        #endregion

    }
}
