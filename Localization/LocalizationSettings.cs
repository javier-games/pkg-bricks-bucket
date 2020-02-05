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
        /// Language settings for localization.
        /// </summary>
        [SerializeField]
        [BoxGroup("Language Settings"), HideLabel]
        private LanguageSettings _languageSettings;

        #endregion



        #region Properties

        /// <summary>
        /// Language settings for localization.
        /// </summary>
        public LanguageSettings LanguageSettings
        {
            get => _languageSettings;
            private set => _languageSettings = value;
        }

        #endregion



    }
}
