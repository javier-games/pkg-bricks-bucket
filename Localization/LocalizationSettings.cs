using UnityEngine;
using BricksBucket.Generics;
using Sirenix.OdinInspector;


namespace BricksBucket.Localization
{
    using ScriptableSingleton = ScriptableSingleton<LocalizationSettings>;

    /// <summary>
    /// 
    /// Localization Settings
    ///
    /// <para>
    /// Settings to set up the localization in the project.
    /// </para>
    /// 
    /// <para> By Javier García | @jvrgms | 2020 </para>
    /// 
    /// </summary>
    public class LocalizationSettings : ScriptableSingleton
    {
        
        

        #region Fields

        /// <summary>
        /// Name of the Localization Project.
        /// </summary>
        [SerializeField]
        [Tooltip("Name of the Localization Project.")]
        private string _projectName;

        /// <summary>
        /// Language settings for localization.
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
        /// Name of the Localization Project.
        /// </summary>
        public static string ProjectName
        {
            get => Instance._projectName;
            private set => Instance._projectName = value;
        }

        /// <summary>
        /// Language settings for localization.
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

    }
}
