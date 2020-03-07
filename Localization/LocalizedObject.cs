using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Sirenix.OdinInspector;

namespace BricksBucket.Localization
{
	/// <summary>
	///
	/// Localized Object.
	/// 
	/// <para>
	/// Object with localized variations.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public abstract class LocalizedObject<T>
	{
		
		
		
		#region Fields
		
		/// <summary>
		/// Code to Identify a Localized Object.
		/// </summary>
		[SerializeField]
		private string _code;
		
		/// <summary>
		/// Book this Localized object belongs to.
		/// </summary>
		[SerializeField]
		private string _book;

		/// <summary>
		/// Type of localized object.
		/// </summary>
		[SerializeField]
		private LocalizationType _type;
		
		/// <summary>
		/// Dictionary of localizations.
		/// </summary>
		[SerializeField]
		[OnValueChanged ("ValuesChanged")]
		public Dictionary<string, T> _localizations;
		
		/// <summary>
		/// Whether this localized object has a value for all languages in
		/// Localization Settings.
		/// </summary>
		[SerializeField]
		private bool _isComplete;
		
		#endregion



		#region Properties
		
		/// <summary>
		/// Returns the localization according to the language code. 
		/// </summary>
		/// <param name="language">Language Code of value to look for.</param>
		public T this [string language]
		{
			get =>
				ContainsLanguage (language) ? Localizations[language] : default;
			set
			{
				if (ContainsLanguage (language))
					Localizations[language] = value;
				else if (LocalizationSettings.ContainsLanguage (language))
				{
					Localizations.Add (language, value);
					ValuesChanged ();
				}
			}
		}
		
		/// <summary>
		/// Code to Identify a Localized Object.
		/// </summary>
		public string Code
		{
			get => _code;
			protected set => _code = value;
		}
		
		/// <summary>
		/// Book this Localized object belongs to.
		/// </summary>
		public string Book
		{
			get => _book;
			protected set => _book = value;
		}
		
		/// <summary>
		/// Type of localized object.
		/// </summary>
		public LocalizationType Type
		{
			get => _type;
			protected set => _type = value;
		}
		
		/// <summary>
		/// Dictionary of localizations.
		/// </summary>
		protected Dictionary<string, T> Localizations
		{
			get =>
				_localizations ??
				(_localizations = new Dictionary<string, T> ());
			set => _localizations = value;
		}
		
		/// <summary>
		/// Whether this localized object has a value for all languages in
		/// Localization Settings.
		/// </summary>
		public bool IsComplete
		{
			get => _isComplete;
			protected set => _isComplete = value;
		}
		
		/// <summary>
		/// Count of localizations in this localized object.
		/// </summary>
		public int LocalizationsCount => Localizations.Count;

		#endregion
		
		
		
		#region Constructor
		
		/// <summary>
		/// Creates a new instance of the localized object.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		protected LocalizedObject (string code, string book)
		{
			Code = code;
			Book = book;
		}

		#endregion
		
		
		
		#region Methods
		
		/// <summary>
		/// Must be called each time the localizations dictionary changes.
		/// </summary>
		protected void ValuesChanged ()
		{
			//IsComplete = LocalizationSettings.IsComplete (this);
		}
		
		/// <summary>
		/// Whether this localized object contains a localization for the
		/// given language.
		/// </summary>
		/// <param name="language">Language to look for.</param>
		/// <returns>Whether this localized object contains a localization for
		/// the given language.</returns>
		public bool ContainsLanguage (string language) =>
			Localizations.ContainsKey (language);
		
		/// <summary>
		/// Tries to get a localization for the given language code.
		/// </summary>
		/// <param name="language">Language Code for the localization.</param>
		/// <param name="localization">Found Localization.</param>
		/// <returns>Whether a localization for the language is found.</returns>
		public bool TryGetLocalization (string language, out T localization) =>
			Localizations.TryGetValue (language, out localization);
		
		/// <summary>
		/// Tries to add a collection of localizations.
		/// </summary>
		/// <param name="localizations">Collection to add.</param>
		/// <returns>Languages successfully added.</returns>
		public string[] TryAddLocalization (Dictionary<string, T> localizations)
		{
			List<string> languagesAdded = new List<string> ();
			foreach (string language in localizations.Keys)
			{
				if (ContainsLanguage (language))
				{
					Localizations[language] = localizations[language];
					languagesAdded.Add (language);
				}
				
				else if (LocalizationSettings.ContainsLanguage (language))
				{
					Localizations.Add (language, localizations[language]);
					languagesAdded.Add (language);
				}
			}
			
			ValuesChanged ();
			return languagesAdded.ToArray ();
		}
		
		#endregion
	}
	
	/// <summary>
	/// 
	/// Localized Text.
	/// 
	/// <para>
	/// Object with localized variations for text.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public class LocalizedText : LocalizedObject<string>
	{
		/// <summary>
		/// Creates a new instance of the localized text.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		public LocalizedText (string code, string book) : base (code, book)
		{
			Type = LocalizationType.TEXT;
		}
	}

	/// <summary>
	/// 
	/// Localized Texture.
	/// 
	/// <para>
	/// Object with localized variations for textures.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public class LocalizedTexture : LocalizedObject<Texture>
	{
		/// <summary>
		/// Creates a new instance of the localized texture.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		public LocalizedTexture (string code, string book) : base (code, book)
		{
			Type = LocalizationType.TEXTURE;
		}
	}
	
	/// <summary>
	/// 
	/// Localized Sprite.
	/// 
	/// <para>
	/// Object with localized variations for sprites.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public class LocalizedSprite : LocalizedObject<Sprite>
	{
		/// <summary>
		/// Creates a new instance of the localized sprite.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		public LocalizedSprite (string code, string book) : base (code, book)
		{
			Type = LocalizationType.SPRITE;
		}
	}
	
	/// <summary>
	/// 
	/// Localized Video Clip.
	/// 
	/// <para>
	/// Object with localized variations for Video Clip.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public class LocalizedVideo : LocalizedObject<VideoClip>
	{
		/// <summary>
		/// Creates a new instance of the localized video.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		public LocalizedVideo (string code, string book) : base (code, book)
		{
			Type = LocalizationType.VIDEO;
		}
	}
	
	/// <summary>
	/// 
	/// Localized Audio.
	/// 
	/// <para>
	/// Object with localized variations for Audio Clip.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public class LocalizedAudio : LocalizedObject<AudioClip>
	{
		/// <summary>
		/// Creates a new instance of the localized audio.
		/// </summary>
		/// <param name="code">Code for this localized object.</param>
		/// <param name="book">Book this localized object belongs to.</param>
		public LocalizedAudio (string code, string book) : base (code, book)
		{
			Type = LocalizationType.AUDIO;
		}
	}
}