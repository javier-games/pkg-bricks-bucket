using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BricksBucket.Localization
{
    /// <summary>
    /// Structure to categorize languages.
    /// </summary>
    [System.Serializable]
    public struct LanguageCategory
    {

        #region  Members

        /// <summary>Code to identify language category.</summary>
        [SerializeField, EnableIf("_isCustom")]
        private string _code;

        /// <summary>Name for language category.</summary>
        [SerializeField, EnableIf("_isCustom")]
        private string _name;

        /// <summary>Language ISO-639 code.</summary>
        [SerializeField, HideIf("_isCustom"), OnValueChanged("OnISOChanged")]
        private ISO639_1 _language;

        /// <summary>Country ISO-3166 code.</summary>
        [SerializeField, OnValueChanged("OnISOChanged")]
        private ISO3166_2 _coutry;

        /// <summary>LCID code.</summary>
        [SerializeField, HideIf("_isCustom"), OnValueChanged("OnLCIDChanged")]
        private LCID _LCID;

        /// <summary>Whether this catecory is custom.</summary>
        [SerializeField, OnValueChanged("OnIsCustomChanged")]
        private bool _isCustom;

        #endregion


        #region  Accessors

        public string DisplayName
        {
            get{ return _name; }
            private set{ _name = value;}
        }

        /// <summary>Whether this catecory is custom.</summary>
        public bool IsCustom
        {
            get { return _isCustom; }
            private set { _isCustom = value; }
        }

        /// <summary>Language ISO-639 code.</summary>
        public ISO639_1 Language
        {
            get
            {
                if (_isCustom)
                    return ISO639_1.NONE;
                return _language;
            }
            private set { _language = value; }
        }

        /// <summary>Country ISO-3166 code.</summary>
        public ISO3166_2 Country
        {
            get
            {
                if (_isCustom)
                    return ISO3166_2.NONE;
                return _coutry;
            }
            private set { _coutry = value; }
        }

        public string Code
        {
            get
            {
                return _code;
            }
        }

        #endregion



#if UNITY_EDITOR

        /// <summary>Called from inspector when an ISO code changed.</summary>
        internal void OnISOChanged()
        {
            if (IsCustom)
                return;

            _LCID = LocalizationUtils.ToLCID(_language, _coutry);

            if (_LCID == LCID.INVARIANT)
            {
                _code = _LCID.ToString();
                _name = "Invariant Language";
            }
            else
            {
                _code = _language.ToString();
                _name = LocalizationUtils.ISO639_Names[(int)_language];

                if (_coutry != ISO3166_2.NONE)
                {
                    _code = StringUtils.Concat(
                        _code, "-", _coutry.ToString()
                    );
                    _name = StringUtils.Concat(
                        _name,
                        " (", LocalizationUtils.ISO3166_Names[(int)_coutry], ")"
                    );
                }
            }
        }

        /// <summary>Called from inspector when LCID code changed.</summary>
        internal void OnLCIDChanged()
        {
            if (IsCustom)
                return;

            _coutry = (ISO3166_2)LocalizationUtils.ToISO3166(_LCID);
            _language = (ISO639_1)LocalizationUtils.ToISO639(_LCID);
            var info = new CultureInfo((int)_LCID);

            if (_LCID == LCID.INVARIANT)
            {
                _code = _LCID.ToString().ToUpper();
                _name = "Invariant Language";
            }
            else
            {
                _code = info.Name.ToUpper();
                _name = info.DisplayName;
            }
        }

        /// <summary>Called on is custom variable changes.</summary>
        internal void OnIsCustomChanged()
        {
            _name = string.Empty;
            _code = string.Empty;
            _coutry = ISO3166_2.NONE;
            _language = ISO639_1.NONE;
            _LCID = LCID.NONE;
        }

#endif
    }

    [System.Serializable]
    public struct LanguageSettings
    {

        [SerializeField]
        private string _default;

        [SerializeField, ListDrawerSettings(HideAddButton = true, DraggableItems = true), OnValueChanged("CategoriesChanged"), ReadOnly]
        private List<LanguageCategory> _categories;

        private string[] CategoriesCodes
        {
            get
            {
                var categoriesCodes = new string [_categories.Count];
                for(int i = 0; i < categoriesCodes.Length; i++)
                    categoriesCodes[i] = _categories[i].Code;

                return categoriesCodes;
            }
        }

        private void CategoriesChanged()
        {
            _default = _categories.Count >= 0 ? _categories[0].DisplayName : string.Empty;
        }

#if UNITY_EDITOR

        [SerializeField, HideInInspector]
        private bool _activateAddMenu;

        [SerializeField, ShowIf("_activateAddMenu")]
        private LanguageCategory _newCategory;

        [Button("Add new Category"), HideIf("_activateAddMenu")]
        private void ActivateCategoryButton()
        {
            _activateAddMenu = true;
        }

        [Button("Add"), ShowIf("_activateAddMenu")]
        private void Add()
        {
            if(_newCategory.Equals(default(LanguageCategory)))
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            if(_categories.Contains(_newCategory))
            {
                //  TODO: Add fancy pop up feedback.
                return;
            }

            _categories.Add(_newCategory);
            CategoriesChanged();
            Cancel();
        }



        [SerializeField, HideInInspector]
        private bool _activateRemoveMenu;

        [SerializeField, ShowIf("_activateRemoveMenu")]
        [ValueDropdown("CategoriesCodes")]
        private string _categoryToRemove;
        
        [Button("Remove Category"), HideIf("_activateRemoveMenu")]
        private void ActivateRemoveMenu()
        {
            _activateRemoveMenu = true;
        }

        [Button("Remove"), ShowIf("_activateRemoveMenu")]
        private void Remove()
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


        [Button("Cancel")]
        private void Cancel()
        {
            _activateAddMenu = false;
            _newCategory = default(LanguageCategory);
            _activateRemoveMenu = false;
            _categoryToRemove = string.Empty;
        }
        
#endif





    }
}