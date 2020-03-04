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
        [Space(), Title("Book Settings"), HideLabel()]
        [Tooltip("Section to edit books of localizations.")]
        private BookSettings _bookSettings;

        #endregion



        #region Properties

        /// <summary>
        /// Name of the LocalizedObject Project.
        /// </summary>
        public static string ProjectName
        {
            get => Instance._projectName;
            private set => Instance._projectName = value;
        }

        /// <summary>
        /// Language settings for localizedObject.
        /// </summary>
        public static LanguageSettings LanguageSettings
        {
            get => Instance._languageSettings;
            private set => Instance._languageSettings = value;
        }

        /// <summary>
        /// Book settings for localizations.
        /// </summary>
        public static BookSettings BookSettings
        {
            get => Instance._bookSettings;
            private set => Instance._bookSettings = value;
        }

        #endregion



        #region Methods
        
        /// <summary>
        /// Evaluates a localizedObject as complete.
        /// </summary>
        /// <param name="localizedObject">LocalizedObject to evaluate.</param>
        /// <typeparam name="T">Type of localizedObject content.</typeparam>
        /// <returns>Whether the localizedObject is complete.</returns>
        public static bool IsComplete <T> (LocalizedObject<T> localizedObject)
        {
            var codes = BookSettings.BooksCodes;
            var isComplete = codes.Aggregate (
                seed: true,
                (current, t) => current & localizedObject.ContainsLanguage (t)
            );
            return true;
        }

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

        #endregion

    }
}
