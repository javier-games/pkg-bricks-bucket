using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        /// <returns>The format.</returns>
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



        #region Rich Text

        /// <summary> Constant formats for rich text. </summary>
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



        #region RegularExpressions;

        /// <summary> Constant patterns for regex. </summary>
        public static class RegexPatterns
        {
            /// <summary> Pattern for Camel Case. </summary>
            public const string ToCamelCasePattern =
                "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))";

            /// <summary> Pattern from Camel Case. </summary>
            public const string FromCamelCasePatternFirstStep =
                @"(\P{Ll})(\P{Ll}\p{Ll})";

            /// <summary> Pattern from Camel Case. </summary>
            public const string FromCamelCasePatternSecondStep =
                @"(\p{Ll})(\P{Ll})";

            /// <summary> Pattern for element identifier. </summary>
            public const string ElementIdentifier =
                @"^[_a-zA-Z][_a-zA-Z0-9]*(\[[0-9]*\])+$";

            /// <summary> Pattern for element index. </summary>
            public const string ElementIndex =
                @"^(\[[0-9]*\])+$";

            /// <summary> Pattern for member identifier. </summary>
            public const string MemberIdentifier =
                @"^[_a-zA-Z][_a-zA-Z0-9]*$";

            /// <summary> Replacement for one. </summary>
            public const string Replacement1 = "$1 ";

            /// <summary> Replacement for two. </summary>
            public const string Replacement2 = "$1 $2";
        }

        /// <summary>
        /// Converts a string from camel case.
        /// </summary>
        /// <param name="toConvert">String to convert.</param>
        /// <returns>String from camel case.</returns>
        public static string FromCamelCase (this string toConvert)
        {
            if (string.IsNullOrEmpty (toConvert))
                return toConvert;

            string camelCase = Regex.Replace (
                input: Regex.Replace (
                    input: toConvert,
                    pattern: RegexPatterns.FromCamelCasePatternFirstStep,
                    replacement: RegexPatterns.Replacement2
                ),
                pattern: RegexPatterns.FromCamelCasePatternSecondStep,
                replacement: RegexPatterns.Replacement2
            );

            string firstLetter = camelCase.Substring (0, 1).ToUpper ();

            if (toConvert.Length <= 1)
                return firstLetter;
            
            string rest = camelCase.Substring (1);
            return firstLetter + rest;
        }

		/// <summary>
        /// Converts a string to camel case.
        /// </summary>
		/// <param name="toConvert">String to convert.</param>
		/// <returns>Camel case string.</returns>
		public static string ToCamelCase (this string toConvert) =>
            Regex.Replace (
                input: toConvert,
                pattern: RegexPatterns.ToCamelCasePattern,
                replacement: RegexPatterns.Replacement1
            ).Trim ();

		/// <summary>
        /// Whether this is an element identifier.
        /// </summary>
		/// <param name="toValidate">String to validate.</param>
		/// <returns>Whether this is an element identifier or not.</returns>
		public static bool IsElementIdentifier (this string toValidate) =>
            Regex.IsMatch (toValidate, RegexPatterns.ElementIdentifier);

		/// <summary>
        /// Whether this is an element index.
        /// </summary>
		/// <param name="toValidate">String to validate.</param>
		/// <returns>Whether this is an element index or not.</returns>
		public static bool IsElementIndex (this string toValidate) =>
            Regex.IsMatch (toValidate, RegexPatterns.ElementIndex);

		/// <summary>
        /// Whether this is a member identifier.
        /// </summary>
		/// <param name="toValidate">String to validate.</param>
		/// <returns>Whether this is a member identifier or not.</returns>
		public static bool IsMemberIdentifier (this string toValidate) =>
            Regex.IsMatch (toValidate, RegexPatterns.MemberIdentifier);

        #endregion



        #region Collections

        /// <summary>
        /// Parse value path.
        /// </summary>
        /// <param name="path">Path to parse.</param>
        public static IEnumerable<object> ParseValuePath (string path)
        {
            var keys = path.Split ('.');
            foreach (var key in keys)
            {
                //  For element identifier.
                if (key.IsElementIdentifier ())
                {
                    var subKeys = key.Split ('[', ']');
                    yield return subKeys[0];
                    foreach (var subKey in subKeys.Skip (1))
                    {
                        if (string.IsNullOrEmpty (subKey))
                            continue;

                        int index = int.Parse (subKey);
                        yield return index;
                    }

                    //  Continue the key iteration.
                    continue;
                }

                //  For element index.
                if (key.IsElementIndex ())
                {
                    var subKeys = key.Split ('[', ']');
                    foreach (var subKey in subKeys)
                    {
                        if (string.IsNullOrEmpty (subKey))
                            continue;

                        int index = int.Parse (subKey);
                        yield return index;
                    }

                    //  Continue the key iteration.
                    continue;
                }

                //  For member identifier.
                if (key.IsMemberIdentifier ())
                {
                    yield return key;

                    //  Continue the key iteration.
                    continue;
                }

                //  Else Exception.
                throw new System.Exception (
                    ConcatFormat ( "Invalid path: {0}", path)
                );
            }
        }

        #endregion
    }
}
