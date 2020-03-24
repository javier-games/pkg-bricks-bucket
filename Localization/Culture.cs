using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;


#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
#endif


// ReSharper disable InconsistentNaming
namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// Culture.
    ///
    /// <para>
    /// Structure to categorize languages.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    [System.Serializable]
    public struct Culture
    {



        #region Fields

        /// <summary>
        /// Code to identify culture.
        /// </summary>
        [SerializeField]
        [Tooltip ("Code to identify language category.")]
        [OnValueChanged ("OnCodeChanged")]
        private string _code;

        /// <summary>
        /// Name for culture.
        /// </summary>
        [SerializeField]
        [Tooltip ("Name for language category.")]
        private string _name;

        /// <summary>
        /// Windows Language Code ID.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip ("Windows Language Code ID.")]
        [OnValueChanged ("OnLCIDChanged")]
        private LCID _LCID;

        /// <summary>
        /// Language ISO-639 code.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip ("Language ISO-639 code.")]
        [OnValueChanged ("OnISOChanged")]
        private ISO639_1 _language;

        /// <summary>
        /// Country ISO-3166 code.
        /// </summary>
        [SerializeField, EnumPaging]
        [Tooltip ("Country ISO-3166 code.")]
        [OnValueChanged ("OnISOChanged")]
        private ISO3166_2 _country;

        /// <summary>
        /// Specifies a region for the culture.
        /// </summary>
        [SerializeField]
        [Tooltip ("Specifies a region for the language.")]
        [OnValueChanged ("OnRegionChanged")]
        private string _region;

        /// <summary>
        /// Whether this culture is custom.
        /// </summary>
        [SerializeField]
        [Tooltip ("Whether this category is custom.")]
        [OnValueChanged ("OnIsCustomChanged")]
        private bool _isCustom;

        #endregion



        #region Properties

        /// <summary>
        /// Code of the culture.
        /// </summary>
        public string Code
        {
            get => _code;
            private set => _code = value;
        }

        /// <summary>
        /// Name to display.
        /// </summary>
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        /// <summary>
        /// Windows Language Code Identifier.
        /// </summary>
        public LCID LCID
        {
            get => _LCID;
            private set => _LCID = value;
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
            get => _country;
            private set => _country = value;
        }

        /// <summary>
        /// Region of the culture.
        /// </summary>
        public string Region
        {
            get => _region;
            private set => _region = value;
        }

        /// <summary>
        /// Whether this culture is custom.
        /// </summary>
        public bool IsCustom
        {
            get => _isCustom;
            private set => _isCustom = value;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Called from inspector when an ISO code changed.
        /// </summary>
        private void OnISOChanged ()
        {
            if (IsCustom) return;

            LCID = LocalizationUtils.ToLCID (Language, Country);
            UpdateData ();
        }

        /// <summary>
        /// Called from inspector when LCID code changed.
        /// </summary>
        private void OnLCIDChanged ()
        {
            if (IsCustom) return;

            Country = (ISO3166_2) LocalizationUtils.ToISO3166 (LCID);
            Language = (ISO639_1) LocalizationUtils.ToISO639 (LCID);
            UpdateData ();
        }

        /// <summary>
        /// Called from inspector when the Region changes.
        /// </summary>
        private void OnRegionChanged ()
        {
            if (IsCustom) return;
            UpdateData ();
        }

        /// <summary>
        /// Called from inspector when the Code Changes.
        /// </summary>
        private void OnCodeChanged () => Code = Code.ToCodeFormat ();

        /// <summary>
        /// Called on is custom variable changes.
        /// </summary>
        private void OnIsCustomChanged ()
        {
            Name = string.Empty;
            Code = string.Empty;
            Country = ISO3166_2.NONE;
            Region = string.Empty;
            Language = ISO639_1.NONE;
            LCID = LCID.NONE;
        }

        /// <summary>
        /// Fetch all data.
        /// </summary>
        private void UpdateData ()
        {
            // It has a match for an LCID.
            if (LCID != LCID.NONE)
            {
                if (LCID == LCID.INVARIANT)
                {
                    Code = LCID.ToString ();
                    Name = "Invariant Language";
                }
                else
                {
                    var info = new CultureInfo ((int) LCID);
                    Code = info.Name.ToUpper ().Replace ("-", "_");
                    Name = info.DisplayName;

                    if (string.IsNullOrWhiteSpace (Region)) return;
                    Code = StringUtils.Concat (Code, "_", Region.ToUpper ());

                    if (Name.Contains (")"))
                    {
                        Name = Name.Replace (")", string.Empty);
                        Name = StringUtils.Concat (
                            Name, " - ", Region, ")"
                        );
                    }
                    else
                    {
                        Name = StringUtils.Concat (
                            Name, " (", Region, ")"
                        );
                    }
                }
            }

            // It has not a match for language and country.
            else
            {
                Code = Language.ToString ();
                Name = LocalizationUtils.ISO639_Names[(int) Language];

                if (Country != ISO3166_2.NONE)
                {
                    Code = StringUtils.Concat (Code, "_", Country.ToString ());
                    Name = StringUtils.Concat (
                        Name, " (",
                        LocalizationUtils.ISO3166_Names[(int) Country]
                    );
                }

                if (!string.IsNullOrWhiteSpace (Region))
                {
                    Code = StringUtils.Concat (Code, "_", Region.ToUpper ());

                    Name = Country == ISO3166_2.NONE
                        ? StringUtils.Concat (Name, " (", Region, ")")
                        : StringUtils.Concat (Name, " - ", Region, ")");
                }

                else if (Country != ISO3166_2.NONE)
                {
                    Name = StringUtils.Concat (Name, ")");
                }
            }
        }

        #endregion



        #region Editor

#if UNITY_EDITOR

        #region Drawer

        /// <summary>
        /// Language Category Drawer Class.
        /// </summary>
        public class LanguageCategoryDrawer : OdinValueDrawer<Culture>
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
            protected override void DrawPropertyLayout (GUIContent label)
            {
                var value = ValueEntry.SmartValue;

                //  Draws the label on Foldout.
                if (label != null)
                {
                    label.text = string.IsNullOrEmpty (label.text)
                        ? value.Code
                        : label.text;
                }
                else
                {
                    label = new GUIContent (value.Name, value.Code);
                }

                // Draws the fold out.
                _isVisible = SirenixEditorGUI.Foldout (
                    _isVisible,
                    label,
                    SirenixGUIStyles.Foldout
                );

                //  Draws content in fold out.
                if (_isVisible)
                {
                    EditorGUI.indentLevel++;
                    EditorGUI.indentLevel++;

                    var children = ValueEntry.Property.Children;

                    //  Draws LCID Options.
                    if (!value.IsCustom)
                    {
                        GUI.enabled = false;
                        EditorGUILayout.LabelField (
                            new GUIContent (
                                "Code", "Code to Identify language category."
                            ),
                            new GUIContent (value.Code),
                            EditorStyles.textField
                        );

                        EditorGUILayout.LabelField (
                            new GUIContent (
                                "Name", "Name for language category."
                            ),
                            new GUIContent (value.Name),
                            EditorStyles.textField
                        );
                        GUI.enabled = true;

                        children.Get ("_LCID").Draw ();
                        children.Get ("_language").Draw ();
                        children.Get ("_country").Draw ();
                        children.Get ("_region").Draw ();
                        children.Get ("_isCustom").Draw ();
                    }

                    //  Draws Custom options.
                    else
                    {
                        children.Get ("_code").Draw ();
                        children.Get ("_name").Draw ();
                        children.Get ("_country").Draw ();
                        children.Get ("_region").Draw ();
                        children.Get ("_isCustom").Draw ();
                    }

                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                }

                ValueEntry.SmartValue = value;
            }

            #endregion
        }

        /// <summary>
        /// Draws a Culture in the editor.
        /// </summary>
        /// <param name="culture">Culture to Draw.</param>
        /// <returns>Returns the edited culture value.</returns>
        public static Culture DrawEditorField (Culture culture)
        {
            if (!culture.IsCustom)
            {
                GUI.enabled = false;
                EditorGUILayout.LabelField (
                    new GUIContent (
                        "Code", "Code to Identify language category."
                    ),
                    new GUIContent (culture.Code),
                    EditorStyles.label
                );

                EditorGUILayout.LabelField (
                    new GUIContent (
                        "Name", "Name for language category."
                    ),
                    new GUIContent (culture.Name),
                    EditorStyles.label
                );
                GUI.enabled = true;

                EditorGUI.BeginChangeCheck ();
                culture.LCID = (LCID) SirenixEditorFields.EnumDropdown (
                    label: "LCID",
                    selected: culture.LCID
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnLCIDChanged ();

                EditorGUI.BeginChangeCheck ();
                culture.Language = (ISO639_1) SirenixEditorFields.EnumDropdown (
                    label: "Language",
                    selected: culture.Language
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnISOChanged ();

                EditorGUI.BeginChangeCheck ();
                culture.Country = (ISO3166_2) SirenixEditorFields.EnumDropdown (
                    label: "Country",
                    selected: culture.Country
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnISOChanged ();

                EditorGUI.BeginChangeCheck ();
                culture.Region = SirenixEditorFields.TextField (
                    label: "Region",
                    value: culture.Region
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnRegionChanged ();

                EditorGUI.BeginChangeCheck ();
                culture.IsCustom = EditorGUILayout.Toggle (
                    label: "Is Custom",
                    value: culture.IsCustom
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnIsCustomChanged ();

            }

            //  Draws Custom options.
            else
            {
                EditorGUI.BeginChangeCheck ();
                culture.Code = SirenixEditorFields.TextField (
                    label: "Code",
                    value: culture.Code
                );
                if (EditorGUI.EndChangeCheck ()) culture.OnCodeChanged ();

                culture.Name = SirenixEditorFields.TextField (
                    label: "Name",
                    value: culture.Name
                );

                culture.Country = (ISO3166_2) SirenixEditorFields.EnumDropdown (
                    label: "Country",
                    selected: culture.Country
                );

                culture.Region = SirenixEditorFields.TextField (
                    label: "Region",
                    value: culture.Region
                );

                culture.IsCustom = EditorGUILayout.Toggle (
                    label: "Is Custom",
                    value: culture.IsCustom
                );
            }

            return culture;
        }

        #endregion

#endif

        #endregion
    }
}