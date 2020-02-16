using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;



namespace BricksBucket
{
    /// <summary>
    ///
    /// StringUtils.
    ///
    /// <para>
    /// Useful utilities to work with strings.
    /// </para>
    ///
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class StringUtils
    {

        #region Concatenation

        /// <summary>
        /// Concatenate the specified strings.
        /// </summary>
        /// <returns>The concatenated string.</returns>
        /// <param name="array">String array.</param>
        public static string Concat (params object[] array)
        {

            /*
             * Encapsulation of string builder to not interact with other
             * code and avoid unusual behaviour.
            */

            var stringBuilder = new StringBuilder ();
            for (int i = 0; i < array.Length; i++)
                stringBuilder.Append (array[i]);
            return stringBuilder.ToString ();
        }

        /// <summary>
        /// Concatenates with the format.
        /// </summary>
        /// <returns>The formatted concatenation.</returns>
        /// <param name="format">Format.</param>
        /// <param name="array">Array.</param>
        public static string ConcatFormat (string format, params object[] array)
        {

            /*
             * Encapsulation of string builder to not interact with other
             * code and avoid unusual behaviour.
            */

            var stringBuilder = new StringBuilder ();
            stringBuilder.AppendFormat (format, array);
            return stringBuilder.ToString ();
        }

        #endregion



        #region System.IO

        /// <summary>
        /// Generates an stream from string.
        /// </summary>
        /// <returns>The stream from string.</returns>
        /// <param name="toConvert">String to convert.</param>
        public static Stream ToStream (this string toConvert)
        {
            var stream = new MemoryStream ();
            var writer = new StreamWriter (stream);
            writer.Write (toConvert);
            writer.Flush ();
            stream.Position = 0;
            return stream;
        }

        #endregion



        #region Unity Rich Text

        /// <summary> Constant formats for Unity rich text. </summary>
        public static class RichTextFormat
        {
            /// <summary> Color Format. </summary>
            public const string Color = "<color={1}>{0}</color>";
            /// <summary> Size Format. </summary>
            public const string Size = "<size={1}>{0}</size>";
            /// <summary> Bold Format. </summary>
            public const string Bold = "<b>{0}</b>";
            /// <summary> Italic Format. </summary>
            public const string Italic = "<i>{0}</i>";
        }

        /// <summary>
        /// Add RichText Color Tags to the message.
        /// </summary>
        /// <param name="message">Message to format.</param>
        /// <param name="color">Color to use.</param>
        /// <returns>Formatted RichText string.</returns>
        public static string RichTextColor(this string message, Color color) =>
            ConcatFormat(RichTextFormat.Color, message, color.HEX());

        /// <summary>
        /// Add RichText Size Tags to the message.
        /// </summary>
		/// <param name="message">Message to format.</param>
		/// <param name="size">Size to use.</param>
		/// <returns>Formatted RichText string.</returns>
		public static string RichTextSize (this string message, int size) =>
            ConcatFormat (RichTextFormat.Size, message, size);

		/// <summary>
        /// Add RichText Bold Tags to the message.
        /// </summary>
		/// <param name="message">Message to format.</param>
		/// <returns>Formatted RichText string.</returns>
		public static string RichTextBold (this string message) =>
            ConcatFormat (RichTextFormat.Bold, message);

		/// <summary>
        /// Add RichText Italics Tags to the message.
        /// </summary>
		/// <param name="message">Message to format.</param>
		/// <returns>Formatted RichText string.</returns>
		public static string RichTextItalics (this string message) =>
            ConcatFormat (RichTextFormat.Italic, message);

        #endregion



        #region Regular Expressions;

        /// <summary> Regular Expressions Predefined Patterns. </summary>
        public static class RegexPattern
        {
            /// <summary>
            /// Pattern to identify an upper camel case string.
            /// </summary>
            public const string UpperCamelCase =
                @"\b[A-Z][a-z|0-9]*([A-Z][a-z|0-9]*)*\b";

            /// <summary>
            /// Pattern to identify a lower camel case. string.
            /// </summary>
            public const string LowerCamelCase =
                @"\b[a-z][a-z|0-9]*([A-Z][a-z|0-9]*)*\b";

            /// <summary>
            /// Pattern to identify an element identifier string.
            /// </summary>
            public const string ElementIdentifier =
                @"^[_a-zA-Z][_a-zA-Z0-9]*(\[[0-9]*\])+$";

            /// <summary>
            /// Pattern to identify an element index string.
            /// </summary>
            public const string ElementIndex = 
                @"^(\[[0-9]*\])+$";

            /// <summary>
            /// Pattern to identify a member identifier string.
            /// </summary>
            public const string MemberIdentifier = 
                @"^[_a-zA-Z][_a-zA-Z0-9]*$";

            /// <summary>
            /// Editable pattern to identify special characters.
            /// Format this string to add tolerance for special characters.
            /// </summary>
            public const string SpecialCharacters =
                "[^0-9a-zA-Z{0}]+";
        }

        /// <summary>
        /// Whether this string is lower camel case.
        /// </summary>
        /// <param name="text">Text to analyze.</param>
        /// <returns>Whether this string is lower camel case.</returns>
        public static bool IsLowerCamelCase (this string text) =>
            Regex.IsMatch (text, RegexPattern.LowerCamelCase);

        /// <summary>
        /// Whether this string is lower camel case.
        /// </summary>
        /// <param name="text">Text to analyze.</param>
        /// <returns>Whether this string is upper camel case.</returns>
        public static bool IsUpperCamelCase (this string text) =>
            Regex.IsMatch (text, RegexPattern.UpperCamelCase);
        
        /// <summary>
        /// Whether this is an element identifier.
        /// </summary>
		/// <param name="text">String to validate.</param>
		/// <returns>Whether this is an element identifier or not.</returns>
		public static bool IsElementIdentifier (this string text) =>
            Regex.IsMatch (text, RegexPattern.ElementIdentifier);

		/// <summary>
        /// Whether this is an element index.
        /// </summary>
		/// <param name="text">String to validate.</param>
		/// <returns>Whether this is an element index or not.</returns>
		public static bool IsElementIndex (this string text) =>
            Regex.IsMatch (text, RegexPattern.ElementIndex);

		/// <summary>
        /// Whether this is a member identifier.
        /// </summary>
		/// <param name="text">String to validate.</param>
		/// <returns>Whether this is a member identifier or not.</returns>
		public static bool IsMemberIdentifier (this string text) =>
            Regex.IsMatch (text, RegexPattern.MemberIdentifier);

        /// <summary>
        /// Removes Special Characters from the string maintaining only
        /// characters from A - Z, Digits, and exceptions.
        /// </summary>
        /// <param name="text">Any string text.</param>
        /// <param name="exceptions">Any special character to maintain.</param>
        /// <returns>Text with out special characters.</returns>
        public static string RemoveSpecialCharacters (
            this string text, params char[] exceptions
        )
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < exceptions.Length; i++)
                stringBuilder.Append (exceptions[i]);
            
            var pattern = ConcatFormat (
                RegexPattern.SpecialCharacters,
                stringBuilder.ToString()
            );
            
            return Regex.Replace (
                input: text,
                pattern,
                replacement:string.Empty
            );
        }

        /// <summary>
        /// Removes Districts for a regular text.
        /// Based on the following stack overflow thread.
        /// https://stackoverflow.com/questions/249087
        /// </summary>
        /// <param name="text">Any string text.</param>
        /// <returns>Text with out diacritics.</returns>
        public static string RemoveDiacritics(this string text) 
        {
            var normalizedText = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedText.Length; i++)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(
                    normalizedText[i]
                );
                
                if (category != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append (normalizedText[i]);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        #endregion

    }
}
