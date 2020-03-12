using System.Linq;
using UnityEngine;
using BricksBucket.Generics;
using Sirenix.OdinInspector;


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
    public class LocalizationSettings : ScriptableSingleton
    {
        
        

        #region Fields

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        [SerializeField]
        [Tooltip("Name of the LocalizedObject Project.")]
        private string _projectName;

        /// <summary>
        /// Language settings for localizedObject.
        /// </summary>
        [SerializeField]
        [Space(), Title("Language Settings"), HideLabel()]
        [Tooltip("Section to edit language categories.")]
        private LanguageSettings _languageSettings;
        
        [SerializeField]
        [Space(), Title("BookInfo Settings"), HideLabel()]
        [Tooltip("Section to edit books of localizations.")]
        private BookSettings _bookSettings;

        [SerializeField]
        private bool _useDefaultForEmptyLocalizations;

        [SerializeField]
        private Book _book;
        #endregion



        #region Properties

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        public static string ProjectName => Instance._projectName;

        /// <summary>
        /// Language settings for localizedObject.
        /// </summary>
        private static LanguageSettings LanguageSettings =>
            Instance._languageSettings;
        
        /// <summary>
        /// Array with languages codes of this project.
        /// </summary>
        public static string[] LanguagesCodes =>
            LanguageSettings.CategoriesCodes;
        
        /// <summary>
        /// Array with languages names of this project.
        /// </summary>
        public static string[] LanguagesNames =>
            LanguageSettings.CategoriesDisplayNames;
        
        /// <summary>
        /// BookInfo settings for localizations.
        /// </summary>
        private static BookSettings BookSettings => Instance._bookSettings;
        
        /// <summary>
        /// Array with books codes of this project.
        /// </summary>
        public static string[] BooksCodes => BookSettings.BooksCodes;
        
        /// <summary>
        /// Array with books names of this project.
        /// </summary>
        public static string[] BooksNames => BookSettings.BooksNames;


        #endregion



        #region Methods

        /// <summary>
        /// Evaluates if the language code is in settings.
        /// </summary>
        /// <param name="code">Language code to evaluate.</param>
        /// <returns>Whether the language code is in settings.</returns>
        public static bool ContainsLanguage (string code)
        {
            var languages = LanguageSettings.CategoriesCodes;
            return languages.Contains (code);
        }
        
        /// <summary>
        /// Resets the localization settings to its default.
        /// </summary>
        internal void ClearSettings ()
        {
            _projectName = string.Empty;
            _languageSettings = default;
            _bookSettings = default;
        }

        #endregion
    }
}
