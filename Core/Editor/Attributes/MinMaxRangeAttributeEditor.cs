using BricksBucket.Core;
using BricksBucket.Core.Math;
using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    // ReSharper disable CommentTypo
    /// <!-- MinMaxRangeIntAttributeDrawer -->
    ///
    /// <summary>
    ///
    /// <para>
    /// Drawer of a range of integers values.
    /// </para>
    /// 
    /// <para>
    /// Based in the <see href=
    /// "https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/">
    /// Scriptable Object Demo</see> project by <see href=
    /// "https://bitbucket.org/richardfine">@richardfine</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href=
    /// "https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/">
    /// richardfine/scriptableobjectdemo</seealso>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [CustomPropertyDrawer (typeof (RangeIntSerialized), true)]
    public class MinMaxRangeIntAttributeDrawer : PropertyDrawer
    {
        #region Fields

        /// <summary> Width of label. </summary>
        private const float RangeBoundsLabelWidth = 40f;

        #endregion
        

        #region Methods Override

        /// <inheritdoc cref="PropertyDrawer.GetPropertyHeight"/>
        public override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        /// <inheritdoc cref="PropertyDrawer.OnGUI"/>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, label);

            SerializedProperty minProp = property.FindPropertyRelative ("Min");
            SerializedProperty maxProp = property.FindPropertyRelative ("Max");

            float minValue = minProp.intValue;
            float maxValue = maxProp.intValue;

            float rangeMin = 0;
            float rangeMax = 1;

            var attributes = fieldInfo.GetCustomAttributes (
                attributeType: typeof (MinMaxRangeAttribute),
                inherit: true
            );

            var ranges = (MinMaxRangeAttribute[]) attributes;
            if (ranges.Length > 0)
            {
                rangeMin = ranges[0].min;
                rangeMax = ranges[0].max;
            }

            var rangeBoundsLabel1Rect = new Rect (position)
            {
                width = RangeBoundsLabelWidth
            };
            GUI.Label (rangeBoundsLabel1Rect, new GUIContent (
                text: minValue.ToString ("F2"))
            );
            position.xMin += RangeBoundsLabelWidth;

            var rangeBoundsLabel2Rect = new Rect (position);
            rangeBoundsLabel2Rect.xMin =
                rangeBoundsLabel2Rect.xMax - RangeBoundsLabelWidth;

            GUI.Label (
                position: rangeBoundsLabel2Rect,
                content: new GUIContent (maxValue.ToString ("F2"))
            );
            position.xMax -= RangeBoundsLabelWidth;

            EditorGUI.BeginChangeCheck ();
            EditorGUI.MinMaxSlider (
                position: position,
                minValue: ref minValue,
                maxValue: ref maxValue,
                minLimit: rangeMin,
                maxLimit: rangeMax
            );

            if (EditorGUI.EndChangeCheck ())
            {
                minProp.intValue = Mathf.RoundToInt (minValue);
                maxProp.intValue = Mathf.RoundToInt (maxValue);
            }

            EditorGUI.EndProperty ();
        }

        #endregion
    }
    
    // ReSharper disable CommentTypo
    /// <!-- MinMaxRangeFloatAttributeDrawer -->
    ///
    /// <summary>
    ///
    /// <para>
    /// Min Max Range Float Attribute Drawer.
    /// </para>
    /// 
    /// <para>
    /// Based in the <see href=
    /// "https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/">
    /// Scriptable Object Demo</see> project by <see href=
    /// "https://bitbucket.org/richardfine">@richardfine</see>.
    /// </para>
    ///
    /// </summary>
    ///
    /// <seealso href=
    /// "https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/">
    /// richardfine/scriptableobjectdemo</seealso>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    // ReSharper restore CommentTypo
    [CustomPropertyDrawer (typeof (RangeFloatSerialized), true)]
    public class MinMaxRangeFloatAttributeDrawer : PropertyDrawer
    {
        #region Fields

        /// <summary> Width of label. </summary>
        private const float RangeBoundsLabelWidth = 40f;

        #endregion
        

        #region Method Overrides

        /// <summary> Called to return the Height of a property. </summary>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        /// <returns> Height to draw property.</returns>
        public override float
        GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        /// <summary> Called on GUI to draw property. </summary>
        /// <param name="position"> Position to draw property. </param>
        /// <param name="property"> Property to draw. </param>
        /// <param name="label"> Label to draw. </param>
        public override void
        OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, label);

            SerializedProperty minProp = property.FindPropertyRelative ("Min");
            SerializedProperty maxProp = property.FindPropertyRelative ("Max");

            float minValue = minProp.floatValue;
            float maxValue = maxProp.floatValue;

            float rangeMin = 0;
            float rangeMax = 1;

            var attributes = fieldInfo.GetCustomAttributes (
                attributeType: typeof (MinMaxRangeAttribute),
                inherit: true
            );

            var ranges = (MinMaxRangeAttribute[]) attributes;
            if (ranges.Length > 0)
            {
                rangeMin = ranges[0].min;
                rangeMax = ranges[0].max;
            }

            var rangeBoundsLabel1Rect = new Rect (position)
            {
                width = RangeBoundsLabelWidth
            };
            GUI.Label (rangeBoundsLabel1Rect, new GUIContent (
                text: minValue.ToString ("F2"))
            );
            position.xMin += RangeBoundsLabelWidth;

            var rangeBoundsLabel2Rect = new Rect (position);
            rangeBoundsLabel2Rect.xMin =
                rangeBoundsLabel2Rect.xMax - RangeBoundsLabelWidth;

            GUI.Label (
                position: rangeBoundsLabel2Rect,
                content: new GUIContent (maxValue.ToString ("F2"))
            );

            position.xMax -= RangeBoundsLabelWidth;

            EditorGUI.BeginChangeCheck ();
            EditorGUI.MinMaxSlider (
                position: position,
                minValue: ref minValue,
                maxValue: ref maxValue,
                minLimit: rangeMin,
                maxLimit: rangeMax
            );
            if (EditorGUI.EndChangeCheck ())
            {
                minProp.floatValue = minValue;
                maxProp.floatValue = maxValue;
            }
            EditorGUI.EndProperty ();
        }

        #endregion
    }
}
