using Sirenix.OdinInspector;
using UnityEngine;

namespace BricksBucket.Localization
{
	[System.Serializable]
	public struct Book
	{


		#region Fields

		/// <summary>
		/// Name of the book.
		/// </summary>
		[SerializeField]
		[Tooltip ("Name of the book.")]
		[OnValueChanged("OnNameChanged")]
		private string _name;

		/// <summary>
		/// What does this book is for.
		/// </summary>
		[SerializeField]
		[Tooltip ("What does this book is for.")]
		private string _description;

		/// <summary>
		/// Code to identify this book.
		/// </summary>
		[SerializeField]
		[Tooltip ("Code to identify this book.")]
		private string _code;

		#endregion



		#region Properties

		/// <summary>
		/// Name of the book.
		/// </summary>
		public string Name
		{
			get => _name;
			private set => _name = value;
		}

		/// <summary>
		/// What this book is about.
		/// </summary>
		public string Description
		{
			get => _description;
			private set => _description = value;
		}

		/// <summary>
		/// Code to identify this book.
		/// </summary>
		public string Code
		{
			get => _code;
			private set => _code = value;
		}

		#endregion


		#region Editor

#if UNITY_EDITOR

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