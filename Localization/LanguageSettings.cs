using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif


namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// Language Settings
    ///
    /// <para>
    /// Struct to contain language settings.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    [System.Serializable]
    public struct LanguageSettings
    {



        #region Fields

        /// <summary>
        /// Collection of categories of languages.
        /// </summary>
        [SerializeField]
        [Space, ReadOnly]
        [ListDrawerSettings (HideAddButton = true, NumberOfItemsPerPage = 4)]
        [Tooltip("Collection of categories of languages.")]
        private List<LanguageCategory> _categories;

        #endregion



        #region Properties

        /// <summary>
        /// Category by code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LanguageCategory this [string code]
        {
            get
            {
                var category = _categories.Find (c => c.Code == code);
                return category;
            }
        }

        /// <summary>
        /// Default Language.
        /// </summary>
        public LanguageCategory Default =>
            Categories.Length > 0 ? Categories[0] : default;

        /// <summary>
        /// Collection of codes of languages categories.
        /// </summary>
        public string[] CategoriesCodes
        {
            get
            {
                var categoriesCodes = new string[_categories.Count];
                for (int i = 0; i < categoriesCodes.Length; i++)
                    categoriesCodes[i] = _categories[i].Code;

                return categoriesCodes;
            }
        }

        /// <summary>
        /// Collection of display names of languages categories.
        /// </summary>
        public string[] CategoriesDisplayNames
        {
            get
            {
                var categoriesDisplayNames = new string[_categories.Count];
                for (int i = 0; i < categoriesDisplayNames.Length; i++)
                    categoriesDisplayNames[i] = _categories[i].DisplayName;

                return categoriesDisplayNames;
            }
        }

        /// <summary>
        /// Collection of languages categories.
        /// </summary>
        public LanguageCategory[] Categories => _categories.ToArray ();

        #endregion



        #region Editor

#if UNITY_EDITOR

        #region Editor Fields

        /// <summary>
        /// Category to add.
        /// </summary>
        [SerializeField]
        [LabelText("Category to Add")]
        [Tooltip("Edit the fields of the category to add.")]
        private LanguageCategory _toAdd;

        /// <summary>
        /// Category to remove.
        /// </summary>
        [SerializeField]
        [LabelText("Category to Remove"), ValueDropdown ("CategoriesCodes")]
        [Tooltip("Select the code of the category to remove.")]
        private string _toRemove;

        /// <summary>
        /// Category to Set as Default.
        /// </summary>
        [SerializeField]
        [LabelText("Default Category"), ValueDropdown ("CategoriesCodes")]
        [Tooltip("Select the code of the category to set as default.")]
        private string _toDefault;

        #endregion



        #region Drawer

        /// <summary>
        /// Language Category Drawer Class.
        /// </summary>
        private class LanguageSettingsDrawer : OdinValueDrawer<LanguageSettings>
        {



            #region Fields

            /// <summary>
            /// Whether to show the Set Default menu.
            /// </summary>
            private bool _setDefaultMenu;

            /// <summary>
            /// Whether to show the Add menu.
            /// </summary>
            private bool _addMenu;

            /// <summary>
            /// Whether to show the Remove menu.
            /// </summary>
            private bool _removeMenu;

            /// <summary>
            /// Label for code.
            /// </summary>
            private readonly GUIContent _defaultLabel = new GUIContent (
                "Default Category", "Default language category."
            );

            /// <summary>
            /// Label and tooltip for add button.
            /// </summary>
            private readonly GUIContent _addLabel = new GUIContent (
                "Add", "Add a new category."
            );

            /// <summary>
            /// Label and tooltip for remove button.
            /// </summary>
            private readonly GUIContent _removeLabel = new GUIContent (
                "Remove", "Remove a category."
            );

            /// <summary>
            /// Label and tooltip for set button.
            /// </summary>
            private readonly GUIContent _setLabel = new GUIContent (
                "Set", "Set the default category."
            );

            /// <summary>
            /// Label and tooltip for cancel button.
            /// </summary>
            private readonly GUIContent _cancelLabel = new GUIContent (
                "Cancel", "Cancel the current action."
            );

            #endregion



            #region Override Methods

            /// <summary>
            /// Draws the property.
            /// </summary>
            /// <param name="label">Label of the property.</param>
            protected override void DrawPropertyLayout (GUIContent label)
            {
                EditorGUILayout.Space ();

                var value = ValueEntry.SmartValue;
                var children = ValueEntry.Property.Children;
                var defaultIsNull = string.IsNullOrEmpty (value.Default.Code);

                //  Draws the label on Foldout.
                if (label != null) EditorGUILayout.LabelField (label);

                // Draws the default language category.
                if (!_setDefaultMenu && !defaultIsNull)
                {
                    EditorGUILayout.BeginHorizontal ();
                    EditorGUILayout.LabelField (
                        _defaultLabel,
                        new GUIContent (value.Default.DisplayName)
                    );

                    // Draws the button to set the default language.
                    if (SirenixEditorGUI.IconButton (EditorIcons.SettingsCog))
                        _setDefaultMenu = true;
                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the menu to set the default category.
                else if (!defaultIsNull)
                {
                    children.Get ("_toDefault").Draw ();

                    EditorGUILayout.BeginHorizontal ();
                    GUI.enabled = !string.IsNullOrEmpty (value._toDefault);

                    if (GUILayout.Button (_setLabel))
                    {
                        var tempDefault = value._toDefault;
                        var newDefault = value._categories.Find (
                            c => c.Code == tempDefault
                        );
                        value._categories.Remove (newDefault);
                        value._categories.Insert (0, newDefault);
                        _setDefaultMenu = false;
                    }

                    GUI.enabled = true;

                    if (GUILayout.Button (_cancelLabel))
                        value = Cancel (value);

                    EditorGUILayout.EndHorizontal ();
                }

                // Draws list of categories.
                children.Get ("_categories").Draw ();

                // Draws the plus and minus buttons to edit list.
                if (!_addMenu && !_removeMenu)
                {
                    EditorGUILayout.BeginHorizontal ();
                    EditorGUILayout.Space (0f, true);

                    if (SirenixEditorGUI.IconButton (EditorIcons.Plus))
                        _addMenu = true;

                    GUI.enabled = !string.IsNullOrEmpty (value.Default.Code);

                    if (SirenixEditorGUI.IconButton (EditorIcons.Minus))
                        _removeMenu = true;

                    GUI.enabled = true;

                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the Menu to Add a new Category.
                else if (_addMenu)
                {
                    SirenixEditorGUI.BeginBox ();
                    children.Get ("_toAdd").Draw ();
                    SirenixEditorGUI.EndBox ();

                    EditorGUILayout.BeginHorizontal ();
                    var categoryToAdd = value._toAdd;
                    GUI.enabled =
                        !categoryToAdd.Equals (default (LanguageCategory)) &&
                        !value._categories.Exists (
                            category => category.Code == categoryToAdd.Code
                        ) &&
                        !string.IsNullOrWhiteSpace (categoryToAdd.Code);

                    if (GUILayout.Button (_addLabel))
                    {
                        value._categories.Add (value._toAdd);
                        _addMenu = false;
                    }

                    GUI.enabled = true;

                    if (GUILayout.Button (_cancelLabel))
                        value = Cancel (value);

                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the Menu to remove a category.
                else if (_removeMenu)
                {

                    children.Get ("_toRemove").Draw ();
                    EditorGUILayout.BeginHorizontal ();
                    GUI.enabled = !string.IsNullOrEmpty (value._toRemove);

                    if (GUILayout.Button (_removeLabel))
                    {
                        var categoryToRemove = value._toRemove;
                        value._categories.Remove (
                            value._categories.Find (
                                c => c.Code == categoryToRemove
                            )
                        );
                        _removeMenu = false;
                    }

                    GUI.enabled = true;

                    if (GUILayout.Button (_cancelLabel))
                        value = Cancel (value);

                    EditorGUILayout.EndHorizontal ();
                }

                EditorGUILayout.Space ();

                // Update all values in smart value.
                ValueEntry.SmartValue = value;
            }

            #endregion

            private LanguageSettings Cancel (LanguageSettings value)
            {
                value._toAdd = default;
                value._toRemove = string.Empty;
                value._toDefault = string.Empty;
                
                _removeMenu = false;
                _addMenu = false;
                _setDefaultMenu = false;

                return value;
            }

        }

        #endregion

#endif

        #endregion

    }
}