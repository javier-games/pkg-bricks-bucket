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

        [SerializeField, ReadOnly]
        private string _defaultLanguage;

        [SerializeField, ReadOnly]
        private string[] _languages;

        /// <summary>
        /// Language settings for localization.
        /// </summary>
        [SerializeField]
        [BoxGroup("Language Settings"), HideLabel]
        [OnValueChanged("LanguageSettingsChanged")]
        private LanguageSettings _languageSettings;

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

        /// <summary>
        /// Default Language of Localization Settings.
        /// </summary>
        public static string DefaultLanguage
        {
            get => Instance._defaultLanguage;
            private set => Instance._defaultLanguage = value;
        }

        /// <summary>
        /// Default Languages
        /// </summary>
        public static string[] Languages
        {
            get => Instance._languages;
            private set => Instance._languages = value;
        }

        #endregion



        #region Editor

        internal void LanguageSettingsChanged()
        {
            DefaultLanguage = LanguageSettings.Default.DisplayName;
            Languages = LanguageSettings.CategoriesDisplayNames;
        }


        #endregion



    }
}
