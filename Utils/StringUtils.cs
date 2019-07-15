using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BricksBucket.Utils
{
    /// <summary>
    /// 
    /// StringUtils.
    /// 
    /// <para>
    /// Usefull utilities to work with strings.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2019 </para>
    /// </summary>
    public static class StringUtils
    {

        #region Concatenation

        /// <summary> Concatenate the specified strings. </summary>
        /// <returns>The concatenated string.</returns>
        /// <param name="array">String array.</param>
        public static string Concat (params object[] array)
        {

            /*
             * Encapsulation of string builder to not interact with other
             * code and avoid unusal behaviour.        
            */

            StringBuilder stringBuilder = new StringBuilder ();
            for (int i = 0; i < array.Length; i++)
                stringBuilder.Append (array[i]);
            return stringBuilder.ToString ();
        }

        /// <summary> Concatenates with the format. </summary>
        /// <returns>The format.</returns>
        /// <param name="format">Format.</param>
        /// <param name="array">Array.</param>
        public static string ConcatFormat (string format, params object[] array)
        {

            /*
             * Encapsulation of string builder to not interact with other
             * code and avoid unusal behaviour.        
            */

            StringBuilder stringBuilder = new StringBuilder ();
            stringBuilder.AppendFormat (format, array);
            return stringBuilder.ToString ();
        }

        #endregion



        #region System.IO

        /// <summary> Generates an stream from string. </summary>
        /// <returns>The stream from string.</returns>
        /// <param name="stringToConvert">String to convert.</param>
        public static Stream ToStream (this string stringToConvert)
        {
            MemoryStream stream = new MemoryStream ();
            StreamWriter writer = new StreamWriter (stream);
            writer.Write (stringToConvert);
            writer.Flush ();
            stream.Position = 0;
            return stream;
        }

        #endregion



        #region UI

        /// <summary> Constant formats for rich text. </summary>
        public static class RichTextFormat
        {
            /// <summary> Color Format. </summary>
            public const string Color = "<color={0}>{1}</color>";
            /// <summary> Size Format. </summary>
            public const string Size = "<size={0}>{1}</size>";
            /// <summary> Bold Format. </summary>
            public const string Bold = "<b>{0}</b>";
            /// <summary> Italic Format. </summary>
            public const string Italic = "<i>{0}</i>";
        }

        /// <summary> Add RichText Color Tags to the message. </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <returns> Formated RichText string. </returns>
        public static string RichTextColor (this string message, Color color)
        {
            return string.Format (RichTextFormat.Color, color.HEX (), message);
        }

        /// <summary> Add RichText Color Tags to the message. </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <returns> Formated RichText string. </returns>
        public static string RichTextColor (this string message, string color)
        {
            return string.Format (RichTextFormat.Color, color, message);
        }

        /// <summary> Add RichText Size Tags to the message. </summary>
        /// <param name="message"></param>
        /// <param name="size"></param>
        /// <returns> Formated RichText string. </returns>
        public static string RichTextSize (this string message, int size)
        {
            return string.Format (RichTextFormat.Size, size, message);
        }

        /// <summary> Add RichText Bold Tags to the message. </summary>
        /// <param name="message"></param>
        /// <returns> Formated RichText string. </returns>
        public static string RichTextBold (this string message)
        {
            return string.Format (RichTextFormat.Bold, message);
        }

        /// <summary> Add RichText Italics Tags to the message. </summary>
        /// <param name="message"></param>
        /// <returns> Formated RichText string. </returns>
        public static string RichTextItalics (this string message)
        {
            return string.Format (RichTextFormat.Italic, message);
        }

        #endregion



        #region RegularExpressions;

        /// <summary> Converts a string to camel case. </summary>
        /// <param name="stringToConvert"></param>
        /// <returns> Camel case string. </returns>
        public static string ToCamelCase (this string stringToConvert)
        {
            return Regex.Replace (
                input: stringToConvert,
                pattern: "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))",
                replacement: "$1 "
            ).Trim ();
        }

        /// <summary> Converts a string from camel case. </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public static string FromCamelCase (this string stringToConvert)
        {
            if (string.IsNullOrWhiteSpace (stringToConvert))
                return stringToConvert;

            string camelCase = Regex.Replace (
                input: Regex.Replace (
                    input: stringToConvert,
                    pattern: @"(\P{Ll})(\P{Ll}\p{Ll})",
                    replacement: "$1 $2"
                ),
                pattern: @"(\p{Ll})(\P{Ll})",
                replacement: "$1 $2"
            );

            string firstLetter = camelCase.Substring (0, 1).ToUpper ();

            if (stringToConvert.Length > 1)
            {
                string rest = camelCase.Substring (1);
                return firstLetter + rest;
            }
            return firstLetter;
        }

        #endregion
    }
}