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
        [SerializeField, ReadOnly]
        [ListDrawerSettings (HideAddButton = true, NumberOfItemsPerPage = 4)]
        [Tooltip ("Collection of categories of languages.")]
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
        /// Shows Add Menu.
        /// </summary>
        [SerializeField]
        private bool _addMenu;

        /// <summary>
        /// Whether to show or hide the Remove menu.
        /// </summary>
        [SerializeField]
        private bool _removeMenu;

        /// <summary>
        /// Whether to show or hide the add menu.
        /// </summary>
        [SerializeField]
        private bool _setDefaultMenu;

        /// <summary>
        /// Category to add.
        /// </summary>
        [SerializeField]
        [LabelText ("Category to Add")]
        [Tooltip ("Edit the fields of the category to add.")]
        private LanguageCategory _toAdd;

        /// <summary>
        /// Category to remove.
        /// </summary>
        [SerializeField]
        [LabelText ("Category to Remove"), ValueDropdown ("CategoriesCodes")]
        [Tooltip ("Select the code of the category to remove.")]
        private string _toRemove;

        /// <summary>
        /// Category to Set as Default.
        /// </summary>
        [SerializeField]
        [LabelText ("Default Category"), ValueDropdown ("CategoriesCodes")]
        [Tooltip ("Select the code of the category to set as default.")]
        private string _toDefault;

        #endregion

        #region Editor Methods

        /// <summary>
        /// Called by the editor to add a new category.
        /// </summary>
        [Button ("Add")]
        private void Add ()
        {
            _categories.Add (_toAdd);
            Cancel ();
        }

        /// <summary>
        /// Called by editor to remove a category.
        /// </summary>
        [Button ("Remove")]
        private void Remove ()
        {
            var categoryToRemove = _toRemove;
            _categories.Remove (
                _categories.Find (c => c.Code == categoryToRemove)
            );

            Cancel ();
        }

        /// <summary>
        /// Called by editor to set the default language category.
        /// </summary>
        [Button ("Set")]
        private void SetDefault ()
        {
            var tempDefault = _toDefault;
            var newDefault = _categories.Find (c => c.Code == tempDefault);
            _categories.Remove (newDefault);
            _categories.Insert (0, newDefault);

            Cancel ();
        }

        /// <summary>
        /// Called by editor to cancel the current process.
        /// </summary>
        [Button ("Cancel")]
        private void Cancel ()
        {
            _addMenu = false;
            _removeMenu = false;
            _setDefaultMenu = false;

            _toRemove = string.Empty;
            _toAdd = default;
            _toDefault = string.Empty;
        }

        #endregion

        #region Drawer

        /// <summary>
        /// Language Category Drawer Class.
        /// </summary>
        private class LanguageSettingsDrawer : OdinValueDrawer<LanguageSettings>
        {
            
            #region Fields

            /// <summary>
            /// Label for code.
            /// </summary>
            private readonly GUIContent _defaultLabel = new GUIContent (
                "Default Category", "This is the Default language category."
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

                var addMenu = value._addMenu;
                var removeMenu = value._removeMenu;
                var setDefaultMenu = value._setDefaultMenu;

                //  Draws the label on Foldout.
                if (label != null) EditorGUILayout.LabelField (label);

                // Draws the default language category.
                if (!setDefaultMenu && !defaultIsNull)
                {
                    EditorGUILayout.BeginHorizontal ();
                    EditorGUILayout.LabelField (
                        _defaultLabel,
                        new GUIContent (value.Default.DisplayName)
                    );

                    // Draws the button to set the default language.
                    if (SirenixEditorGUI.IconButton (EditorIcons.SettingsCog))
                        value._setDefaultMenu = true;
                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the menu to set the default category.
                else if (!defaultIsNull)
                {
                    children.Get ("_toDefault").Draw ();

                    EditorGUILayout.BeginHorizontal ();
                    GUI.enabled = !string.IsNullOrEmpty (value._toDefault);
                    children.Get ("SetDefault").Draw ();
                    GUI.enabled = true;
                    children.Get ("Cancel").Draw ();
                    EditorGUILayout.EndHorizontal ();
                }

                // Draws list of categories.
                EditorGUILayout.Space ();
                children.Get ("_categories").Draw ();

                // Draws the plus and minus buttons to edit list.
                if (!addMenu && !removeMenu)
                {
                    EditorGUILayout.BeginHorizontal ();
                    EditorGUILayout.Space (0f, true);

                    if (SirenixEditorGUI.IconButton (EditorIcons.Plus, 14, 14))
                        value._addMenu = true;

                    GUI.enabled = !string.IsNullOrEmpty (value.Default.Code);

                    if (SirenixEditorGUI.IconButton (EditorIcons.Minus, 14, 14))
                        value._removeMenu = true;

                    GUI.enabled = true;

                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the Menu to Add a new Category.
                else if (addMenu)
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
                        !string.IsNullOrWhiteSpace (categoryToAdd.Code) &&
                        !string.IsNullOrWhiteSpace (categoryToAdd.DisplayName);
                    children.Get ("Add").Draw ();
                    GUI.enabled = true;
                    children.Get ("Cancel").Draw ();
                    EditorGUILayout.EndHorizontal ();
                }

                // Draws the Menu to remove a category.
                else
                {
                    children.Get ("_toRemove").Draw ();
                    EditorGUILayout.BeginHorizontal ();
                    GUI.enabled = !string.IsNullOrEmpty (value._toRemove);
                    children.Get ("Remove").Draw ();
                    GUI.enabled = true;
                    children.Get ("Cancel").Draw ();
                    EditorGUILayout.EndHorizontal ();
                }

                EditorGUILayout.Space ();

                // Update all values in smart value.
                ValueEntry.SmartValue = value;
                ValueEntry.ApplyChanges ();
            }
            
            #endregion
            
        }
        
        #endregion
        
#endif
        
        #endregion
    }
}