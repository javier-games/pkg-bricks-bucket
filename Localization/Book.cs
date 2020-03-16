using Sirenix.OdinInspector;
using UnityEngine;

namespace BricksBucket.Localization
{

	/// <summary>
	/// 
	/// Book.
	///
	/// <para>
	/// Asset with localization.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[CreateAssetMenu (fileName = "Book", menuName = "Bricks Bucket")]
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

		[SerializeField]
		private TextGroup _textLocalizations;

		[SerializeField]
		private TextureGroup _textureLocalizations;

		[SerializeField]
		private SpriteGroup _spriteLocalizations;

		[SerializeField]
		private AudioLocalizations _audioLocalizations;

		[SerializeField]
		private VideoLocalizations _videoLocalizations;

		[SerializeField]
		private UnityObjectLocalizations _unityObjectLocalizations;

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

		internal TextGroup TextLocalizations
		{
			get => _textLocalizations;
			set => _textLocalizations = value;
		}

		internal TextureGroup TextureLocalization
		{
			get => _textureLocalizations;
			set => _textureLocalizations = value;
		}

		internal SpriteGroup SpriteLocalizations
		{
			get => _spriteLocalizations;
			set => _spriteLocalizations = value;
		}

		internal AudioLocalizations AudioLocalizations
		{
			get => _audioLocalizations;
			set => _audioLocalizations = value;
		}

		internal VideoLocalizations VideoLocalizations
		{
			get => _videoLocalizations;
			set => _videoLocalizations = value;
		}

		internal UnityObjectLocalizations UnityObjectLocalizations
		{
			get => _unityObjectLocalizations;
			set => _unityObjectLocalizations = value;
		}

		#endregion



		#region Methods

		/// <summary>
		/// Compares to an object.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>Value of comparision.</returns>
		public int CompareTo (object obj)
		{
			var book = (BookInfo) obj;
			return string.Compare (
				Code, book.Code,
				System.StringComparison.InvariantCultureIgnoreCase
			);
		}

		/// <summary>
		/// Determines whether this group has a localized object of
		/// the given code.
		/// </summary>
		/// <returns><value>True</value> if exists.</returns>
		public bool ContainsLocalizedObject (string code)
		{
			if (string.IsNullOrWhiteSpace (code)) return false;
			return _textLocalizations.ContainsKey (code) ||
				_textureLocalizations.ContainsKey (code) ||
				_spriteLocalizations.ContainsKey (code) ||
				_audioLocalizations.ContainsKey (code) ||
				_videoLocalizations.ContainsKey (code) ||
				_unityObjectLocalizations.ContainsKey (code);
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

			Code = Name.RemoveDiacritics ().
				ToUpper ().
				Replace (' ', '_').
				RemoveSpecialCharacters ('_');
		}
#endif
		#endregion

	}
}