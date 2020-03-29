using UnityEngine;
using Sirenix.OdinInspector;
using BricksBucket.Localization.Internal;


namespace BricksBucket.Localization
{
	/// <summary>
	/// 
	/// <!-- Book -->
	///
	/// The book is an scriptable object that contains a group of localizations.
	/// A book can store different localization types as text, images, audio
	/// and video, it can also stores any type that inherits from
	/// <see href="https://docs.unity3d.com/ScriptReference/Object.html">
	/// UnityEngine.Object</see>, that means that can store custom assets.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	[System.Serializable]
	public class Book : ScriptableObject
	{



		#region Fields

		/// <summary>
		/// Code to identify the book in the books collection of
		/// <see cref="BricksBucket.Localization.LocalizationSettings">
		/// Localizations Settings</see>. Only editable on inspector.
		/// </summary>
		[SerializeField]
		[DisplayAsString]
		[Tooltip ("Code to identify this book.")]
		private string _code;

		/// <summary>
		/// Name of the book useful to displays the book instead of the code.
		/// Only editable on inspector.
		/// </summary>
		[SerializeField]
		[Tooltip ("Name of the book.")]
		[OnValueChanged ("OnNameChanged")]
		private string _name;

		/// <summary>
		/// Documented summary of what does this book is for.
		/// Only editable on inspector.
		/// </summary>
		[SerializeField]
		[Multiline (3)]
		[Tooltip ("What does this book is for.")]
		private string _description;

		/// <summary>
		/// Group of localizations of <see cref="System.string"/> values.
		/// </summary>
		[SerializeField]
		private TextGroup _textGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Texture"/>
		/// values.
		/// </summary>
		[SerializeField]
		private TextureGroup _textureGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Sprite"/> values.
		/// </summary>
		[SerializeField]
		private SpriteGroup _spriteGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.AudioClip"/>
		/// values.
		/// </summary>
		[SerializeField]
		private AudioLocalizations _audioGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Video.VideoClip"/>
		/// values.
		/// </summary>
		[SerializeField]
		private VideoLocalizations _videoGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Object"/> values.
		/// </summary>
		[SerializeField]
		private UnityObjectLocalizations _unityObjectGroup;

		#endregion



		#region Properties

		/// <summary>
		/// Name of the book useful to displays the book instead of the code.
		/// Only editable on inspector.
		/// </summary>
		/// <returns>Name of the book.</returns>
		public string Name
		{
			get => _name;
			internal set => _name = value;
		}

		/// <summary>
		/// Documented summary of what does this book is for.
		/// Only editable from inspector.
		/// </summary>
		/// <returns>Description of the book.</returns>
		public string Description
		{
			get => _description;
			internal set => _description = value;
		}

		/// <summary>
		/// Code to identify the book in the books collection of
		/// <see cref="BricksBucket.Localization.LocalizationSettings">
		/// Localizations Settings</see>. Only editable from inspector.
		/// </summary>
		/// <returns>Code of the book in <c>UPPER_SNAKE</c> case
		/// format.</returns>
		public string Code
		{
			get => _code;
			internal set => _code = value;
		}

		/// <summary>
		/// Count of localized objects in the book.
		/// </summary>
		/// <returns>Count of localized objects.</returns>
		public int Count =>
			TextGroup.Count +
			TextureGroup.Count +
			SpriteGroup.Count +
			AudioGroup.Count +
			VideoGroup.Count +
			UnityObjectGroup.Count;

		/// <summary>
		/// Count of localized objects that are
		/// uncompleted in the book.
		/// </summary>
		/// <returns>Count of uncompleted localized objects.</returns>
		public int UncompletedCount =>
			TextGroup.UncompletedCount +
			TextureGroup.UncompletedCount +
			SpriteGroup.UncompletedCount +
			AudioGroup.UncompletedCount +
			VideoGroup.UncompletedCount +
			UnityObjectGroup.UncompletedCount;

		/// <summary>
		/// Whether each localized object in this book has a value different
		/// from the default value for each cultures in
		/// <see cref="BricksBucket.Localization.LocalizationSettings">
		/// Localizations Settings</see>.
		/// </summary>
		/// <returns><value>True</value> if this book has a non default value
		/// for each culture in each localized object.</returns>
		public bool IsCompleted =>
			TextGroup.IsCompleted () &&
			TextureGroup.IsCompleted () &&
			SpriteGroup.IsCompleted () &&
			AudioGroup.IsCompleted () &&
			VideoGroup.IsCompleted () &&
			UnityObjectGroup.IsCompleted ();

		/// <summary>
		/// Group of localizations of <see cref="System.string"/> values.
		/// </summary>
		internal TextGroup TextGroup
		{
			get => _textGroup ?? new TextGroup ();
			set => _textGroup = value;
		}

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Texture"/>
		/// values.
		/// </summary>
		internal TextureGroup TextureGroup
		{
			get => _textureGroup ?? new TextureGroup ();
			set => _textureGroup = value;
		}

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Sprite"/> values.
		/// </summary>
		internal SpriteGroup SpriteGroup
		{
			get => _spriteGroup ?? new SpriteGroup ();
			set => _spriteGroup = value;
		}

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.AudioClip"/>
		/// values.
		/// </summary>
		internal AudioLocalizations AudioGroup
		{
			get => _audioGroup ?? new AudioLocalizations ();
			set => _audioGroup = value;
		}

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Video.VideoClip"/>
		/// values.
		/// </summary>
		internal VideoLocalizations VideoGroup
		{
			get => _videoGroup ?? new VideoLocalizations ();
			set => _videoGroup = value;
		}

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Object"/> values.
		/// </summary>
		internal UnityObjectLocalizations UnityObjectGroup
		{
			get => _unityObjectGroup ?? new UnityObjectLocalizations ();
			set => _unityObjectGroup = value;
		}

		#endregion



		#region Methods

		/// <summary>
		/// Determines whether this group has a localized object for
		/// the given code.
		/// </summary>
		/// <returns><value>True</value> if  exists.</returns>
		internal bool ContainsLocalizedObject (string code)
		{
			if (string.IsNullOrWhiteSpace (code)) return false;
			return _textGroup.ContainsKey (code) ||
				_textureGroup.ContainsKey (code) ||
				_spriteGroup.ContainsKey (code) ||
				_audioGroup.ContainsKey (code) ||
				_videoGroup.ContainsKey (code) ||
				_unityObjectGroup.ContainsKey (code);
		}

		/// <summary>
		/// Adds a new culture to all localized objects.
		/// </summary>
		/// <param name="code">Code of the culture to add.</param>
		internal void AddCulture (string code)
		{
			foreach (var localizedText in TextGroup.Values)
				localizedText.AddEmpty (code);
			foreach (var localizedText in TextureGroup.Values)
				localizedText.AddEmpty (code);
			foreach (var localizedText in SpriteGroup.Values)
				localizedText.AddEmpty (code);
			foreach (var localizedText in AudioGroup.Values)
				localizedText.AddEmpty (code);
			foreach (var localizedText in VideoGroup.Values)
				localizedText.AddEmpty (code);
			foreach (var localizedText in UnityObjectGroup.Values)
				localizedText.AddEmpty (code);
		}

		/// <summary>
		/// Removes the localizations for the given culture code.
		/// </summary>
		/// <param name="code">Code of the culture to remove.</param>
		internal void RemoveCulture (string code)
		{
			foreach (var localizedText in TextGroup.Values)
				if (localizedText.ContainsKey (code))
					localizedText.Remove (code);
			foreach (var localizedTexture in TextureGroup.Values)
				if (localizedTexture.ContainsKey (code))
					localizedTexture.Remove (code);
			foreach (var localizedSprite in SpriteGroup.Values)
				if (localizedSprite.ContainsKey (code))
					localizedSprite.Remove (code);
			foreach (var localizedAudio in AudioGroup.Values)
				if (localizedAudio.ContainsKey (code))
					localizedAudio.Remove (code);
			foreach (var localizedVideo in VideoGroup.Values)
				if (localizedVideo.ContainsKey (code))
					localizedVideo.Remove (code);
			foreach (var localizedObject in UnityObjectGroup.Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}

		/// <summary>
		/// Updates the code of a culture for a new one.
		/// </summary>
		/// <param name="oldCode">Old code to update.</param>
		/// <param name="newCode">Code of the new culture.</param>
		internal void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localized in TextGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}

			foreach (var localized in TextureGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}

			foreach (var localized in SpriteGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}

			foreach (var localized in AudioGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}

			foreach (var localized in VideoGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}

			foreach (var localized in UnityObjectGroup.Values)
			{
				if (!localized.ContainsCulture (oldCode)) continue;
				localized.Add (newCode, localized[oldCode]);
				localized.Remove (oldCode);
			}
		}

		#endregion



		#region Editor

#if UNITY_EDITOR
		/// <summary>
		/// Called by inspector each time the name changes.
		/// </summary>
		internal void OnNameChanged ()
		{
			if (string.IsNullOrWhiteSpace (Name)) Code = string.Empty;
			Code = Name.ToCodeFormat ();
		}
#endif

		#endregion

	}
}