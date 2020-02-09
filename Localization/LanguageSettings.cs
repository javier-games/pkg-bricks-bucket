using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
        [Space, SerializeField, ReadOnly, Indent]
        [Tooltip("Language Category to use as default.")]
        [LabelText("Default Category")]
        private string _default;

        /// <summary>
        /// Collection of categories of languages.
        /// </summary>
        [Space, SerializeField, ReadOnly, Indent]
        [ListDrawerSettings(HideAddButton = true, NumberOfItemsPerPage = 4)]
        [OnValueChanged("OnCategoriesChanged")]
        [Tooltip("Collection of categories of languages.")]
        private List<LanguageCategory> _categories;

        #endregion



        #region Properties

        /// <summary>
        /// Category by code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public LanguageCategory this[string code]
        {
            get
            {
                var category = _categories.Find(c => c.Code == code);
                return category;
            }
        }

        /// <summary>
        /// Default Language.
        /// </summary>
        public LanguageCategory Default
        {
            get => this[_default];
        }

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
        public LanguageCategory[] Categories
        {
            get => _categories.ToArray();
        }

        #endregion



        #region Class Implementation

#if UNITY_EDITOR

        #region Menu

        [PropertySpace, Indent]
        [Button("Add Category")]
        [HideIf("_activateAddMenu"), HideIf("_activateRemoveMenu")]
        [HideIf("_activateSetDefaultMenu")]
        internal void ActivateCategoryButton()
        {
            _activateAddMenu = true;
        }

        [SerializeField, HideInInspector]
        internal bool _activateAddMenu;

        [Indent]
        [Button("Remove Category")]
        [HideIf("_activateRemoveMenu"), HideIf("_activateAddMenu")]
        [HideIf("_activateSetDefaultMenu"), DisableIf("_default", "")]
        internal void ActivateRemoveMenu()
        {
            _activateRemoveMenu = true;
        }

        [SerializeField, HideInInspector]
        internal bool _activateRemoveMenu;

        [Indent]
        [Button("Set Default Category")]
        [HideIf("_activateSetDefaultMenu"), HideIf("_activateRemoveMenu")]
        [HideIf("_activateAddMenu"), DisableIf("_default", "")]
        internal void ActivateSetDefault()
        {
            _activateSetDefaultMenu = true;
        }

        [SerializeField, HideInInspector]
        internal bool _activateSetDefaultMenu;

        #endregion

        #region New Category

        [PropertySpace, Indent]
        [SerializeField]
        [ShowIf("_activateAddMenu")]
        [OnValueChanged("NewCategoryChanged")]
        internal LanguageCategory _newCategory;

        [SerializeField, HideInInspector]
        internal bool _isValidNew;

        [Indent]
        [Button("Add")]
        [ShowIf("_activateAddMenu"), EnableIf("_isValidNew")]
        internal void Add()
        {
            if (_newCategory.Equals(default(LanguageCategory)))
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            if (_categories.Contains(_newCategory))
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            _categories.Add(_newCategory);
            OnCategoriesChanged();
            CancelAdd();
        }

        internal void NewCategoryChanged()
        {
            _isValidNew =
                !_newCategory.Equals(default(LanguageCategory)) &&
                !_categories.Contains(_newCategory);
        }

        [Indent]
        [Button("Cancel")]
        [ShowIf("_activateAddMenu")]
        internal void CancelAdd()
        {
            _activateAddMenu = false;
            _newCategory = default;
            NewCategoryChanged();
        }

        #endregion

        #region Remove Category

        [Indent]
        [SerializeField, ValueDropdown("CategoriesCodes")]
        [ShowIf("_activateRemoveMenu")]
        internal string _categoryToRemove;

        [Indent]
        [Button("Remove")]
        [ShowIf("_activateRemoveMenu"), DisableIf("_categoryToRemove", "")]
        internal void Remove()
        {
            if (_categories.Count == 0)
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            var categoryToRemove = _categoryToRemove;
            _categories.Remove(
                _categories.Find(c => c.Code == categoryToRemove)
            );

            if (_categories.Count == 0)
                _default = string.Empty;

            CancelRemove();
        }

        [Indent]
        [Button("Cancel")]
        [ShowIf("_activateRemoveMenu")]
        internal void CancelRemove()
        {
            _activateRemoveMenu = false;
            _categoryToRemove = string.Empty;
        }

        #endregion

        #region Set Default Category

        [Indent]
        [SerializeField, ValueDropdown("CategoriesCodes")]
        [ShowIf("_activateSetDefaultMenu")]
        internal string _newDefault;

        [Indent]
        [Button("Set")]
        [ShowIf("_activateSetDefaultMenu"), DisableIf("_newDefault", "")]
        internal void SetDefault()
        {
            if (_categories.Count == 0)
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            var tempDefault = _newDefault;
            var newDefault = _categories.Find(c => c.Code == tempDefault);
            _categories.Remove(newDefault);
            _categories.Insert(0,newDefault);

            _default = _newDefault;

            CancelSetDefault();
        }

        [Indent]
        [Button("Cancel")]
        [ShowIf("_activateSetDefaultMenu")]
        internal void CancelSetDefault()
        {
            _activateSetDefaultMenu = false;
            _newDefault = string.Empty;
        }

        #endregion


        private void OnCategoriesChanged()
        {
            _default = _categories.Count >= 0 ?
                _categories[0].DisplayName :
                string.Empty;
        }

#endif
        #endregion





    }
}