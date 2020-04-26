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
            ISO639.Alpha1 language, ISO3166.Alpha2 country
        )
        {
            if (language == ISO639.Alpha1.NONE) return LCID.INVARIANT;

            var textCode = language.ToString ();

            if (country != ISO3166.Alpha2.NONE)
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
            ToLCID ((ISO639.Alpha1) language, (ISO3166.Alpha2) country);

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
            if (System.Enum.TryParse (language, out ISO639.Alpha1 iso))
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
            if (System.Enum.TryParse (country, out ISO3166.Alpha2 iso))
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
                RemoveSpecialCharacters ('_');
        }

        #endregion
    }
}