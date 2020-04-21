using System.Collections.Generic;


// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
// ReSharper disable UnusedType.Global
namespace BricksBucket.Localization
{
    /// <summary>
    ///
    /// <!-- ISO3166 -->
    ///
    /// <para>
    /// <i>"The purpose of ISO 3166 is to define internationally recognized
    /// codes of letters and/or numbers that we can use when we refer to
    /// countries and their subdivisions."</i> - <see href=
    /// "https://www.iso.org/iso-3166-country-codes.html">
    /// International Organization for Standardization</see>.
    /// </para>
    ///
    /// <para>
    /// This class integrates the standard ISO 3166 for countries into the
    /// <see href="../articles/localization">Bricks Bucket Localization System
    /// </see>. The relation ship between the different part of the standard,
    /// considered in the project, is shown in the <see href=
    /// "../articles/localization/standard_iso3166.html">ISO 3166 Table</see>
    /// </para>
    /// 
    /// </summary>
    ///
    /// <seealso cref="BricksBucket.Localization.ISO3166.Alpha2"/>
    /// <seealso cref="BricksBucket.Localization.ISO3166.Alpha3"/>
    /// <seealso cref="BricksBucket.Localization.ISO639"/>
    /// <seealso cref="BricksBucket.Localization.LCID"/>
    /// <seealso cref="BricksBucket.Localization.Culture"/>
    ///
    /// <!-- Note: The code of the members of the enum have been generated with
    /// the following table: https://bit.ly/bb-localization-iso3166 -->
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    public static class ISO3166
    {
        
        
        
        /// <summary>
        /// Collection of displays names of the ISO 3166 standard.
        /// </summary>
        /// <value>Display name for ISO 3166 standard.</value>
        public static readonly Dictionary<int, string> Names =
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

        
        
        /// <summary>
        ///
        /// <!-- Alpha2 -->
        /// 
        /// Defines two letters codes for the names of the principal
        /// subdivisions according to the <b>ISO 3166-2</b> standard derived
        /// from the <see href=
        /// "https://www.iso.org/iso-3166-country-codes.html">ISO 3166</see>
        /// standard. Its relation ship with the other parts of the standard
        /// is shown in the <see href=
        /// "../articles/localization/standard_iso3166.html">ISO 3166 Table
        /// </see>
        /// 
        /// </summary>
        ///
        /// <seealso cref="BricksBucket.Localization.ISO3166"/>
        /// <seealso cref="BricksBucket.Localization.ISO3166.Alpha3"/>
        public enum Alpha2
        {
            /// <summary> No country. </summary>
            NONE = 0,

            /// <summary> Afghanistan ISO 3166-2 Code. </summary>
            AF = 4,

            /// <summary> Åland Islands ISO 3166-2 Code. </summary>
            AX = 248,

            /// <summary> Albania ISO 3166-2 Code. </summary>
            AL = 8,

            /// <summary> Algeria ISO 3166-2 Code. </summary>
            DZ = 12,

            /// <summary> American Samoa ISO 3166-2 Code. </summary>
            AS = 16,

            /// <summary> Andorra ISO 3166-2 Code. </summary>
            AD = 20,

            /// <summary> Angola ISO 3166-2 Code. </summary>
            AO = 24,

            /// <summary> Anguilla ISO 3166-2 Code. </summary>
            AI = 660,

            /// <summary> Antarctica ISO 3166-2 Code. </summary>
            AQ = 10,

            /// <summary> Antigua and Barbuda ISO 3166-2 Code. </summary>
            AG = 28,

            /// <summary> Argentina ISO 3166-2 Code. </summary>
            AR = 32,

            /// <summary> Armenia ISO 3166-2 Code. </summary>
            AM = 51,

            /// <summary> Aruba ISO 3166-2 Code. </summary>
            AW = 533,

            /// <summary> Australia ISO 3166-2 Code. </summary>
            AU = 36,

            /// <summary> Austria ISO 3166-2 Code. </summary>
            AT = 40,

            /// <summary> Azerbaijan ISO 3166-2 Code. </summary>
            AZ = 31,

            /// <summary> Bahamas ISO 3166-2 Code. </summary>
            BS = 44,

            /// <summary> Bahrain ISO 3166-2 Code. </summary>
            BH = 48,

            /// <summary> Bangladesh ISO 3166-2 Code. </summary>
            BD = 50,

            /// <summary> Barbados ISO 3166-2 Code. </summary>
            BB = 52,

            /// <summary> Belarus ISO 3166-2 Code. </summary>
            BY = 112,

            /// <summary> Belgium ISO 3166-2 Code. </summary>
            BE = 56,

            /// <summary> Belize ISO 3166-2 Code. </summary>
            BZ = 84,

            /// <summary> Benin ISO 3166-2 Code. </summary>
            BJ = 204,

            /// <summary> Bermuda ISO 3166-2 Code. </summary>
            BM = 60,

            /// <summary> Bhutan ISO 3166-2 Code. </summary>
            BT = 64,

            /// <summary> Bolivia (Plurinational State of) ISO 3166-2
            /// Code. </summary>
            BO = 68,

            /// <summary> Bonaire, Sint Eustatius and Saba ISO 3166-2
            /// Code. </summary>
            BQ = 535,

            /// <summary> Bosnia and Herzegovina ISO 3166-2 Code. </summary>
            BA = 70,

            /// <summary> Botswana ISO 3166-2 Code. </summary>
            BW = 72,

            /// <summary> Bouvet Island ISO 3166-2 Code. </summary>
            BV = 74,

            /// <summary> Brazil ISO 3166-2 Code. </summary>
            BR = 76,

            /// <summary> British Indian Ocean Territory ISO 3166-2 Code.
            /// </summary>
            IO = 86,

            /// <summary> Brunei Darussalam ISO 3166-2 Code. </summary>
            BN = 96,

            /// <summary> Bulgaria ISO 3166-2 Code. </summary>
            BG = 100,

            /// <summary> Burkina Faso ISO 3166-2 Code. </summary>
            BF = 854,

            /// <summary> Burundi ISO 3166-2 Code. </summary>
            BI = 108,

            /// <summary> Cabo Verde ISO 3166-2 Code. </summary>
            CV = 132,

            /// <summary> Cambodia ISO 3166-2 Code. </summary>
            KH = 116,

            /// <summary> Cameroon ISO 3166-2 Code. </summary>
            CM = 120,

            /// <summary> Canada ISO 3166-2 Code. </summary>
            CA = 124,

            /// <summary> Cayman Islands ISO 3166-2 Code. </summary>
            KY = 136,

            /// <summary> Central African Republic ISO 3166-2 Code. </summary>
            CF = 140,

            /// <summary> Chad ISO 3166-2 Code. </summary>
            TD = 148,

            /// <summary> Chile ISO 3166-2 Code. </summary>
            CL = 152,

            /// <summary> China ISO 3166-2 Code. </summary>
            CN = 156,

            /// <summary> Christmas Island ISO 3166-2 Code. </summary>
            CX = 162,

            /// <summary> Cocos (Keeling) Islands ISO 3166-2 Code. </summary>
            CC = 166,

            /// <summary> Colombia ISO 3166-2 Code. </summary>
            CO = 170,

            /// <summary> Comoros ISO 3166-2 Code. </summary>
            KM = 174,

            /// <summary> Congo ISO 3166-2 Code. </summary>
            CG = 178,

            /// <summary> Congo, Democratic Republic of the ISO 3166-2
            /// Code. </summary>
            CD = 180,

            /// <summary> Cook Islands ISO 3166-2 Code. </summary>
            CK = 184,

            /// <summary> Costa Rica ISO 3166-2 Code. </summary>
            CR = 188,

            /// <summary> Côte d'Ivoire ISO 3166-2 Code. </summary>
            CI = 384,

            /// <summary> Croatia ISO 3166-2 Code. </summary>
            HR = 191,

            /// <summary> Cuba ISO 3166-2 Code. </summary>
            CU = 192,

            /// <summary> Curaçao ISO 3166-2 Code. </summary>
            CW = 531,

            /// <summary> Cyprus ISO 3166-2 Code. </summary>
            CY = 196,

            /// <summary> Czechia ISO 3166-2 Code. </summary>
            CZ = 203,

            /// <summary> Denmark ISO 3166-2 Code. </summary>
            DK = 208,

            /// <summary> Djibouti ISO 3166-2 Code. </summary>
            DJ = 262,

            /// <summary> Dominica ISO 3166-2 Code. </summary>
            DM = 212,

            /// <summary> Dominican Republic ISO 3166-2 Code. </summary>
            DO = 214,

            /// <summary> Ecuador ISO 3166-2 Code. </summary>
            EC = 218,

            /// <summary> Egypt ISO 3166-2 Code. </summary>
            EG = 818,

            /// <summary> El Salvador ISO 3166-2 Code. </summary>
            SV = 222,

            /// <summary> Equatorial Guinea ISO 3166-2 Code. </summary>
            GQ = 226,

            /// <summary> Eritrea ISO 3166-2 Code. </summary>
            ER = 232,

            /// <summary> Estonia ISO 3166-2 Code. </summary>
            EE = 233,

            /// <summary> Eswatini ISO 3166-2 Code. </summary>
            SZ = 748,

            /// <summary> Ethiopia ISO 3166-2 Code. </summary>
            ET = 231,

            /// <summary> Falkland Islands (Malvinas) ISO 3166-2
            /// Code. </summary>
            FK = 238,

            /// <summary> Faroe Islands ISO 3166-2 Code. </summary>
            FO = 234,

            /// <summary> Fiji ISO 3166-2 Code. </summary>
            FJ = 242,

            /// <summary> Finland ISO 3166-2 Code. </summary>
            FI = 246,

            /// <summary> France ISO 3166-2 Code. </summary>
            FR = 250,

            /// <summary> French Guiana ISO 3166-2 Code. </summary>
            GF = 254,

            /// <summary> French Polynesia ISO 3166-2 Code. </summary>
            PF = 258,

            /// <summary> French Southern Territories ISO 3166-2 Code.
            /// </summary>
            TF = 260,

            /// <summary> Gabon ISO 3166-2 Code. </summary>
            GA = 266,

            /// <summary> Gambia ISO 3166-2 Code. </summary>
            GM = 270,

            /// <summary> Georgia ISO 3166-2 Code. </summary>
            GE = 268,

            /// <summary> Germany ISO 3166-2 Code. </summary>
            DE = 276,

            /// <summary> Ghana ISO 3166-2 Code. </summary>
            GH = 288,

            /// <summary> Gibraltar ISO 3166-2 Code. </summary>
            GI = 292,

            /// <summary> Greece ISO 3166-2 Code. </summary>
            GR = 300,

            /// <summary> Greenland ISO 3166-2 Code. </summary>
            GL = 304,

            /// <summary> Grenada ISO 3166-2 Code. </summary>
            GD = 308,

            /// <summary> Guadeloupe ISO 3166-2 Code. </summary>
            GP = 312,

            /// <summary> Guam ISO 3166-2 Code. </summary>
            GU = 316,

            /// <summary> Guatemala ISO 3166-2 Code. </summary>
            GT = 320,

            /// <summary> Guernsey ISO 3166-2 Code. </summary>
            GG = 831,

            /// <summary> Guinea ISO 3166-2 Code. </summary>
            GN = 324,

            /// <summary> Guinea-Bissau ISO 3166-2 Code. </summary>
            GW = 624,

            /// <summary> Guyana ISO 3166-2 Code. </summary>
            GY = 328,

            /// <summary> Haiti ISO 3166-2 Code. </summary>
            HT = 332,

            /// <summary> Heard Island and McDonald Islands ISO 3166-2
            /// Code. </summary>
            HM = 334,

            /// <summary> Holy See ISO 3166-2 Code. </summary>
            VA = 336,

            /// <summary> Honduras ISO 3166-2 Code. </summary>
            HN = 340,

            /// <summary> Hong Kong ISO 3166-2 Code. </summary>
            HK = 344,

            /// <summary> Hungary ISO 3166-2 Code. </summary>
            HU = 348,

            /// <summary> Iceland ISO 3166-2 Code. </summary>
            IS = 352,

            /// <summary> India ISO 3166-2 Code. </summary>
            IN = 356,

            /// <summary> Indonesia ISO 3166-2 Code. </summary>
            ID = 360,

            /// <summary> Iran (Islamic Republic of) ISO 3166-2
            /// Code. </summary>
            IR = 364,

            /// <summary> Iraq ISO 3166-2 Code. </summary>
            IQ = 368,

            /// <summary> Ireland ISO 3166-2 Code. </summary>
            IE = 372,

            /// <summary> Isle of Man ISO 3166-2 Code. </summary>
            IM = 833,

            /// <summary> Israel ISO 3166-2 Code. </summary>
            IL = 376,

            /// <summary> Italy ISO 3166-2 Code. </summary>
            IT = 380,

            /// <summary> Jamaica ISO 3166-2 Code. </summary>
            JM = 388,

            /// <summary> Japan ISO 3166-2 Code. </summary>
            JP = 392,

            /// <summary> Jersey ISO 3166-2 Code. </summary>
            JE = 832,

            /// <summary> Jordan ISO 3166-2 Code. </summary>
            JO = 400,

            /// <summary> Kazakhstan ISO 3166-2 Code. </summary>
            KZ = 398,

            /// <summary> Kenya ISO 3166-2 Code. </summary>
            KE = 404,

            /// <summary> Kiribati ISO 3166-2 Code. </summary>
            KI = 296,

            /// <summary> Korea (Democratic People's Republic of) ISO 3166-2
            /// Code. </summary>
            KP = 408,

            /// <summary> Korea, Republic of ISO 3166-2 Code. </summary>
            KR = 410,

            /// <summary> Kuwait ISO 3166-2 Code. </summary>
            KW = 414,

            /// <summary> Kyrgyzstan ISO 3166-2 Code. </summary>
            KG = 417,

            /// <summary> Lao People's Democratic Republic ISO 3166-2
            /// Code. </summary>
            LA = 418,

            /// <summary> Latvia ISO 3166-2 Code. </summary>
            LV = 428,

            /// <summary> Lebanon ISO 3166-2 Code. </summary>
            LB = 422,

            /// <summary> Lesotho ISO 3166-2 Code. </summary>
            LS = 426,

            /// <summary> Liberia ISO 3166-2 Code. </summary>
            LR = 430,

            /// <summary> Libya ISO 3166-2 Code. </summary>
            LY = 434,

            /// <summary> Liechtenstein ISO 3166-2 Code. </summary>
            LI = 438,

            /// <summary> Lithuania ISO 3166-2 Code. </summary>
            LT = 440,

            /// <summary> Luxembourg ISO 3166-2 Code. </summary>
            LU = 442,

            /// <summary> Macao ISO 3166-2 Code. </summary>
            MO = 446,

            /// <summary> Madagascar ISO 3166-2 Code. </summary>
            MG = 450,

            /// <summary> Malawi ISO 3166-2 Code. </summary>
            MW = 454,

            /// <summary> Malaysia ISO 3166-2 Code. </summary>
            MY = 458,

            /// <summary> Maldives ISO 3166-2 Code. </summary>
            MV = 462,

            /// <summary> Mali ISO 3166-2 Code. </summary>
            ML = 466,

            /// <summary> Malta ISO 3166-2 Code. </summary>
            MT = 470,

            /// <summary> Marshall Islands ISO 3166-2 Code. </summary>
            MH = 584,

            /// <summary> Martinique ISO 3166-2 Code. </summary>
            MQ = 474,

            /// <summary> Mauritania ISO 3166-2 Code. </summary>
            MR = 478,

            /// <summary> Mauritius ISO 3166-2 Code. </summary>
            MU = 480,

            /// <summary> Mayotte ISO 3166-2 Code. </summary>
            YT = 175,

            /// <summary> Mexico ISO 3166-2 Code. </summary>
            MX = 484,

            /// <summary> Micronesia (Federated States of) ISO 3166-2
            /// Code. </summary>
            FM = 583,

            /// <summary> Moldova, Republic of ISO 3166-2 Code. </summary>
            MD = 498,

            /// <summary> Monaco ISO 3166-2 Code. </summary>
            MC = 492,

            /// <summary> Mongolia ISO 3166-2 Code. </summary>
            MN = 496,

            /// <summary> Montenegro ISO 3166-2 Code. </summary>
            ME = 499,

            /// <summary> Montserrat ISO 3166-2 Code. </summary>
            MS = 500,

            /// <summary> Morocco ISO 3166-2 Code. </summary>
            MA = 504,

            /// <summary> Mozambique ISO 3166-2 Code. </summary>
            MZ = 508,

            /// <summary> Myanmar ISO 3166-2 Code. </summary>
            MM = 104,

            /// <summary> Namibia ISO 3166-2 Code. </summary>
            NA = 516,

            /// <summary> Nauru ISO 3166-2 Code. </summary>
            NR = 520,

            /// <summary> Nepal ISO 3166-2 Code. </summary>
            NP = 524,

            /// <summary> Netherlands ISO 3166-2 Code. </summary>
            NL = 528,

            /// <summary> New Caledonia ISO 3166-2 Code. </summary>
            NC = 540,

            /// <summary> New Zealand ISO 3166-2 Code. </summary>
            NZ = 554,

            /// <summary> Nicaragua ISO 3166-2 Code. </summary>
            NI = 558,

            /// <summary> Niger ISO 3166-2 Code. </summary>
            NE = 562,

            /// <summary> Nigeria ISO 3166-2 Code. </summary>
            NG = 566,

            /// <summary> Niue ISO 3166-2 Code. </summary>
            NU = 570,

            /// <summary> Norfolk Island ISO 3166-2 Code. </summary>
            NF = 574,

            /// <summary> North Macedonia ISO 3166-2 Code. </summary>
            MK = 807,

            /// <summary> Northern Mariana Islands ISO 3166-2 Code. </summary>
            MP = 580,

            /// <summary> Norway ISO 3166-2 Code. </summary>
            NO = 578,

            /// <summary> Oman ISO 3166-2 Code. </summary>
            OM = 512,

            /// <summary> Pakistan ISO 3166-2 Code. </summary>
            PK = 586,

            /// <summary> Palau ISO 3166-2 Code. </summary>
            PW = 585,

            /// <summary> Palestine, State of ISO 3166-2 Code. </summary>
            PS = 275,

            /// <summary> Panama ISO 3166-2 Code. </summary>
            PA = 591,

            /// <summary> Papua New Guinea ISO 3166-2 Code. </summary>
            PG = 598,

            /// <summary> Paraguay ISO 3166-2 Code. </summary>
            PY = 600,

            /// <summary> Peru ISO 3166-2 Code. </summary>
            PE = 604,

            /// <summary> Philippines ISO 3166-2 Code. </summary>
            PH = 608,

            /// <summary> Pitcairn ISO 3166-2 Code. </summary>
            PN = 612,

            /// <summary> Poland ISO 3166-2 Code. </summary>
            PL = 616,

            /// <summary> Portugal ISO 3166-2 Code. </summary>
            PT = 620,

            /// <summary> Puerto Rico ISO 3166-2 Code. </summary>
            PR = 630,

            /// <summary> Qatar ISO 3166-2 Code. </summary>
            QA = 634,

            /// <summary> Réunion ISO 3166-2 Code. </summary>
            RE = 638,

            /// <summary> Romania ISO 3166-2 Code. </summary>
            RO = 642,

            /// <summary> Russian Federation ISO 3166-2 Code. </summary>
            RU = 643,

            /// <summary> Rwanda ISO 3166-2 Code. </summary>
            RW = 646,

            /// <summary> Saint Barthélemy ISO 3166-2 Code. </summary>
            BL = 652,

            /// <summary> Saint Helena, Ascension and Tristan da Cunha
            /// ISO 3166-2 Code. </summary>
            SH = 654,

            /// <summary> Saint Kitts and Nevis ISO 3166-2 Code. </summary>
            KN = 659,

            /// <summary> Saint Lucia ISO 3166-2 Code. </summary>
            LC = 662,

            /// <summary> Saint Martin (French part) ISO 3166-2 Code. </summary>
            MF = 663,

            /// <summary> Saint Pierre and Miquelon ISO 3166-2 Code. </summary>
            PM = 666,

            /// <summary> Saint Vincent and the Grenadines ISO 3166-2
            /// Code. </summary>
            VC = 670,

            /// <summary> Samoa ISO 3166-2 Code. </summary>
            WS = 882,

            /// <summary> San Marino ISO 3166-2 Code. </summary>
            SM = 674,

            /// <summary> Sao Tome and Principe ISO 3166-2 Code. </summary>
            ST = 678,

            /// <summary> Saudi Arabia ISO 3166-2 Code. </summary>
            SA = 682,

            /// <summary> Senegal ISO 3166-2 Code. </summary>
            SN = 686,

            /// <summary> Serbia ISO 3166-2 Code. </summary>
            RS = 688,

            /// <summary> Seychelles ISO 3166-2 Code. </summary>
            SC = 690,

            /// <summary> Sierra Leone ISO 3166-2 Code. </summary>
            SL = 694,

            /// <summary> Singapore ISO 3166-2 Code. </summary>
            SG = 702,

            /// <summary> Sint Maarten (Dutch part) ISO 3166-2 Code. </summary>
            SX = 534,

            /// <summary> Slovakia ISO 3166-2 Code. </summary>
            SK = 703,

            /// <summary> Slovenia ISO 3166-2 Code. </summary>
            SI = 705,

            /// <summary> Solomon Islands ISO 3166-2 Code. </summary>
            SB = 90,

            /// <summary> Somalia ISO 3166-2 Code. </summary>
            SO = 706,

            /// <summary> South Africa ISO 3166-2 Code. </summary>
            ZA = 710,

            /// <summary> South Georgia and the South Sandwich Islands ISO
            /// 3166-2 Code. </summary>
            GS = 239,

            /// <summary> South Sudan ISO 3166-2 Code. </summary>
            SS = 728,

            /// <summary> Spain ISO 3166-2 Code. </summary>
            ES = 724,

            /// <summary> Sri Lanka ISO 3166-2 Code. </summary>
            LK = 144,

            /// <summary> Sudan ISO 3166-2 Code. </summary>
            SD = 729,

            /// <summary> Suriname ISO 3166-2 Code. </summary>
            SR = 740,

            /// <summary> Svalbard and Jan Mayen ISO 3166-2 Code. </summary>
            SJ = 744,

            /// <summary> Sweden ISO 3166-2 Code. </summary>
            SE = 752,

            /// <summary> Switzerland ISO 3166-2 Code. </summary>
            CH = 756,

            /// <summary> Syrian Arab Republic ISO 3166-2 Code. </summary>
            SY = 760,

            /// <summary> Taiwan, Province of China[a] ISO 3166-2 Code.
            /// </summary>
            TW = 158,

            /// <summary> Tajikistan ISO 3166-2 Code. </summary>
            TJ = 762,

            /// <summary> Tanzania, United Republic of ISO 3166-2 Code.
            /// </summary>
            TZ = 834,

            /// <summary> Thailand ISO 3166-2 Code. </summary>
            TH = 764,

            /// <summary> Timor-Leste ISO 3166-2 Code. </summary>
            TL = 626,

            /// <summary> Togo ISO 3166-2 Code. </summary>
            TG = 768,

            /// <summary> Tokelau ISO 3166-2 Code. </summary>
            TK = 772,

            /// <summary> Tonga ISO 3166-2 Code. </summary>
            TO = 776,

            /// <summary> Trinidad and Tobago ISO 3166-2 Code. </summary>
            TT = 780,

            /// <summary> Tunisia ISO 3166-2 Code. </summary>
            TN = 788,

            /// <summary> Turkey ISO 3166-2 Code. </summary>
            TR = 792,

            /// <summary> Turkmenistan ISO 3166-2 Code. </summary>
            TM = 795,

            /// <summary> Turks and Caicos Islands ISO 3166-2 Code. </summary>
            TC = 796,

            /// <summary> Tuvalu ISO 3166-2 Code. </summary>
            TV = 798,

            /// <summary> Uganda ISO 3166-2 Code. </summary>
            UG = 800,

            /// <summary> Ukraine ISO 3166-2 Code. </summary>
            UA = 804,

            /// <summary> United Arab Emirates ISO 3166-2 Code. </summary>
            AE = 784,

            /// <summary> United Kingdom of Great Britain and Northern Ireland
            /// ISO 3166-2 Code. </summary>
            GB = 826,

            /// <summary> United States of America ISO 3166-2 Code. </summary>
            US = 840,

            /// <summary> United States Minor Outlying Islands ISO 3166-2
            /// Code. </summary>
            UM = 581,

            /// <summary> Uruguay ISO 3166-2 Code. </summary>
            UY = 858,

            /// <summary> Uzbekistan ISO 3166-2 Code. </summary>
            UZ = 860,

            /// <summary> Vanuatu ISO 3166-2 Code. </summary>
            VU = 548,

            /// <summary> Venezuela (Bolivarian Republic of) ISO 3166-2
            /// Code. </summary>
            VE = 862,

            /// <summary> Viet Nam ISO 3166-2 Code. </summary>
            VN = 704,

            /// <summary> Virgin Islands (British) ISO 3166-2 Code. </summary>
            VG = 92,

            /// <summary> Virgin Islands (U.S.) ISO 3166-2 Code. </summary>
            VI = 850,

            /// <summary> Wallis and Futuna ISO 3166-2 Code. </summary>
            WF = 876,

            /// <summary> Western Sahara ISO 3166-2 Code. </summary>
            EH = 732,

            /// <summary> Yemen ISO 3166-2 Code. </summary>
            YE = 887,

            /// <summary> Zambia ISO 3166-2 Code. </summary>
            ZM = 894,

            /// <summary> Zimbabwe ISO 3166-2 Code. </summary>
            ZW = 716,
        }


        
        /// <summary>
        /// 
        /// <!-- Alpha3 -->
        /// 
        /// Defines three letters codes for the names of the principal
        /// subdivisions according to the <b>ISO 3166-3</b> standard derived
        /// from the <see href=
        /// "https://www.iso.org/iso-3166-country-codes.html">ISO 3166</see>
        /// standard. Its relation ship with the other parts of the standard
        /// is shown in the <see href=
        /// "../articles/localization/standard_iso3166.html">ISO 3166 Table
        /// </see>
        /// 
        /// </summary>
        ///
        /// <seealso cref="BricksBucket.Localization.ISO3166"/>
        /// <seealso cref="BricksBucket.Localization.ISO3166.Alpha2"/>
        public enum Alpha3
        {
            /// <summary> No country. </summary>
            NONE = 0,

            /// <summary> Afghanistan ISO 3166-2 Code. </summary>
            AFG = 4,

            /// <summary> Åland Islands ISO 3166-2 Code. </summary>
            ALA = 248,

            /// <summary> Albania ISO 3166-2 Code. </summary>
            ALB = 8,

            /// <summary> Algeria ISO 3166-2 Code. </summary>
            DZA = 12,

            /// <summary> American Samoa ISO 3166-2 Code. </summary>
            ASM = 16,

            /// <summary> Andorra ISO 3166-2 Code. </summary>
            AND = 20,

            /// <summary> Angola ISO 3166-2 Code. </summary>
            AGO = 24,

            /// <summary> Anguilla ISO 3166-2 Code. </summary>
            AIA = 660,

            /// <summary> Antarctica ISO 3166-2 Code. </summary>
            ATA = 10,

            /// <summary> Antigua and Barbuda ISO 3166-2 Code. </summary>
            ATG = 28,

            /// <summary> Argentina ISO 3166-2 Code. </summary>
            ARG = 32,

            /// <summary> Armenia ISO 3166-2 Code. </summary>
            ARM = 51,

            /// <summary> Aruba ISO 3166-2 Code. </summary>
            ABW = 533,

            /// <summary> Australia ISO 3166-2 Code. </summary>
            AUS = 36,

            /// <summary> Austria ISO 3166-2 Code. </summary>
            AUT = 40,

            /// <summary> Azerbaijan ISO 3166-2 Code. </summary>
            AZE = 31,

            /// <summary> Bahamas ISO 3166-2 Code. </summary>
            BHS = 44,

            /// <summary> Bahrain ISO 3166-2 Code. </summary>
            BHR = 48,

            /// <summary> Bangladesh ISO 3166-2 Code. </summary>
            BGD = 50,

            /// <summary> Barbados ISO 3166-2 Code. </summary>
            BRB = 52,

            /// <summary> Belarus ISO 3166-2 Code. </summary>
            BLR = 112,

            /// <summary> Belgium ISO 3166-2 Code. </summary>
            BEL = 56,

            /// <summary> Belize ISO 3166-2 Code. </summary>
            BLZ = 84,

            /// <summary> Benin ISO 3166-2 Code. </summary>
            BEN = 204,

            /// <summary> Bermuda ISO 3166-2 Code. </summary>
            BMU = 60,

            /// <summary> Bhutan ISO 3166-2 Code. </summary>
            BTN = 64,

            /// <summary> Bolivia (Plurinational State of) ISO 3166-2
            /// Code. </summary>
            BOL = 68,

            /// <summary> Bonaire, Sint Eustatius and Saba ISO 3166-2
            /// Code. </summary>
            BES = 535,

            /// <summary> Bosnia and Herzegovina ISO 3166-2 Code. </summary>
            BIH = 70,

            /// <summary> Botswana ISO 3166-2 Code. </summary>
            BWA = 72,

            /// <summary> Bouvet Island ISO 3166-2 Code. </summary>
            BVT = 74,

            /// <summary> Brazil ISO 3166-2 Code. </summary>
            BRA = 76,

            /// <summary> British Indian Ocean Territory ISO 3166-2
            /// Code. </summary>
            IOT = 86,

            /// <summary> Brunei Darussalam ISO 3166-2 Code. </summary>
            BRN = 96,

            /// <summary> Bulgaria ISO 3166-2 Code. </summary>
            BGR = 100,

            /// <summary> Burkina Faso ISO 3166-2 Code. </summary>
            BFA = 854,

            /// <summary> Burundi ISO 3166-2 Code. </summary>
            BDI = 108,

            /// <summary> Cabo Verde ISO 3166-2 Code. </summary>
            CPV = 132,

            /// <summary> Cambodia ISO 3166-2 Code. </summary>
            KHM = 116,

            /// <summary> Cameroon ISO 3166-2 Code. </summary>
            CMR = 120,

            /// <summary> Canada ISO 3166-2 Code. </summary>
            CAN = 124,

            /// <summary> Cayman Islands ISO 3166-2 Code. </summary>
            CYM = 136,

            /// <summary> Central African Republic ISO 3166-2 Code. </summary>
            CAF = 140,

            /// <summary> Chad ISO 3166-2 Code. </summary>
            TCD = 148,

            /// <summary> Chile ISO 3166-2 Code. </summary>
            CHL = 152,

            /// <summary> China ISO 3166-2 Code. </summary>
            CHN = 156,

            /// <summary> Christmas Island ISO 3166-2 Code. </summary>
            CXR = 162,

            /// <summary> Cocos (Keeling) Islands ISO 3166-2 Code. </summary>
            CCK = 166,

            /// <summary> Colombia ISO 3166-2 Code. </summary>
            COL = 170,

            /// <summary> Comoros ISO 3166-2 Code. </summary>
            COM = 174,

            /// <summary> Congo ISO 3166-2 Code. </summary>
            COG = 178,

            /// <summary> Congo, Democratic Republic of the ISO 3166-2
            /// Code. </summary>
            COD = 180,

            /// <summary> Cook Islands ISO 3166-2 Code. </summary>
            COK = 184,

            /// <summary> Costa Rica ISO 3166-2 Code. </summary>
            CRI = 188,

            /// <summary> Côte d'Ivoire ISO 3166-2 Code. </summary>
            CIV = 384,

            /// <summary> Croatia ISO 3166-2 Code. </summary>
            HRV = 191,

            /// <summary> Cuba ISO 3166-2 Code. </summary>
            CUB = 192,

            /// <summary> Curaçao ISO 3166-2 Code. </summary>
            CUW = 531,

            /// <summary> Cyprus ISO 3166-2 Code. </summary>
            CYP = 196,

            /// <summary> Czechia ISO 3166-2 Code. </summary>
            CZE = 203,

            /// <summary> Denmark ISO 3166-2 Code. </summary>
            DNK = 208,

            /// <summary> Djibouti ISO 3166-2 Code. </summary>
            DJI = 262,

            /// <summary> Dominica ISO 3166-2 Code. </summary>
            DMA = 212,

            /// <summary> Dominican Republic ISO 3166-2 Code. </summary>
            DOM = 214,

            /// <summary> Ecuador ISO 3166-2 Code. </summary>
            ECU = 218,

            /// <summary> Egypt ISO 3166-2 Code. </summary>
            EGY = 818,

            /// <summary> El Salvador ISO 3166-2 Code. </summary>
            SLV = 222,

            /// <summary> Equatorial Guinea ISO 3166-2 Code. </summary>
            GNQ = 226,

            /// <summary> Eritrea ISO 3166-2 Code. </summary>
            ERI = 232,

            /// <summary> Estonia ISO 3166-2 Code. </summary>
            EST = 233,

            /// <summary> Eswatini ISO 3166-2 Code. </summary>
            SWZ = 748,

            /// <summary> Ethiopia ISO 3166-2 Code. </summary>
            ETH = 231,

            /// <summary> Falkland Islands (Malvinas) ISO 3166-2 Code.
            /// </summary>
            FLK = 238,

            /// <summary> Faroe Islands ISO 3166-2 Code. </summary>
            FRO = 234,

            /// <summary> Fiji ISO 3166-2 Code. </summary>
            FJI = 242,

            /// <summary> Finland ISO 3166-2 Code. </summary>
            FIN = 246,

            /// <summary> France ISO 3166-2 Code. </summary>
            FRA = 250,

            /// <summary> French Guiana ISO 3166-2 Code. </summary>
            GUF = 254,

            /// <summary> French Polynesia ISO 3166-2 Code. </summary>
            PYF = 258,

            /// <summary> French Southern Territories ISO 3166-2 Code.
            /// </summary>
            ATF = 260,

            /// <summary> Gabon ISO 3166-2 Code. </summary>
            GAB = 266,

            /// <summary> Gambia ISO 3166-2 Code. </summary>
            GMB = 270,

            /// <summary> Georgia ISO 3166-2 Code. </summary>
            GEO = 268,

            /// <summary> Germany ISO 3166-2 Code. </summary>
            DEU = 276,

            /// <summary> Ghana ISO 3166-2 Code. </summary>
            GHA = 288,

            /// <summary> Gibraltar ISO 3166-2 Code. </summary>
            GIB = 292,

            /// <summary> Greece ISO 3166-2 Code. </summary>
            GRC = 300,

            /// <summary> Greenland ISO 3166-2 Code. </summary>
            GRL = 304,

            /// <summary> Grenada ISO 3166-2 Code. </summary>
            GRD = 308,

            /// <summary> Guadeloupe ISO 3166-2 Code. </summary>
            GLP = 312,

            /// <summary> Guam ISO 3166-2 Code. </summary>
            GUM = 316,

            /// <summary> Guatemala ISO 3166-2 Code. </summary>
            GTM = 320,

            /// <summary> Guernsey ISO 3166-2 Code. </summary>
            GGY = 831,

            /// <summary> Guinea ISO 3166-2 Code. </summary>
            GIN = 324,

            /// <summary> Guinea-Bissau ISO 3166-2 Code. </summary>
            GNB = 624,

            /// <summary> Guyana ISO 3166-2 Code. </summary>
            GUY = 328,

            /// <summary> Haiti ISO 3166-2 Code. </summary>
            HTI = 332,

            /// <summary> Heard Island and McDonald Islands ISO 3166-2
            /// Code. </summary>
            HMD = 334,

            /// <summary> Holy See ISO 3166-2 Code. </summary>
            VAT = 336,

            /// <summary> Honduras ISO 3166-2 Code. </summary>
            HND = 340,

            /// <summary> Hong Kong ISO 3166-2 Code. </summary>
            HKG = 344,

            /// <summary> Hungary ISO 3166-2 Code. </summary>
            HUN = 348,

            /// <summary> Iceland ISO 3166-2 Code. </summary>
            ISL = 352,

            /// <summary> India ISO 3166-2 Code. </summary>
            IND = 356,

            /// <summary> Indonesia ISO 3166-2 Code. </summary>
            IDN = 360,

            /// <summary> Iran (Islamic Republic of) ISO 3166-2 Code. </summary>
            IRN = 364,

            /// <summary> Iraq ISO 3166-2 Code. </summary>
            IRQ = 368,

            /// <summary> Ireland ISO 3166-2 Code. </summary>
            IRL = 372,

            /// <summary> Isle of Man ISO 3166-2 Code. </summary>
            IMN = 833,

            /// <summary> Israel ISO 3166-2 Code. </summary>
            ISR = 376,

            /// <summary> Italy ISO 3166-2 Code. </summary>
            ITA = 380,

            /// <summary> Jamaica ISO 3166-2 Code. </summary>
            JAM = 388,

            /// <summary> Japan ISO 3166-2 Code. </summary>
            JPN = 392,

            /// <summary> Jersey ISO 3166-2 Code. </summary>
            JEY = 832,

            /// <summary> Jordan ISO 3166-2 Code. </summary>
            JOR = 400,

            /// <summary> Kazakhstan ISO 3166-2 Code. </summary>
            KAZ = 398,

            /// <summary> Kenya ISO 3166-2 Code. </summary>
            KEN = 404,

            /// <summary> Kiribati ISO 3166-2 Code. </summary>
            KIR = 296,

            /// <summary> Korea (Democratic People's Republic of) ISO 3166-2
            /// Code. </summary>
            PRK = 408,

            /// <summary> Korea, Republic of ISO 3166-2 Code. </summary>
            KOR = 410,

            /// <summary> Kuwait ISO 3166-2 Code. </summary>
            KWT = 414,

            /// <summary> Kyrgyzstan ISO 3166-2 Code. </summary>
            KGZ = 417,

            /// <summary> Lao People's Democratic Republic ISO 3166-2
            /// Code. </summary>
            LAO = 418,

            /// <summary> Latvia ISO 3166-2 Code. </summary>
            LVA = 428,

            /// <summary> Lebanon ISO 3166-2 Code. </summary>
            LBN = 422,

            /// <summary> Lesotho ISO 3166-2 Code. </summary>
            LSO = 426,

            /// <summary> Liberia ISO 3166-2 Code. </summary>
            LBR = 430,

            /// <summary> Libya ISO 3166-2 Code. </summary>
            LBY = 434,

            /// <summary> Liechtenstein ISO 3166-2 Code. </summary>
            LIE = 438,

            /// <summary> Lithuania ISO 3166-2 Code. </summary>
            LTU = 440,

            /// <summary> Luxembourg ISO 3166-2 Code. </summary>
            LUX = 442,

            /// <summary> Macao ISO 3166-2 Code. </summary>
            MAC = 446,

            /// <summary> Madagascar ISO 3166-2 Code. </summary>
            MDG = 450,

            /// <summary> Malawi ISO 3166-2 Code. </summary>
            MWI = 454,

            /// <summary> Malaysia ISO 3166-2 Code. </summary>
            MYS = 458,

            /// <summary> Maldives ISO 3166-2 Code. </summary>
            MDV = 462,

            /// <summary> Mali ISO 3166-2 Code. </summary>
            MLI = 466,

            /// <summary> Malta ISO 3166-2 Code. </summary>
            MLT = 470,

            /// <summary> Marshall Islands ISO 3166-2 Code. </summary>
            MHL = 584,

            /// <summary> Martinique ISO 3166-2 Code. </summary>
            MTQ = 474,

            /// <summary> Mauritania ISO 3166-2 Code. </summary>
            MRT = 478,

            /// <summary> Mauritius ISO 3166-2 Code. </summary>
            MUS = 480,

            /// <summary> Mayotte ISO 3166-2 Code. </summary>
            MYT = 175,

            /// <summary> Mexico ISO 3166-2 Code. </summary>
            MEX = 484,

            /// <summary> Micronesia (Federated States of) ISO 3166-2
            /// Code. </summary>
            FSM = 583,

            /// <summary> Moldova, Republic of ISO 3166-2 Code. </summary>
            MDA = 498,

            /// <summary> Monaco ISO 3166-2 Code. </summary>
            MCO = 492,

            /// <summary> Mongolia ISO 3166-2 Code. </summary>
            MNG = 496,

            /// <summary> Montenegro ISO 3166-2 Code. </summary>
            MNE = 499,

            /// <summary> Montserrat ISO 3166-2 Code. </summary>
            MSR = 500,

            /// <summary> Morocco ISO 3166-2 Code. </summary>
            MAR = 504,

            /// <summary> Mozambique ISO 3166-2 Code. </summary>
            MOZ = 508,

            /// <summary> Myanmar ISO 3166-2 Code. </summary>
            MMR = 104,

            /// <summary> Namibia ISO 3166-2 Code. </summary>
            NAM = 516,

            /// <summary> Nauru ISO 3166-2 Code. </summary>
            NRU = 520,

            /// <summary> Nepal ISO 3166-2 Code. </summary>
            NPL = 524,

            /// <summary> Netherlands ISO 3166-2 Code. </summary>
            NLD = 528,

            /// <summary> New Caledonia ISO 3166-2 Code. </summary>
            NCL = 540,

            /// <summary> New Zealand ISO 3166-2 Code. </summary>
            NZL = 554,

            /// <summary> Nicaragua ISO 3166-2 Code. </summary>
            NIC = 558,

            /// <summary> Niger ISO 3166-2 Code. </summary>
            NER = 562,

            /// <summary> Nigeria ISO 3166-2 Code. </summary>
            NGA = 566,

            /// <summary> Niue ISO 3166-2 Code. </summary>
            NIU = 570,

            /// <summary> Norfolk Island ISO 3166-2 Code. </summary>
            NFK = 574,

            /// <summary> North Macedonia ISO 3166-2 Code. </summary>
            MKD = 807,

            /// <summary> Northern Mariana Islands ISO 3166-2 Code. </summary>
            MNP = 580,

            /// <summary> Norway ISO 3166-2 Code. </summary>
            NOR = 578,

            /// <summary> Oman ISO 3166-2 Code. </summary>
            OMN = 512,

            /// <summary> Pakistan ISO 3166-2 Code. </summary>
            PAK = 586,

            /// <summary> Palau ISO 3166-2 Code. </summary>
            PLW = 585,

            /// <summary> Palestine, State of ISO 3166-2 Code. </summary>
            PSE = 275,

            /// <summary> Panama ISO 3166-2 Code. </summary>
            PAN = 591,

            /// <summary> Papua New Guinea ISO 3166-2 Code. </summary>
            PNG = 598,

            /// <summary> Paraguay ISO 3166-2 Code. </summary>
            PRY = 600,

            /// <summary> Peru ISO 3166-2 Code. </summary>
            PER = 604,

            /// <summary> Philippines ISO 3166-2 Code. </summary>
            PHL = 608,

            /// <summary> Pitcairn ISO 3166-2 Code. </summary>
            PCN = 612,

            /// <summary> Poland ISO 3166-2 Code. </summary>
            POL = 616,

            /// <summary> Portugal ISO 3166-2 Code. </summary>
            PRT = 620,

            /// <summary> Puerto Rico ISO 3166-2 Code. </summary>
            PRI = 630,

            /// <summary> Qatar ISO 3166-2 Code. </summary>
            QAT = 634,

            /// <summary> Réunion ISO 3166-2 Code. </summary>
            REU = 638,

            /// <summary> Romania ISO 3166-2 Code. </summary>
            ROU = 642,

            /// <summary> Russian Federation ISO 3166-2 Code. </summary>
            RUS = 643,

            /// <summary> Rwanda ISO 3166-2 Code. </summary>
            RWA = 646,

            /// <summary> Saint Barthélemy ISO 3166-2 Code. </summary>
            BLM = 652,

            /// <summary> Saint Helena, Ascension and Tristan da Cunha ISO
            /// 3166-2 Code. </summary>
            SHN = 654,

            /// <summary> Saint Kitts and Nevis ISO 3166-2 Code. </summary>
            KNA = 659,

            /// <summary> Saint Lucia ISO 3166-2 Code. </summary>
            LCA = 662,

            /// <summary> Saint Martin (French part) ISO 3166-2 Code. </summary>
            MAF = 663,

            /// <summary> Saint Pierre and Miquelon ISO 3166-2 Code. </summary>
            SPM = 666,

            /// <summary> Saint Vincent and the Grenadines ISO 3166-2
            /// Code. </summary>
            VCT = 670,

            /// <summary> Samoa ISO 3166-2 Code. </summary>
            WSM = 882,

            /// <summary> San Marino ISO 3166-2 Code. </summary>
            SMR = 674,

            /// <summary> Sao Tome and Principe ISO 3166-2 Code. </summary>
            STP = 678,

            /// <summary> Saudi Arabia ISO 3166-2 Code. </summary>
            SAU = 682,

            /// <summary> Senegal ISO 3166-2 Code. </summary>
            SEN = 686,

            /// <summary> Serbia ISO 3166-2 Code. </summary>
            SRB = 688,

            /// <summary> Seychelles ISO 3166-2 Code. </summary>
            SYC = 690,

            /// <summary> Sierra Leone ISO 3166-2 Code. </summary>
            SLE = 694,

            /// <summary> Singapore ISO 3166-2 Code. </summary>
            SGP = 702,

            /// <summary> Sint Maarten (Dutch part) ISO 3166-2 Code. </summary>
            SXM = 534,

            /// <summary> Slovakia ISO 3166-2 Code. </summary>
            SVK = 703,

            /// <summary> Slovenia ISO 3166-2 Code. </summary>
            SVN = 705,

            /// <summary> Solomon Islands ISO 3166-2 Code. </summary>
            SLB = 90,

            /// <summary> Somalia ISO 3166-2 Code. </summary>
            SOM = 706,

            /// <summary> South Africa ISO 3166-2 Code. </summary>
            ZAF = 710,

            /// <summary> South Georgia and the South Sandwich Islands ISO
            /// 3166-2 Code. </summary>
            SGS = 239,

            /// <summary> South Sudan ISO 3166-2 Code. </summary>
            SSD = 728,

            /// <summary> Spain ISO 3166-2 Code. </summary>
            ESP = 724,

            /// <summary> Sri Lanka ISO 3166-2 Code. </summary>
            LKA = 144,

            /// <summary> Sudan ISO 3166-2 Code. </summary>
            SDN = 729,

            /// <summary> Suriname ISO 3166-2 Code. </summary>
            SUR = 740,

            /// <summary> Svalbard and Jan Mayen ISO 3166-2 Code. </summary>
            SJM = 744,

            /// <summary> Sweden ISO 3166-2 Code. </summary>
            SWE = 752,

            /// <summary> Switzerland ISO 3166-2 Code. </summary>
            CHE = 756,

            /// <summary> Syrian Arab Republic ISO 3166-2 Code. </summary>
            SYR = 760,

            /// <summary> Taiwan, Province of China[a] ISO 3166-2 Code.
            /// </summary>
            TWN = 158,

            /// <summary> Tajikistan ISO 3166-2 Code. </summary>
            TJK = 762,

            /// <summary> Tanzania, United Republic of ISO 3166-2 Code.
            /// </summary>
            TZA = 834,

            /// <summary> Thailand ISO 3166-2 Code. </summary>
            THA = 764,

            /// <summary> Timor-Leste ISO 3166-2 Code. </summary>
            TLS = 626,

            /// <summary> Togo ISO 3166-2 Code. </summary>
            TGO = 768,

            /// <summary> Tokelau ISO 3166-2 Code. </summary>
            TKL = 772,

            /// <summary> Tonga ISO 3166-2 Code. </summary>
            TON = 776,

            /// <summary> Trinidad and Tobago ISO 3166-2 Code. </summary>
            TTO = 780,

            /// <summary> Tunisia ISO 3166-2 Code. </summary>
            TUN = 788,

            /// <summary> Turkey ISO 3166-2 Code. </summary>
            TUR = 792,

            /// <summary> Turkmenistan ISO 3166-2 Code. </summary>
            TKM = 795,

            /// <summary> Turks and Caicos Islands ISO 3166-2 Code. </summary>
            TCA = 796,

            /// <summary> Tuvalu ISO 3166-2 Code. </summary>
            TUV = 798,

            /// <summary> Uganda ISO 3166-2 Code. </summary>
            UGA = 800,

            /// <summary> Ukraine ISO 3166-2 Code. </summary>
            UKR = 804,

            /// <summary> United Arab Emirates ISO 3166-2 Code. </summary>
            ARE = 784,

            /// <summary> United Kingdom of Great Britain and Northern Ireland
            /// ISO 3166-2 Code. </summary>
            GBR = 826,

            /// <summary> United States of America ISO 3166-2 Code. </summary>
            USA = 840,

            /// <summary> United States Minor Outlying Islands ISO 3166-2
            /// Code. </summary>
            UMI = 581,

            /// <summary> Uruguay ISO 3166-2 Code. </summary>
            URY = 858,

            /// <summary> Uzbekistan ISO 3166-2 Code. </summary>
            UZB = 860,

            /// <summary> Vanuatu ISO 3166-2 Code. </summary>
            VUT = 548,

            /// <summary> Venezuela (Bolivarian Republic of) ISO 3166-2
            /// Code. </summary>
            VEN = 862,

            /// <summary> Viet Nam ISO 3166-2 Code. </summary>
            VNM = 704,

            /// <summary> Virgin Islands (British) ISO 3166-2 Code. </summary>
            VGB = 92,

            /// <summary> Virgin Islands (U.S.) ISO 3166-2 Code. </summary>
            VIR = 850,

            /// <summary> Wallis and Futuna ISO 3166-2 Code. </summary>
            WLF = 876,

            /// <summary> Western Sahara ISO 3166-2 Code. </summary>
            ESH = 732,

            /// <summary> Yemen ISO 3166-2 Code. </summary>
            YEM = 887,

            /// <summary> Zambia ISO 3166-2 Code. </summary>
            ZMB = 894,

            /// <summary> Zimbabwe ISO 3166-2 Code. </summary>
            ZWE = 716,
        }
    }
}