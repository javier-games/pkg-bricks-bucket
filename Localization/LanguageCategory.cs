using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;


#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable InconsistentNaming

namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// Language Category
    ///
    /// <para>
    /// Structure to categorize languages.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    [System.Serializable]
    public struct LanguageCategory
    {



        #region  Fields

        /// <summary>
        /// Code to identify language category.
        /// </summary>
        [SerializeField]
        [Tooltip("Code to identify language category.")]
        private string _code;

        /// <summary>
        /// Name for language category.
        /// </summary>
        [SerializeField]
        [Tooltip("Name for language category.")]
        private string _name;

        /// <summary>
        /// Windows Language Code ID.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip("Windows Language Code ID.")]
        [OnValueChanged("OnLCIDChanged")]
        private LCID _LCID;

        /// <summary>
        /// Language ISO-639 code.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip("Language ISO-639 code.")]
        [OnValueChanged("OnISOChanged")]
        private ISO639_1 _language;

        /// <summary>
        /// Country ISO-3166 code.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip("Country ISO-3166 code.")]
        [OnValueChanged("OnISOChanged")]
        private ISO3166_2 _country;

        /// <summary>
        /// Specifies a region for the language.
        /// </summary>
        [SerializeField]
        [Tooltip("Specifies a region for the language.")]
        [OnValueChanged("OnRegionChanged")]
        private string _region;

        /// <summary>
        /// Whether this category is custom.
        /// </summary>
        [SerializeField]
        [Tooltip("Whether this category is custom.")]
        [OnValueChanged("OnIsCustomChanged")]
        private bool _isCustom;

        #endregion



        #region  Properties

        /// <summary>
        /// Name to display nicely.
        /// </summary>
        public string DisplayName
        {
            get => _name;
            private set => _name = value;
        }

        /// <summary>
        /// Whether this category is custom.
        /// </summary>
        public bool IsCustom
        {
            get => _isCustom;
            private set => _isCustom = value;
        }

        /// <summary>
        /// Language ISO-639 code.
        /// </summary>
        public ISO639_1 Language
        {
            get => _isCustom ? ISO639_1.NONE : _language;
            private set => _language = value;
        }

        /// <summary>
        /// Country ISO-3166 code.
        /// </summary>
        public ISO3166_2 Country
        {
            get => _isCustom ? ISO3166_2.NONE : _country;
            private set => _country = value;
        }


        public string Region
        {
            get => _region;
            private set => _region = value;
        }

        /// <summary>
        /// Code of the language.
        /// </summary>
        public string Code
        {
            get => _code;
            private set => _code = value;
        }

        /// <summary>
        /// Windows Language Code Identifier.
        /// </summary>
        public LCID LCID
        {
            get => _LCID;
            private set => _LCID = value;
        }

        #endregion


        
        #region Editor

#if UNITY_EDITOR

        /// <summary>
        /// Called from inspector when an ISO code changed.
        /// </summary>
        internal void OnISOChanged()
        {
            if (IsCustom)
                return;

            LCID = LocalizationUtils.ToLCID(Language, Country);
            SetDisplay();
        }

        /// <summary>
        /// Called from inspector when LCID code changed.
        /// </summary>
        internal void OnLCIDChanged()
        {
            if (IsCustom)
                return;

            Country = (ISO3166_2)LocalizationUtils.ToISO3166(LCID);
            Language = (ISO639_1)LocalizationUtils.ToISO639(LCID);

            SetDisplay();
        }

        /// <summary>
        /// Called from inspector when the Region changes.
        /// </summary>
        internal void OnRegionChanged()
        {
            if (IsCustom)
                return;

            SetDisplay();
        }

        /// <summary>
        /// Sets the display name.
        /// </summary>
        private void SetDisplay()
        {
            switch (LCID)
            {
                //  There is not matching LCID.
                case LCID.NONE:

                    Code = Language.ToString();
                    DisplayName = LocalizationUtils.ISO639_Names[(int)Language];

                    if (Country != ISO3166_2.NONE)
                    {
                        Code = StringUtils.Concat(Code,"-", Country.ToString());
                        DisplayName = StringUtils.Concat(
                            DisplayName, " (",
                            LocalizationUtils.ISO3166_Names[(int)Country]
                        );
                    }

                    if (!string.IsNullOrWhiteSpace(Region))
                    {
                        Code = StringUtils.Concat(Code, "-", Region.ToUpper());

                        DisplayName = Country == ISO3166_2.NONE ?
                            StringUtils.Concat(DisplayName, " (", Region, ")") :
                            StringUtils.Concat(DisplayName, " - ", Region, ")");
                    }

                    else if(Country != ISO3166_2.NONE)
                    {
                        DisplayName = StringUtils.Concat(DisplayName, ")");
                    }

                    break;

                //  Language and country are invariant.
                case LCID.INVARIANT:

                    Code = LCID.ToString();
                    DisplayName = "Invariant Language";

                    break;

                //  It has a match.
                default:

                    var info = new CultureInfo((int)LCID);
                    Code = info.Name.ToUpper();
                    DisplayName = info.DisplayName;

                    if (!string.IsNullOrWhiteSpace(Region))
                    {
                        Code = StringUtils.Concat(Code, "-", Region.ToUpper());

                        if (DisplayName.Contains(")"))
                        {
                            DisplayName = DisplayName.Replace(")",string.Empty);
                            DisplayName = StringUtils.Concat(
                                DisplayName, " - ", Region, ")"
                            );
                        }
                        else
                        {
                            DisplayName = StringUtils.Concat(
                                DisplayName, " (", Region, ")"
                            );
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Called on is custom variable changes.
        /// </summary>
        internal void OnIsCustomChanged()
        {
            DisplayName = string.Empty;
            Code = string.Empty;
            Country = ISO3166_2.NONE;
            Region = string.Empty;
            Language = ISO639_1.NONE;
            LCID = LCID.NONE;
        }



        /// <summary>
        /// Language Category Drawer Class.
        /// </summary>
        public class LanguageCategoryDrawer : OdinValueDrawer<LanguageCategory>
        {



            #region Fields

            /// <summary>
            /// Whether the foldout is visible.
            /// </summary>
            private bool _isVisible;

            #endregion



            #region Override Methods

            /// <summary>
            /// Draws the property.
            /// </summary>
            /// <param name="label">Label of the property.</param>
            protected override void DrawPropertyLayout(GUIContent label)
            {
                var value = ValueEntry.SmartValue;

                //  Draws the label on Foldout.
                if (label != null)
                {
                    label.text = string.IsNullOrEmpty(label.text) ?
                        value.Code : label.text;
                }
                else
                {
                    label = new GUIContent(
                        value.Code,
                        string.Concat("Code ID for " , value.DisplayName , ".")
                    );
                }

                _isVisible = SirenixEditorGUI.Foldout(
                    _isVisible,
                    label,
                    SirenixGUIStyles.Foldout
                );

                //  Draws content in fold out.
                if (_isVisible)
                {
                    //  Draws LCID Options.
                    if (!value.IsCustom)
                    {

                        EditorGUI.indentLevel++;
                        EditorGUI.indentLevel++;

                        EditorGUILayout.LabelField(
                            new GUIContent("Name","Name of category."),
                            new GUIContent(value.DisplayName),
                            SirenixGUIStyles.BoldLabel
                        );

                        var children = ValueEntry.Property.Children;

                        children.Get("_LCID").Draw();
                        children.Get("_language").Draw();
                        children.Get("_country").Draw();
                        children.Get("_region").Draw();
                        children.Get("_isCustom").Draw();

                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                    }

                    //  Draws Custom options.
                    else
                    {
                        EditorGUI.indentLevel++;
                        EditorGUI.indentLevel++;

                        var children = ValueEntry.Property.Children;

                        children.Get("_code").Draw();
                        children.Get("_name").Draw();
                        children.Get("_country").Draw();
                        children.Get("_region").Draw();
                        children.Get("_isCustom").Draw();

                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                    }
                }

                ValueEntry.SmartValue = value;
            }

            #endregion
        }

#endif

        #endregion

    }
}