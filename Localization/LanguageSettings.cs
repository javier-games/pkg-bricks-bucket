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
        /// Collection of categories of languages.
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
        /// Language to use as default.
        /// </summary>
        public string Default
        {
            get => _default;
            private set => _default = value;
        }

        #endregion



        #region Class Implementation

#if UNITY_EDITOR

        #region Menu

        [PropertySpace, Title("Languages Categories Menu", bold: false)]

        [Indent, Button("Add Category")]
        [HideIf("_activateAddMenu"), HideIf("_activateRemoveMenu")]
        internal void ActivateCategoryButton()
        {
            _activateAddMenu = true;
        }

        [SerializeField, HideInInspector]
        internal bool _activateAddMenu;

        [Indent, Button("Remove Category")]
        [HideIf("_activateRemoveMenu"), HideIf("_activateAddMenu")]
        internal void ActivateRemoveMenu()
        {
            _activateRemoveMenu = true;
        }

        [SerializeField, HideInInspector]
        internal bool _activateRemoveMenu;

        #endregion

        #region New Category

        [PropertySpace, Title("New Category", bold: false)]

        [Indent, SerializeField, ShowIf("_activateAddMenu")]
        internal LanguageCategory _newCategory;

        [Indent, Button("Add"), ShowIf("_activateAddMenu")]
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
            Cancel();
        }

        #endregion

















        /*

        [SerializeField, ShowIf("_activateRemoveMenu")]
        [ValueDropdown("CategoriesCodes")]
        internal string _categoryToRemove;
        
        

        [Button("Remove"), ShowIf("_activateRemoveMenu")]
        internal void Remove()
        {
            if(_categories.Count == 0)
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            var categoryToRemove = _categoryToRemove;
            _categories.Remove(_categories.Find(c => c.Code == categoryToRemove));
            Cancel();
        }




    */
        [Button("Cancel")]
        internal void Cancel()
        {
            _activateAddMenu = false;
            _newCategory = default;
            _activateRemoveMenu = false;
            //_categoryToRemove = string.Empty;
        }

        private void OnCategoriesChanged()
        {
            Default = _categories.Count >= 0 ? _categories[0].DisplayName : string.Empty;
        }

#endif
        #endregion





    }
}