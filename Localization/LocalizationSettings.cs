using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BricksBucket.Generics;
using Sirenix.OdinInspector;

namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// LocalizationSettings.cs
    /// 
    /// Settings for localization.
    /// 
    /// </summary>
    public class LocalizationSettings : ScriptableSingleton<LocalizationSettings>
    {
        [SerializeField, BoxGroup("Default Language"), HideLabel]
        private LanguageCategory _default;

        

        
    }

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

        /// <summary>Whether this catecory is custom.</summary>
        public bool IsCustom
        {
            get{ return _isCustom; }
            private set { _isCustom = value; }
        }

        /// <summary>Language ISO-639 code.</summary>
        public ISO639_1 Language
        {
            get
            {
                if(_isCustom)
                    return ISO639_1.NONE;
                return _language;
            }
            private set { _language = value;}
        }

        /// <summary>Country ISO-3166 code.</summary>
        public ISO3166_2 Country
        {
            get
            {
                if(_isCustom)
                    return ISO3166_2.NONE;
                return _coutry;
            }
            private set { _coutry = value; }
        }

        #endregion



#if UNITY_EDITOR

        /// <summary>Called from inspector when an ISO code changed.</summary>
        internal void OnISOChanged()
        {
            _LCID = LocalizationUtils.ToLCID(_language, _coutry);

            if(_LCID == LCID.INVARIANT)
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
}
