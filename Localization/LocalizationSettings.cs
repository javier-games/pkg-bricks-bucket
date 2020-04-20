using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Sirenix.OdinInspector;
using BricksBucket.Localization.Internal;

#if UNITY_EDITOR
using BricksBucket.Editor;
using UnityEditor;
#endif


// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable TailRecursiveCall
namespace BricksBucket.Localization
{
    using ScriptableSingleton =
        Generics.ScriptableSingleton<LocalizationSettings>;

    /// <summary>
    /// 
    /// <!-- LocalizationSettings -->
    ///
    /// Class that administrates the cultures, books and its localizations
    /// withing other settings related to the localizations for the project.
    /// 
    /// <!-- By Javier García | @jvrgms | 2020 -->
    /// 
    /// </summary>
    public class LocalizationSettings : ScriptableSingleton
    {

        #region Fields

        /// <summary>
        /// Name of the Project.
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
        /// Names of cultures in the cultures collection. Serialized Field
        /// dedicated for the culture names to avoid garbage collection and
        /// speed up the process to get an array of names.
        /// </summary>
        [SerializeField, HideInInspector]
        private string[] _culturesNames;

        /// <summary>
        /// Codes of cultures in the cultures collection. Serialized Field
        /// dedicated for the culture codes to avoid garbage collection and
        /// speed up the process to get an array of codes.
        /// </summary>
        [SerializeField, HideInInspector]
        private string[] _culturesCodes;

        /// <summary>
        /// Dictionary of books by codes.
        /// </summary>
        [SerializeField, ReadOnly]
        [DictionaryDrawerSettings (KeyLabel = "Code", ValueLabel = "Book",
            IsReadOnly = true)]
        [Tooltip ("Collection of books")]
        private BooksDictionary _books;

        /// <summary>
        /// Names of books in the books dictionary. Serialized Field
        /// dedicated for the books names to avoid garbage collection and
        /// speed up the process to get an array of names.
        /// </summary>
        [SerializeField, HideInInspector]
        private string[] _booksNames;
        
        /// <summary>
        /// Codes of books in the books dictionary. Serialized Field
        /// dedicated for the books codes to avoid garbage collection and
        /// speed up the process to get an array of codes.
        /// </summary>
        [SerializeField, HideInInspector]
        private string[] _booksCodes;


        #endregion

        
        #region Instance Properties

        /// <summary>
        /// List of cultures categories.
        /// </summary>
        private List<Culture> CulturesList =>
            _cultures ??
            (_cultures = new List<Culture> ());

        /// <summary>
        /// Dictionary of books.
        /// </summary>
        private BooksDictionary BooksDictionary =>
            _books ?? (_books = new BooksDictionary ());
        
        /// <summary>
        /// Names of cultures in the cultures collection. Serialized Field
        /// dedicated for the culture names to avoid garbage collection and
        /// speed up the process to get an array of names.
        /// </summary>
        internal string[] CulturesNamesArray
        {
            get
            {
                if (_culturesNames == null) UpdateCultureArrays ();
                return _culturesNames;
            }
            private set => _culturesNames = value;
        }

        /// <summary>
        /// Codes of cultures in the cultures collection. Serialized Field
        /// dedicated for the culture codes to avoid garbage collection and
        /// speed up the process to get an array of codes.
        /// </summary>
        internal string[] CulturesCodesArray
        {
            get
            {
                if(_culturesCodes == null) UpdateCultureArrays ();
                return _culturesCodes;
            }
            private set => _culturesCodes = value;
        }
        
        /// <summary>
        /// Names of books in the books dictionary. Serialized Field
        /// dedicated for the books names to avoid garbage collection and
        /// speed up the process to get an array of names.
        /// </summary>
        internal string[] BooksNamesArray
        {
            get
            {
                if(_booksNames == null) UpdateBooksArrays ();
                return _booksNames;
            }
            private set => _booksNames = value;
        }

        /// <summary>
        /// Codes of books in the books dictionary. Serialized Field
        /// dedicated for the books codes to avoid garbage collection and
        /// speed up the process to get an array of codes.
        /// </summary>
        internal string[] BooksCodesArray
        {
            get
            {
                if(_booksCodes == null) UpdateBooksArrays ();
                return _booksCodes;
            }
            private set => _booksCodes = value;
        }

        #endregion
        

        #region Static Properties

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        /// <returns>Name of the project.</returns>
        public static string ProjectName => Instance._projectName;

        /// <summary>
        /// If it is set to <value>true</value> then when a localization
        /// is requested for a certain culture and the localization is
        /// equals or equivalent to null then returns the value of the default
        /// culture.
        /// </summary>
        /// <returns>Whether to use default culture for localizations that are
        /// equals or equivalent to null in a certain culture.
        /// </returns>
        public static bool UseDefaultCulture
        {
            get => Instance._useDefaultCulture;
            set => Instance._useDefaultCulture = value;
        }

        /// <summary>
        /// Default culture to use for a localization instead of a culture with
        /// an empty or null value for the localization.
        /// </summary>
        /// <returns>Empty culture if the list of cultures is empty.</returns>
        public static Culture DefaultCulture =>
            Instance.CulturesList.Count > 0 ? Instance.CulturesList[0] : default;

        /// <summary>
        /// Array of the cultures registered in the project.
        /// <div class="WARNING">
        /// <h5>WARNING</h5>
        /// <p>The usage of this property generates garbage collection. Consider
        /// use the properties <see cref="CulturesNames"/> and <see cref=
        /// "CulturesCodes"/> within the <see cref="GetCulture"/> method.</p>
        /// </div>
        /// </summary>
        /// <returns>Array of cultures.</returns>
        public static Culture[] Cultures => Instance.CulturesList.ToArray ();

        /// <summary>
        /// Collection of display names of languages categories.
        /// </summary>
        public static string[] CulturesNames
        {
            get => Instance.CulturesNamesArray;
            private set => Instance.CulturesNamesArray = value;
        }

        /// <summary>
        /// Collection of codes of languages categories.
        /// </summary>
        public static string[] CulturesCodes
        {
            get => Instance.CulturesCodesArray;
            private set => Instance.CulturesCodesArray = value;
        }

        /// <summary>
        /// Name of books.
        /// </summary>
        public static string[] BooksNames
        {
            get => Instance.BooksNamesArray;
            private set => Instance.BooksNamesArray = value;
        }

        /// <summary>
        /// Code of books.
        /// </summary>
        public static string[] BooksCodes
        {
            get => Instance.BooksCodesArray;
            private set => Instance.BooksCodesArray = value;
        }

        #endregion

        
        #region Instance Methods
        
        /// <summary>
        /// Evaluates if the language code is in settings.
        /// </summary>
        /// <param name="code">Language code to evaluate.</param>
        /// <returns>Whether the language code is in settings.</returns>
        internal bool ContainsCulture (string code) =>
            CulturesList.Exists (culture => culture.Code == code);

        /// <summary>
        /// Returns the count of cultures in settings.
        /// </summary>
        /// <returns></returns>
        internal int GetCulturesCount () => CulturesList.Count;
        
        /// <summary>
        /// Gets the culture at the given index.
        /// </summary>
        /// <param name="index">Index of the culture to return.</param>
        /// <param name="culture">Culture found at the given index.</param>
        /// <returns>Whether a culture was found.</returns>
        internal bool GetCulture (int index, out Culture culture)
        {
            if (index >= CulturesList.Count || index < 0)
            {
                culture = new Culture ();
                return false;
            }

            culture = CulturesList[index];
            return true;
        }

        /// <summary>
        /// Sets an existing culture by the new one.
        /// </summary>
        /// <param name="index">Index of the culture to set.</param>
        /// <param name="culture">Updated culture.</param>
        internal void SetCulture (int index, Culture culture)
        {
            if (index >= CulturesList.Count || index < 0) return;
            var oldCode = CulturesList[index].Code;
            CulturesList.RemoveAt (index);
            CulturesList.Insert (index, culture);

            foreach (var book in BooksDictionary.Values)
                book.UpdateCulture (oldCode, culture.Code);

            UpdateCultureArrays ();
        }

        /// <summary>
        /// Adds a new culture to the list of cultures.
        /// </summary>
        /// <param name="culture">Culture to Add.</param>
        internal void AddCulture (Culture culture)
        {
            CulturesList.Add (culture);
            foreach (var book in BooksDictionary.Values)
                book.AddCulture (culture.Code);
            UpdateCultureArrays ();
        }

        /// <summary>
        /// Removes a culture by its index.
        /// </summary>
        /// <param name="index">Index of the culture to remove.</param>
        internal void RemoveCulture (int index)
        {
            var culture = CulturesList[index];
            CulturesList.RemoveAt (index);
            foreach (var book in BooksDictionary.Values)
                book.RemoveCulture (culture.Code);
            UpdateCultureArrays ();
        }

        /// <summary>
        /// Sets the default culture.
        /// </summary>
        /// <param name="index">Index of the new default culture.</param>
        internal void SetDefaultCulture (int index)
        {
            var newOne = CulturesList[index];
            CulturesList.Remove (newOne);
            CulturesList.Insert (0, newOne);
            UpdateCultureArrays ();
        }

        /// <summary>
        /// Updates Culture Serialized Arrays.
        /// </summary>
        internal void UpdateCultureArrays ()
        {
            var cultures = CulturesList;

            var culturesNames = new string[cultures.Count];
            var culturesCodes = new string[cultures.Count];

            for (int i = 0; i < cultures.Count; i++)
            {
                culturesNames[i] = cultures[i].Name;
                culturesCodes[i] = cultures[i].Code;
            }

            CulturesNames = culturesNames;
            CulturesCodes = culturesCodes;
        }

        /// <summary>
        /// Gets the count of books in books dictionary.
        /// </summary>
        /// <returns>Count of books in books dictionary.</returns>
        internal int GetBooksCount () => BooksDictionary.Count;

        /// <summary>
        /// Evaluates if the dictionary of books contains the key code.
        /// </summary>
        /// <param name="code">Code to look for.</param>
        /// <returns>Whether the dictionary contains the book code.</returns>
        public bool ContainsBook (string code) =>
            BooksDictionary.ContainsKey (code);

        /// <summary>
        /// Adds a new book by code.
        /// </summary>
        /// <param name="code">Code where to add book.</param>
        /// <param name="book">Book to add.</param>
        internal void AddBook (string code, Book book)
        {
            BooksDictionary.Add (code, book);
            UpdateBooksArrays ();
        }

        /// <summary>
        /// Removes a book by code.
        /// </summary>
        /// <param name="code">Code of the book to remove.</param>
        internal void RemoveBook (string code)
        {
            var book = BooksDictionary[code];
            BooksDictionary.Remove (code);
#if UNITY_EDITOR
            book.TryDestroyImmediate ();
#endif
            UpdateBooksArrays ();
        }
        
        /// <summary>
        /// Updates Books Serialized Arrays.
        /// </summary>
        internal void UpdateBooksArrays ()
        {
            var books = BooksDictionary;

            var culturesNames = new string[books.Count];
            var culturesCodes = new string[books.Count];

            var i = 0;
            foreach (var key in BooksDictionary.Keys)
            {
                culturesNames[i] = books[key].Name;
                culturesCodes[i] = books[key].Code;
                i++;
            }

            BooksNames = culturesNames;
            BooksCodes = culturesCodes;
        }

        #endregion

        
        #region Static Methods

#if UNITY_EDITOR
        /// <summary>
        /// Creates a new instances of the localization settings into the
        /// Resources folder.
        /// </summary>
        [MenuItem ("Tools/Bricks Bucket/Localization/Initialize")]
        public static void InitializeLocalization ()
        {
            if(instance == null)
                Debug.Log (Instance + " has been created.", Instance);
        }
#endif

        /// <summary>
        /// Gets the book of the specified code.
        /// </summary>
        /// <param name="code">Code of the book to look for.</param>
        /// <returns>Book found in settings.</returns>
        /// <exception cref="LocalizationNotFoundException">
        /// Thrown when the code does not exists in the books collection.
        /// </exception>
        public static Book GetBook (string code)
        {
            if(!Instance.ContainsBook (code))
                throw Exception ("Book " + code + " not found.");

            return Instance.BooksDictionary[code];
        }

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
        public static Texture GetTexture (
            string book, string code, string culture
        )
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
        public static Sprite GetSprite (
            string book, string code, string culture
        )
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
        public static AudioClip GetAudio (
            string book, string code, string culture
        )
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
        public static VideoClip GetVideo (
            string book, string code, string culture
        )
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
        public static Object GetObject (
            string book, string code, string culture
        )
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
        /// Returns an exception for a localization not found.
        /// </summary>
        /// <param name="message">Message to display on console.</param>
        /// <returns>Exception for a localization not found.</returns>
        private static System.Exception Exception (string message) =>
            new LocalizationNotFoundException (message);

        #endregion
    }
}
