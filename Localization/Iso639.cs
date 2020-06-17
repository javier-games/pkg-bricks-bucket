// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
namespace BricksBucket.Localization
{
	/// <!-- Iso639 -->
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
	/// Defines two letters codes for the names of languages according to
	/// the <b>ISO 639-1</b> standard, is the first part of the <see
	/// href= "https://www.iso.org/iso-639-language-codes.html">ISO 639</see>
	/// series and represents most of the major languages of the world. 
	/// </para>
	/// 
	/// </summary>
	///
	/// <seealso cref="Iso3166"/>
	/// <seealso cref="Lcid"/>
	/// <seealso cref="BricksBucket.Localization.Culture"/>
	/// 
	/// <!-- Note: The code of the members of the enum have been generated with
	/// the following table: https://bit.ly/bb-localization-iso639. Notice that
	/// the value of each enum member has been created by the concatenation of
	/// the unicode value of each char in the ISO639-3 Code or ISO639-2/T Code
	/// with the  special case of the macro language "Bihari languages" wich
	/// uses only the ISO639-2/T Code since it does not have a value in the
	/// ISO639-3 standard. -->
	///
	/// <!-- By Javier García | @jvrgms | 2020 -->
	public enum Iso639
	{
		/// <summary>No Language ISO 639-1 Code.</summary>
		NONE = 0x00000,

		/// <summary>Afar ISO 639-1 Code.</summary>
		AA = 0xA04C6,

		/// <summary>Abkhazian ISO 639-1 Code.</summary>
		AB = 0xA0523,

		/// <summary>Avestan ISO 639-1 Code.</summary>
		AE = 0xA0CED,

		/// <summary>Afrikaans ISO 639-1 Code.</summary>
		AF = 0xA06BA,

		/// <summary>Akan ISO 639-1 Code.</summary>
		AK = 0xA089D,

		/// <summary>Amharic ISO 639-1 Code.</summary>
		AM = 0xA096C,

		/// <summary>Aragonese ISO 639-1 Code.</summary>
		AN = 0xA0B5F,

		/// <summary>Arabic ISO 639-1 Code.</summary>
		AR = 0xA0B59,

		/// <summary>Assamese ISO 639-1 Code.</summary>
		AS = 0xA0BC9,

		/// <summary>Avaric ISO 639-1 Code.</summary>
		AV = 0xA0CE9,

		/// <summary>Aymara ISO 639-1 Code.</summary>
		AY = 0xA0E21,

		/// <summary>Azerbaijani ISO 639-1 Code.</summary>
		AZ = 0xA0E7D,

		/// <summary>Bashkir ISO 639-1 Code.</summary>
		BA = 0xA2BCF,

		/// <summary>Belarusian ISO 639-1 Code.</summary>
		BE = 0xA2D60,

		/// <summary>Bulgarian ISO 639-1 Code.</summary>
		BG = 0xA33A0,

		/// <summary>Bihari languages ISO 639-1 Code.</summary>
		BH = 0xA2EEC,

		/// <summary>Bislama ISO 639-1 Code.</summary>
		BI = 0xA2EF7,

		/// <summary>Bambara ISO 639-1 Code.</summary>
		BM = 0xA2BD1,

		/// <summary>Bengali ISO 639-1 Code.</summary>
		BN = 0xA2D62,

		/// <summary>Tibetan ISO 639-1 Code.</summary>
		BO = 0xA3140,

		/// <summary>Breton ISO 639-1 Code.</summary>
		BR = 0xA326D,

		/// <summary>Bosnian ISO 639-1 Code.</summary>
		BS = 0xA314F,

		/// <summary>Catalan ISO 639-1 Code.</summary>
		CA = 0xA52E8,

		/// <summary>Chechen ISO 639-1 Code.</summary>
		CE = 0xA5595,

		/// <summary>Chamorro ISO 639-1 Code.</summary>
		CH = 0xA5591,

		/// <summary>Corsican ISO 639-1 Code.</summary>
		CO = 0xA585F,

		/// <summary>Cree ISO 639-1 Code.</summary>
		CR = 0xA597D,

		/// <summary>Czech ISO 639-1 Code.</summary>
		CS = 0xA5477,

		/// <summary>Old Church Slavonic ISO 639-1 Code.</summary>
		CU = 0xA55A5,

		/// <summary>Chuvash ISO 639-1 Code.</summary>
		CV = 0xA55A6,

		/// <summary>Welsh ISO 639-1 Code.</summary>
		CY = 0xA5C41,

		/// <summary>Danish ISO 639-1 Code.</summary>
		DA = 0xA79F2,

		/// <summary>German ISO 639-1 Code.</summary>
		DE = 0xA7B89,

		/// <summary>Divehi ISO 639-1 Code.</summary>
		DV = 0xA7D1A,

		/// <summary>Dzongkha ISO 639-1 Code.</summary>
		DZ = 0xA83B7,

		/// <summary>Ewe ISO 639-1 Code.</summary>
		EE = 0xAA991,

		/// <summary>Greek ISO 639-1 Code.</summary>
		EL = 0xAA54C,

		/// <summary>English ISO 639-1 Code.</summary>
		EN = 0xAA60F,

		/// <summary>Esperanto ISO 639-1 Code.</summary>
		EO = 0xAA6DF,

		/// <summary>Spanish ISO 639-1 Code.</summary>
		ES = 0xCC9B1,

		/// <summary>Estonian ISO 639-1 Code.</summary>
		ET = 0xAA810,

		/// <summary>Basque ISO 639-1 Code.</summary>
		EU = 0xAA8D7,

		/// <summary>Persian ISO 639-1 Code.</summary>
		FA = 0xAC817,

		/// <summary>Fulah ISO 639-1 Code.</summary>
		FF = 0xACFE0,

		/// <summary>Finnish ISO 639-1 Code.</summary>
		FI = 0xACB32,

		/// <summary>Fijian ISO 639-1 Code.</summary>
		FJ = 0xACB2E,

		/// <summary>Faroese ISO 639-1 Code.</summary>
		FO = 0xAC813,

		/// <summary>French ISO 639-1 Code.</summary>
		FR = 0xACEA9,

		/// <summary>Western Frisian ISO 639-1 Code.</summary>
		FY = 0xACEC1,

		/// <summary>Irish ISO 639-1 Code.</summary>
		GA = 0xAF365,

		/// <summary>Scottish Gaelic ISO 639-1 Code.</summary>
		GD = 0xAF361,

		/// <summary>Galician ISO 639-1 Code.</summary>
		GL = 0xAF367,

		/// <summary>Guarani ISO 639-1 Code.</summary>
		GN = 0xAF5C6,

		/// <summary>Gujarati ISO 639-1 Code.</summary>
		GU = 0xAF6EE,

		/// <summary>Manx ISO 639-1 Code.</summary>
		GV = 0xAF376,

		/// <summary>Hausa ISO 639-1 Code.</summary>
		HA = 0xB1639,

		/// <summary>Hebrew ISO 639-1 Code.</summary>
		HE = 0xB17B6,

		/// <summary>Hindi ISO 639-1 Code.</summary>
		HI = 0xB1952,

		/// <summary>Hiri Motu ISO 639-1 Code.</summary>
		HO = 0xB1AE3,

		/// <summary>Croatian ISO 639-1 Code.</summary>
		HR = 0xB1CDE,

		/// <summary>Haitian Creole ISO 639-1 Code.</summary>
		HT = 0xB1638,

		/// <summary>Hungarian ISO 639-1 Code.</summary>
		HU = 0xB1E02,

		/// <summary>Armenian ISO 639-1 Code.</summary>
		HY = 0xB1F89,

		/// <summary>Herero ISO 639-1 Code.</summary>
		HZ = 0xB17C6,

		/// <summary>Interlingua ISO 639-1 Code.</summary>
		IA = 0xB4249,

		/// <summary>Indonesian ISO 639-1 Code.</summary>
		ID = 0xB424C,

		/// <summary>Interlingue ISO 639-1 Code.</summary>
		IE = 0xB4185,

		/// <summary>Igbo ISO 639-1 Code.</summary>
		IG = 0xB3DA7,

		/// <summary>Yi ISO 639-1 Code.</summary>
		II = 0xB405D,

		/// <summary>Inupiaq ISO 639-1 Code.</summary>
		IK = 0xB431B,

		/// <summary>Ido ISO 639-1 Code.</summary>
		IO = 0xB3E6F,

		/// <summary>Icelandic ISO 639-1 Code.</summary>
		IS = 0xB4448,

		/// <summary>Italian ISO 639-1 Code.</summary>
		IT = 0xB44A1,

		/// <summary>Inuktitut ISO 639-1 Code.</summary>
		IU = 0xB4131,

		/// <summary>Japanese ISO 639-1 Code.</summary>
		JA = 0xB6A2E,

		/// <summary>Javanese ISO 639-1 Code.</summary>
		JV = 0xB645A,

		/// <summary>Georgian ISO 639-1 Code.</summary>
		KA = 0xB8B68,

		/// <summary>Kongo ISO 639-1 Code.</summary>
		KG = 0xB90DA,

		/// <summary>Kikuyu ISO 639-1 Code.</summary>
		KI = 0xB8E7F,

		/// <summary>Kwanyama ISO 639-1 Code.</summary>
		KJ = 0xB9325,

		/// <summary>Kazakh ISO 639-1 Code.</summary>
		KK = 0xB8B6E,

		/// <summary>Greenlandic ISO 639-1 Code.</summary>
		KL = 0xB8B60,

		/// <summary>Khmer ISO 639-1 Code.</summary>
		KM = 0xB8E1D,

		/// <summary>Kannada ISO 639-1 Code.</summary>
		KN = 0xB8B62,

		/// <summary>Korean ISO 639-1 Code.</summary>
		KO = 0xB90DE,

		/// <summary>Kanuri ISO 639-1 Code.</summary>
		KR = 0xB8B69,

		/// <summary>Kashmiri ISO 639-1 Code.</summary>
		KS = 0xB8B67,

		/// <summary>Kurdish ISO 639-1 Code.</summary>
		KU = 0xB9336,

		/// <summary>Komi ISO 639-1 Code.</summary>
		KV = 0xB90D9,

		/// <summary>Cornish ISO 639-1 Code.</summary>
		KW = 0xA585E,

		/// <summary>Kyrgyz ISO 639-1 Code.</summary>
		KY = 0xB8E86,

		/// <summary>Latin ISO 639-1 Code.</summary>
		LA = 0xBB278,

		/// <summary>Luxembourgish ISO 639-1 Code.</summary>
		LB = 0xBB9EA,

		/// <summary>Ganda ISO 639-1 Code.</summary>
		LG = 0xBBA3B,

		/// <summary>Limburgish ISO 639-1 Code.</summary>
		LI = 0xBB591,

		/// <summary>Lingala ISO 639-1 Code.</summary>
		LN = 0xBB592,

		/// <summary>Lao ISO 639-1 Code.</summary>
		LO = 0xBB273,

		/// <summary>Lithuanian ISO 639-1 Code.</summary>
		LT = 0xBB598,

		/// <summary>Luba-Katanga ISO 639-1 Code.</summary>
		LU = 0xBBA36,

		/// <summary>Latvian ISO 639-1 Code.</summary>
		LV = 0xBB27A,

		/// <summary>Malagasy ISO 639-1 Code.</summary>
		MG = 0xBDDC7,

		/// <summary>Marshallese ISO 639-1 Code.</summary>
		MH = 0xBD97C,

		/// <summary>Maori ISO 639-1 Code.</summary>
		MI = 0xBE021,

		/// <summary>Macedonian ISO 639-1 Code.</summary>
		MK = 0xBDD60,

		/// <summary>Malayalam ISO 639-1 Code.</summary>
		ML = 0xBD980,

		/// <summary>Mongolian ISO 639-1 Code.</summary>
		MN = 0xBDEFA,

		/// <summary>Marathi ISO 639-1 Code.</summary>
		MR = 0xBD986,

		/// <summary>Malay ISO 639-1 Code.</summary>
		MS = 0xBE07D,

		/// <summary>Maltese ISO 639-1 Code.</summary>
		MT = 0xBDDD4,

		/// <summary>Burmese ISO 639-1 Code.</summary>
		MY = 0xBE2D5,

		/// <summary>Nauru ISO 639-1 Code.</summary>
		NA = 0xC0099,

		/// <summary>Norwegian Bokmål ISO 639-1 Code.</summary>
		NB = 0xC05FE,

		/// <summary>North Ndebele ISO 639-1 Code.</summary>
		ND = 0xC01B5,

		/// <summary>Nepali ISO 639-1 Code.</summary>
		NE = 0xC0224,

		/// <summary>Ndonga ISO 639-1 Code.</summary>
		NG = 0xC01BF,

		/// <summary>Dutch ISO 639-1 Code.</summary>
		NL = 0xC04D4,

		/// <summary>Norwegian Nynorsk ISO 639-1 Code.</summary>
		NN = 0xC05A7,

		/// <summary>Norwegian ISO 639-1 Code.</summary>
		NO = 0xC060E,

		/// <summary>South Ndebele ISO 639-1 Code.</summary>
		NR = 0xC00F4,

		/// <summary>Navajo ISO 639-1 Code.</summary>
		NV = 0xC009A,

		/// <summary>Chewa ISO 639-1 Code.</summary>
		NY = 0xC09E5,

		/// <summary>Occitan ISO 639-1 Code.</summary>
		OC = 0xC2865,

		/// <summary>Ojibwa ISO 639-1 Code.</summary>
		OJ = 0xC2B21,

		/// <summary>Oromo ISO 639-1 Code.</summary>
		OM = 0xC2E45,

		/// <summary>Odia ISO 639-1 Code.</summary>
		OR = 0xC2E41,

		/// <summary>Ossetian ISO 639-1 Code.</summary>
		OS = 0xC2EAF,

		/// <summary>Punjabi ISO 639-1 Code.</summary>
		PA = 0xC4EB2,

		/// <summary>Pali ISO 639-1 Code.</summary>
		PI = 0xC52F9,

		/// <summary>Polish ISO 639-1 Code.</summary>
		PL = 0xC5428,

		/// <summary>Pashto ISO 639-1 Code.</summary>
		PS = 0xC5687,

		/// <summary>Portuguese ISO 639-1 Code.</summary>
		PT = 0xC542E,

		/// <summary>Quechua ISO 639-1 Code.</summary>
		QU = 0xC7D89,

		/// <summary>Romansh ISO 639-1 Code.</summary>
		RM = 0xCA244,

		/// <summary>Rundi ISO 639-1 Code.</summary>
		RN = 0xCA4A2,

		/// <summary>Romanian ISO 639-1 Code.</summary>
		RO = 0xCA24A,

		/// <summary>Russian ISO 639-1 Code.</summary>
		RU = 0xCA4A7,

		/// <summary>Kinyarwanda ISO 639-1 Code.</summary>
		RW = 0xB8E82,

		/// <summary>Sanskrit ISO 639-1 Code.</summary>
		SA = 0xCC3E2,

		/// <summary>Sardinian ISO 639-1 Code.</summary>
		SC = 0xCCA7C,

		/// <summary>Sindhi ISO 639-1 Code.</summary>
		SD = 0xCC8EC,

		/// <summary>Northern Sami ISO 639-1 Code.</summary>
		SE = 0xCC889,

		/// <summary>Sango ISO 639-1 Code.</summary>
		SG = 0xCC3DB,

		/// <summary>Sinhala ISO 639-1 Code.</summary>
		SI = 0xCC702,

		/// <summary>Slovak ISO 639-1 Code.</summary>
		SK = 0xCC82B,

		/// <summary>Slovenian ISO 639-1 Code.</summary>
		SL = 0xCC836,

		/// <summary>Samoan ISO 639-1 Code.</summary>
		SM = 0xCC893,

		/// <summary>Shona ISO 639-1 Code.</summary>
		SN = 0xCC8E9,

		/// <summary>Somali ISO 639-1 Code.</summary>
		SO = 0xCC959,

		/// <summary>Albanian ISO 639-1 Code.</summary>
		SQ = 0xCCA1D,

		/// <summary>Serbian ISO 639-1 Code.</summary>
		SR = 0xCCA88,

		/// <summary>Swati ISO 639-1 Code.</summary>
		SS = 0xCCAF3,

		/// <summary>Sotho ISO 639-1 Code.</summary>
		ST = 0xCC960,

		/// <summary>Sundanese ISO 639-1 Code.</summary>
		SU = 0xCCBB2,

		/// <summary>Swedish ISO 639-1 Code.</summary>
		SV = 0xCCC71,

		/// <summary>Swahili ISO 639-1 Code.</summary>
		SW = 0xCCC6D,

		/// <summary>Tamil ISO 639-1 Code.</summary>
		TA = 0xCEAF1,

		/// <summary>Telugu ISO 639-1 Code.</summary>
		TE = 0xCEC80,

		/// <summary>Tajik ISO 639-1 Code.</summary>
		TG = 0xCED47,

		/// <summary>Thai ISO 639-1 Code.</summary>
		TH = 0xCEDA1,

		/// <summary>Tigrinya ISO 639-1 Code.</summary>
		TI = 0xCEE16,

		/// <summary>Turkmen ISO 639-1 Code.</summary>
		TK = 0xCF2BF,

		/// <summary>Tagalog ISO 639-1 Code.</summary>
		TL = 0xCED48,

		/// <summary>Tswana ISO 639-1 Code.</summary>
		TN = 0xCF1FA,

		/// <summary>Tongan ISO 639-1 Code.</summary>
		TO = 0xCF06A,

		/// <summary>Turkish ISO 639-1 Code.</summary>
		TR = 0xCF2C6,

		/// <summary>Tsonga ISO 639-1 Code.</summary>
		TS = 0xCF1FB,

		/// <summary>Tatar ISO 639-1 Code.</summary>
		TT = 0xCEAF8,

		/// <summary>Twi ISO 639-1 Code.</summary>
		TW = 0xCF385,

		/// <summary>Tahitian ISO 639-1 Code.</summary>
		TY = 0xCEAEC,

		/// <summary>Uyghur ISO 639-1 Code.</summary>
		UG = 0xD151B,

		/// <summary>Ukrainian ISO 639-1 Code.</summary>
		UK = 0xD15EE,

		/// <summary>Urdu ISO 639-1 Code.</summary>
		UR = 0xD189C,

		/// <summary>Uzbek ISO 639-1 Code.</summary>
		UZ = 0xD1BBA,

		/// <summary>Venda ISO 639-1 Code.</summary>
		VE = 0xD3AA2,

		/// <summary>Vietnamese ISO 639-1 Code.</summary>
		VI = 0xD3C29,

		/// <summary>Volapük ISO 639-1 Code.</summary>
		VO = 0xD3E88,

		/// <summary>Walloon ISO 639-1 Code.</summary>
		WA = 0xD646E,

		/// <summary>Wolof ISO 639-1 Code.</summary>
		WO = 0xD6598,

		/// <summary>Xhosa ISO 639-1 Code.</summary>
		XH = 0xD89EF,

		/// <summary>Yiddish ISO 639-1 Code.</summary>
		YI = 0xDB158,

		/// <summary>Yoruba ISO 639-1 Code.</summary>
		YO = 0xDB3BE,

		/// <summary>Zhuang ISO 639-1 Code.</summary>
		ZA = 0xDD801,

		/// <summary>Chinese ISO 639-1 Code.</summary>
		ZH = 0xDD80F,

		/// <summary>Zulu ISO 639-1 Code.</summary>
		ZU = 0xDDD20,
	}
}