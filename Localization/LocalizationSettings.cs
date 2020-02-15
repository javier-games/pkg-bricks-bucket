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
        private LanguageSettings _languageSettings;

        [SerializeField]
        private BookSettings _bookSettings;

        #endregion



        #region Properties

        /// <summary>
        /// Language settings for localization.
        /// </summary>
        public static LanguageSettings LanguageSettings
        {
            get => Instance._languageSettings;
            private set => Instance._languageSettings = value;
        }

        /// <summary>
        /// Name of the Localization Project.
        /// </summary>
        public static string ProjectName
        {
            get => Instance._projectName;
            private set => Instance._projectName = value;
        }

        #endregion

    }
}
