using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Generics;
using BricksBucket.Localization.Internal;
using Sirenix.OdinInspector;


// ReSharper disable TailRecursiveCall
namespace BricksBucket.Localization
{
    using ScriptableSingleton = ScriptableSingleton<LocalizationSettings>;

    /// <summary>
    /// 
    /// LocalizedObject Settings
    ///
    /// <para>
    /// Settings to set up the localizedObject in the project.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    [CreateAssetMenu (fileName = "Localization Settings",
        menuName = "Bricks Bucket/Localization Settings")]
    public class LocalizationSettings : ScriptableSingleton
    {



        #region Fields

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        [SerializeField]
        [Tooltip ("Name of the LocalizedObject Project.")]
        private string _projectName;

        /// <summary>
        /// Whether to use default culture for default values.
        /// </summary>
        [Tooltip ("Whether to use default culture for default values.")]
        [SerializeField]
        private bool _useDefaultCulture = true;

        /// <summary>
        /// Collection of categories of languages.
        /// </summary>
        [SerializeField, ReadOnly]
        [ListDrawerSettings (IsReadOnly = true, NumberOfItemsPerPage = 4)]
        [Tooltip ("Collection of cultures.")]
        private List<Culture> _cultures;

        /// <summary>
        /// Dictionary of books by codes.
        /// </summary>
        [SerializeField, ReadOnly]
        [DictionaryDrawerSettings (KeyLabel = "Code", ValueLabel = "Book",
            IsReadOnly = true)]
        [Tooltip ("Collection of books")]
        private BooksDictionary _books;

        #endregion



        #region Properties

        /// <summary>
        /// List of cultures categories.
        /// </summary>
        internal List<Culture> CulturesList =>
            _cultures ??
            (_cultures = new List<Culture> ());

        /// <summary>
        /// Dictionary of books.
        /// </summary>
        internal BooksDictionary BooksDictionary =>
            _books ?? (_books = new BooksDictionary ());

        #endregion



        #region Static Properties

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        public static string ProjectName => Instance._projectName;

        /// <summary>
        /// Whether to use default culture for cultures not found.
        /// </summary>
        public static bool UseDefaultCulture => Instance._useDefaultCulture;

        /// <summary>
        /// Default Language.
        /// </summary>
        public static Culture DefaultCulture =>
            Instance.CulturesList.Count > 0 ? Cultures[0] : default;

        /// <summary>
        /// Returns the array of cultures.
        /// </summary>
        public static Culture[] Cultures => Instance._cultures.ToArray ();

        /// <summary>
        /// Collection of display names of languages categories.
        /// </summary>
        public static string[] CulturesNames
        {
            get
            {
                var cultures = Cultures;
                var culturesNames = new string[cultures.Length];
                for (int i = 0; i < cultures.Length; i++)
                    culturesNames[i] = Cultures[i].Name;
                return culturesNames;
            }
        }

        /// <summary>
        /// Collection of codes of languages categories.
        /// </summary>
        public static string[] CulturesCodes
        {
            get
            {
                var cultures = Cultures;
                var culturesCodes = new string[cultures.Length];
                for (int i = 0; i < cultures.Length; i++)
                    culturesCodes[i] = Cultures[i].Code;
                return culturesCodes;
            }
        }

        /// <summary>
        /// Collection of books.
        /// </summary>
        public static Book[] Books => Instance._books.Values.ToArray ();

        /// <summary>
        /// Name of books.
        /// </summary>
        public static string[] BooksNames
        {
            get
            {
                var books = Books;
                var booksNames = new string[Books.Length];
                for (int i = 0; i < books.Length; i++)
                    booksNames[i] = books[i].Name;
                return booksNames;
            }
        }

        /// <summary>
        /// Code of books.
        /// </summary>
        public static string[] BooksCodes
        {
            get
            {
                var books = Books;
                var booksCodes = new string[Books.Length];
                for (int i = 0; i < books.Length; i++)
                    booksCodes[i] = books[i].Code;
                return booksCodes;
            }
        }

        #endregion



        #region Static Methods

        /// <summary>
        /// Gets the text localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization text in the specified location.</returns>
        public static string GetText (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.TextGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var text = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                string.IsNullOrWhiteSpace (text) &&
                UseDefaultCulture;

            return returnDefault
                ? GetText (book, code, DefaultCulture.Code)
                : text;
        }

        /// <summary>
        /// Gets the texture localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization texture in the specified location.</returns>
        public static Texture GetTexture (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.TextureGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var texture = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                texture == null &&
                UseDefaultCulture;

            return returnDefault
                ? GetTexture (book, code, DefaultCulture.Code)
                : texture;
        }

        /// <summary>
        /// Gets the sprite localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization sprite in the specified location.</returns>
        public static Sprite GetSprite (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.SpriteGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var sprite = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                sprite == null &&
                UseDefaultCulture;

            return returnDefault
                ? GetSprite (book, code, DefaultCulture.Code)
                : sprite;
        }

        /// <summary>
        /// Gets the audio localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization audio in the specified location.</returns>
        public static AudioClip GetAudio (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.AudioGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var audioClip = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                audioClip == null &&
                UseDefaultCulture;

            return returnDefault
                ? GetAudio (book, code, DefaultCulture.Code)
                : audioClip;
        }

        /// <summary>
        /// Gets the video localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization video in the specified location.</returns>
        public static VideoClip GetVideo (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.VideoGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var videoClip = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                videoClip == null &&
                UseDefaultCulture;

            return returnDefault
                ? GetVideo (book, code, DefaultCulture.Code)
                : videoClip;
        }

        /// <summary>
        /// Gets the object localization.
        /// </summary>
        /// <param name="book">Code of book.</param>
        /// <param name="code">Code of the localization.</param>
        /// <param name="culture">Culture of the localization.</param>
        /// <returns>Localization object in the specified location.</returns>
        public static Object GetObject (string book, string code, string culture)
        {
            if (!Instance.BooksDictionary.ContainsKey (book))
                throw Exception ("Book " + book + " not found.");

            var bookObject = Instance.BooksDictionary[book];
            if (bookObject == null)
                throw Exception ("Book " + book + " is null");

            var group = bookObject.UnityObjectGroup;
            if (!group.ContainsLocalizedObject (code))
                throw Exception ("Localized object " + code + " not found.");

            var localizedObject = group[code];
            if (localizedObject == null)
                throw Exception ("Localized object " + code + " is null.");

            if (!localizedObject.ContainsCulture (culture))
                throw Exception ("Culture " + culture + " not found.");

            var o = localizedObject[culture];

            var returnDefault =
                culture != DefaultCulture.Code &&
                o == null &&
                UseDefaultCulture;

            return returnDefault
                ? GetObject (book, code, DefaultCulture.Code)
                : o;
        }

        /// <summary>
        /// Evaluates if the language code is in settings.
        /// </summary>
        /// <param name="code">Language code to evaluate.</param>
        /// <returns>Whether the language code is in settings.</returns>
        public static bool ContainsCulture (string code) =>
            Instance.CulturesList.Exists (culture => culture.Code == code);

        /// <summary>
        /// Evaluates if the dictionary of books contains the key code.
        /// </summary>
        /// <param name="code">Code to look for.</param>
        /// <returns>Whether the dictionary contains the book code.</returns>
        public static bool ContainsBook (string code) =>
            Instance.BooksDictionary.ContainsKey (code);

        /// <summary>
        /// Adds a new book by code.
        /// </summary>
        /// <param name="code">Code where to add book.</param>
        /// <param name="book">Book to add.</param>
        internal static void AddBook (string code, Book book) =>
            Instance.BooksDictionary.Add (code, book);

        /// <summary>
        /// Removes a book by code.
        /// </summary>
        /// <param name="code">Code of the book to remove.</param>
        internal static void RemoveBook (string code) =>
            Instance.BooksDictionary.Remove (code);

        /// <summary>
        /// Returns an exception for a localization not found.
        /// </summary>
        /// <param name="message">Message to display on console.</param>
        /// <returns>Exception for a localization not found.</returns>
        private static System.Exception Exception (string message) =>
            new LocalizationNotFoundException (message);

        #endregion

    }
}
