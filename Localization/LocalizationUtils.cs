using System.Collections.Generic;

namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// LocalizedObject Utils
    ///
    /// <para>
    /// Static collection of methods and variables for localization.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    public static class LocalizationUtils
    {



        #region Convertion Methods

        /// <summary>
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="coutry">Conutry reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID (ISO639_1 language, ISO3166_2 coutry)
        {
            if (language == ISO639_1.NONE) return LCID.INVARIANT;

            var textCode = language.ToString ();

            if (coutry != ISO3166_2.NONE)
                textCode = StringUtils.Concat (textCode, "_", coutry);

            if (System.Enum.TryParse (textCode, out LCID lcid)) return lcid;

            return LCID.NONE;
        }

        /// <summary>
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="coutry">Conutry reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID (int language, int coutry) =>
            ToLCID ((ISO639_1) language, (ISO3166_2) coutry);

        /// <summary>
        /// Converts LCID to ISO-639 Language numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
        public static int ToISO639 (LCID lcid)
        {
            var splittedLCID = lcid.ToString ().Split ('_');
            if (splittedLCID.Length == 0) return 0;

            var language = splittedLCID[0];
            if (System.Enum.TryParse (language, out ISO639_1 iso))
                return (int) iso;

            return 0;
        }

        /// <summary>
        /// Converts LCID to ISO-3166 coutry numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
        public static int ToISO3166 (LCID lcid)
        {
            var splittedLCID = lcid.ToString ().Split ('_');

            if (splittedLCID.Length == 0 || splittedLCID.Length == 1) return 0;

            var country = splittedLCID[1];
            if (System.Enum.TryParse (country, out ISO3166_2 iso))
                return (int) iso;

            return 0;
        }

        #endregion



        #region Display Names

        /// <summary>
        /// Collection of displays names for the ISO 639 standard.
        /// </summary>
        /// <value>Display name for ISO 639 standard.</value>
        public static readonly Dictionary<int, string> ISO639_Names =
            new Dictionary<int, string>
            {
                {0, "No Language"},
                {1, "Afar"},
                {2, "Abkhazian"},
                {3, "Avestan"},
                {4, "Afrikaans"},
                {5, "Akan"},
                {6, "Amharic"},
                {7, "Aragonese"},
                {8, "Arabic"},
                {9, "Assamese"},
                {10, "Avaric"},
                {11, "Aymara"},
                {12, "Azerbaijani"},
                {13, "Bashkir"},
                {14, "Belarusian"},
                {15, "Bulgarian"},
                {16, "Bihari languages"},
                {17, "Bislama"},
                {18, "Bambara"},
                {19, "Bengali"},
                {20, "Tibetan"},
                {21, "Breton"},
                {22, "Bosnian"},
                {23, "Catalan, Valencian"},
                {24, "Chechen"},
                {25, "Chamorro"},
                {26, "Corsican"},
                {27, "Cree"},
                {28, "Czech"},
                {29, "Church Slavic, Old-Church Slavonic, Old Bulgarian"},
                {30, "Chuvash"},
                {31, "Welsh"},
                {32, "Danish"},
                {33, "German"},
                {34, "Divehi, Dhivehi, Maldivian"},
                {35, "Dzongkha"},
                {36, "Ewe"},
                {37, "Greek, Modern (1453-)"},
                {38, "English"},
                {39, "Esperanto"},
                {40, "Spanish, Castilian"},
                {41, "Estonian"},
                {42, "Basque"},
                {43, "Persian"},
                {44, "Fulah"},
                {45, "Finnish"},
                {46, "Fijian"},
                {47, "Faroese"},
                {48, "French"},
                {49, "Western Frisian"},
                {50, "Irish"},
                {51, "Gaelic, Scottish Gaelic"},
                {52, "Galician"},
                {53, "Guarani"},
                {54, "Gujarati"},
                {55, "Manx"},
                {56, "Hausa"},
                {57, "Hebrew"},
                {58, "Hindi"},
                {59, "Hiri Motu"},
                {60, "Croatian"},
                {61, "Haitian, Haitian Creole"},
                {62, "Hungarian"},
                {63, "Armenian"},
                {64, "Herero"},
                {
                    65,
                    "Interlingua(International Auxiliary Language Association)"
                },
                {66, "Indonesian"},
                {67, "Interlingue, Occidental"},
                {68, "Igbo"},
                {69, "Sichuan Yi, Nuosu"},
                {70, "Inupiaq"},
                {71, "Ido"},
                {72, "Icelandic"},
                {73, "Italian"},
                {74, "Inuktitut"},
                {75, "Japanese"},
                {76, "Javanese"},
                {77, "Georgian"},
                {78, "Kongo"},
                {79, "Kikuyu, Gikuyu"},
                {80, "Kuanyama, Kwanyama"},
                {81, "Kazakh"},
                {82, "Kalaallisut, Greenlandic"},
                {83, "Central Khmer"},
                {84, "Kannada"},
                {85, "Korean"},
                {86, "Kanuri"},
                {87, "Kashmiri"},
                {88, "Kurdish"},
                {89, "Komi"},
                {90, "Cornish"},
                {91, "Kirghiz, Kyrgyz"},
                {92, "Latin"},
                {93, "Luxembourgish, Letzeburgesch"},
                {94, "Ganda"},
                {95, "Limburgan, Limburger, Limburgish"},
                {96, "Lingala"},
                {97, "Lao"},
                {98, "Lithuanian"},
                {99, "Luba-Katanga"},
                {100, "Latvian"},
                {101, "Malagasy"},
                {102, "Marshallese"},
                {103, "Maori"},
                {104, "Macedonian"},
                {105, "Malayalam"},
                {106, "Mongolian"},
                {107, "Marathi"},
                {108, "Malay"},
                {109, "Maltese"},
                {110, "Burmese"},
                {111, "Nauru"},
                {112, "Norwegian Bokmål"},
                {113, "North Ndebele"},
                {114, "Nepali"},
                {115, "Ndonga"},
                {116, "Dutch, Flemish"},
                {117, "Norwegian Nynorsk"},
                {118, "Norwegian"},
                {119, "South Ndebele"},
                {120, "Navajo, Navaho"},
                {121, "Chichewa, Chewa, Nyanja"},
                {122, "Occitan"},
                {123, "Ojibwa"},
                {124, "Oromo"},
                {125, "Oriya"},
                {126, "Ossetian, Ossetic"},
                {127, "Punjabi, Panjabi"},
                {128, "Pali"},
                {129, "Polish"},
                {130, "Pashto, Pushto"},
                {131, "Portuguese"},
                {132, "Quechua"},
                {133, "Romansh"},
                {134, "Rundi"},
                {135, "Romanian, Moldavian, Moldovan"},
                {136, "Russian"},
                {137, "Kinyarwanda"},
                {138, "Sanskrit"},
                {139, "Sardinian"},
                {140, "Sindhi"},
                {141, "Northern Sami"},
                {142, "Sango"},
                {143, "Sinhala, Sinhalese"},
                {144, "Slovak"},
                {145, "Slovenian"},
                {146, "Samoan"},
                {147, "Shona"},
                {148, "Somali"},
                {149, "Albanian"},
                {150, "Serbian"},
                {151, "Swati"},
                {152, "Southern Sotho"},
                {153, "Sundanese"},
                {154, "Swedish"},
                {155, "Swahili"},
                {156, "Tamil"},
                {157, "Telugu"},
                {158, "Tajik"},
                {159, "Thai"},
                {160, "Tigrinya"},
                {161, "Turkmen"},
                {162, "Tagalog"},
                {163, "Tswana"},
                {164, "Tonga(Tonga Islands)"},
                {165, "Turkish"},
                {166, "Tsonga"},
                {167, "Tatar"},
                {168, "Twi"},
                {169, "Tahitian"},
                {170, "Uighur, Uyghur"},
                {171, "Ukrainian"},
                {172, "Urdu"},
                {173, "Uzbek"},
                {174, "Venda"},
                {175, "Vietnamese"},
                {176, "Volapük"},
                {177, "Walloon"},
                {178, "Wolof"},
                {179, "Xhosa"},
                {180, "Yiddish"},
                {181, "Yoruba"},
                {182, "Zhuang, Chuang"},
                {183, "Chinese"},
                {184, "Zulu"}
            };

        /// <summary>
        /// Collection of displays names for the ISO 3166 standard.
        /// </summary>
        /// <value>Display name for ISO 3166 standard.</value>
        public static readonly Dictionary<int, string> ISO3166_Names =
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
                {826, "United Kingdom of Great Britain and Northern Ireland"},
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

    #endregion
}