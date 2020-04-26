using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using BricksBucket.Localization.Internal;


// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
namespace BricksBucket.Localization
{
	/// <!-- Book -->
	/// 
	/// <summary>
	/// The book is an scriptable object that contains a group of localizations.
	/// A book can store different localization types as text, images, audio
	/// and video, it can also stores any type that inherits from
	/// <see href="https://docs.unity3d.com/ScriptReference/Object.html">
	/// UnityEngine.Object</see>, that means that can store custom assets.
	/// </summary>
	/// 
	/// <seealso href=
	/// "https://docs.unity3d.com/ScriptReference/ScriptableObject.html">
	/// ScriptableObject</seealso>
	/// <seealso cref="BricksBucket.Localization.LocalizationSettings"/>
	/// <seealso cref="BricksBucket.Localization.Culture"/>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	public class Book : ScriptableObject
	{



		#region Serialized Fields

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
		private AudioGroup _audioGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Video.VideoClip"/>
		/// values.
		/// </summary>
		[SerializeField]
		private VideoGroup _videoGroup;

		/// <summary>
		/// Group of localizations of <see cref="UnityEngine.Object"/> values.
		/// </summary>
		[SerializeField]
		private UnityObjectGroup _unityObjectGroup;
		#endregion


		#region Read Only / Constants
		
		/*
		 * NOTE:
		 * For each new type of data to add to the book do the following steps:
		 * - Add the new value to the Localization Type enum.
		 * - Create a new LocalizedObject<TNewType>.
		 * - Create a new LocalizationGroup<TNewType>.
		 * - Add the serialized group to this book.
		 * - Add the name to the GroupsNames dictionary.
		 * - Add the group to the _groupsDictionary.
		 */

		/// <summary>
		/// Names of the groups by type.
		/// </summary>
		internal static readonly Dictionary<LocalizationType, string>
			GroupsNames = new Dictionary<LocalizationType, string> ()
			{
				{LocalizationType.TEXT, "Text"},
				{LocalizationType.TEXTURE, "Texture"},
				{LocalizationType.SPRITE, "Sprite"},
				{LocalizationType.AUDIO, "Audio"},
				{LocalizationType.VIDEO, "Video"},
				{LocalizationType.OBJECT, "Other"}
			};


			/// <summary>
		/// Dictionary of groups that ignores its types.
		/// </summary>
		private Dictionary<LocalizationType, ILocalizedGroup>
			_groupsDictionary;


		/// <summary>
		/// Dictionary of all groups in the dictionary.
		/// </summary>
		internal Dictionary<LocalizationType, ILocalizedGroup>
			GroupsDictionary
		{
			get
			{
				var groups = _groupsDictionary;
				if (groups != null)
				{
					return groups;
				}

				if (_textGroup == null)
					_textGroup = new TextGroup ();
				if(_textureGroup == null)
					_textureGroup = new TextureGroup ();
				if(_spriteGroup == null)
					_spriteGroup = new SpriteGroup ();
				if(_audioGroup == null)
					_audioGroup = new AudioGroup ();
				if(_videoGroup == null)
					_videoGroup = new VideoGroup ();
				if(_unityObjectGroup == null)
					_unityObjectGroup = new UnityObjectGroup ();


				return (_groupsDictionary =
					new Dictionary<LocalizationType, ILocalizedGroup> ()
					{
						{LocalizationType.TEXT, _textGroup},
						{LocalizationType.TEXTURE, _textureGroup},
						{LocalizationType.SPRITE, _spriteGroup},
						{LocalizationType.AUDIO, _audioGroup},
						{LocalizationType.VIDEO, _videoGroup},
						{LocalizationType.OBJECT, _unityObjectGroup}
					});
			}
		}

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
		/// Only editable on inspector.
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
		/// Localizations Settings</see>. Only editable on inspector.
		/// </summary>
		/// <returns>Code of the book in <c>UPPER_SNAKE</c> case
		/// format.</returns>
		public string Code
		{
			get => _code;
			internal set => _code = value;
		}

		/// <summary>
		/// Count of localized objects in the book. This count includes all
		/// text, media and object localizations.
		/// </summary>
		/// <returns>Count of localized objects.</returns>
		public int Count
		{
			get
			{
				var count = 0;
				for (int i = 0; i < GroupsNames.Count; i++)
					count += GroupsDictionary[(LocalizationType)i].Count;
				return count;
			}
		}
		

		/// <summary>
		/// Count of localized objects that are uncompleted in the book. This
		/// count includes all text, media and object localizations.
		/// </summary>
		/// <returns>Count of uncompleted localized objects.</returns>
		internal int UncompletedCount 
		{
			get
			{
				var count = 0;
				for (int i = 0; i < GroupsNames.Count; i++)
					count += GroupsDictionary[(LocalizationType) i].
						UncompletedCount;
				return count;
			}
		}

		/// <summary>
		/// Whether each localized object in this book has a value different
		/// from the default value for each cultures in
		/// <see cref="BricksBucket.Localization.LocalizationSettings">
		/// Localizations Settings</see>.
		/// </summary>
		/// <returns><value>True</value> if this book has a non default value
		/// for each culture in each localized object.</returns>
		internal bool IsCompleted 
		{
			get
			{
				var isCompleted = true;
				for (int i = 0; i < GroupsNames.Count; i++)
					isCompleted &= GroupsDictionary[(LocalizationType)i].
						IsCompleted ();
				return isCompleted;
			}
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
			var containsLocalizedObject = false;
			for (int i = 0; i < GroupsNames.Count; i++)
				containsLocalizedObject |=
					GroupsDictionary[(LocalizationType) i].
						ContainsLocalizedObject (code);
			return containsLocalizedObject;
		}

		/// <summary>
		/// Adds a new culture to all localized objects.
		/// </summary>
		/// <param name="code">Code of the culture to add.</param>
		internal void AddNewCulture (string code)
		{
			for (int i = 0; i < GroupsNames.Count; i++)
				GroupsDictionary[(LocalizationType) i].AddNewCulture (code);
		}

		/// <summary>
		/// Removes the localizations for the given culture code.
		/// </summary>
		/// <param name="code">Code of the culture to remove.</param>
		internal void RemoveCulture (string code)
		{
			for (int i = 0; i < GroupsNames.Count; i++)
				GroupsDictionary[(LocalizationType) i].RemoveCulture (code);
		}

		/// <summary>
		/// Updates the code of a culture for a new one.
		/// </summary>
		/// <param name="oldCode">Old code to update.</param>
		/// <param name="newCode">Code of the new culture.</param>
		internal void UpdateCulture (string oldCode, string newCode)
		{
			for (int i = 0; i < GroupsNames.Count; i++)
				GroupsDictionary[(LocalizationType) i].
					UpdateCulture (oldCode, newCode);
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