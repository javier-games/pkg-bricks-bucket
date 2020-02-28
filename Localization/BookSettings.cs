using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif


namespace BricksBucket.Localization
{
	/// <summary>
	/// 
	/// Book Settings
	///
	/// <para>
	/// Struct to contain book settings.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public struct BookSettings
	{



		#region Fields

		/// <summary>
		/// Collection of books of localizations.
		/// </summary>
		[SerializeField]
		[Space, ReadOnly]
		[ListDrawerSettings (HideAddButton = true, NumberOfItemsPerPage = 4)]
		[Tooltip ("Collection of books of localizations.")]
		private List<Book> _books;

		#endregion



		#region Properties

		/// <summary>
		/// Book by code.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public Book this [string code]
		{
			get
			{
				var book = _books.Find (c => c.Code == code);
				return book;
			}
		}

		/// <summary>
		/// Collection of codes of books of localizations.
		/// </summary>
		public string[] BooksCodes
		{
			get
			{
				var booksCodes = new string[_books.Count];
				for (int i = 0; i < booksCodes.Length; i++)
					booksCodes[i] = _books[i].Code;

				return booksCodes;
			}
		}

		/// <summary>
		/// Collection of names of books of localizations.
		/// </summary>
		public string[] BooksNames
		{
			get
			{
				var booksNames = new string[_books.Count];
				for (int i = 0; i < booksNames.Length; i++)
					booksNames[i] = _books[i].Name;

				return booksNames;
			}
		}

		/// <summary>
		/// Collection of books of localizations.
		/// </summary>
		public Book[] Books => _books.ToArray ();

		#endregion



		#region Editor

#if UNITY_EDITOR

		#region Editor Fields
		
		/// <summary>
		/// Shows Add Menu.
		/// </summary>
		[SerializeField]
		private bool _addMenu;

		/// <summary>
		/// Whether to show or hide the Remove menu.
		/// </summary>
		[SerializeField]
		private bool _removeMenu;

		/// <summary>
		/// Book to add.
		/// </summary>
		[SerializeField]
		[LabelText ("Book to Add")]
		[Tooltip ("Edit the fields of the book to add.")]
		private Book _toAdd;

		/// <summary>
		/// Category to remove.
		/// </summary>
		[SerializeField]
		[LabelText ("Book to Remove"), ValueDropdown ("BooksCodes")]
		[Tooltip ("Select the code of the book to remove.")]
		private string _toRemove;

		#endregion



		#region Editor Methods

		/// <summary>
		/// Hides all menus.
		/// </summary>
		[Button ("Cancel")]
		private void Cancel ()
		{
			_toAdd = default;
			_toRemove = string.Empty;
			
			_addMenu = false;
			_removeMenu = false;
		}

		/// <summary>
		/// Adds a new category.
		/// </summary>
		[Button ("Add")]
		private void Add ()
		{
			_books.Add (_toAdd);
			_books.Sort();
			Cancel ();
		}

		/// <summary>
		/// Removes the indicated category.
		/// </summary>
		[Button ("Remove")]
		private void Remove ()
		{
			var categoryToRemove = _toRemove;
			_books.Remove (_books.Find (c => c.Code == categoryToRemove));
			_books.Sort();
			Cancel ();
		}

		#endregion



		#region Drawer

		/// <summary>
		/// Language Category Drawer Class.
		/// </summary>
		private class BookSettingsDrawer : OdinValueDrawer<BookSettings>
		{
			/// <summary>
			/// Draws the property.
			/// </summary>
			/// <param name="label">Label of the property.</param>
			protected override void DrawPropertyLayout (GUIContent label)
			{
				EditorGUILayout.Space ();

				var value = ValueEntry.SmartValue;
				var children = ValueEntry.Property.Children;
				
				var addMenu = value._addMenu;
				var removeMenu = value._removeMenu;

				//  Draws the label on Foldout.
				if (label != null) EditorGUILayout.LabelField (label);

				// Draws list of books.
				children.Get ("_books").Draw ();

				// Draws the plus and minus buttons to edit list.
				if (!addMenu && !removeMenu)
				{
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.Space (0f, true);

					if (SirenixEditorGUI.IconButton (EditorIcons.Plus, 14, 14))
						value._addMenu = true;

					GUI.enabled = value.Books.Length > 0;

					if (SirenixEditorGUI.IconButton (EditorIcons.Minus, 14, 14))
						value._removeMenu = true;

					GUI.enabled = true;

					EditorGUILayout.EndHorizontal ();
				}

				// Draws the Menu to Add a new book.
				else if (addMenu)
				{
					SirenixEditorGUI.BeginBox ();
					children.Get ("_toAdd").Draw ();
					SirenixEditorGUI.EndBox ();

					EditorGUILayout.BeginHorizontal ();
					var bookToAdd = value._toAdd;
					GUI.enabled =
						!bookToAdd.Equals (default (Book)) &&
						!value._books.Exists (
							book => book.Code == bookToAdd.Code
						) &&
						!string.IsNullOrWhiteSpace (bookToAdd.Code);
					children.Get ("Add").Draw();
					GUI.enabled = true;
					children.Get ("Cancel").Draw ();
					EditorGUILayout.EndHorizontal ();
				}

				// Draws the Menu to remove a book.
				else
				{

					children.Get ("_toRemove").Draw ();
					EditorGUILayout.BeginHorizontal ();
					GUI.enabled = !string.IsNullOrEmpty (value._toRemove);
					children.Get ("Remove").Draw ();
					GUI.enabled = true;
					children.Get ("Cancel").Draw ();
					EditorGUILayout.EndHorizontal ();
				}

				EditorGUILayout.Space ();
				
				// Update all values in smart value.
				ValueEntry.SmartValue = value;
				ValueEntry.ApplyChanges ();
			}
		}

		#endregion

#endif

		#endregion
	}
}