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
		/// Code to identify this book.
		/// </summary>
		[SerializeField]
		[DisplayAsString]
		[Tooltip ("Code to identify this book.")]
		private string _code;

		/// <summary>
		/// Name of the book.
		/// </summary>
		[SerializeField]
		[Tooltip ("Name of the book.")]
		[OnValueChanged ("OnNameChanged")]
		private string _name;

		/// <summary>
		/// What does this book is for.
		/// </summary>
		[SerializeField]
		[Multiline (3)]
		[Tooltip ("What does this book is for.")]
		private string _description;

		/// <summary>
		/// Group of text localizations.
		/// </summary>
		[SerializeField]
		private TextGroup _textGroup;

		/// <summary>
		/// Group of texture localizations.
		/// </summary>
		[SerializeField]
		private TextureGroup _textureGroup;

		/// <summary>
		/// Group of sprite localizations.
		/// </summary>
		[SerializeField]
		private SpriteGroup _spriteGroup;

		/// <summary>
		/// Group of audio localizations.
		/// </summary>
		[SerializeField]
		private AudioLocalizations _audioGroup;

		/// <summary>
		/// Group of video localizations.
		/// </summary>
		[SerializeField]
		private VideoLocalizations _videoGroup;

		/// <summary>
		/// Group of object localizations.
		/// </summary>
		[SerializeField]
		private UnityObjectLocalizations _unityObjectGroup;

		#endregion



		#region Properties

		/// <summary>
		/// Name of the book.
		/// </summary>
		public string Name
		{
			get => _name;
			internal set => _name = value;
		}

		/// <summary>
		/// What this book is about.
		/// </summary>
		public string Description
		{
			get => _description;
			internal set => _description = value;
		}

		/// <summary>
		/// Code to identify this book.
		/// </summary>
		public string Code
		{
			get => _code;
			internal set => _code = value;
		}

		/// <summary>
		/// Count of localized objects in book.
		/// </summary>
		public int Count =>
			TextGroup.Count +
			TextureGroup.Count +
			SpriteGroup.Count +
			AudioGroup.Count +
			VideoGroup.Count +
			UnityObjectGroup.Count;

		/// <summary>
		/// Whether this book is complete.
		/// </summary>
		public bool IsCompleted =>
			TextGroup.IsCompleted () &&
			TextureGroup.IsCompleted () &&
			SpriteGroup.IsCompleted () &&
			AudioGroup.IsCompleted () &&
			VideoGroup.IsCompleted () &&
			UnityObjectGroup.IsCompleted ();

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		internal int UncompletedCount =>
			TextGroup.UncompletedCount +
			TextureGroup.UncompletedCount +
			SpriteGroup.UncompletedCount +
			AudioGroup.UncompletedCount +
			VideoGroup.UncompletedCount +
			UnityObjectGroup.UncompletedCount;

		/// <summary>
		/// 
		/// </summary>
		internal TextGroup TextGroup
		{
			get => _textGroup ?? new TextGroup ();
			set => _textGroup = value;
		}

		internal TextureGroup TextureGroup
		{
			get => _textureGroup ?? new TextureGroup ();
			set => _textureGroup = value;
		}

		internal SpriteGroup SpriteGroup
		{
			get => _spriteGroup ?? new SpriteGroup ();
			set => _spriteGroup = value;
		}

		internal AudioLocalizations AudioGroup
		{
			get => _audioGroup ?? new AudioLocalizations ();
			set => _audioGroup = value;
		}

		internal VideoLocalizations VideoGroup
		{
			get => _videoGroup ?? new VideoLocalizations ();
			set => _videoGroup = value;
		}

		internal UnityObjectLocalizations UnityObjectGroup
		{
			get => _unityObjectGroup ?? new UnityObjectLocalizations ();
			set => _unityObjectGroup = value;
		}

		#endregion



		#region Methods

		/// <summary>
		/// Determines whether this group has a localized object of
		/// the given code.
		/// </summary>
		/// <returns><value>True</value> if exists.</returns>
		public bool ContainsLocalizedObject (string code)
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
		/// Compares to an object.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>Value of comparision.</returns>
		public int CompareTo (object obj)
		{
			var book = (Book) obj;
			return string.Compare (
				Code, book.Code,
				System.StringComparison.InvariantCultureIgnoreCase
			);
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
		/// Updates the code of a culture.
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