using System;
using UnityEngine;

/*
 *
 * Attributes blaced in the same sirenix name space to just use one
 * library for all editor attributes.
 *
 */
namespace Sirenix.OdinInspector
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
    public class AutoFillAttribute : PropertyAttribute { }

    /// <summary>
    ///
    /// Defined Localizations Attribute.
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

        /// <summary> Creates new instance of Defined Localizations. </summary>
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
    public class EnumMaskAttribute : PropertyAttribute
    {

        #region Class Members

        /// <summary> Reference to the type of enum. </summary>
        public readonly Type EnumType;

        #endregion



        #region Constructor

        /// <summary> Creates a new instance of attribute. </summary>
        /// <param name="enumType"> Type of enum. </param>
        public EnumMaskAttribute (Type enumType)
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
    /// On Save Attribute.
    ///
    /// <para>
    /// Custom callback called on save.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    ///
    /// </summary>
    [AttributeUsage (AttributeTargets.Method)]
    public class OnSaveAttribute : PropertyAttribute { }
}
