// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedType.Global
namespace BricksBucket.Localization
{
	/// <!-- ISO15924 -->
	///
	/// <summary>
	///
	/// <para>
	/// <i>"Provides a code for the presentation of names of scripts."</i>
	/// - <see href= "https://www.iso.org/standard/29546.html">
	/// International Organization for Standardization</see>.
	/// </para>
	///
	/// <para>
	/// Defines four letters codes and a number for scripts, according to the
	/// <see href= "https://www.iso.org/iso-3166-country-codes.html">
	/// ISO 15924</see> standard. 
	/// </para>
	/// 
	/// </summary>
	///
	/// <seealso cref="BricksBucket.Localization.ISO639"/>
	/// <seealso cref="BricksBucket.Localization.LCID"/>
	/// <seealso cref="BricksBucket.Localization.Culture"/>
	///
	/// <!-- Note: The code of the members of the enum have been generated with
	/// the following table: https://bit.ly/bb-localization-iso3166 -->
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	public enum ISO15924
	{
		/// <summary>None ISO 15924 Code.</summary>
		NONE = 0x000,

		/// <summary>Adlam ISO 15924 Code.</summary>
		ADLM = 0x0A6,

		/// <summary>Afaka ISO 15924 Code.</summary>
		AFAK = 0x1B7,

		/// <summary>Caucasian Albanian ISO 15924 Code.</summary>
		AGHB = 0x0EF,

		/// <summary>Tai Ahom ISO 15924 Code.</summary>
		AHOM = 0x152,

		/// <summary>Arabic ISO 15924 Code.</summary>
		ARAB = 0x0A0,

		/// <summary>Arabic Nastaliq ISO 15924 Code.</summary>
		ARAN = 0x0A1,

		/// <summary>Imperial Aramaic ISO 15924 Code.</summary>
		ARMI = 0x07C,

		/// <summary>Armenian ISO 15924 Code.</summary>
		ARMN = 0x0E6,

		/// <summary>Avestan ISO 15924 Code.</summary>
		AVST = 0x086,

		/// <summary>Balinese ISO 15924 Code.</summary>
		BALI = 0x168,

		/// <summary>Bamum ISO 15924 Code.</summary>
		BAMU = 0x1B3,

		/// <summary>Bassa Vah ISO 15924 Code.</summary>
		BASS = 0x103,

		/// <summary>Batak ISO 15924 Code.</summary>
		BATK = 0x16D,

		/// <summary>Bengali Bangla ISO 15924 Code.</summary>
		BENG = 0x145,

		/// <summary>Bhaiksuki ISO 15924 Code.</summary>
		BHKS = 0x14E,

		/// <summary>Blissymbols ISO 15924 Code.</summary>
		BLIS = 0x226,

		/// <summary>Bopomofo ISO 15924 Code.</summary>
		BOPO = 0x11D,

		/// <summary>Brahmi ISO 15924 Code.</summary>
		BRAH = 0x12C,

		/// <summary>Braille ISO 15924 Code.</summary>
		BRAI = 0x23A,

		/// <summary>Buginese ISO 15924 Code.</summary>
		BUGI = 0x16F,

		/// <summary>Buhid ISO 15924 Code.</summary>
		BUHD = 0x174,

		/// <summary>Chakma ISO 15924 Code.</summary>
		CAKM = 0x15D,

		/// <summary>Canadian Aboriginal ISO 15924 Code.</summary>
		CANS = 0x1B8,

		/// <summary>Carian ISO 15924 Code.</summary>
		CARI = 0x0C9,

		/// <summary>Cham ISO 15924 Code.</summary>
		CHAM = 0x166,

		/// <summary>Cherokee ISO 15924 Code.</summary>
		CHER = 0x1BD,

		/// <summary>Chorasmian ISO 15924 Code.</summary>
		CHRS = 0x06D,

		/// <summary>Cirth ISO 15924 Code.</summary>
		CIRT = 0x123,

		/// <summary>Coptic ISO 15924 Code.</summary>
		COPT = 0x0CC,

		/// <summary>Cypro-Minoan ISO 15924 Code.</summary>
		CPMN = 0x192,

		/// <summary>Cypriot Syllabary ISO 15924 Code.</summary>
		CPRT = 0x193,

		/// <summary>Cyrillic ISO 15924 Code.</summary>
		CYRL = 0x0DC,

		/// <summary>Cyrillic Old Church Slavonic ISO 15924 Code.</summary>
		CYRS = 0x0DD,

		/// <summary>Devanagari Nagari ISO 15924 Code.</summary>
		DEVA = 0x13B,

		/// <summary>Dives Akuru ISO 15924 Code.</summary>
		DIAK = 0x156,

		/// <summary>Dogra ISO 15924 Code.</summary>
		DOGR = 0x148,

		/// <summary>Deseret Mormon ISO 15924 Code.</summary>
		DSRT = 0x0FA,

		/// <summary>Duployan Shorthand ISO 15924 Code.</summary>
		DUPL = 0x2F3,

		/// <summary>Egyptian Demotic ISO 15924 Code.</summary>
		EGYD = 0x046,

		/// <summary>Egyptian Hieratic ISO 15924 Code.</summary>
		EGYH = 0x03C,

		/// <summary>Egyptian Hieroglyphs ISO 15924 Code.</summary>
		EGYP = 0x032,

		/// <summary>Elbasan ISO 15924 Code.</summary>
		ELBA = 0x0E2,

		/// <summary>Elymaic ISO 15924 Code.</summary>
		ELYM = 0x080,

		/// <summary>Ethiopic ISO 15924 Code.</summary>
		ETHI = 0x1AE,

		/// <summary>Khutsuri ISO 15924 Code.</summary>
		GEOK = 0x0F1,

		/// <summary>Georgian ISO 15924 Code.</summary>
		GEOR = 0x0F0,

		/// <summary>Glagolitic ISO 15924 Code.</summary>
		GLAG = 0x0E1,

		/// <summary>Gunjala Gondi ISO 15924 Code.</summary>
		GONG = 0x138,

		/// <summary>Masaram Gondi ISO 15924 Code.</summary>
		GONM = 0x139,

		/// <summary>Gothic ISO 15924 Code.</summary>
		GOTH = 0x0CE,

		/// <summary>Grantha ISO 15924 Code.</summary>
		GRAN = 0x157,

		/// <summary>Greek ISO 15924 Code.</summary>
		GREK = 0x0C8,

		/// <summary>Gujarati ISO 15924 Code.</summary>
		GUJR = 0x140,

		/// <summary>Gurmukhi ISO 15924 Code.</summary>
		GURU = 0x136,

		/// <summary>Han Bopomofo ISO 15924 Code.</summary>
		HANB = 0x1F7,

		/// <summary>Hangul ISO 15924 Code.</summary>
		HANG = 0x11E,

		/// <summary>Han ISO 15924 Code.</summary>
		HANI = 0x1F4,

		/// <summary>Hanunoo ISO 15924 Code.</summary>
		HANO = 0x173,

		/// <summary>Han Simplified ISO 15924 Code.</summary>
		HANS = 0x1F5,

		/// <summary>Han Traditional ISO 15924 Code.</summary>
		HANT = 0x1F6,

		/// <summary>Hatran ISO 15924 Code.</summary>
		HATR = 0x07F,

		/// <summary>Hebrew ISO 15924 Code.</summary>
		HEBR = 0x07D,

		/// <summary>Hiragana ISO 15924 Code.</summary>
		HIRA = 0x19A,

		/// <summary>Anatolian Hieroglyphs ISO 15924 Code.</summary>
		HLUW = 0x050,

		/// <summary>Pahawh Hmong ISO 15924 Code.</summary>
		HMNG = 0x1C2,

		/// <summary>Nyiakeng Puachue Hmong ISO 15924 Code.</summary>
		HMNP = 0x1C3,

		/// <summary>Japanese Syllabaries ISO 15924 Code.</summary>
		HRKT = 0x19C,

		/// <summary>Old Hungarian Runic ISO 15924 Code.</summary>
		HUNG = 0x0B0,

		/// <summary>Indus Harappan ISO 15924 Code.</summary>
		INDS = 0x262,

		/// <summary>Old Italic ISO 15924 Code.</summary>
		ITAL = 0x0D2,

		/// <summary>Jamo ISO 15924 Code.</summary>
		JAMO = 0x11C,

		/// <summary>Javanese ISO 15924 Code.</summary>
		JAVA = 0x169,

		/// <summary>Japanese ISO 15924 Code.</summary>
		JPAN = 0x19D,

		/// <summary>Jurchen ISO 15924 Code.</summary>
		JURC = 0x1FE,

		/// <summary>Kayah Li ISO 15924 Code.</summary>
		KALI = 0x165,

		/// <summary>Katakana ISO 15924 Code.</summary>
		KANA = 0x19B,

		/// <summary>Kharoshthi ISO 15924 Code.</summary>
		KHAR = 0x131,

		/// <summary>Khmer ISO 15924 Code.</summary>
		KHMR = 0x163,

		/// <summary>Khojki ISO 15924 Code.</summary>
		KHOJ = 0x142,

		/// <summary>Khitan Large Script ISO 15924 Code.</summary>
		KITL = 0x1F9,

		/// <summary>Khitan Small Script ISO 15924 Code.</summary>
		KITS = 0x120,

		/// <summary>Kannada ISO 15924 Code.</summary>
		KNDA = 0x159,

		/// <summary>Korean ISO 15924 Code.</summary>
		KORE = 0x11F,

		/// <summary>Kpelle ISO 15924 Code.</summary>
		KPEL = 0x1B4,

		/// <summary>Kaithi ISO 15924 Code.</summary>
		KTHI = 0x13D,

		/// <summary>Tai Tham ISO 15924 Code.</summary>
		LANA = 0x15F,

		/// <summary>Lao ISO 15924 Code.</summary>
		LAOO = 0x164,

		/// <summary>Latin Fraktur ISO 15924 Code.</summary>
		LATF = 0x0D9,

		/// <summary>Latin Gaelic ISO 15924 Code.</summary>
		LATG = 0x0D8,

		/// <summary>Latin ISO 15924 Code.</summary>
		LATN = 0x0D7,

		/// <summary>Leke ISO 15924 Code.</summary>
		LEKE = 0x16C,

		/// <summary>Lepcha ISO 15924 Code.</summary>
		LEPC = 0x14F,

		/// <summary>Limbu ISO 15924 Code.</summary>
		LIMB = 0x150,

		/// <summary>Linear A ISO 15924 Code.</summary>
		LINA = 0x190,

		/// <summary>Linear B ISO 15924 Code.</summary>
		LINB = 0x191,

		/// <summary>Lisu Fraser ISO 15924 Code.</summary>
		LISU = 0x18F,

		/// <summary>Loma ISO 15924 Code.</summary>
		LOMA = 0x1B5,

		/// <summary>Lycian ISO 15924 Code.</summary>
		LYCI = 0x0CA,

		/// <summary>Lydian ISO 15924 Code.</summary>
		LYDI = 0x074,

		/// <summary>Mahajani ISO 15924 Code.</summary>
		MAHJ = 0x13A,

		/// <summary>Makasar ISO 15924 Code.</summary>
		MAKA = 0x16E,

		/// <summary>Mandaic ISO 15924 Code.</summary>
		MAND = 0x08C,

		/// <summary>Manichaean ISO 15924 Code.</summary>
		MANI = 0x08B,

		/// <summary>Marchen ISO 15924 Code.</summary>
		MARC = 0x14C,

		/// <summary>Mayan Hieroglyphs ISO 15924 Code.</summary>
		MAYA = 0x05A,

		/// <summary>Medefaidrin ISO 15924 Code.</summary>
		MEDF = 0x109,

		/// <summary>Mende Kikakui ISO 15924 Code.</summary>
		MEND = 0x1B6,

		/// <summary>Meroitic Cursive ISO 15924 Code.</summary>
		MERC = 0x065,

		/// <summary>Meroitic Hieroglyphs ISO 15924 Code.</summary>
		MERO = 0x064,

		/// <summary>Malayalam ISO 15924 Code.</summary>
		MLYM = 0x15B,

		/// <summary>Modi, Moḍī ISO 15924 Code.</summary>
		MODI = 0x144,

		/// <summary>Mongolian ISO 15924 Code.</summary>
		MONG = 0x091,

		/// <summary>Moon ISO 15924 Code.</summary>
		MOON = 0x0DA,

		/// <summary>Mro ISO 15924 Code.</summary>
		MROO = 0x108,

		/// <summary>Meitei Mayek ISO 15924 Code.</summary>
		MTEI = 0x151,

		/// <summary>Multani ISO 15924 Code.</summary>
		MULT = 0x143,

		/// <summary>Myanmar ISO 15924 Code.</summary>
		MYMR = 0x15E,

		/// <summary>Nandinagari ISO 15924 Code.</summary>
		NAND = 0x137,

		/// <summary>Old North Arabian ISO 15924 Code.</summary>
		NARB = 0x06A,

		/// <summary>Nabataean ISO 15924 Code.</summary>
		NBAT = 0x09F,

		/// <summary>Newa ISO 15924 Code.</summary>
		NEWA = 0x14D,

		/// <summary>Naxi Dongba ISO 15924 Code.</summary>
		NKDB = 0x055,

		/// <summary>Nakhi Geba ISO 15924 Code.</summary>
		NKGB = 0x1A4,

		/// <summary>N’Ko ISO 15924 Code.</summary>
		NKOO = 0x0A5,

		/// <summary>Nüshu ISO 15924 Code.</summary>
		NSHU = 0x1F3,

		/// <summary>Ogham ISO 15924 Code.</summary>
		OGAM = 0x0D4,

		/// <summary>Ol Chiki ISO 15924 Code.</summary>
		OLCK = 0x105,

		/// <summary>Old Turkic ISO 15924 Code.</summary>
		ORKH = 0x0AF,

		/// <summary>Oriya ISO 15924 Code.</summary>
		ORYA = 0x147,

		/// <summary>Osage ISO 15924 Code.</summary>
		OSGE = 0x0DB,

		/// <summary>Osmanya ISO 15924 Code.</summary>
		OSMA = 0x104,

		/// <summary>Palmyrene ISO 15924 Code.</summary>
		PALM = 0x07E,

		/// <summary>Pau Cin Hau ISO 15924 Code.</summary>
		PAUC = 0x107,

		/// <summary>Old Permic ISO 15924 Code.</summary>
		PERM = 0x0E3,

		/// <summary>Phags-pa ISO 15924 Code.</summary>
		PHAG = 0x14B,

		/// <summary>Inscriptional Pahlavi ISO 15924 Code.</summary>
		PHLI = 0x083,

		/// <summary>Psalter Pahlavi ISO 15924 Code.</summary>
		PHLP = 0x084,

		/// <summary>Book Pahlavi ISO 15924 Code.</summary>
		PHLV = 0x085,

		/// <summary>Phoenician ISO 15924 Code.</summary>
		PHNX = 0x073,

		/// <summary>Klingon ISO 15924 Code.</summary>
		PIQD = 0x125,

		/// <summary>Miao ISO 15924 Code.</summary>
		PLRD = 0x11A,

		/// <summary>Inscriptional Parthian ISO 15924 Code.</summary>
		PRTI = 0x082,

		/// <summary>Reserved for Private Use ISO 15924 Code.</summary>
		QAAA = 0x384,

		/// <summary>Private Use ISO 15924 Code.</summary>
		QAAI = 0x38C,

		/// <summary>Reserved for Private Use ISO 15924 Code.</summary>
		QABX = 0x3B5,

		/// <summary>Rejang ISO 15924 Code.</summary>
		RJNG = 0x16B,

		/// <summary>Hanifi Rohingya ISO 15924 Code.</summary>
		ROHG = 0x0A7,

		/// <summary>Rongorongo ISO 15924 Code.</summary>
		RORO = 0x26C,

		/// <summary>Runic ISO 15924 Code.</summary>
		RUNR = 0x0D3,

		/// <summary>Samaritan ISO 15924 Code.</summary>
		SAMR = 0x07B,

		/// <summary>Sarati ISO 15924 Code.</summary>
		SARA = 0x124,

		/// <summary>Old South Arabian ISO 15924 Code.</summary>
		SARB = 0x069,

		/// <summary>Saurashtra ISO 15924 Code.</summary>
		SAUR = 0x158,

		/// <summary>SignWriting ISO 15924 Code.</summary>
		SGNW = 0x05F,

		/// <summary>Shavian ISO 15924 Code.</summary>
		SHAW = 0x119,

		/// <summary>Sharada ISO 15924 Code.</summary>
		SHRD = 0x13F,

		/// <summary>Shuishu ISO 15924 Code.</summary>
		SHUI = 0x212,

		/// <summary>Siddham ISO 15924 Code.</summary>
		SIDD = 0x12E,

		/// <summary>Khudawadi ISO 15924 Code.</summary>
		SIND = 0x13E,

		/// <summary>Sinhala ISO 15924 Code.</summary>
		SINH = 0x15C,

		/// <summary>Sogdian ISO 15924 Code.</summary>
		SOGD = 0x08D,

		/// <summary>Old Sogdian ISO 15924 Code.</summary>
		SOGO = 0x08E,

		/// <summary>Sora Sompeng ISO 15924 Code.</summary>
		SORA = 0x18E,

		/// <summary>Soyombo ISO 15924 Code.</summary>
		SOYO = 0x149,

		/// <summary>Sundanese ISO 15924 Code.</summary>
		SUND = 0x16A,

		/// <summary>Syloti Nagri ISO 15924 Code.</summary>
		SYLO = 0x13C,

		/// <summary>Syriac ISO 15924 Code.</summary>
		SYRC = 0x087,

		/// <summary>Syriac Estrangelo ISO 15924 Code.</summary>
		SYRE = 0x08A,

		/// <summary>Syriac Western ISO 15924 Code.</summary>
		SYRJ = 0x089,

		/// <summary>Syriac Eastern ISO 15924 Code.</summary>
		SYRN = 0x088,

		/// <summary>Tagbanwa ISO 15924 Code.</summary>
		TAGB = 0x175,

		/// <summary>Takri ISO 15924 Code.</summary>
		TAKR = 0x141,

		/// <summary>Tai Le ISO 15924 Code.</summary>
		TALE = 0x161,

		/// <summary>New Tai Lue ISO 15924 Code.</summary>
		TALU = 0x162,

		/// <summary>Tamil ISO 15924 Code.</summary>
		TAML = 0x15A,

		/// <summary>Tangut ISO 15924 Code.</summary>
		TANG = 0x208,

		/// <summary>Tai Viet ISO 15924 Code.</summary>
		TAVT = 0x167,

		/// <summary>Telugu ISO 15924 Code.</summary>
		TELU = 0x154,

		/// <summary>Tengwar ISO 15924 Code.</summary>
		TENG = 0x122,

		/// <summary>Tifinagh ISO 15924 Code.</summary>
		TFNG = 0x078,

		/// <summary>Tagalog ISO 15924 Code.</summary>
		TGLG = 0x172,

		/// <summary>Thaana ISO 15924 Code.</summary>
		THAA = 0x0AA,

		/// <summary>Thai ISO 15924 Code.</summary>
		THAI = 0x160,

		/// <summary>Tibetan ISO 15924 Code.</summary>
		TIBT = 0x14A,

		/// <summary>Tirhuta ISO 15924 Code.</summary>
		TIRH = 0x146,

		/// <summary>Toto ISO 15924 Code.</summary>
		TOTO = 0x126,

		/// <summary>Ugaritic ISO 15924 Code.</summary>
		UGAR = 0x028,

		/// <summary>Vai ISO 15924 Code.</summary>
		VAII = 0x1D6,

		/// <summary>Visible Speech ISO 15924 Code.</summary>
		VISP = 0x118,

		/// <summary>Warang Citi ISO 15924 Code.</summary>
		WARA = 0x106,

		/// <summary>Wancho ISO 15924 Code.</summary>
		WCHO = 0x11B,

		/// <summary>Woleai ISO 15924 Code.</summary>
		WOLE = 0x1E0,

		/// <summary>Old Persian ISO 15924 Code.</summary>
		XPEO = 0x01E,

		/// <summary>Cuneiform ISO 15924 Code.</summary>
		XSUX = 0x014,

		/// <summary>Yezidi ISO 15924 Code.</summary>
		YEZI = 0x0C0,

		/// <summary>Yi ISO 15924 Code.</summary>
		YIII = 0x1CC,

		/// <summary>Zanabazar Square ISO 15924 Code.</summary>
		ZANB = 0x153,

		/// <summary>Code for Inherited Script ISO 15924 Code.</summary>
		ZINH = 0x3E2,

		/// <summary>Mathematical Notation ISO 15924 Code.</summary>
		ZMTH = 0x3E3,

		/// <summary>Symbols Emoji ISO 15924 Code.</summary>
		ZSYE = 0x3E1,

		/// <summary>Symbols ISO 15924 Code.</summary>
		ZSYM = 0x3E4,

		/// <summary>Code for Unwritten Documents ISO 15924 Code.</summary>
		ZXXX = 0x3E5,

		/// <summary>Code for Undetermined Script ISO 15924 Code.</summary>
		ZYYY = 0x3E6,

		/// <summary>Code for Uncoded Script ISO 15924 Code.</summary>
		ZZZZ = 0x3E7,
	}
}