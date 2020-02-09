using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BricksBucket.Localization
{
    /// <summary>
    /// Structure for languages categorizing.
    /// </summary>
    [System.Serializable]
    public struct LanguageCategory
    {



        #region  Fields

        /// <summary>
        /// Code to identify language category.
        /// </summary>
        [SerializeField, EnableIf("_isCustom")]
        private string _code;

        /// <summary>
        /// Name for language category.
        /// </summary>
        [Indent, SerializeField, EnableIf("_isCustom")]
        private string _name;

        /// <summary>
        /// LCID code.
        /// </summary>
        [Indent, EnumPaging]
        [SerializeField, HideIf("_isCustom"), OnValueChanged("OnLCIDChanged")]
        private LCID _LCID;

        /// <summary>
        /// Language ISO-639 code.
        /// </summary>
        [Indent, EnumPaging]
        [SerializeField, HideIf("_isCustom"), OnValueChanged("OnISOChanged")]
        private ISO639_1 _language;

        /// <summary>
        /// Country ISO-3166 code.
        /// </summary>
        [Indent, EnumPaging]
        [SerializeField, OnValueChanged("OnISOChanged")]
        private ISO3166_2 _country;

        /// <summary>
        /// Specifies a region for the language.
        /// </summary>
        [Indent, SerializeField, OnValueChanged("OnRegionChanged")]
        private string _region;

        /// <summary>
        /// Whether this catecory is custom.
        /// </summary>
        [Indent, SerializeField, OnValueChanged("OnIsCustomChanged")]
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
        /// Whether this catecory is custom.
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
            get
            {
                if (_isCustom)
                    return ISO639_1.NONE;
                return _language;
            }
            private set => _language = value;
        }

        /// <summary>
        /// Country ISO-3166 code.
        /// </summary>
        public ISO3166_2 Country
        {
            get
            {
                if (_isCustom)
                    return ISO3166_2.NONE;
                return _country;
            }
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
            SetDisblay();
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

            SetDisblay();
        }

        /// <summary>
        /// Called from inspector when the Region changes.
        /// </summary>
        internal void OnRegionChanged()
        {
            if (IsCustom)
                return;

            SetDisblay();
        }

        private void SetDisblay()
        {
            switch (LCID)
            {
                case LCID.NONE:

                    Code = Language.ToString();
                    DisplayName = LocalizationUtils.ISO639_Names[(int)Language];

                    if (Country != ISO3166_2.NONE)
                    {
                        Code = StringUtils.Concat(Code, "-", Country.ToString());
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

                case LCID.INVARIANT:

                    Code = LCID.ToString();
                    DisplayName = "Invariant Language";

                    break;

                default:

                    var info = new CultureInfo((int)LCID);
                    Code = info.Name.ToUpper();
                    DisplayName = info.DisplayName;

                    if (!string.IsNullOrWhiteSpace(Region))
                    {
                        Code = StringUtils.Concat(Code, "-", Region.ToUpper());

                        if (DisplayName.Contains(")"))
                        {
                            DisplayName = DisplayName.Replace(")", string.Empty);
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

#endif

        #endregion

    }
}