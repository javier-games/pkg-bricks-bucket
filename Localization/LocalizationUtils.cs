using System.Collections.Generic;
using UnityEngine;

namespace BricksBucket.Localization
{
    public static class LocalizationUtils
    {



        #region Convertion Methods

        /// <summary>
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="coutry">Conutry reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID(ISO639_1 language, ISO3166_2 coutry)
        {
            if (language == ISO639_1.NONE)
                return LCID.INVARIANT;

            var textCode = language.ToString();

            if (coutry != ISO3166_2.NONE)
                textCode = StringUtils.Concat(textCode, "_", coutry);

            if (System.Enum.TryParse(textCode, out LCID lcid))
                return lcid;

            return LCID.NONE;
        }

        /// <summary>
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="coutry">Conutry reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID(int language, int coutry) =>
            ToLCID((ISO639_1)language, (ISO3166_2)coutry);

        /// <summary>
        /// Converts LCID to ISO-639 Language numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
        public static int ToISO639(LCID lcid)
        {
            var splittedLCID = lcid.ToString().Split('_');
            if (splittedLCID.Length == 0)
                return 0;

            var language = splittedLCID[0];
            if (System.Enum.TryParse(language, out ISO639_1 iso))
                return (int)iso;

            return 0;
        }

        /// <summary>
        /// Converts LCID to ISO-3166 coutry numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
        public static int ToISO3166(LCID lcid)
        {
            var splittedLCID = lcid.ToString().Split('_');

            if (splittedLCID.Length == 0 || splittedLCID.Length == 1)
                return 0;

            var country = splittedLCID[1];
            if (System.Enum.TryParse(country, out ISO3166_2 iso))
                return (int)iso;

            return 0;
        }

        #endregion



        #region Display Names

        /// <summary>
        /// Collection of displays names for the ISO 639 standard.
        /// </summary>
        /// <value>Display name for ISO 639 standard.</value>
        public static readonly Dictionary<int,string> ISO639_Names =
        new Dictionary<int, string>
        {
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
            {29, "Church Slavic, Old Slavonic, Church Slavonic, Old Bulgarian, Old Church Slavonic"},
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
            {65, "Interlingua(International Auxiliary Language Association)"},
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
            {29, "Church Slavic, Old Slavonic, Church Slavonic, Old Bulgarian, Old Church Slavonic"},
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
            {65, "Interlingua(International Auxiliary Language Association)"},
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
    }

    #endregion
}