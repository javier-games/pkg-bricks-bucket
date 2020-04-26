using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using BricksBucket.Editor;

namespace BricksBucket.Localization.Editor
{
	/// <!-- LocalizationSettingsEditor -->
	/// 
	/// <summary>
	/// Custom editor to draw the localizations settings.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[CustomEditor (typeof (LocalizationSettings))]
	internal class LocalizationSettingsEditor : OdinEditor
	{



		#region Const and ReadOnly Fields

		/// <summary>
		/// Size for icon buttons.
		/// </summary>
		private const int IconSize = 14;

		/// <summary>
		/// Icon for add a new element button.
		/// </summary>
		private readonly EditorIcon _addIcon = EditorIcons.Plus;

		/// <summary>
		/// Icon for remove a new element button.
		/// </summary>
		private readonly EditorIcon _removeIcon = EditorIcons.Minus;

		/// <summary>
		/// Icon for edit a new element button.
		/// </summary>
		private readonly EditorIcon _editIcon = EditorIcons.Pen;

		#endregion



		#region Override Methods

		/// <summary>
		/// Called on inspector GUI.
		/// </summary>
		public override void OnInspectorGUI ()
		{
			// Getting the target.
			var tree = this.Tree;
			var settings = this.target as LocalizationSettings;
			if (settings == null) return;
			InspectorUtilities.BeginDrawPropertyTree (tree, true);

			//	Draws the Project Name.
			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_projectName").Draw ();

			//	Draws the settings for cultures.
			EditorGUILayout.Space ();
			DrawCultureSettings (settings, tree);

			//	Draws the settings for books.
			EditorGUILayout.Space ();
			DrawBookSettings (settings, tree);

			//	Applies changes.
			if (GUI.changed) EditorUtility.SetDirty (settings);
			InspectorUtilities.EndDrawPropertyTree (tree);
			Tree.UpdateTree ();
		}

		#endregion



		#region Culture Settings

		#region Fields

		/// <summary>
		/// Whether to show the menu to add a new culture.
		/// </summary>
		private bool _showAddCultureMenu;

		/// <summary>
		/// Whether to show the menu to remove a culture.
		/// </summary>
		private bool _showRemoveCultureMenu;

		/// <summary>
		/// Whether to show the menu to set default culture.
		/// </summary>
		private bool _showSetDefaultCultureMenu;

		/// <summary>
		/// Whether to show the menu to replace culture.
		/// </summary>
		private bool _showEditCultureMenu;

		/// <summary>
		/// Culture to add.
		/// </summary>
		private Culture _cultureToAdd;

		/// <summary>
		/// Index of the culture to remove.
		/// </summary>
		private int _cultureToRemoveIndex = -1;

		/// <summary>
		/// Index of the culture to set as default.
		/// </summary>
		private int _cultureToDefaultIndex = -1;

		/// <summary>
		/// Index of the culture to edit.
		/// </summary>
		private int _cultureToEditIndex = -1;

		/// <summary>
		/// GUI Content label for the culture to remove.
		/// </summary>
		private readonly GUIContent _cultureToRemoveLabel = new GUIContent (
			"Culture to Remove", "Name of the culture to remove."
		);

		/// <summary>
		/// GUI Content label for default culture.
		/// </summary>
		private readonly GUIContent _cultureToDefaultLabel = new GUIContent (
			"Default Culture", "Culture to use when a culture is not found."
		);

		/// <summary>
		/// GUI Content label for culture to edit.
		/// </summary>
		private readonly GUIContent _cultureToEditLabel = new GUIContent (
			"Culture to Edit", "Culture to edit."
		);

		/// <summary>
		/// GUI Content for the button of the culture to add.
		/// </summary>
		private readonly GUIContent _cultureToAddButton = new GUIContent (
			"Add", "Add the new configured culture."
		);

		/// <summary>
		/// GUI Content for the button of the culture to remove.
		/// </summary>
		private readonly GUIContent _cultureToRemoveButton = new GUIContent (
			"Remove", "Remove the selected culture."
		);

		/// <summary>
		/// GUI Content for the button of the culture to set as default.
		/// </summary>
		private readonly GUIContent _cultureToDefaultButton = new GUIContent (
			"Set", "Replace the default culture with the selected one."
		);

		/// <summary>
		/// GUI Content for the button of the culture to edit.
		/// </summary>
		private readonly GUIContent _cultureToEditButton = new GUIContent (
			"Set", "Edit the selected culture."
		);

		#endregion

		#region Methods

		/// <summary>
		/// Draws the culture settings.
		/// </summary>
		/// <param name="settings">Reference to settings object.</param>
		/// <param name="tree">Reference to tree.</param>
		private void DrawCultureSettings (
			LocalizationSettings settings,
			PropertyTree tree
		)
		{
			//	Drawing the title.
			DrawTitle ("Cultures");

			// Draws the default language category.
			var defaultIsNull = string.IsNullOrEmpty (
				LocalizationSettings.DefaultCulture.Code
			);
			if (!_showSetDefaultCultureMenu && !defaultIsNull)
			{
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField (
					label: _cultureToDefaultLabel,
					label2: new GUIContent (LocalizationSettings.DefaultCulture.
						Name)
				);

				// Draws the button to set the default language.
				if (DrawIconButton (_editIcon))
					_showSetDefaultCultureMenu = true;
				EditorGUILayout.EndHorizontal ();

				tree.GetPropertyAtPath ("_useDefaultCulture").Draw ();
				EditorGUILayout.Space ();
			}

			// Draws the menu to set the default category.
			else if (!defaultIsNull)
			{
				DrawDropdown (
					label: _cultureToDefaultLabel,
					index: ref _cultureToDefaultIndex,
					options: LocalizationSettings.CulturesNames
				);

				EditorGUILayout.BeginHorizontal ();
				GUI.enabled =
					_cultureToDefaultIndex >= 0 &&
					_cultureToDefaultIndex < settings.GetCulturesCount ();
				if (GUILayout.Button (_cultureToDefaultButton))
				{
					settings.SetDefaultCulture (_cultureToDefaultIndex);
					ResetCultureFields ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetCultureFields);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
			}

			// Draws list of categories.
			tree.GetPropertyAtPath ("_cultures").Draw ();

			// Draws the plus, minus and edit buttons to edit list.
			if (
				!_showAddCultureMenu &&
				!_showRemoveCultureMenu &&
				!_showEditCultureMenu
			)
			{
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space (0, true);
				var codeIsNull = string.IsNullOrEmpty (
					LocalizationSettings.DefaultCulture.Code
				);
				GUI.enabled = !codeIsNull;
				if (DrawIconButton (_editIcon)) _showEditCultureMenu = true;
				GUI.enabled = true;
				if (DrawIconButton (_addIcon, IconSize))
					_showAddCultureMenu = true;
				GUI.enabled = !codeIsNull;
				if (DrawIconButton (_removeIcon, IconSize))
					_showRemoveCultureMenu = true;
				GUI.enabled = true;
				EditorGUILayout.EndHorizontal ();
			}

			// Draws the menu to Add a new culture.
			else if (_showAddCultureMenu)
			{
				SirenixEditorGUI.BeginBox ();
				_cultureToAdd = Culture.DrawEditorField (_cultureToAdd);
				EditorGUILayout.BeginHorizontal ();
				GUI.enabled =
					!_cultureToAdd.Equals (default (Culture)) &&
					!settings.ContainsCulture (_cultureToAdd.Code) &&
					!string.IsNullOrWhiteSpace (_cultureToAdd.Code) &&
					!string.IsNullOrWhiteSpace (_cultureToAdd.Name);
				if (GUILayout.Button (_cultureToAddButton))
				{
					settings.AddCulture (_cultureToAdd);
					ResetCultureFields ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetCultureFields);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
			}

			// Draws the menu to remove a culture.
			else if (_showRemoveCultureMenu)
			{
				SirenixEditorGUI.BeginBox ();
				DrawDropdown (
					label: _cultureToRemoveLabel,
					index: ref _cultureToRemoveIndex,
					options: LocalizationSettings.CulturesNames
				);
				EditorGUILayout.BeginHorizontal ();
				GUI.enabled = _cultureToRemoveIndex >= 0 &&
					_cultureToRemoveIndex < settings.GetCulturesCount ();
				if (GUILayout.Button (_cultureToRemoveButton))
				{
					settings.RemoveCulture (_cultureToRemoveIndex);
					ResetCultureFields ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetCultureFields);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
			}

			// Draws the menu to edit a culture.
			else
			{
				SirenixEditorGUI.BeginBox ();
				EditorGUI.BeginChangeCheck ();
				DrawDropdown (
					label: _cultureToEditLabel,
					index: ref _cultureToEditIndex,
					options: LocalizationSettings.CulturesNames
				);
				if (EditorGUI.EndChangeCheck ())
				{
					settings.GetCultureByIndex (_cultureToEditIndex,
						out var cultureToAdd);
					_cultureToAdd = cultureToAdd;
				}

				if (_cultureToEditIndex != -1)
					_cultureToAdd = Culture.DrawEditorField (_cultureToAdd);

				EditorGUILayout.BeginHorizontal ();
				GUI.enabled =
					!_cultureToAdd.Equals (default (Culture)) &&
					!settings.ContainsCulture (_cultureToAdd.Code) &&
					!string.IsNullOrWhiteSpace (_cultureToAdd.Code) &&
					!string.IsNullOrWhiteSpace (_cultureToAdd.Name);
				if (GUILayout.Button (_cultureToEditButton))
				{
					settings.SetCulture (_cultureToEditIndex, _cultureToAdd);
					ResetCultureFields ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetCultureFields);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
			}
		}

		/// <summary>
		/// Reset culture menu fields.
		/// </summary>
		private void ResetCultureFields ()
		{
			_showAddCultureMenu = false;
			_showRemoveCultureMenu = false;
			_showSetDefaultCultureMenu = false;
			_showEditCultureMenu = false;
			_cultureToAdd = default;
			_cultureToRemoveIndex = -1;
			_cultureToDefaultIndex = -1;
			_cultureToEditIndex = -1;
		}

		#endregion

		#endregion



		#region Book Settings

		#region Fields

		/// <summary>
		/// Whether to show the menu to add a new book.
		/// </summary>
		private bool _showAddBookMenu;

		/// <summary>
		/// Whether to show the menu to remove a book.
		/// </summary>
		private bool _showRemoveBookMenu;

		/// <summary>
		/// Name of the book to add.
		/// </summary>
		private string _bookToAddName = string.Empty;

		/// <summary>
		/// Description of the book to add.
		/// </summary>
		private string _bookToAddDescription = string.Empty;

		/// <summary>
		/// Index of the book to remove.
		/// </summary>
		private int _bookToRemoveIndex = -1;

		/// <summary>
		/// GUI Content label of code of the book to add.
		/// </summary>
		private readonly GUIContent _bookToAddCodeLabel = new GUIContent (
			"Code", "Code of the book to add."
		);

		/// <summary>
		/// GUI Content label of name of the book to add.
		/// </summary>
		private readonly GUIContent _bookToAddNameLabel = new GUIContent (
			"Name", "Name of the book to add."
		);

		/// <summary>
		/// GUI Content label of description of the book to add.
		/// </summary>
		private readonly GUIContent _bookToAddDescriptionLabel = new GUIContent
		(
			"Description", "Description of the book to add."
		);

		/// <summary>
		/// GUI Content for the label of the book to remove.
		/// </summary>
		private readonly GUIContent _bookToRemoveLabel = new GUIContent (
			"Book to Remove", "Name of the culture to remove."
		);

		/// <summary>
		/// GUI Content for the button of the book to add.
		/// </summary>
		private readonly GUIContent _bookToAddButton = new GUIContent (
			"Add", "Add the new configured book."
		);

		/// <summary>
		/// GUI Content for the button of the book to remove.
		/// </summary>
		private readonly GUIContent _bookToRemoveButton = new GUIContent (
			"Remove", "Remove the selected book."
		);

		#endregion

		#region Methods

		/// <summary>
		/// Draws the book settings.
		/// </summary>
		/// <param name="settings">Reference to settings object.</param>
		/// <param name="tree">Reference to tree.</param>
		private void DrawBookSettings (
			LocalizationSettings settings,
			PropertyTree tree
		)
		{
			//	Drawing title.
			DrawTitle ("Books");
			tree.GetPropertyAtPath ("_books").Draw ();

			// Draws the plus and minus buttons to edit list.
			if (!_showAddBookMenu && !_showRemoveBookMenu)
			{
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space (0, true);
				if (DrawIconButton (_addIcon, IconSize))
					_showAddBookMenu = true;
				GUI.enabled = settings.GetBooksCount () > 0;
				if (DrawIconButton (_removeIcon, IconSize))
					_showRemoveBookMenu = true;
				GUI.enabled = true;
				EditorGUILayout.EndHorizontal ();
			}

			// Draws the menu to Add a new culture.
			else if (_showAddBookMenu)
			{
				SirenixEditorGUI.BeginBox ();
				EditorGUILayout.LabelField (
					_bookToAddCodeLabel,
					new GUIContent (_bookToAddName.ToCodeFormat ())
				);
				_bookToAddName = SirenixEditorFields.TextField (
					_bookToAddNameLabel,
					_bookToAddName
				);
				_bookToAddDescription = SirenixEditorFields.TextField (
					_bookToAddDescriptionLabel,
					_bookToAddDescription,
					EditorStyles.textArea,
					GUILayout.Height (50)
				);

				EditorGUILayout.BeginHorizontal ();
				var code = _bookToAddName.ToCodeFormat ();
				GUI.enabled =
					!string.IsNullOrWhiteSpace (code) &&
					!settings.ContainsBook (code);
				if (GUILayout.Button (_bookToAddButton))
				{
					var book = ScriptableObject.CreateInstance<Book> ();
					book.name = _bookToAddName;
					book.Code = code;
					book.Name = _bookToAddName;
					book.Description = _bookToAddDescription;
					settings.AddSubAsset (book);
					settings.AddBook (
						code ?? throw new System.ArgumentNullException (),
						book);
					ResetBookFields ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetBookFields);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
			}

			// Draws the menu to remove a culture.
			else
			{
				SirenixEditorGUI.BeginBox ();
				DrawDropdown (
					label: _bookToRemoveLabel,
					index: ref _bookToRemoveIndex,
					options: LocalizationSettings.BooksNames
				);
				EditorGUILayout.BeginHorizontal ();
				GUI.enabled = _bookToRemoveIndex >= 0 &&
					_bookToRemoveIndex < settings.GetBooksCount ();
				if (GUILayout.Button (_bookToRemoveButton))
				{
					var bookCode =
						LocalizationSettings.BooksCodes[_bookToRemoveIndex];
					settings.RemoveBook (bookCode);
					ResetBookFields ();

					if (GUI.changed) EditorUtility.SetDirty (settings);
					InspectorUtilities.EndDrawPropertyTree (tree);
					Tree.UpdateTree ();

					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
				}

				GUI.enabled = true;
				DrawCancelButton (ResetBookFields);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
			}
		}

		/// <summary>
		/// Reset Book menu fields.
		/// </summary>
		private void ResetBookFields ()
		{
			_showAddBookMenu = false;
			_showRemoveBookMenu = false;
			_bookToRemoveIndex = -1;
			_bookToAddName = string.Empty;
			_bookToAddDescription = string.Empty;
		}

		#endregion

		#endregion



		#region Static Methods

		/// <summary>
		/// Draws a Title.
		/// </summary>
		/// <param name="title">Title to draw.</param>
		private static void DrawTitle (string title) =>
			SirenixEditorGUI.Title (
				title,
				subtitle: string.Empty,
				TextAlignment.Left,
				horizontalLine: true
			);

		/// <summary>
		/// Draws an icon button.
		/// </summary>
		/// <param name="icon">Icon to draw.</param>
		/// <param name="size">Size if the icon.</param>
		/// <returns>Whether the button was preset.</returns>
		private static bool DrawIconButton (EditorIcon icon, int size = 18) =>
			SirenixEditorGUI.IconButton (icon, size, size);

		/// <summary>
		/// Draws a dropdown.
		/// </summary>
		/// <param name="label">Label for dropdown.</param>
		/// <param name="index">Index selected.</param>
		/// <param name="options">Array of options where to select.</param>
		private static void DrawDropdown (
			GUIContent label, ref int index, string[] options
		) =>
			index = SirenixEditorFields.Dropdown (label, index, options);


		/// <summary>
		/// Draws a cancel button.
		/// </summary>
		private static void DrawCancelButton (System.Action resetMethod)
		{
			if (!GUILayout.Button (
				new GUIContent ("Cancel", "Cancel the current process.")
			))
				return;
			resetMethod?.Invoke ();
		}

		#endregion
	}
}