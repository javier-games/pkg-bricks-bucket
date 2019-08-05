using System;
using UnityEngine;

namespace BricksBucket
{
    /// <summary>
    /// 
    /// Auto Property Attribute.
    /// 
    /// <para>
    /// Automatically assign components to the Game Object
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class AutoPropertyAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Button Method Attribute.
    /// 
    /// <para>
    /// 
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// <para>
    /// Original version by @Kaynn-Cahya
    /// https://github.com/Kaynn-Cahya
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Method)]
    public class ButtonMethodAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Conditional Field Attribute.
    /// 
    /// <para>
    /// Hides or show the property according to other property.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {

        #region Class Members

        /// <summary> Name of the property to check. </summary>
        public readonly string PropertyToCheck;

        /// <summary> Value to compare. </summary>
        public readonly object CompareValue;

        /// <summary> Inverts the behaviour. </summary>
        public readonly bool Inverse;

        #endregion



        #region Constructor

        /// <summary> Creates an instance of this attribute. </summary>
        /// <param name="propertyToCheck"> Property to check. </param>
        /// <param name="compareValue"> Use this value to compare. </param>
        /// <param name="inverse"> Inverse the behaviour. </param>
        public ConditionalFieldAttribute (
            string propertyToCheck,
            object compareValue = null,
            bool inverse = false
        )
        {
            PropertyToCheck = propertyToCheck;
            CompareValue = compareValue;
            Inverse = inverse;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// Defined Values Attribute.
    /// 
    /// <para>
    /// Creates Popup with predefined values for string, int or float property.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class DefinedValuesAttribute : PropertyAttribute
    {

        #region Class Members

        /// <summary> Collection of defined values. </summary>
        public readonly object[] ValuesArray;

        #endregion



        #region Constructor

        /// <summary> Creates new instance of Defined Values. </summary>
        /// <param name="definedValues"> Defined values to use. </param>
        public DefinedValuesAttribute (params object[] definedValues)
        {
            ValuesArray = definedValues;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// Sprite Layer Attribute.
    /// 
    /// <para>
    /// Creates Popup with sprite layers available.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class SpriteLayerAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Layer Attribute.
    /// 
    /// <para>
    /// Shows an int field as LayerMask.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class LayerAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Custom Layer Attribute.
    /// 
    /// <para>
    /// Shows an int field as a mask for enums.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class CustomMaskAttribute : PropertyAttribute
    {

        #region Class Members

        /// <summary> Reference to the type of enum. </summary>
        public readonly Type EnumType;

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of attribute. </summary>
        /// <param name="enumType"> Type of enum. </param>
        public CustomMaskAttribute (Type enumType)
        {
            EnumType = enumType;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// Tag Attribute.
    /// 
    /// <para>
    /// Shows an int field as LayerMask.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// <para>
    /// Original version by @Kaynn-Cahya
    /// https://github.com/Kaynn-Cahya
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class TagAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Tag Attribute.
    /// 
    /// <para>
    /// Shows an int field as LayerMask.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// <para>
    /// Original version by @Kaynn-Cahya
    /// https://github.com/Kaynn-Cahya
    /// </para>
    /// 
    /// </summary>
    public class MinMaxRangeAttribute : Attribute
    {

        #region Class Members

        /// <summary> Minimum value to use for the property. </summary>
        public readonly float Min;

        /// <summary> Maximum value to use for the property. </summary>
        public readonly float Max;

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of min maxx attribute. </summary>
        /// <param name="min"> Minimum value to use for the property. </param>
        /// <param name="max"> Maximum value to use for the property. </param>
        public MinMaxRangeAttribute (float min, float max)
        {
            Min = min;
            Max = max;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// Positive Value Only Attribute.
    /// 
    /// <para>
    /// Constrains the value to be positive only.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    ///
    /// <para>
    /// Original version by @Kaynn-Cahya
    /// https://github.com/Kaynn-Cahya
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class PositiveValueOnlyAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Positive Value Only Attribute.
    /// 
    /// <para>
    /// Constrins the value to be positive only.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the MyBox project by @deadcows.
    /// https://github.com/Deadcows/MyBox
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class DisplayOnlyAttribute : PropertyAttribute { }

    /// <summary>
    /// 
    /// Searchable Enum Attribute.
    /// 
    /// <para>
    /// Improved enum selector popup. The enum list is scrollable and
    /// can be filtered by typing.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// <para>
    /// Based in the UnityEditorJunkie project by @roboryantron.
    /// https://github.com/roboryantron/UnityEditorJunkie
    /// </para>
    /// 
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class SearchableEnumAttribute : PropertyAttribute { }
}