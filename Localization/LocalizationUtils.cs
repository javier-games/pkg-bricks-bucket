using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
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
    /// </summary>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    public static class LocalizationUtils
    {



        #region Convertion Methods

        /// <summary>
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="country">Country reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID (ISO639.Alpha1 language, ISO3166.Alpha2 country)
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
        /// Converts ISO639 Language and ISO3166 Country to Language Code ID.
        /// </summary>
        /// <param name="language">Language to convert.</param>
        /// <param name="country">Country reference.</param>
        /// <returns>Language Code Identifier.</returns>
        public static LCID ToLCID (int language, int country) =>
            ToLCID ((ISO639.Alpha1) language, (ISO3166.Alpha2) country);

        /// <summary>
        /// Converts LCID to ISO-639 Language numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
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
        /// Converts LCID to ISO-3166 country numeric code.
        /// </summary>
        /// <param name="lcid">Language code identifier.</param>
        /// <returns>Numeric code.</returns>
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
        /// Converts a regular string to a code formatted string.
        /// </summary>
        /// <param name="unformattedCode">string to convert.</param>
        /// <returns>Formatted string.</returns>
        public static string ToCodeFormat (this string unformattedCode)
        {
            if (string.IsNullOrWhiteSpace (unformattedCode))
                return string.Empty;

            return unformattedCode.RemoveDiacritics ().
                ToUpper ().
                Replace (' ', '_').
                RemoveSpecialCharacters ('_');
        }

        #endregion



        #region Display Names

        #endregion
    }


    #region Exceptions

    /// <summary>
    /// 
    /// Localization Not Found Exception
    ///
    /// <para>
    /// Exception class for localizations not found.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    internal class LocalizationNotFoundException : System.Exception
    {
        public LocalizationNotFoundException (string message) :
            base (message) { }
    }

    #endregion
}