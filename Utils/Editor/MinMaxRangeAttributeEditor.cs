using UnityEngine;
using UnityEditor;

namespace BricksBucket
{
    /// <summary>
    ///
    /// Min Max Range Int Attribute Drawer
    ///
    /// <para>
    /// Drawer of a range of integers values.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the Scriptable Object Demo project by @richardfine
    /// https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/
    /// </para>
    ///
    /// </summary>
    [CustomPropertyDrawer (typeof (RangeIntSerialized), true)]
    public class MinMaxRangeIntAttributeDrawer : PropertyDrawer
    {

        #region Class Members

        /// <summary> Width of label. </summary>
        const float RangeBoundsLabelWidth = 40f;

        #endregion



        #region Property Drawer Override

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
                rangeMin = ranges[0].Min;
                rangeMax = ranges[0].Max;
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

    /// <summary>
    ///
    /// Min Max Range Float Attribute Drawer
    ///
    /// <para>
    /// Drawer of a range of floating values.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the Scriptable Object Demo project by @richardfine
    /// https://bitbucket.org/richardfine/scriptableobjectdemo/src/default/
    /// </para>
    ///
    /// </summary>
    [CustomPropertyDrawer (typeof (RangeFloatSerialized), true)]
    public class MinMaxRangeFloatAttributeDrawer : PropertyDrawer
    {

        #region Class Members

        /// <summary> Width of label. </summary>
        const float RangeBoundsLabelWidth = 40f;

        #endregion



        #region Property Drawer Overrides

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
                rangeMin = ranges[0].Min;
                rangeMax = ranges[0].Max;
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
