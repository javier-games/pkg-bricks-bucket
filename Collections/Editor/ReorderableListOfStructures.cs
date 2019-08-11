using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BricksBucket.Collections
{
    /// <summary>
    ///
    /// Reorderable List of Structures.
    ///
    /// <para>
    /// Implementation of a reorderable lis of values to draw structures.
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
    internal class ReorderableListOfStructures : ReorderableListOfValues
    {

        #region Class Members

        protected float _idealLabelWidth;

        protected static readonly GUIStyle _headerBackgroundStyle = "Toolbar";

        #endregion



        #region Accessors

        /// <summary> Wether or not show element header. </summary>
        public override bool ShowElementHeader
        {
            get { return HasElementHeaderFormat; }
        }

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of list of structures. </summary>
        /// <param name="attribute"></param>
        /// <param name="property"></param>
        /// <param name="listType"></param>
        /// <param name="elementType"></param>
        public ReorderableListOfStructures (
            ReorderableListAttribute attribute,
            SerializedProperty property,
            Type listType,
            Type elementType)
        : base (attribute, property, listType, elementType) { }

        #endregion



        #region Reorderable Lost Of Values Overrides

        /// <summary> Returns the height of a property. </summary>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <returns></returns>
        protected override float
        GetElementHeight (SerializedProperty element, int elementIndex)
        {
            var properties = element.EnumerateChildProperties ();
            return GetElementHeight (properties);
        }

        /// <summary> Returns Indent drawed size. </summary>
        protected override float DrawElementIndent { get { return 12; } }

        /// <summary> Draws an element. </summary>
        /// <param name="position"></param>
        /// <param name="element"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        protected override void DrawElement (
            Rect position,
            SerializedProperty element,
            int elementIndex, bool isActive
        ) {
            var properties = element.EnumerateChildProperties ();
            DrawElement (position, properties, elementIndex, isActive);
        }

        #endregion



        #region Class Implementation

        /// <summary> Draws an element. </summary>
        /// <param name="position"></param>
        /// <param name="properties"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        protected void DrawElement (
            Rect position,
            IEnumerable<SerializedProperty> properties,
            int elementIndex,
            bool isActive
        ) {
            var spacing = EditorGUIUtility.standardVerticalSpacing;
            if (ShowElementHeader)
            {
                DrawElementHeader (position, elementIndex, isActive);
                position.y += headerHeight + spacing;
            }

            var labelWidth = Mathf.Min (
                a: _idealLabelWidth,
                b: position.width * 0.4f
            );

            using (LabelWidthScope (labelWidth))
            {
                var propertyCount = 0;
                foreach (var property in properties)
                {
                    if (propertyCount++ > 0)
                        position.y += spacing;

                    position.height = GetPropertyHeight (property);
                    PropertyField (position, property);
                    position.y += position.height;
                }
            }
        }

        /// <summary> Returns element Height. </summary>
        /// <param name="properties"></param>
        /// <returns> Element Height. </returns>
        protected float
        GetElementHeight (IEnumerable<SerializedProperty> properties)
        {
            var spacing = EditorGUIUtility.standardVerticalSpacing;
            var height = 0f;

            if (ShowElementHeader)
                height += headerHeight + spacing;

            _idealLabelWidth = 0f;
            var labelStyle = EditorStyles.label;
            var labelContent = new GUIContent ();

            var propertyCount = 0;
            foreach (var property in properties)
            {
                if (propertyCount++ > 0)
                    height += spacing;

                height += GetPropertyHeight (property);

                labelContent.text = property.displayName;
                var minLabelWidth = labelStyle.CalcSize (labelContent).x;
                _idealLabelWidth = Mathf.Max (_idealLabelWidth, minLabelWidth);
            }
            _idealLabelWidth += 8;
            return height;
        }

        /// <summary> Draws Element Header. </summary>
        /// <param name="position"></param>
        /// <param name="elementIndex"></param>
        /// <param name="isActive"></param>
        private void
        DrawElementHeader (Rect position, int elementIndex, bool isActive)
        {
            position.xMin -= DrawElementIndent;
            position.height = headerHeight;

            var titleContent = base._titleContent;

            titleContent.text = HasElementHeaderFormat ?
                string.Format (_elementHeaderFormat, elementIndex) :
                elementIndex.ToString ();

            var titleStyle = EditorStyles.boldLabel;

            var titleWidth =
                titleStyle
                .CalcSize (titleContent)
                .x;

            if (IsRepaint ())
            {
                var fillRect = position;
                fillRect.xMin -= draggable ? 18 : 4;
                fillRect.xMax += 4;
                fillRect.y -= 2;

                var fillStyle = _headerBackgroundStyle;

                using (ColorAlphaScope (0.75f))
                {
                    fillStyle.Draw (fillRect, false, false, false, false);
                }

                var embossStyle = EditorStyles.whiteBoldLabel;
                var embossRect = position;
                embossRect.yMin -= 0;

                EditorGUI.BeginDisabledGroup (true);
                embossStyle.Draw (
                    position: embossRect,
                    content: titleContent,
                    isHover: false,
                    isActive: false,
                    on: false,
                    hasKeyboardFocus: false
                );
                EditorGUI.EndDisabledGroup ();

                var titleRect = position;
                titleRect.yMin -= 1;
                titleRect.width = titleWidth;
                titleStyle.Draw (
                    position: titleRect,
                    content: titleContent,
                    isHover: false,
                    isActive: false,
                    on: false,
                    hasKeyboardFocus: false
                );

                var menuRect = position;
                menuRect.xMin = menuRect.xMax - 16;
                menuRect.yMin += 4;
                _contextMenuButtonStyle.Draw (
                    position: menuRect,
                    isHover: false,
                    isActive: false,
                    on: false,
                    hasKeyboardFocus: false
                );
            }
        }

        #endregion
    }
}
