using System.Collections.Generic;
using UnityEngine;
using BricksBucket.Generics;
using Sirenix.OdinInspector;

namespace BricksBucket.Localization
{
    /// <summary>
    /// 
    /// LocalizationSettings.cs
    /// 
    /// Settings for localization.
    /// 
    /// </summary>
    public class LocalizationSettings : ScriptableSingleton<LocalizationSettings>
    {
        [SerializeField]
        [BoxGroup("Boxed Struct"), HideLabel]
        public LanguageSettings languageSettings;

        
        
    }
}
