using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
namespace BricksBucket.Localization
{
    /// <!-- LocalizationUtils -->
    /// 
    /// <summary>
    /// Static collection of utilities (methods and extensions) for the
    /// localization system.
    /// </summary>
    ///
    /// <seealso cref="BricksBucket.Localization.LCID"/>
    /// <seealso cref="BricksBucket.Localization.ISO639"/>
    /// <seealso cref="BricksBucket.Localization.ISO3166"/>
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    public static class LocalizationUtils
    {



        #region Convertion Methods

        /// <summary>
        /// Converts a language in the ISO 639-1 standard and a country in the
        /// ISO 3166-2 standard to the corresponding Language Code ID.
        /// </summary>
        /// <param name="language">ISO 639-1 Language to convert.</param>
        /// <param name="country">ISO 3166-2 Country reference.</param>
        /// <returns>Language Code Identifier. Returns <value><c>LCID.NONE</c>
        /// </value> if no match was found.</returns>
        public static LCID ToLCID (
            ISO639 language, ISO3166 country
        )
        {
            if (language == ISO639.NONE) return LCID.INVARIANT;

            var textCode = language.ToString ();

            if (country != Localization.ISO3166.NONE)
                textCode = StringUtils.Concat (textCode, "_", country);

            return System.Enum.TryParse (textCode, out LCID lcid)
                ? lcid
                : LCID.NONE;
        }

        /// <summary>
        /// Converts a language int value and a country int value to the
        /// corresponding Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="country">Country reference.</param>
        /// <returns>Language Code Identifier. Returns <value><c>LCID.NONE</c>
        /// </value> if no match was found.</returns>
        public static LCID ToLCID (int language, int country) =>
            ToLCID ((ISO639) language, (ISO3166) country);

        /// <summary>
        /// Converts a Language Code ID to a language ISO-639 numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier to convert.</param>
        /// <returns>Numeric representation of the ISO-639 standard.</returns>
        public static int ToISO639 (LCID lcid)
        {
            var dividedLCID = lcid.ToString ().Split ('_');
            if (dividedLCID.Length == 0) return 0;

            var language = dividedLCID[0];
            if (System.Enum.TryParse (language, out ISO639 iso))
                return (int) iso;

            return 0;
        }

        /// <summary>
        /// Converts a Language Code ID to a country ISO-3166 numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier to convert.</param>
        /// <returns>Numeric representation of the ISO-3166 standard.</returns>
        public static int ToISO3166 (LCID lcid)
        {
            var dividedLCID = lcid.ToString ().Split ('_');

            if (dividedLCID.Length == 0 || dividedLCID.Length == 1) return 0;

            var country = dividedLCID[1];
            if (System.Enum.TryParse (country, out ISO3166 iso))
                return (int) iso;

            return 0;
        }

        /// <summary>
        /// Converts a regular string to a code formatted string for
        /// localization codes.
        /// </summary>
        /// <param name="unformattedCode">String to convert.</param>
        /// <returns>String in <c>UPPER_SNAKE_CASE</c> format without any
        /// diacritics and any special characters.</returns>
        public static string ToCodeFormat (this string unformattedCode)
        {
            if (string.IsNullOrWhiteSpace (unformattedCode))
                return string.Empty;

            return unformattedCode.
                RemoveDiacritics ().
                ToUpper ().
                Replace (' ', '_').
                Replace ('-', '_').
                RemoveSpecialCharacters ('_');
        }

        #endregion

        
        public static class Names
        {


            /// <summary>
            /// Collection of displays names for the ISO 639 standard.
            /// </summary>
            /// <value>Display name for ISO 639 standard.</value>
            public static readonly Dictionary<ISO639, string> ISO639 =
                new Dictionary<ISO639, string>
                {
                    {Localization.ISO639.IS, ""},
                };



            /// <summary>
            /// Collection of displays names of the ISO 3166 standard.
            /// </summary>
            /// <value>Display name for ISO 3166 standard.</value>
            public static readonly Dictionary<int, string> ISO3166 =
                new Dictionary<int, string>
                {
                    {0, "No Country"},
                    {4, "Afghanistan"},
                    {248, "Åland Islands"},
                    {8, "Albania"},
                    {12, "Algeria"},
                    {16, "American Samoa"},
                    {20, "Andorra"},
                    {24, "Angola"},
                    {660, "Anguilla"},
                    {10, "Antarctica"},
                    {28, "Antigua and Barbuda"},
                    {32, "Argentina"},
                    {51, "Armenia"},
                    {533, "Aruba"},
                    {36, "Australia"},
                    {40, "Austria"},
                    {31, "Azerbaijan"},
                    {44, "Bahamas"},
                    {48, "Bahrain"},
                    {50, "Bangladesh"},
                    {52, "Barbados"},
                    {112, "Belarus"},
                    {56, "Belgium"},
                    {84, "Belize"},
                    {204, "Benin"},
                    {60, "Bermuda"},
                    {64, "Bhutan"},
                    {68, "Bolivia (Plurinational State of)"},
                    {535, "Bonaire, Sint Eustatius and Saba"},
                    {70, "Bosnia and Herzegovina"},
                    {72, "Botswana"},
                    {74, "Bouvet Island"},
                    {76, "Brazil"},
                    {86, "British Indian Ocean Territory"},
                    {96, "Brunei Darussalam"},
                    {100, "Bulgaria"},
                    {854, "Burkina Faso"},
                    {108, "Burundi"},
                    {132, "Cabo Verde"},
                    {116, "Cambodia"},
                    {120, "Cameroon"},
                    {124, "Canada"},
                    {136, "Cayman Islands"},
                    {140, "Central African Republic"},
                    {148, "Chad"},
                    {152, "Chile"},
                    {156, "China"},
                    {162, "Christmas Island"},
                    {166, "Cocos (Keeling) Islands"},
                    {170, "Colombia"},
                    {174, "Comoros"},
                    {178, "Congo"},
                    {180, "Congo, Democratic Republic of the"},
                    {184, "Cook Islands"},
                    {188, "Costa Rica"},
                    {384, "Côte d'Ivoire"},
                    {191, "Croatia"},
                    {192, "Cuba"},
                    {531, "Curaçao"},
                    {196, "Cyprus"},
                    {203, "Czechia"},
                    {208, "Denmark"},
                    {262, "Djibouti"},
                    {212, "Dominica"},
                    {214, "Dominican Republic"},
                    {218, "Ecuador"},
                    {818, "Egypt"},
                    {222, "El Salvador"},
                    {226, "Equatorial Guinea"},
                    {232, "Eritrea"},
                    {233, "Estonia"},
                    {748, "Eswatini"},
                    {231, "Ethiopia"},
                    {238, "Falkland Islands (Malvinas)"},
                    {234, "Faroe Islands"},
                    {242, "Fiji"},
                    {246, "Finland"},
                    {250, "France"},
                    {254, "French Guiana"},
                    {258, "French Polynesia"},
                    {260, "French Southern Territories"},
                    {266, "Gabon"},
                    {270, "Gambia"},
                    {268, "Georgia"},
                    {276, "Germany"},
                    {288, "Ghana"},
                    {292, "Gibraltar"},
                    {300, "Greece"},
                    {304, "Greenland"},
                    {308, "Grenada"},
                    {312, "Guadeloupe"},
                    {316, "Guam"},
                    {320, "Guatemala"},
                    {831, "Guernsey"},
                    {324, "Guinea"},
                    {624, "Guinea-Bissau"},
                    {328, "Guyana"},
                    {332, "Haiti"},
                    {334, "Heard Island and McDonald Islands"},
                    {336, "Holy See"},
                    {340, "Honduras"},
                    {344, "Hong Kong"},
                    {348, "Hungary"},
                    {352, "Iceland"},
                    {356, "India"},
                    {360, "Indonesia"},
                    {364, "Iran (Islamic Republic of)"},
                    {368, "Iraq"},
                    {372, "Ireland"},
                    {833, "Isle of Man"},
                    {376, "Israel"},
                    {380, "Italy"},
                    {388, "Jamaica"},
                    {392, "Japan"},
                    {832, "Jersey"},
                    {400, "Jordan"},
                    {398, "Kazakhstan"},
                    {404, "Kenya"},
                    {296, "Kiribati"},
                    {408, "Korea (Democratic People's Republic of)"},
                    {410, "Korea, Republic of"},
                    {414, "Kuwait"},
                    {417, "Kyrgyzstan"},
                    {418, "Lao People's Democratic Republic"},
                    {428, "Latvia"},
                    {422, "Lebanon"},
                    {426, "Lesotho"},
                    {430, "Liberia"},
                    {434, "Libya"},
                    {438, "Liechtenstein"},
                    {440, "Lithuania"},
                    {442, "Luxembourg"},
                    {446, "Macao"},
                    {450, "Madagascar"},
                    {454, "Malawi"},
                    {458, "Malaysia"},
                    {462, "Maldives"},
                    {466, "Mali"},
                    {470, "Malta"},
                    {584, "Marshall Islands"},
                    {474, "Martinique"},
                    {478, "Mauritania"},
                    {480, "Mauritius"},
                    {175, "Mayotte"},
                    {484, "Mexico"},
                    {583, "Micronesia (Federated States of)"},
                    {498, "Moldova, Republic of"},
                    {492, "Monaco"},
                    {496, "Mongolia"},
                    {499, "Montenegro"},
                    {500, "Montserrat"},
                    {504, "Morocco"},
                    {508, "Mozambique"},
                    {104, "Myanmar"},
                    {516, "Namibia"},
                    {520, "Nauru"},
                    {524, "Nepal"},
                    {528, "Netherlands"},
                    {540, "New Caledonia"},
                    {554, "New Zealand"},
                    {558, "Nicaragua"},
                    {562, "Niger"},
                    {566, "Nigeria"},
                    {570, "Niue"},
                    {574, "Norfolk Island"},
                    {807, "North Macedonia"},
                    {580, "Northern Mariana Islands"},
                    {578, "Norway"},
                    {512, "Oman"},
                    {586, "Pakistan"},
                    {585, "Palau"},
                    {275, "Palestine, State of"},
                    {591, "Panama"},
                    {598, "Papua New Guinea"},
                    {600, "Paraguay"},
                    {604, "Peru"},
                    {608, "Philippines"},
                    {612, "Pitcairn"},
                    {616, "Poland"},
                    {620, "Portugal"},
                    {630, "Puerto Rico"},
                    {634, "Qatar"},
                    {638, "Réunion"},
                    {642, "Romania"},
                    {643, "Russian Federation"},
                    {646, "Rwanda"},
                    {652, "Saint Barthélemy"},
                    {654, "Saint Helena, Ascension and Tristan da Cunha"},
                    {659, "Saint Kitts and Nevis"},
                    {662, "Saint Lucia"},
                    {663, "Saint Martin (French part)"},
                    {666, "Saint Pierre and Miquelon"},
                    {670, "Saint Vincent and the Grenadines"},
                    {882, "Samoa"},
                    {674, "San Marino"},
                    {678, "Sao Tome and Principe"},
                    {682, "Saudi Arabia"},
                    {686, "Senegal"},
                    {688, "Serbia"},
                    {690, "Seychelles"},
                    {694, "Sierra Leone"},
                    {702, "Singapore"},
                    {534, "Sint Maarten (Dutch part)"},
                    {703, "Slovakia"},
                    {705, "Slovenia"},
                    {90, "Solomon Islands"},
                    {706, "Somalia"},
                    {710, "South Africa"},
                    {239, "South Georgia and the South Sandwich Islands"},
                    {728, "South Sudan"},
                    {724, "Spain"},
                    {144, "Sri Lanka"},
                    {729, "Sudan"},
                    {740, "Suriname"},
                    {744, "Svalbard and Jan Mayen"},
                    {752, "Sweden"},
                    {756, "Switzerland"},
                    {760, "Syrian Arab Republic"},
                    {158, "Taiwan, Province of China[a]"},
                    {762, "Tajikistan"},
                    {834, "Tanzania, United Republic of"},
                    {764, "Thailand"},
                    {626, "Timor-Leste"},
                    {768, "Togo"},
                    {772, "Tokelau"},
                    {776, "Tonga"},
                    {780, "Trinidad and Tobago"},
                    {788, "Tunisia"},
                    {792, "Turkey"},
                    {795, "Turkmenistan"},
                    {796, "Turks and Caicos Islands"},
                    {798, "Tuvalu"},
                    {800, "Uganda"},
                    {804, "Ukraine"},
                    {784, "United Arab Emirates"},
                    {
                        826,
                        "United Kingdom of Great Britain and Northern Ireland"
                    },
                    {840, "United States of America"},
                    {581, "United States Minor Outlying Islands"},
                    {858, "Uruguay"},
                    {860, "Uzbekistan"},
                    {548, "Vanuatu"},
                    {862, "Venezuela (Bolivarian Republic of)"},
                    {704, "Viet Nam"},
                    {92, "Virgin Islands (British)"},
                    {850, "Virgin Islands (U.S.)"},
                    {876, "Wallis and Futuna"},
                    {732, "Western Sahara"},
                    {887, "Yemen"},
                    {894, "Zambia"},
                    {716, "Zimbabwe"},
                };

        }
    }
}