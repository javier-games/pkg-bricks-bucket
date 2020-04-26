using System.Collections.Generic;


// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
// ReSharper disable UnusedType.Global
namespace BricksBucket.Localization
{
    /// <!-- ISO639 -->
    ///
    /// <summary>
    /// 
    /// <para>
    /// <i>"This ISO standard can be applied across many types of organization
    /// and situations. It’s invaluable for bibliographic purposes, in libraries
    /// or information management, including computerized systems, and for the
    /// representation of different language versions on Websites."</i> - <see
    /// href= "https://www.iso.org/iso-3166-country-codes.html">
    /// International Organization for Standardization</see>.
    /// </para>
    ///
    /// <para>
    /// This class integrates the standard ISO 639 for languages into the
    /// <see href="../articles/localization">Bricks Bucket Localization System
    /// </see>. The relation ship between the different part of the standard,
    /// considered in the project, is shown in the <see href=
    /// "../articles/localization/standard_iso639.html">ISO 639 Table</see>.
    /// </para>
    /// 
    /// </summary>
    ///
    /// <seealso cref="BricksBucket.Localization.ISO639.Alpha1"/>
    /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2T"/>
    /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2B"/>
    /// <seealso cref="BricksBucket.Localization.ISO3166"/>
    /// <seealso cref="BricksBucket.Localization.LCID"/>
    /// <seealso cref="BricksBucket.Localization.Culture"/>
    /// 
    /// <!-- Note: The code of the members of the enum have been generated with
    /// the following table: https://bit.ly/bb-localization-iso639 -->
    ///
    /// <!-- By Javier García | @jvrgms | 2020 -->
    public static class ISO639
    {

        

        /// <summary>
        /// Collection of displays names for the ISO 639 standard.
        /// </summary>
        /// <value>Display name for ISO 639 standard.</value>
        public static readonly Dictionary<int, string> Names =
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



        /// <!-- Alpha1 -->
        ///
        /// <summary>
        /// Defines two letters codes for the names of languages according to
        /// the <b>ISO 639-1</b> standard, is the first part of the  <see href=
        /// "https://www.iso.org/iso-639-language-codes.html">ISO 639</see>
        /// series. Its relation ship with the other parts of the standard
        /// is shown in the <see href=
        /// "../articles/localization/standard_iso639.html">ISO 639 Table</see>.
        /// </summary>
        /// 
        /// <seealso cref="BricksBucket.Localization.ISO639"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2T"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2B"/>
        public enum Alpha1
        {
            /// <summary> No language. </summary>
            NONE = 0,

            /// <summary> Afar ISO 639-1 Code. </summary>
            AA = 1,

            /// <summary> Abkhazian ISO 639-1 Code. </summary>
            AB = 2,

            /// <summary> Avestan ISO 639-1 Code. </summary>
            AE = 3,

            /// <summary> Afrikaans ISO 639-1 Code. </summary>
            AF = 4,

            /// <summary> Akan ISO 639-1 Code. </summary>
            AK = 5,

            /// <summary> Amharic ISO 639-1 Code. </summary>
            AM = 6,

            /// <summary> Aragonese ISO 639-1 Code. </summary>
            AN = 7,

            /// <summary> Arabic ISO 639-1 Code. </summary>
            AR = 8,

            /// <summary> Assamese ISO 639-1 Code. </summary>
            AS = 9,

            /// <summary> Avaric ISO 639-1 Code. </summary>
            AV = 10,

            /// <summary> Aymara ISO 639-1 Code. </summary>
            AY = 11,

            /// <summary> Azerbaijani ISO 639-1 Code. </summary>
            AZ = 12,

            /// <summary> Bashkir ISO 639-1 Code. </summary>
            BA = 13,

            /// <summary> Belarusian ISO 639-1 Code. </summary>
            BE = 14,

            /// <summary> Bulgarian ISO 639-1 Code. </summary>
            BG = 15,

            /// <summary> Bihari languages ISO 639-1 Code. </summary>
            BH = 16,

            /// <summary> Bislama ISO 639-1 Code. </summary>
            BI = 17,

            /// <summary> Bambara ISO 639-1 Code. </summary>
            BM = 18,

            /// <summary> Bengali ISO 639-1 Code. </summary>
            BN = 19,

            /// <summary> Tibetan ISO 639-1 Code. </summary>
            BO = 20,

            /// <summary> Breton ISO 639-1 Code. </summary>
            BR = 21,

            /// <summary> Bosnian ISO 639-1 Code. </summary>
            BS = 22,

            /// <summary> Catalan, Valencian ISO 639-1 Code. </summary>
            CA = 23,

            /// <summary> Chechen ISO 639-1 Code. </summary>
            CE = 24,

            /// <summary> Chamorro ISO 639-1 Code. </summary>
            CH = 25,

            /// <summary> Corsican ISO 639-1 Code. </summary>
            CO = 26,

            /// <summary> Cree ISO 639-1 Code. </summary>
            CR = 27,

            /// <summary> Czech ISO 639-1 Code. </summary>
            CS = 28,

            /// <summary> Church Slavic, Old Slavonic, Church Slavonic,
            /// Old Bulgarian,Old Church Slavonic ISO 639-1 Code. </summary>
            CU = 29,

            /// <summary> Chuvash ISO 639-1 Code. </summary>
            CV = 30,

            /// <summary> Welsh ISO 639-1 Code. </summary>
            CY = 31,

            /// <summary> Danish ISO 639-1 Code. </summary>
            DA = 32,

            /// <summary> German ISO 639-1 Code. </summary>
            DE = 33,

            /// <summary> Divehi, Dhivehi, Maldivian ISO 639-1 Code. </summary>
            DV = 34,

            /// <summary> Dzongkha ISO 639-1 Code. </summary>
            DZ = 35,

            /// <summary> Ewe ISO 639-1 Code. </summary>
            EE = 36,

            /// <summary> Greek, Modern (1453-) ISO 639-1 Code. </summary>
            EL = 37,

            /// <summary> English ISO 639-1 Code. </summary>
            EN = 38,

            /// <summary> Esperanto ISO 639-1 Code. </summary>
            EO = 39,

            /// <summary> Spanish, Castilian ISO 639-1 Code. </summary>
            ES = 40,

            /// <summary> Estonian ISO 639-1 Code. </summary>
            ET = 41,

            /// <summary> Basque ISO 639-1 Code. </summary>
            EU = 42,

            /// <summary> Persian ISO 639-1 Code. </summary>
            FA = 43,

            /// <summary> Fulah ISO 639-1 Code. </summary>
            FF = 44,

            /// <summary> Finnish ISO 639-1 Code. </summary>
            FI = 45,

            /// <summary> Fijian ISO 639-1 Code. </summary>
            FJ = 46,

            /// <summary> Faroese ISO 639-1 Code. </summary>
            FO = 47,

            /// <summary> French ISO 639-1 Code. </summary>
            FR = 48,

            /// <summary> Western Frisian ISO 639-1 Code. </summary>
            FY = 49,

            /// <summary> Irish ISO 639-1 Code. </summary>
            GA = 50,

            /// <summary> Gaelic, Scottish Gaelic ISO 639-1 Code. </summary>
            GD = 51,

            /// <summary> Galician ISO 639-1 Code. </summary>
            GL = 52,

            /// <summary> Guarani ISO 639-1 Code. </summary>
            GN = 53,

            /// <summary> Gujarati ISO 639-1 Code. </summary>
            GU = 54,

            /// <summary> Manx ISO 639-1 Code. </summary>
            GV = 55,

            /// <summary> Hausa ISO 639-1 Code. </summary>
            HA = 56,

            /// <summary> Hebrew ISO 639-1 Code. </summary>
            HE = 57,

            /// <summary> Hindi ISO 639-1 Code. </summary>
            HI = 58,

            /// <summary> Hiri Motu ISO 639-1 Code. </summary>
            HO = 59,

            /// <summary> Croatian ISO 639-1 Code. </summary>
            HR = 60,

            /// <summary> Haitian, Haitian Creole ISO 639-1 Code. </summary>
            HT = 61,

            /// <summary> Hungarian ISO 639-1 Code. </summary>
            HU = 62,

            /// <summary> Armenian ISO 639-1 Code. </summary>
            HY = 63,

            /// <summary> Herero ISO 639-1 Code. </summary>
            HZ = 64,

            /// <summary> Interlingua(International Auxiliary Language
            /// Association) ISO 639-1 Code. </summary>
            IA = 65,

            /// <summary> Indonesian ISO 639-1 Code. </summary>
            ID = 66,

            /// <summary> Interlingue, Occidental ISO 639-1 Code. </summary>
            IE = 67,

            /// <summary> Igbo ISO 639-1 Code. </summary>
            IG = 68,

            /// <summary> Sichuan Yi, Nuosu ISO 639-1 Code. </summary>
            II = 69,

            /// <summary> Inupiaq ISO 639-1 Code. </summary>
            IK = 70,

            /// <summary> Ido ISO 639-1 Code. </summary>
            IO = 71,

            /// <summary> Icelandic ISO 639-1 Code. </summary>
            IS = 72,

            /// <summary> Italian ISO 639-1 Code. </summary>
            IT = 73,

            /// <summary> Inuktitut ISO 639-1 Code. </summary>
            IU = 74,

            /// <summary> Japanese ISO 639-1 Code. </summary>
            JA = 75,

            /// <summary> Javanese ISO 639-1 Code. </summary>
            JV = 76,

            /// <summary> Georgian ISO 639-1 Code. </summary>
            KA = 77,

            /// <summary> Kongo ISO 639-1 Code. </summary>
            KG = 78,

            /// <summary> Kikuyu, Gikuyu ISO 639-1 Code. </summary>
            KI = 79,

            /// <summary> Kuanyama, Kwanyama ISO 639-1 Code. </summary>
            KJ = 80,

            /// <summary> Kazakh ISO 639-1 Code. </summary>
            KK = 81,

            /// <summary> Kalaallisut, Greenlandic ISO 639-1 Code. </summary>
            KL = 82,

            /// <summary> Central Khmer ISO 639-1 Code. </summary>
            KM = 83,

            /// <summary> Kannada ISO 639-1 Code. </summary>
            KN = 84,

            /// <summary> Korean ISO 639-1 Code. </summary>
            KO = 85,

            /// <summary> Kanuri ISO 639-1 Code. </summary>
            KR = 86,

            /// <summary> Kashmiri ISO 639-1 Code. </summary>
            KS = 87,

            /// <summary> Kurdish ISO 639-1 Code. </summary>
            KU = 88,

            /// <summary> Komi ISO 639-1 Code. </summary>
            KV = 89,

            /// <summary> Cornish ISO 639-1 Code. </summary>
            KW = 90,

            /// <summary> Kirghiz, Kyrgyz ISO 639-1 Code. </summary>
            KY = 91,

            /// <summary> Latin ISO 639-1 Code. </summary>
            LA = 92,

            /// <summary> Luxembourgish, Letzeburgesch ISO 639-1 Code.
            /// </summary>
            LB = 93,

            /// <summary> Ganda ISO 639-1 Code. </summary>
            LG = 94,

            /// <summary> Limburgan, Limburger, Limburgish ISO 639-1
            /// Code. </summary>
            LI = 95,

            /// <summary> Lingala ISO 639-1 Code. </summary>
            LN = 96,

            /// <summary> Lao ISO 639-1 Code. </summary>
            LO = 97,

            /// <summary> Lithuanian ISO 639-1 Code. </summary>
            LT = 98,

            /// <summary> Luba-Katanga ISO 639-1 Code. </summary>
            LU = 99,

            /// <summary> Latvian ISO 639-1 Code. </summary>
            LV = 100,

            /// <summary> Malagasy ISO 639-1 Code. </summary>
            MG = 101,

            /// <summary> Marshallese ISO 639-1 Code. </summary>
            MH = 102,

            /// <summary> Maori ISO 639-1 Code. </summary>
            MI = 103,

            /// <summary> Macedonian ISO 639-1 Code. </summary>
            MK = 104,

            /// <summary> Malayalam ISO 639-1 Code. </summary>
            ML = 105,

            /// <summary> Mongolian ISO 639-1 Code. </summary>
            MN = 106,

            /// <summary> Marathi ISO 639-1 Code. </summary>
            MR = 107,

            /// <summary> Malay ISO 639-1 Code. </summary>
            MS = 108,

            /// <summary> Maltese ISO 639-1 Code. </summary>
            MT = 109,

            /// <summary> Burmese ISO 639-1 Code. </summary>
            MY = 110,

            /// <summary> Nauru ISO 639-1 Code. </summary>
            NA = 111,

            /// <summary> Norwegian Bokmål ISO 639-1 Code. </summary>
            NB = 112,

            /// <summary> North Ndebele ISO 639-1 Code. </summary>
            ND = 113,

            /// <summary> Nepali ISO 639-1 Code. </summary>
            NE = 114,

            /// <summary> Ndonga ISO 639-1 Code. </summary>
            NG = 115,

            /// <summary> Dutch, Flemish ISO 639-1 Code. </summary>
            NL = 116,

            /// <summary> Norwegian Nynorsk ISO 639-1 Code. </summary>
            NN = 117,

            /// <summary> Norwegian ISO 639-1 Code. </summary>
            NO = 118,

            /// <summary> South Ndebele ISO 639-1 Code. </summary>
            NR = 119,

            /// <summary> Navajo, Navaho ISO 639-1 Code. </summary>
            NV = 120,

            /// <summary> Chichewa, Chewa, Nyanja ISO 639-1 Code. </summary>
            NY = 121,

            /// <summary> Occitan ISO 639-1 Code. </summary>
            OC = 122,

            /// <summary> Ojibwa ISO 639-1 Code. </summary>
            OJ = 123,

            /// <summary> Oromo ISO 639-1 Code. </summary>
            OM = 124,

            /// <summary> Oriya ISO 639-1 Code. </summary>
            OR = 125,

            /// <summary> Ossetian, Ossetic ISO 639-1 Code. </summary>
            OS = 126,

            /// <summary> Punjabi, Panjabi ISO 639-1 Code. </summary>
            PA = 127,

            /// <summary> Pali ISO 639-1 Code. </summary>
            PI = 128,

            /// <summary> Polish ISO 639-1 Code. </summary>
            PL = 129,

            /// <summary> Pashto, Pushto ISO 639-1 Code. </summary>
            PS = 130,

            /// <summary> Portuguese ISO 639-1 Code. </summary>
            PT = 131,

            /// <summary> Quechua ISO 639-1 Code. </summary>
            QU = 132,

            /// <summary> Romansh ISO 639-1 Code. </summary>
            RM = 133,

            /// <summary> Rundi ISO 639-1 Code. </summary>
            RN = 134,

            /// <summary> Romanian, Moldavian, Moldovan ISO 639-1 Code.
            /// </summary>
            RO = 135,

            /// <summary> Russian ISO 639-1 Code. </summary>
            RU = 136,

            /// <summary> Kinyarwanda ISO 639-1 Code. </summary>
            RW = 137,

            /// <summary> Sanskrit ISO 639-1 Code. </summary>
            SA = 138,

            /// <summary> Sardinian ISO 639-1 Code. </summary>
            SC = 139,

            /// <summary> Sindhi ISO 639-1 Code. </summary>
            SD = 140,

            /// <summary> Northern Sami ISO 639-1 Code. </summary>
            SE = 141,

            /// <summary> Sango ISO 639-1 Code. </summary>
            SG = 142,

            /// <summary> Sinhala, Sinhalese ISO 639-1 Code. </summary>
            SI = 143,

            /// <summary> Slovak ISO 639-1 Code. </summary>
            SK = 144,

            /// <summary> Slovenian ISO 639-1 Code. </summary>
            SL = 145,

            /// <summary> Samoan ISO 639-1 Code. </summary>
            SM = 146,

            /// <summary> Shona ISO 639-1 Code. </summary>
            SN = 147,

            /// <summary> Somali ISO 639-1 Code. </summary>
            SO = 148,

            /// <summary> Albanian ISO 639-1 Code. </summary>
            SQ = 149,

            /// <summary> Serbian ISO 639-1 Code. </summary>
            SR = 150,

            /// <summary> Swati ISO 639-1 Code. </summary>
            SS = 151,

            /// <summary> Southern Sotho ISO 639-1 Code. </summary>
            ST = 152,

            /// <summary> Sundanese ISO 639-1 Code. </summary>
            SU = 153,

            /// <summary> Swedish ISO 639-1 Code. </summary>
            SV = 154,

            /// <summary> Swahili ISO 639-1 Code. </summary>
            SW = 155,

            /// <summary> Tamil ISO 639-1 Code. </summary>
            TA = 156,

            /// <summary> Telugu ISO 639-1 Code. </summary>
            TE = 157,

            /// <summary> Tajik ISO 639-1 Code. </summary>
            TG = 158,

            /// <summary> Thai ISO 639-1 Code. </summary>
            TH = 159,

            /// <summary> Tigrinya ISO 639-1 Code. </summary>
            TI = 160,

            /// <summary> Turkmen ISO 639-1 Code. </summary>
            TK = 161,

            /// <summary> Tagalog ISO 639-1 Code. </summary>
            TL = 162,

            /// <summary> Tswana ISO 639-1 Code. </summary>
            TN = 163,

            /// <summary> Tonga(Tonga Islands) ISO 639-1 Code. </summary>
            TO = 164,

            /// <summary> Turkish ISO 639-1 Code. </summary>
            TR = 165,

            /// <summary> Tsonga ISO 639-1 Code. </summary>
            TS = 166,

            /// <summary> Tatar ISO 639-1 Code. </summary>
            TT = 167,

            /// <summary> Twi ISO 639-1 Code. </summary>
            TW = 168,

            /// <summary> Tahitian ISO 639-1 Code. </summary>
            TY = 169,

            /// <summary> Uighur, Uyghur ISO 639-1 Code. </summary>
            UG = 170,

            /// <summary> Ukrainian ISO 639-1 Code. </summary>
            UK = 171,

            /// <summary> Urdu ISO 639-1 Code. </summary>
            UR = 172,

            /// <summary> Uzbek ISO 639-1 Code. </summary>
            UZ = 173,

            /// <summary> Venda ISO 639-1 Code. </summary>
            VE = 174,

            /// <summary> Vietnamese ISO 639-1 Code. </summary>
            VI = 175,

            /// <summary> Volapük ISO 639-1 Code. </summary>
            VO = 176,

            /// <summary> Walloon ISO 639-1 Code. </summary>
            WA = 177,

            /// <summary> Wolof ISO 639-1 Code. </summary>
            WO = 178,

            /// <summary> Xhosa ISO 639-1 Code. </summary>
            XH = 179,

            /// <summary> Yiddish ISO 639-1 Code. </summary>
            YI = 180,

            /// <summary> Yoruba ISO 639-1 Code. </summary>
            YO = 181,

            /// <summary> Zhuang, Chuang ISO 639-1 Code. </summary>
            ZA = 182,

            /// <summary> Chinese ISO 639-1 Code. </summary>
            ZH = 183,

            /// <summary> Zulu ISO 639-1 Code. </summary>
            ZU = 184,
        }

        
        
        /// <!-- Alpha2T -->
        ///
        /// <summary>
        /// Defines three letters codes for the names of languages according to
        /// the <b>ISO 639-2/T</b> terminological standard, is the second part
        /// of the <see href=
        /// "https://www.iso.org/iso-639-language-codes.html">ISO 639</see>
        /// series. Its relation ship with the other parts of the standard
        /// is shown in the <see href=
        /// "../articles/localization/standard_iso639.html">ISO 639 Table</see>.
        /// </summary>
        /// 
        /// <seealso cref="BricksBucket.Localization.ISO639"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha1"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2B"/>
        public enum Alpha2T
        {
            /// <summary> No language. </summary>
            NONE = 0,

            /// <summary> Afar ISO 639-2T Code. </summary>
            AAR = 1,

            /// <summary> Abkhazian ISO 639-2T Code. </summary>
            ABK = 2,

            /// <summary> Avestan ISO 639-2T Code. </summary>
            AVE = 3,

            /// <summary> Afrikaans ISO 639-2T Code. </summary>
            AFR = 4,

            /// <summary> Akan ISO 639-2T Code. </summary>
            AKA = 5,

            /// <summary> Amharic ISO 639-2T Code. </summary>
            AMH = 6,

            /// <summary> Aragonese ISO 639-2T Code. </summary>
            ARG = 7,

            /// <summary> Arabic ISO 639-2T Code. </summary>
            ARA = 8,

            /// <summary> Assamese ISO 639-2T Code. </summary>
            ASM = 9,

            /// <summary> Avaric ISO 639-2T Code. </summary>
            AVA = 10,

            /// <summary> Aymara ISO 639-2T Code. </summary>
            AYM = 11,

            /// <summary> Azerbaijani ISO 639-2T Code. </summary>
            AZE = 12,

            /// <summary> Bashkir ISO 639-2T Code. </summary>
            BAK = 13,

            /// <summary> Belarusian ISO 639-2T Code. </summary>
            BEL = 14,

            /// <summary> Bulgarian ISO 639-2T Code. </summary>
            BUL = 15,

            /// <summary> Bihari languages ISO 639-2T Code. </summary>
            BIH = 16,

            /// <summary> Bislama ISO 639-2T Code. </summary>
            BIS = 17,

            /// <summary> Bambara ISO 639-2T Code. </summary>
            BAM = 18,

            /// <summary> Bengali ISO 639-2T Code. </summary>
            BEN = 19,

            /// <summary> Tibetan ISO 639-2T Code. </summary>
            BOD = 20,

            /// <summary> Breton ISO 639-2T Code. </summary>
            BRE = 21,

            /// <summary> Bosnian ISO 639-2T Code. </summary>
            BOS = 22,

            /// <summary> Catalan, Valencian ISO 639-2T Code. </summary>
            CAT = 23,

            /// <summary> Chechen ISO 639-2T Code. </summary>
            CHE = 24,

            /// <summary> Chamorro ISO 639-2T Code. </summary>
            CHA = 25,

            /// <summary> Corsican ISO 639-2T Code. </summary>
            COS = 26,

            /// <summary> Cree ISO 639-2T Code. </summary>
            CRE = 27,

            /// <summary> Czech ISO 639-2T Code. </summary>
            CES = 28,

            /// <summary> Church Slavic, Old Slavonic, Church Slavonic,
            /// Old Bulgarian,Old Church Slavonic ISO 639-2T Code. </summary>
            CHU = 29,

            /// <summary> Chuvash ISO 639-2T Code. </summary>
            CHV = 30,

            /// <summary> Welsh ISO 639-2T Code. </summary>
            CYM = 31,

            /// <summary> Danish ISO 639-2T Code. </summary>
            DAN = 32,

            /// <summary> German ISO 639-2T Code. </summary>
            DEU = 33,

            /// <summary> Divehi, Dhivehi, Maldivian ISO 639-2T Code. </summary>
            DIV = 34,

            /// <summary> Dzongkha ISO 639-2T Code. </summary>
            DZO = 35,

            /// <summary> Ewe ISO 639-2T Code. </summary>
            EWE = 36,

            /// <summary> Greek, Modern (1453-) ISO 639-2T Code. </summary>
            ELL = 37,

            /// <summary> English ISO 639-2T Code. </summary>
            ENG = 38,

            /// <summary> Esperanto ISO 639-2T Code. </summary>
            EPO = 39,

            /// <summary> Spanish, Castilian ISO 639-2T Code. </summary>
            SPA = 40,

            /// <summary> Estonian ISO 639-2T Code. </summary>
            EST = 41,

            /// <summary> Basque ISO 639-2T Code. </summary>
            EUS = 42,

            /// <summary> Persian ISO 639-2T Code. </summary>
            FAS = 43,

            /// <summary> Fulah ISO 639-2T Code. </summary>
            FUL = 44,

            /// <summary> Finnish ISO 639-2T Code. </summary>
            FIN = 45,

            /// <summary> Fijian ISO 639-2T Code. </summary>
            FIJ = 46,

            /// <summary> Faroese ISO 639-2T Code. </summary>
            FAO = 47,

            /// <summary> French ISO 639-2T Code. </summary>
            FRA = 48,

            /// <summary> Western Frisian ISO 639-2T Code. </summary>
            FRY = 49,

            /// <summary> Irish ISO 639-2T Code. </summary>
            GLE = 50,

            /// <summary> Gaelic, Scottish Gaelic ISO 639-2T Code. </summary>
            GLA = 51,

            /// <summary> Galician ISO 639-2T Code. </summary>
            GLG = 52,

            /// <summary> Guarani ISO 639-2T Code. </summary>
            GRN = 53,

            /// <summary> Gujarati ISO 639-2T Code. </summary>
            GUJ = 54,

            /// <summary> Manx ISO 639-2T Code. </summary>
            GLV = 55,

            /// <summary> Hausa ISO 639-2T Code. </summary>
            HAU = 56,

            /// <summary> Hebrew ISO 639-2T Code. </summary>
            HEB = 57,

            /// <summary> Hindi ISO 639-2T Code. </summary>
            HIN = 58,

            /// <summary> Hiri Motu ISO 639-2T Code. </summary>
            HMO = 59,

            /// <summary> Croatian ISO 639-2T Code. </summary>
            HRV = 60,

            /// <summary> Haitian, Haitian Creole ISO 639-2T Code. </summary>
            HAT = 61,

            /// <summary> Hungarian ISO 639-2T Code. </summary>
            HUN = 62,

            /// <summary> Armenian ISO 639-2T Code. </summary>
            HYE = 63,

            /// <summary> Herero ISO 639-2T Code. </summary>
            HER = 64,

            /// <summary> Interlingua(International Auxiliary Language
            /// Association) ISO 639-2T Code. </summary>
            INA = 65,

            /// <summary> Indonesian ISO 639-2T Code. </summary>
            IND = 66,

            /// <summary> Interlingue, Occidental ISO 639-2T Code. </summary>
            ILE = 67,

            /// <summary> Igbo ISO 639-2T Code. </summary>
            IBO = 68,

            /// <summary> Sichuan Yi, Nuosu ISO 639-2T Code. </summary>
            III = 69,

            /// <summary> Inupiaq ISO 639-2T Code. </summary>
            IPK = 70,

            /// <summary> Ido ISO 639-2T Code. </summary>
            IDO = 71,

            /// <summary> Icelandic ISO 639-2T Code. </summary>
            ISL = 72,

            /// <summary> Italian ISO 639-2T Code. </summary>
            ITA = 73,

            /// <summary> Inuktitut ISO 639-2T Code. </summary>
            IKU = 74,

            /// <summary> Japanese ISO 639-2T Code. </summary>
            JPN = 75,

            /// <summary> Javanese ISO 639-2T Code. </summary>
            JAV = 76,

            /// <summary> Georgian ISO 639-2T Code. </summary>
            KAT = 77,

            /// <summary> Kongo ISO 639-2T Code. </summary>
            KON = 78,

            /// <summary> Kikuyu, Gikuyu ISO 639-2T Code. </summary>
            KIK = 79,

            /// <summary> Kuanyama, Kwanyama ISO 639-2T Code. </summary>
            KUA = 80,

            /// <summary> Kazakh ISO 639-2T Code. </summary>
            KAZ = 81,

            /// <summary> Kalaallisut, Greenlandic ISO 639-2T Code. </summary>
            KAL = 82,

            /// <summary> Central Khmer ISO 639-2T Code. </summary>
            KHM = 83,

            /// <summary> Kannada ISO 639-2T Code. </summary>
            KAN = 84,

            /// <summary> Korean ISO 639-2T Code. </summary>
            KOR = 85,

            /// <summary> Kanuri ISO 639-2T Code. </summary>
            KAU = 86,

            /// <summary> Kashmiri ISO 639-2T Code. </summary>
            KAS = 87,

            /// <summary> Kurdish ISO 639-2T Code. </summary>
            KUR = 88,

            /// <summary> Komi ISO 639-2T Code. </summary>
            KOM = 89,

            /// <summary> Cornish ISO 639-2T Code. </summary>
            COR = 90,

            /// <summary> Kirghiz, Kyrgyz ISO 639-2T Code. </summary>
            KIR = 91,

            /// <summary> Latin ISO 639-2T Code. </summary>
            LAT = 92,

            /// <summary> Luxembourgish, Letzeburgesch ISO 639-2T Code.
            /// </summary>
            LTZ = 93,

            /// <summary> Ganda ISO 639-2T Code. </summary>
            LUG = 94,

            /// <summary> Limburgan, Limburger, Limburgish ISO 639-2T
            /// Code. </summary>
            LIM = 95,

            /// <summary> Lingala ISO 639-2T Code. </summary>
            LIN = 96,

            /// <summary> Lao ISO 639-2T Code. </summary>
            LAO = 97,

            /// <summary> Lithuanian ISO 639-2T Code. </summary>
            LIT = 98,

            /// <summary> Luba-Katanga ISO 639-2T Code. </summary>
            LUB = 99,

            /// <summary> Latvian ISO 639-2T Code. </summary>
            LAV = 100,

            /// <summary> Malagasy ISO 639-2T Code. </summary>
            MLG = 101,

            /// <summary> Marshallese ISO 639-2T Code. </summary>
            MAH = 102,

            /// <summary> Maori ISO 639-2T Code. </summary>
            MRI = 103,

            /// <summary> Macedonian ISO 639-2T Code. </summary>
            MKD = 104,

            /// <summary> Malayalam ISO 639-2T Code. </summary>
            MAL = 105,

            /// <summary> Mongolian ISO 639-2T Code. </summary>
            MON = 106,

            /// <summary> Marathi ISO 639-2T Code. </summary>
            MAR = 107,

            /// <summary> Malay ISO 639-2T Code. </summary>
            MSA = 108,

            /// <summary> Maltese ISO 639-2T Code. </summary>
            MLT = 109,

            /// <summary> Burmese ISO 639-2T Code. </summary>
            MYA = 110,

            /// <summary> Nauru ISO 639-2T Code. </summary>
            NAU = 111,

            /// <summary> Norwegian Bokmål ISO 639-2T Code. </summary>
            NOB = 112,

            /// <summary> North Ndebele ISO 639-2T Code. </summary>
            NDE = 113,

            /// <summary> Nepali ISO 639-2T Code. </summary>
            NEP = 114,

            /// <summary> Ndonga ISO 639-2T Code. </summary>
            NDO = 115,

            /// <summary> Dutch, Flemish ISO 639-2T Code. </summary>
            NLD = 116,

            /// <summary> Norwegian Nynorsk ISO 639-2T Code. </summary>
            NNO = 117,

            /// <summary> Norwegian ISO 639-2T Code. </summary>
            NOR = 118,

            /// <summary> South Ndebele ISO 639-2T Code. </summary>
            NBL = 119,

            /// <summary> Navajo, Navaho ISO 639-2T Code. </summary>
            NAV = 120,

            /// <summary> Chichewa, Chewa, Nyanja ISO 639-2T Code. </summary>
            NYA = 121,

            /// <summary> Occitan ISO 639-2T Code. </summary>
            OCI = 122,

            /// <summary> Ojibwa ISO 639-2T Code. </summary>
            OJI = 123,

            /// <summary> Oromo ISO 639-2T Code. </summary>
            ORM = 124,

            /// <summary> Oriya ISO 639-2T Code. </summary>
            ORI = 125,

            /// <summary> Ossetian, Ossetic ISO 639-2T Code. </summary>
            OSS = 126,

            /// <summary> Punjabi, Panjabi ISO 639-2T Code. </summary>
            PAN = 127,

            /// <summary> Pali ISO 639-2T Code. </summary>
            PLI = 128,

            /// <summary> Polish ISO 639-2T Code. </summary>
            POL = 129,

            /// <summary> Pashto, Pushto ISO 639-2T Code. </summary>
            PUS = 130,

            /// <summary> Portuguese ISO 639-2T Code. </summary>
            POR = 131,

            /// <summary> Quechua ISO 639-2T Code. </summary>
            QUE = 132,

            /// <summary> Romansh ISO 639-2T Code. </summary>
            ROH = 133,

            /// <summary> Rundi ISO 639-2T Code. </summary>
            RUN = 134,

            /// <summary> Romanian, Moldavian, Moldovan ISO 639-2T Code.
            /// </summary>
            RON = 135,

            /// <summary> Russian ISO 639-2T Code. </summary>
            RUS = 136,

            /// <summary> Kinyarwanda ISO 639-2T Code. </summary>
            KIN = 137,

            /// <summary> Sanskrit ISO 639-2T Code. </summary>
            SAN = 138,

            /// <summary> Sardinian ISO 639-2T Code. </summary>
            SRD = 139,

            /// <summary> Sindhi ISO 639-2T Code. </summary>
            SND = 140,

            /// <summary> Northern Sami ISO 639-2T Code. </summary>
            SME = 141,

            /// <summary> Sango ISO 639-2T Code. </summary>
            SAG = 142,

            /// <summary> Sinhala, Sinhalese ISO 639-2T Code. </summary>
            SIN = 143,

            /// <summary> Slovak ISO 639-2T Code. </summary>
            SLK = 144,

            /// <summary> Slovenian ISO 639-2T Code. </summary>
            SLV = 145,

            /// <summary> Samoan ISO 639-2T Code. </summary>
            SMO = 146,

            /// <summary> Shona ISO 639-2T Code. </summary>
            SNA = 147,

            /// <summary> Somali ISO 639-2T Code. </summary>
            SOM = 148,

            /// <summary> Albanian ISO 639-2T Code. </summary>
            SQI = 149,

            /// <summary> Serbian ISO 639-2T Code. </summary>
            SRP = 150,

            /// <summary> Swati ISO 639-2T Code. </summary>
            SSW = 151,

            /// <summary> Southern Sotho ISO 639-2T Code. </summary>
            SOT = 152,

            /// <summary> Sundanese ISO 639-2T Code. </summary>
            SUN = 153,

            /// <summary> Swedish ISO 639-2T Code. </summary>
            SWE = 154,

            /// <summary> Swahili ISO 639-2T Code. </summary>
            SWA = 155,

            /// <summary> Tamil ISO 639-2T Code. </summary>
            TAM = 156,

            /// <summary> Telugu ISO 639-2T Code. </summary>
            TEL = 157,

            /// <summary> Tajik ISO 639-2T Code. </summary>
            TGK = 158,

            /// <summary> Thai ISO 639-2T Code. </summary>
            THA = 159,

            /// <summary> Tigrinya ISO 639-2T Code. </summary>
            TIR = 160,

            /// <summary> Turkmen ISO 639-2T Code. </summary>
            TUK = 161,

            /// <summary> Tagalog ISO 639-2T Code. </summary>
            TGL = 162,

            /// <summary> Tswana ISO 639-2T Code. </summary>
            TSN = 163,

            /// <summary> Tonga(Tonga Islands) ISO 639-2T Code. </summary>
            TON = 164,

            /// <summary> Turkish ISO 639-2T Code. </summary>
            TUR = 165,

            /// <summary> Tsonga ISO 639-2T Code. </summary>
            TSO = 166,

            /// <summary> Tatar ISO 639-2T Code. </summary>
            TAT = 167,

            /// <summary> Twi ISO 639-2T Code. </summary>
            TWI = 168,

            /// <summary> Tahitian ISO 639-2T Code. </summary>
            TAH = 169,

            /// <summary> Uighur, Uyghur ISO 639-2T Code. </summary>
            UIG = 170,

            /// <summary> Ukrainian ISO 639-2T Code. </summary>
            UKR = 171,

            /// <summary> Urdu ISO 639-2T Code. </summary>
            URD = 172,

            /// <summary> Uzbek ISO 639-2T Code. </summary>
            UZB = 173,

            /// <summary> Venda ISO 639-2T Code. </summary>
            VEN = 174,

            /// <summary> Vietnamese ISO 639-2T Code. </summary>
            VIE = 175,

            /// <summary> Volapük ISO 639-2T Code. </summary>
            VOL = 176,

            /// <summary> Walloon ISO 639-2T Code. </summary>
            WLN = 177,

            /// <summary> Wolof ISO 639-2T Code. </summary>
            WOL = 178,

            /// <summary> Xhosa ISO 639-2T Code. </summary>
            XHO = 179,

            /// <summary> Yiddish ISO 639-2T Code. </summary>
            YID = 180,

            /// <summary> Yoruba ISO 639-2T Code. </summary>
            YOR = 181,

            /// <summary> Zhuang, Chuang ISO 639-2T Code. </summary>
            ZHA = 182,

            /// <summary> Chinese ISO 639-2T Code. </summary>
            ZHO = 183,

            /// <summary> Zulu ISO 639-2T Code. </summary>
            ZUL = 184,
        }

        
        
        /// <!-- Alpha2B -->
        ///
        /// <summary>
        /// Defines three letters codes for the names of languages according to
        /// the <b>ISO 639-2/B</b> bibliographic standard, is the second part
        /// of the <see href=
        /// "https://www.iso.org/iso-639-language-codes.html">ISO 639</see>
        /// series. Its relation ship with the other parts of the standard
        /// is shown in the <see href=
        /// "../articles/localization/standard_iso639.html">ISO 639 Table</see>.
        /// </summary>
        /// 
        /// <seealso cref="BricksBucket.Localization.ISO639"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2T"/>
        /// <seealso cref="BricksBucket.Localization.ISO639.Alpha2B"/>
        public enum Alpha2B
        {
            /// <summary> No language. </summary>
            NONE = 0,

            /// <summary> Afar ISO 639-2B Code. </summary>
            AAR = 1,

            /// <summary> Abkhazian ISO 639-2B Code. </summary>
            ABK = 2,

            /// <summary> Avestan ISO 639-2B Code. </summary>
            AVE = 3,

            /// <summary> Afrikaans ISO 639-2B Code. </summary>
            AFR = 4,

            /// <summary> Akan ISO 639-2B Code. </summary>
            AKA = 5,

            /// <summary> Amharic ISO 639-2B Code. </summary>
            AMH = 6,

            /// <summary> Aragonese ISO 639-2B Code. </summary>
            ARG = 7,

            /// <summary> Arabic ISO 639-2B Code. </summary>
            ARA = 8,

            /// <summary> Assamese ISO 639-2B Code. </summary>
            ASM = 9,

            /// <summary> Avaric ISO 639-2B Code. </summary>
            AVA = 10,

            /// <summary> Aymara ISO 639-2B Code. </summary>
            AYM = 11,

            /// <summary> Azerbaijani ISO 639-2B Code. </summary>
            AZE = 12,

            /// <summary> Bashkir ISO 639-2B Code. </summary>
            BAK = 13,

            /// <summary> Belarusian ISO 639-2B Code. </summary>
            BEL = 14,

            /// <summary> Bulgarian ISO 639-2B Code. </summary>
            BUL = 15,

            /// <summary> Bihari languages ISO 639-2B Code. </summary>
            BIH = 16,

            /// <summary> Bislama ISO 639-2B Code. </summary>
            BIS = 17,

            /// <summary> Bambara ISO 639-2B Code. </summary>
            BAM = 18,

            /// <summary> Bengali ISO 639-2B Code. </summary>
            BEN = 19,

            /// <summary> Tibetan ISO 639-2B Code. </summary>
            TIB = 20,

            /// <summary> Breton ISO 639-2B Code. </summary>
            BRE = 21,

            /// <summary> Bosnian ISO 639-2B Code. </summary>
            BOS = 22,

            /// <summary> Catalan, Valencian ISO 639-2B Code. </summary>
            CAT = 23,

            /// <summary> Chechen ISO 639-2B Code. </summary>
            CHE = 24,

            /// <summary> Chamorro ISO 639-2B Code. </summary>
            CHA = 25,

            /// <summary> Corsican ISO 639-2B Code. </summary>
            COS = 26,

            /// <summary> Cree ISO 639-2B Code. </summary>
            CRE = 27,

            /// <summary> Czech ISO 639-2B Code. </summary>
            CZE = 28,

            /// <summary> Church Slavic, Old Slavonic, Church Slavonic,
            /// Old Bulgarian,Old Church Slavonic ISO 639-2B Code. </summary>
            CHU = 29,

            /// <summary> Chuvash ISO 639-2B Code. </summary>
            CHV = 30,

            /// <summary> Welsh ISO 639-2B Code. </summary>
            WEL = 31,

            /// <summary> Danish ISO 639-2B Code. </summary>
            DAN = 32,

            /// <summary> German ISO 639-2B Code. </summary>
            GER = 33,

            /// <summary> Divehi, Dhivehi, Maldivian ISO 639-2B Code. </summary>
            DIV = 34,

            /// <summary> Dzongkha ISO 639-2B Code. </summary>
            DZO = 35,

            /// <summary> Ewe ISO 639-2B Code. </summary>
            EWE = 36,

            /// <summary> Greek, Modern (1453-) ISO 639-2B Code. </summary>
            GRE = 37,

            /// <summary> English ISO 639-2B Code. </summary>
            ENG = 38,

            /// <summary> Esperanto ISO 639-2B Code. </summary>
            EPO = 39,

            /// <summary> Spanish, Castilian ISO 639-2B Code. </summary>
            SPA = 40,

            /// <summary> Estonian ISO 639-2B Code. </summary>
            EST = 41,

            /// <summary> Basque ISO 639-2B Code. </summary>
            BAQ = 42,

            /// <summary> Persian ISO 639-2B Code. </summary>
            PER = 43,

            /// <summary> Fulah ISO 639-2B Code. </summary>
            FUL = 44,

            /// <summary> Finnish ISO 639-2B Code. </summary>
            FIN = 45,

            /// <summary> Fijian ISO 639-2B Code. </summary>
            FIJ = 46,

            /// <summary> Faroese ISO 639-2B Code. </summary>
            FAO = 47,

            /// <summary> French ISO 639-2B Code. </summary>
            FRE = 48,

            /// <summary> Western Frisian ISO 639-2B Code. </summary>
            FRY = 49,

            /// <summary> Irish ISO 639-2B Code. </summary>
            GLE = 50,

            /// <summary> Gaelic, Scottish Gaelic ISO 639-2B Code. </summary>
            GLA = 51,

            /// <summary> Galician ISO 639-2B Code. </summary>
            GLG = 52,

            /// <summary> Guarani ISO 639-2B Code. </summary>
            GRN = 53,

            /// <summary> Gujarati ISO 639-2B Code. </summary>
            GUJ = 54,

            /// <summary> Manx ISO 639-2B Code. </summary>
            GLV = 55,

            /// <summary> Hausa ISO 639-2B Code. </summary>
            HAU = 56,

            /// <summary> Hebrew ISO 639-2B Code. </summary>
            HEB = 57,

            /// <summary> Hindi ISO 639-2B Code. </summary>
            HIN = 58,

            /// <summary> Hiri Motu ISO 639-2B Code. </summary>
            HMO = 59,

            /// <summary> Croatian ISO 639-2B Code. </summary>
            HRV = 60,

            /// <summary> Haitian, Haitian Creole ISO 639-2B Code. </summary>
            HAT = 61,

            /// <summary> Hungarian ISO 639-2B Code. </summary>
            HUN = 62,

            /// <summary> Armenian ISO 639-2B Code. </summary>
            ARM = 63,

            /// <summary> Herero ISO 639-2B Code. </summary>
            HER = 64,

            /// <summary> Interlingua(International Auxiliary Language
            /// Association) ISO 639-2B Code. </summary>
            INA = 65,

            /// <summary> Indonesian ISO 639-2B Code. </summary>
            IND = 66,

            /// <summary> Interlingue, Occidental ISO 639-2B Code. </summary>
            ILE = 67,

            /// <summary> Igbo ISO 639-2B Code. </summary>
            IBO = 68,

            /// <summary> Sichuan Yi, Nuosu ISO 639-2B Code. </summary>
            III = 69,

            /// <summary> Inupiaq ISO 639-2B Code. </summary>
            IPK = 70,

            /// <summary> Ido ISO 639-2B Code. </summary>
            IDO = 71,

            /// <summary> Icelandic ISO 639-2B Code. </summary>
            ICE = 72,

            /// <summary> Italian ISO 639-2B Code. </summary>
            ITA = 73,

            /// <summary> Inuktitut ISO 639-2B Code. </summary>
            IKU = 74,

            /// <summary> Japanese ISO 639-2B Code. </summary>
            JPN = 75,

            /// <summary> Javanese ISO 639-2B Code. </summary>
            JAV = 76,

            /// <summary> Georgian ISO 639-2B Code. </summary>
            GEO = 77,

            /// <summary> Kongo ISO 639-2B Code. </summary>
            KON = 78,

            /// <summary> Kikuyu, Gikuyu ISO 639-2B Code. </summary>
            KIK = 79,

            /// <summary> Kuanyama, Kwanyama ISO 639-2B Code. </summary>
            KUA = 80,

            /// <summary> Kazakh ISO 639-2B Code. </summary>
            KAZ = 81,

            /// <summary> Kalaallisut, Greenlandic ISO 639-2B Code. </summary>
            KAL = 82,

            /// <summary> Central Khmer ISO 639-2B Code. </summary>
            KHM = 83,

            /// <summary> Kannada ISO 639-2B Code. </summary>
            KAN = 84,

            /// <summary> Korean ISO 639-2B Code. </summary>
            KOR = 85,

            /// <summary> Kanuri ISO 639-2B Code. </summary>
            KAU = 86,

            /// <summary> Kashmiri ISO 639-2B Code. </summary>
            KAS = 87,

            /// <summary> Kurdish ISO 639-2B Code. </summary>
            KUR = 88,

            /// <summary> Komi ISO 639-2B Code. </summary>
            KOM = 89,

            /// <summary> Cornish ISO 639-2B Code. </summary>
            COR = 90,

            /// <summary> Kirghiz, Kyrgyz ISO 639-2B Code. </summary>
            KIR = 91,

            /// <summary> Latin ISO 639-2B Code. </summary>
            LAT = 92,

            /// <summary> Luxembourgish, Letzeburgesch ISO 639-2B Code.
            /// </summary>
            LTZ = 93,

            /// <summary> Ganda ISO 639-2B Code. </summary>
            LUG = 94,

            /// <summary> Limburgan, Limburger, Limburgish ISO 639-2B
            /// Code. </summary>
            LIM = 95,

            /// <summary> Lingala ISO 639-2B Code. </summary>
            LIN = 96,

            /// <summary> Lao ISO 639-2B Code. </summary>
            LAO = 97,

            /// <summary> Lithuanian ISO 639-2B Code. </summary>
            LIT = 98,

            /// <summary> Luba-Katanga ISO 639-2B Code. </summary>
            LUB = 99,

            /// <summary> Latvian ISO 639-2B Code. </summary>
            LAV = 100,

            /// <summary> Malagasy ISO 639-2B Code. </summary>
            MLG = 101,

            /// <summary> Marshallese ISO 639-2B Code. </summary>
            MAH = 102,

            /// <summary> Maori ISO 639-2B Code. </summary>
            MAO = 103,

            /// <summary> Macedonian ISO 639-2B Code. </summary>
            MAC = 104,

            /// <summary> Malayalam ISO 639-2B Code. </summary>
            MAL = 105,

            /// <summary> Mongolian ISO 639-2B Code. </summary>
            MON = 106,

            /// <summary> Marathi ISO 639-2B Code. </summary>
            MAR = 107,

            /// <summary> Malay ISO 639-2B Code. </summary>
            MAY = 108,

            /// <summary> Maltese ISO 639-2B Code. </summary>
            MLT = 109,

            /// <summary> Burmese ISO 639-2B Code. </summary>
            BUR = 110,

            /// <summary> Nauru ISO 639-2B Code. </summary>
            NAU = 111,

            /// <summary> Norwegian Bokmål ISO 639-2B Code. </summary>
            NOB = 112,

            /// <summary> North Ndebele ISO 639-2B Code. </summary>
            NDE = 113,

            /// <summary> Nepali ISO 639-2B Code. </summary>
            NEP = 114,

            /// <summary> Ndonga ISO 639-2B Code. </summary>
            NDO = 115,

            /// <summary> Dutch, Flemish ISO 639-2B Code. </summary>
            DUT = 116,

            /// <summary> Norwegian Nynorsk ISO 639-2B Code. </summary>
            NNO = 117,

            /// <summary> Norwegian ISO 639-2B Code. </summary>
            NOR = 118,

            /// <summary> South Ndebele ISO 639-2B Code. </summary>
            NBL = 119,

            /// <summary> Navajo, Navaho ISO 639-2B Code. </summary>
            NAV = 120,

            /// <summary> Chichewa, Chewa, Nyanja ISO 639-2B Code. </summary>
            NYA = 121,

            /// <summary> Occitan ISO 639-2B Code. </summary>
            OCI = 122,

            /// <summary> Ojibwa ISO 639-2B Code. </summary>
            OJI = 123,

            /// <summary> Oromo ISO 639-2B Code. </summary>
            ORM = 124,

            /// <summary> Oriya ISO 639-2B Code. </summary>
            ORI = 125,

            /// <summary> Ossetian, Ossetic ISO 639-2B Code. </summary>
            OSS = 126,

            /// <summary> Punjabi, Panjabi ISO 639-2B Code. </summary>
            PAN = 127,

            /// <summary> Pali ISO 639-2B Code. </summary>
            PLI = 128,

            /// <summary> Polish ISO 639-2B Code. </summary>
            POL = 129,

            /// <summary> Pashto, Pushto ISO 639-2B Code. </summary>
            PUS = 130,

            /// <summary> Portuguese ISO 639-2B Code. </summary>
            POR = 131,

            /// <summary> Quechua ISO 639-2B Code. </summary>
            QUE = 132,

            /// <summary> Romansh ISO 639-2B Code. </summary>
            ROH = 133,

            /// <summary> Rundi ISO 639-2B Code. </summary>
            RUN = 134,

            /// <summary> Romanian, Moldavian, Moldovan ISO 639-2B Code.
            /// </summary>
            RUM = 135,

            /// <summary> Russian ISO 639-2B Code. </summary>
            RUS = 136,

            /// <summary> Kinyarwanda ISO 639-2B Code. </summary>
            KIN = 137,

            /// <summary> Sanskrit ISO 639-2B Code. </summary>
            SAN = 138,

            /// <summary> Sardinian ISO 639-2B Code. </summary>
            SRD = 139,

            /// <summary> Sindhi ISO 639-2B Code. </summary>
            SND = 140,

            /// <summary> Northern Sami ISO 639-2B Code. </summary>
            SME = 141,

            /// <summary> Sango ISO 639-2B Code. </summary>
            SAG = 142,

            /// <summary> Sinhala, Sinhalese ISO 639-2B Code. </summary>
            SIN = 143,

            /// <summary> Slovak ISO 639-2B Code. </summary>
            SLO = 144,

            /// <summary> Slovenian ISO 639-2B Code. </summary>
            SLV = 145,

            /// <summary> Samoan ISO 639-2B Code. </summary>
            SMO = 146,

            /// <summary> Shona ISO 639-2B Code. </summary>
            SNA = 147,

            /// <summary> Somali ISO 639-2B Code. </summary>
            SOM = 148,

            /// <summary> Albanian ISO 639-2B Code. </summary>
            ALB = 149,

            /// <summary> Serbian ISO 639-2B Code. </summary>
            SRP = 150,

            /// <summary> Swati ISO 639-2B Code. </summary>
            SSW = 151,

            /// <summary> Southern Sotho ISO 639-2B Code. </summary>
            SOT = 152,

            /// <summary> Sundanese ISO 639-2B Code. </summary>
            SUN = 153,

            /// <summary> Swedish ISO 639-2B Code. </summary>
            SWE = 154,

            /// <summary> Swahili ISO 639-2B Code. </summary>
            SWA = 155,

            /// <summary> Tamil ISO 639-2B Code. </summary>
            TAM = 156,

            /// <summary> Telugu ISO 639-2B Code. </summary>
            TEL = 157,

            /// <summary> Tajik ISO 639-2B Code. </summary>
            TGK = 158,

            /// <summary> Thai ISO 639-2B Code. </summary>
            THA = 159,

            /// <summary> Tigrinya ISO 639-2B Code. </summary>
            TIR = 160,

            /// <summary> Turkmen ISO 639-2B Code. </summary>
            TUK = 161,

            /// <summary> Tagalog ISO 639-2B Code. </summary>
            TGL = 162,

            /// <summary> Tswana ISO 639-2B Code. </summary>
            TSN = 163,

            /// <summary> Tonga(Tonga Islands) ISO 639-2B Code. </summary>
            TON = 164,

            /// <summary> Turkish ISO 639-2B Code. </summary>
            TUR = 165,

            /// <summary> Tsonga ISO 639-2B Code. </summary>
            TSO = 166,

            /// <summary> Tatar ISO 639-2B Code. </summary>
            TAT = 167,

            /// <summary> Twi ISO 639-2B Code. </summary>
            TWI = 168,

            /// <summary> Tahitian ISO 639-2B Code. </summary>
            TAH = 169,

            /// <summary> Uighur, Uyghur ISO 639-2B Code. </summary>
            UIG = 170,

            /// <summary> Ukrainian ISO 639-2B Code. </summary>
            UKR = 171,

            /// <summary> Urdu ISO 639-2B Code. </summary>
            URD = 172,

            /// <summary> Uzbek ISO 639-2B Code. </summary>
            UZB = 173,

            /// <summary> Venda ISO 639-2B Code. </summary>
            VEN = 174,

            /// <summary> Vietnamese ISO 639-2B Code. </summary>
            VIE = 175,

            /// <summary> Volapük ISO 639-2B Code. </summary>
            VOL = 176,

            /// <summary> Walloon ISO 639-2B Code. </summary>
            WLN = 177,

            /// <summary> Wolof ISO 639-2B Code. </summary>
            WOL = 178,

            /// <summary> Xhosa ISO 639-2B Code. </summary>
            XHO = 179,

            /// <summary> Yiddish ISO 639-2B Code. </summary>
            YID = 180,

            /// <summary> Yoruba ISO 639-2B Code. </summary>
            YOR = 181,

            /// <summary> Zhuang, Chuang ISO 639-2B Code. </summary>
            ZHA = 182,

            /// <summary> Chinese ISO 639-2B Code. </summary>
            CHI = 183,

            /// <summary> Zulu ISO 639-2B Code. </summary>
            ZUL = 184,
        }
    }
}