using System.Text;
using System.IO;
using UnityEngine.UI;

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


        #region Extensions

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

        /// <summary> Updates Unity Text Component Text. </summary>
        /// <param name="textLabel"></param>
        /// <param name="text"></param>
        public static void UpdateText (this Text textLabel, params string[] text)
        {
            textLabel.text = Concat (text);
        }

        #endregion
    }
}