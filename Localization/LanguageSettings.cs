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
        /// Language to use as default.
        /// </summary>
        [SerializeField] private string _default;

        /// <summary>
        /// Collection of categories of languages.
        /// </summary>
        [Space, SerializeField, ReadOnly]
        [ListDrawerSettings (HideAddButton = true, NumberOfItemsPerPage = 4)]
        [OnValueChanged ("OnCategoriesChanged")]
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
        public LanguageCategory Default => this[_default];

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



#if UNITY_EDITOR
        #pragma warning disable IDE0051 // Remove unused private members

        #region Editor



        #region Menu

        /// <summary>
        /// Whether to show or hide the add menu.
        /// </summary>
        [SerializeField] private bool _addMenu;

        /// <summary>
        /// Shows Add Menu.
        /// </summary>
        [Button ("Add Category")]
        private void ActivateCategoryButton () => _addMenu = true;

        /// <summary>
        /// Whether to show or hide the Remove menu.
        /// </summary>
        [SerializeField] private bool _removeMenu;

        /// <summary>
        /// Shows Remove Menu.
        /// </summary>
        [Button ("Remove Category")]
        private void ActivateRemoveMenu () => _removeMenu = true;

        /// <summary>
        /// Whether to show or hide the Set Default menu.
        /// </summary>
        [SerializeField] private bool _setDefaultMenu;

        /// <summary>
        /// Shows Set Default Menu.
        /// </summary>
        [Button ("Set Default Category")]
        private void ActivateSetDefault () => _setDefaultMenu = true;

        /// <summary>
        /// Hides all menus.
        /// </summary>
        [Button ("Cancel")]
        private void Cancel ()
        {
            _addMenu = false;
            _categoryToAdd = default;

            _removeMenu = false;
            _categoryToRemove = string.Empty;

            _setDefaultMenu = false;
            _defaultCategory = string.Empty;
        }

        /// <summary>
        /// Called to set Default Tag.
        /// </summary>
        private void OnCategoriesChanged ()
        {
            _default = _categories.Count > 0
                ? _categories[0].DisplayName
                : string.Empty;
        }

        #endregion



        #region New Category

        /// <summary>
        /// Category to add.
        /// </summary>
        [SerializeField] private LanguageCategory _categoryToAdd;

        /// <summary>
        /// Adds a new category.
        /// </summary>
        [Button ("Add")]
        private void Add ()
        {
            _categories.Add (_categoryToAdd);
            OnCategoriesChanged ();
            Cancel ();
        }

        #endregion



        #region Remove Category

        /// <summary>
        /// Category to remove.
        /// </summary>
        [SerializeField, ValueDropdown ("CategoriesCodes")]
        private string _categoryToRemove;

        /// <summary>
        /// Removes the indicated category.
        /// </summary>
        [Button ("Remove")]
        private void Remove ()
        {

            var categoryToRemove = _categoryToRemove;
            _categories.Remove (
                _categories.Find (c => c.Code == categoryToRemove)
            );

            if (_categories.Count == 0)
                _default = string.Empty;

            Cancel ();
        }

        #endregion



        #region Set Default Category

        /// <summary>
        /// Category to Set as Default.
        /// </summary>
        [SerializeField, ValueDropdown ("CategoriesCodes")]
        private string _defaultCategory;

        /// <summary>
        /// Sets the default language category.
        /// </summary>
        [Button ("Set")]
        private void SetDefault ()
        {

            var tempDefault = _defaultCategory;
            var newDefault = _categories.Find (c => c.Code == tempDefault);
            _categories.Remove (newDefault);
            _categories.Insert (0, newDefault);

            _default = _defaultCategory;

            Cancel ();
        }

        #endregion



        #region Drawer

        /// <summary>
        /// Language Category Drawer Class.
        /// </summary>
        public class LanguageSettingsDrawer : OdinValueDrawer<LanguageSettings>
        {



            #region Fields

            /// <summary>
            /// Label for code.
            /// </summary>
            private readonly GUIContent _defaultLabel = new GUIContent (
                "Default Category", "Default language category."
            );

            /// <summary>
            /// Label for Categories.
            /// </summary>
            private readonly GUIContent _categoriesLabel = new GUIContent (
                "Categories", "Collection of categories of languages."
            );

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
                if (label == null)
                {
                    label = new GUIContent ("Language Settings");
                }

                EditorGUILayout.Space ();
                EditorGUILayout.LabelField (
                    label,
                    SirenixGUIStyles.BoldLabel
                );

                //  Draws default language category.
                if (!string.IsNullOrEmpty (value._default))
                    EditorGUILayout.LabelField (
                        _defaultLabel,
                        new GUIContent (StringUtils.Concat (
                            value._default,
                            " [", value[value._default].DisplayName, "]"
                        ))
                    );

                //  Draws list of categories.
                var children = ValueEntry.Property.Children;
                children.Get ("_categories").Draw (_categoriesLabel);

                //  Draws Menu.
                var addMenu = value._addMenu;
                var removeMenu = value._removeMenu;
                var setDefaultMenu = value._setDefaultMenu;

                if (!(addMenu || removeMenu || setDefaultMenu))
                {
                    children.Get ("ActivateCategoryButton").Draw ();

                    GUI.enabled = !string.IsNullOrEmpty (value._default);

                    children.Get ("ActivateRemoveMenu").Draw ();
                    children.Get ("ActivateSetDefault").Draw ();

                    GUI.enabled = true;

                }

                //  Draws Add Menu.
                if (addMenu)
                {
                    children.Get ("_categoryToAdd").Draw ();

                    GUI.enabled =
                        !value._categoryToAdd.Equals (
                            default (LanguageCategory))
                        && !value._categories.Contains (value._categoryToAdd);
                    children.Get ("Add").Draw ();
                    GUI.enabled = true;

                    children.Get ("Cancel").Draw ();
                }

                //  Draws Remove Menu.
                if (removeMenu)
                {
                    children.Get ("_categoryToRemove").Draw ();

                    GUI.enabled =
                        !string.IsNullOrEmpty (value._categoryToRemove);
                    children.Get ("Remove").Draw ();
                    GUI.enabled = true;

                    children.Get ("Cancel").Draw ();
                }

                //  Draws Set Default Menu.
                if (setDefaultMenu)
                {
                    children.Get ("_defaultCategory").Draw ();

                    GUI.enabled =
                        !string.IsNullOrEmpty (value._defaultCategory);
                    children.Get ("SetDefault").Draw ();
                    GUI.enabled = true;

                    children.Get ("Cancel").Draw ();
                }

                value.OnCategoriesChanged ();

                EditorGUILayout.Space ();
            }

            #endregion

        }

        #endregion

        #endregion

        #pragma warning restore IDE0051 // Remove unused private members
#endif

    }
}