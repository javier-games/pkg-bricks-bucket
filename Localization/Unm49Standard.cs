using System.Collections.Generic;
// ReSharper disable StringLiteralTypo

namespace BricksBucket.Localization
{
	// UN M49 Part.
	// By Javier García | @jvrgms | 2020
	public static partial class Standards
	{
		/// <summary>
		/// Gets the name of the given UN M49 enum value.
		/// </summary>
		/// <param name="region">UN M49  to look for its name.</param>
		/// <returns>Empty if the UN M49  value has not a name.</returns>
		public static string GetName (Unm49 region) =>
			Unm49Names.ContainsKey (region)
				? Unm49Names[region]
				: string.Empty;

		/// <summary>
		/// Gets the code of the given UN M49 enum value.
		/// </summary>
		/// <param name="region">UN M49 to look for its code.</param>
		/// <returns>Empty if the UN M49 value has not a code.</returns>
		public static string GetCode (Unm49 region) =>
			System.Enum.IsDefined (typeof (Unm49), region)
				? ((int) region).ToString ("000")
				: string.Empty;

		/// <summary>
		/// Gets the UN M49 enum value for the given string code.
		/// </summary>
		/// <param name="code">String value of the code of an UN M49.</param>
		/// <returns><value>Unm49.NONE</value> if the code is not found.
		/// </returns>
		public static Unm49 GetUnm49 (string code) =>
			int.TryParse (code, out int result)
				? GetUnm49 (result)
				: Unm49.NONE;
		
		/// <summary>
		/// Gets the UN M49 enum value for the given int code.
		/// </summary>
		/// <param name="code">Int value of the code of an UN M49.</param>
		/// <returns><value>Unm49.NONE</value> if the code is not found.
		/// </returns>
		private static Unm49 GetUnm49 (int code) => 
			System.Enum.IsDefined (typeof (Unm49), code)
				? (Unm49) code
				: Unm49.NONE;
		
		/// <summary>
		/// Gets the ISO 3166 code of an UN M49 enum value.
		/// </summary>
		/// <param name="region">UN M49 to look for its ISO 3166 code.</param>
		/// <returns><value>Iso3166.NONE</value> for a UN M49 not found.
		/// </returns>
		public static Iso3166 GetIso3166 (Unm49 region) =>
			System.Enum.IsDefined (typeof (Iso3166), (int) region)
				? (Iso3166) (int) region
				: Iso3166.NONE;

		/// <summary>
		/// Dictionary of the names for UN M49 enum members.
		/// </summary>
		private static readonly Dictionary<Unm49, string> Unm49Names =
			new Dictionary<Unm49, string>
			{
				{Unm49.NONE, "None"},
				{Unm49.WORLD, "World"},
				{Unm49.AFRICA, "Africa"},
				{Unm49.NORTH_AMERICA, "North America"},
				{Unm49.AF, "Afghanistan"},
				{Unm49.SOUTH_AMERICA, "South America"},
				{Unm49.AL, "Albania"},
				{Unm49.OCEANIA, "Oceania"},
				{Unm49.AQ, "Antarctica"},
				{Unm49.WESTERN_AFRICA, "Western Africa"},
				{Unm49.DZ, "Algeria"},
				{Unm49.CENTRAL_AMERICA, "Central America"},
				{Unm49.EASTERN_AFRICA, "Eastern Africa"},
				{Unm49.NORTHERN_AFRICA, "Northern Africa"},
				{Unm49.AS, "American Samoa"},
				{Unm49.MIDDLE_AFRICA, "Middle Africa"},
				{Unm49.SOUTHERN_AFRICA, "Southern Africa"},
				{Unm49.AMERICAS, "Americas"},
				{Unm49.AD, "Andorra"},
				{Unm49.NORTHERN_AMERICA, "Northern America"},
				{Unm49.AO, "Angola"},
				{Unm49.AG, "Antigua and Barbuda"},
				{Unm49.CARIBBEAN, "Caribbean"},
				{Unm49.EASTERN_ASIA, "Eastern Asia"},
				{Unm49.AZ, "Azerbaijan"},
				{Unm49.AR, "Argentina"},
				{Unm49.SOUTHERN_ASIA, "Southern Asia"},
				{Unm49.SOUTH_EASTERN_ASIA, "South Eastern Asia"},
				{Unm49.AU, "Australia"},
				{Unm49.SOUTHERN_EUROPE, "Southern Europe"},
				{Unm49.AT, "Austria"},
				{Unm49.BS, "Bahamas"},
				{Unm49.BH, "Bahrain"},
				{Unm49.BD, "Bangladesh"},
				{Unm49.AM, "Armenia"},
				{Unm49.BB, "Barbados"},
				{Unm49.AUSTRALIA_AND_NEW_ZEALAND, "Australia and New Zealand"},
				{Unm49.MELANESIA, "Melanesia"},
				{Unm49.BE, "Belgium"},
				{Unm49.MICRONESIA, "Micronesia"},
				{Unm49.BM, "Bermuda"},
				{Unm49.POLYNESIA, "Polynesia"},
				{Unm49.BT, "Bhutan"},
				{Unm49.BO, "Bolivia, Plurinational State of"},
				{Unm49.BA, "Bosnia and Herzegovina"},
				{Unm49.BW, "Botswana"},
				{Unm49.BV, "Bouvet Island"},
				{Unm49.BR, "Brazil"},
				{Unm49.BZ, "Belize"},
				{Unm49.IO, "British Indian Ocean Territory"},
				{Unm49.SB, "Solomon Islands"},
				{Unm49.VG, "Virgin Islands (British)"},
				{Unm49.BN, "Brunei Darussalam"},
				{Unm49.BG, "Bulgaria"},
				{Unm49.MM, "Myanmar"},
				{Unm49.BI, "Burundi"},
				{Unm49.BY, "Belarus"},
				{Unm49.KH, "Cambodia"},
				{Unm49.CM, "Cameroon"},
				{Unm49.CA, "Canada"},
				{Unm49.CV, "Cabo Verde"},
				{Unm49.KY, "Cayman Islands"},
				{Unm49.CF, "Central African Republic"},
				{Unm49.ASIA, "Asia"},
				{Unm49.CENTRAL_ASIA, "Central Asia"},
				{Unm49.LK, "Sri Lanka"},
				{Unm49.WESTERN_ASIA, "Western Asia"},
				{Unm49.TD, "Chad"},
				{Unm49.EUROPE, "Europe"},
				{
					Unm49.EASTERN_EUROPE_INCLUDING_NORTHERN_ASIA,
					"Eastern Europe (Including Northern Asia)"
				},
				{Unm49.CL, "Chile"},
				{Unm49.NORTHERN_EUROPE, "Northern Europe"},
				{Unm49.WESTERN_EUROPE, "Western Europe"},
				{Unm49.CN, "China"},
				{Unm49.TW, "Taiwan, Province of China"},
				{Unm49.CX, "Christmas Island"},
				{Unm49.CC, "Cocos (Keeling) Islands"},
				{Unm49.CO, "Colombia"},
				{Unm49.KM, "Comoros"},
				{Unm49.YT, "Mayotte"},
				{Unm49.CG, "Congo"},
				{Unm49.CD, "Congo, Democratic Republic of the"},
				{Unm49.CK, "Cook Islands"},
				{Unm49.CR, "Costa Rica"},
				{Unm49.HR, "Croatia"},
				{Unm49.CU, "Cuba"},
				{Unm49.CY, "Cyprus"},
				{Unm49.SUB_SAHARAN_AFRICA, "Sub-Saharan Africa"},
				{Unm49.CZ, "Czechia"},
				{Unm49.BJ, "Benin"},
				{Unm49.DK, "Denmark"},
				{Unm49.DM, "Dominica"},
				{Unm49.DO, "Dominican Republic"},
				{Unm49.EC, "Ecuador"},
				{Unm49.SV, "El Salvador"},
				{Unm49.GQ, "Equatorial Guinea"},
				{Unm49.ET, "Ethiopia"},
				{Unm49.ER, "Eritrea"},
				{Unm49.EE, "Estonia"},
				{Unm49.FO, "Faroe Islands"},
				{Unm49.FK, "Falkland Islands (Malvinas)"},
				{Unm49.GS, "South Georgia and the South Sandwich Islands"},
				{Unm49.FJ, "Fiji"},
				{Unm49.FI, "Finland"},
				{Unm49.AX, "Åland Islands"},
				{Unm49.FR, "France"},
				{Unm49.GF, "French Guiana"},
				{Unm49.PF, "French Polynesia"},
				{Unm49.TF, "French Southern Territories"},
				{Unm49.DJ, "Djibouti"},
				{Unm49.GA, "Gabon"},
				{Unm49.GE, "Georgia"},
				{Unm49.GM, "Gambia"},
				{Unm49.PS, "Palestine, State of"},
				{Unm49.DE, "Germany"},
				{Unm49.GH, "Ghana"},
				{Unm49.GI, "Gibraltar"},
				{Unm49.KI, "Kiribati"},
				{Unm49.GR, "Greece"},
				{Unm49.GL, "Greenland"},
				{Unm49.GD, "Grenada"},
				{Unm49.GP, "Guadeloupe"},
				{Unm49.GU, "Guam"},
				{Unm49.GT, "Guatemala"},
				{Unm49.GN, "Guinea"},
				{Unm49.GY, "Guyana"},
				{Unm49.HT, "Haiti"},
				{Unm49.HM, "Heard Island and McDonald Islands"},
				{Unm49.VA, "Holy See"},
				{Unm49.HN, "Honduras"},
				{Unm49.HK, "Hong Kong"},
				{Unm49.HU, "Hungary"},
				{Unm49.IS, "Iceland"},
				{Unm49.IN, "India"},
				{Unm49.ID, "Indonesia"},
				{Unm49.IR, "Iran (Islamic Republic of)"},
				{Unm49.IQ, "Iraq"},
				{Unm49.IE, "Ireland"},
				{Unm49.IL, "Israel"},
				{Unm49.IT, "Italy"},
				{Unm49.CI, "Côte d'Ivoire"},
				{Unm49.JM, "Jamaica"},
				{Unm49.JP, "Japan"},
				{Unm49.KZ, "Kazakhstan"},
				{Unm49.JO, "Jordan"},
				{Unm49.KE, "Kenya"},
				{Unm49.KP, "Korea (Democratic People's Republic of)"},
				{Unm49.KR, "Korea, Republic of"},
				{Unm49.KW, "Kuwait"},
				{Unm49.KG, "Kyrgyzstan"},
				{Unm49.LA, "Lao People's Democratic Republic"},
				{
					Unm49.LATIN_AMERICA_AND_THE_CARIBBEAN,
					"Latin America and the Caribbean"
				},
				{Unm49.LB, "Lebanon"},
				{Unm49.LS, "Lesotho"},
				{Unm49.LV, "Latvia"},
				{Unm49.LR, "Liberia"},
				{Unm49.LY, "Libya"},
				{Unm49.LI, "Liechtenstein"},
				{Unm49.LT, "Lithuania"},
				{Unm49.LU, "Luxembourg"},
				{Unm49.MO, "Macao"},
				{Unm49.MG, "Madagascar"},
				{Unm49.MW, "Malawi"},
				{Unm49.MY, "Malaysia"},
				{Unm49.MV, "Maldives"},
				{Unm49.ML, "Mali"},
				{Unm49.MT, "Malta"},
				{Unm49.MQ, "Martinique"},
				{Unm49.MR, "Mauritania"},
				{Unm49.MU, "Mauritius"},
				{Unm49.MX, "Mexico"},
				{Unm49.MC, "Monaco"},
				{Unm49.MN, "Mongolia"},
				{Unm49.MD, "Moldova, Republic of"},
				{Unm49.ME, "Montenegro"},
				{Unm49.MS, "Montserrat"},
				{Unm49.MA, "Morocco"},
				{Unm49.MZ, "Mozambique"},
				{Unm49.OM, "Oman"},
				{Unm49.NA, "Namibia"},
				{Unm49.NR, "Nauru"},
				{Unm49.NP, "Nepal"},
				{Unm49.NL, "Netherlands"},
				{Unm49.CW, "Curaçao"},
				{Unm49.AW, "Aruba"},
				{Unm49.SX, "Sint Maarten (Dutch part)"},
				{Unm49.BQ, "Bonaire, Sint Eustatius and Saba"},
				{Unm49.NC, "New Caledonia"},
				{Unm49.VU, "Vanuatu"},
				{Unm49.NZ, "New Zealand"},
				{Unm49.NI, "Nicaragua"},
				{Unm49.NE, "Niger"},
				{Unm49.NG, "Nigeria"},
				{Unm49.NU, "Niue"},
				{Unm49.NF, "Norfolk Island"},
				{Unm49.NO, "Norway"},
				{Unm49.MP, "Northern Mariana Islands"},
				{Unm49.UM, "United States Minor Outlying Islands"},
				{Unm49.FM, "Micronesia (Federated States of)"},
				{Unm49.MH, "Marshall Islands"},
				{Unm49.PW, "Palau"},
				{Unm49.PK, "Pakistan"},
				{Unm49.PA, "Panama"},
				{Unm49.PG, "Papua New Guinea"},
				{Unm49.PY, "Paraguay"},
				{Unm49.PE, "Peru"},
				{Unm49.PH, "Philippines"},
				{Unm49.PN, "Pitcairn"},
				{Unm49.PL, "Poland"},
				{Unm49.PT, "Portugal"},
				{Unm49.GW, "Guinea-Bissau"},
				{Unm49.TL, "Timor-Leste"},
				{Unm49.PR, "Puerto Rico"},
				{Unm49.QA, "Qatar"},
				{Unm49.RE, "Réunion"},
				{Unm49.RO, "Romania"},
				{Unm49.RU, "Russian Federation"},
				{Unm49.RW, "Rwanda"},
				{Unm49.BL, "Saint Barthélemy"},
				{Unm49.SH, "Saint Helena, Ascension and Tristan da Cunha"},
				{Unm49.KN, "Saint Kitts and Nevis"},
				{Unm49.AI, "Anguilla"},
				{Unm49.LC, "Saint Lucia"},
				{Unm49.MF, "Saint Martin (French part)"},
				{Unm49.PM, "Saint Pierre and Miquelon"},
				{Unm49.VC, "Saint Vincent and the Grenadines"},
				{Unm49.SM, "San Marino"},
				{Unm49.ST, "Sao Tome and Principe"},
				{Unm49.SARK, "Sark"},
				{Unm49.SA, "Saudi Arabia"},
				{Unm49.SN, "Senegal"},
				{Unm49.RS, "Serbia"},
				{Unm49.SC, "Seychelles"},
				{Unm49.SL, "Sierra Leone"},
				{Unm49.SG, "Singapore"},
				{Unm49.SK, "Slovakia"},
				{Unm49.VN, "Viet Nam"},
				{Unm49.SI, "Slovenia"},
				{Unm49.SO, "Somalia"},
				{Unm49.ZA, "South Africa"},
				{Unm49.ZW, "Zimbabwe"},
				{Unm49.ES, "Spain"},
				{Unm49.SS, "South Sudan"},
				{Unm49.SD, "Sudan"},
				{Unm49.EH, "Western Sahara"},
				{Unm49.SR, "Suriname"},
				{Unm49.SJ, "Svalbard and Jan Mayen"},
				{Unm49.SZ, "Eswatini"},
				{Unm49.SE, "Sweden"},
				{Unm49.CH, "Switzerland"},
				{Unm49.SY, "Syrian Arab Republic"},
				{Unm49.TJ, "Tajikistan"},
				{Unm49.TH, "Thailand"},
				{Unm49.TG, "Togo"},
				{Unm49.TK, "Tokelau"},
				{Unm49.TO, "Tonga"},
				{Unm49.TT, "Trinidad and Tobago"},
				{Unm49.AE, "United Arab Emirates"},
				{Unm49.TN, "Tunisia"},
				{Unm49.TR, "Turkey"},
				{Unm49.TM, "Turkmenistan"},
				{Unm49.TC, "Turks and Caicos Islands"},
				{Unm49.TV, "Tuvalu"},
				{Unm49.UG, "Uganda"},
				{Unm49.UA, "Ukraine"},
				{Unm49.MK, "North Macedonia"},
				{Unm49.EG, "Egypt"},
				{
					Unm49.GB,
					"United Kingdom of Great Britain and Northern Ireland"
				},
				{Unm49.GG, "Guernsey"},
				{Unm49.JE, "Jersey"},
				{Unm49.IM, "Isle of Man"},
				{Unm49.TZ, "Tanzania, United Republic of"},
				{Unm49.US, "United States of America"},
				{Unm49.VI, "Virgin Islands (U.S.)"},
				{Unm49.BF, "Burkina Faso"},
				{Unm49.UY, "Uruguay"},
				{Unm49.UZ, "Uzbekistan"},
				{Unm49.VE, "Venezuela (Bolivarian Republic of)"},
				{Unm49.WF, "Wallis and Futuna"},
				{Unm49.WS, "Samoa"},
				{Unm49.YE, "Yemen"},
				{Unm49.ZM, "Zambia"},
			};
	}
}