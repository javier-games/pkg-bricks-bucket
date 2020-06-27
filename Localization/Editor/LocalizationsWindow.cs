using BricksBucket.Localization.Internal;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Localization.Editor
{
	/// <!-- LocalizationsWindow -->
	/// 
	/// <summary>
	/// Window that shows the localizations settings.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	internal class LocalizationsWindow : OdinEditorWindow
	{
		
		#region Constants
		
		/// <summary>
		/// Width of a field in the window.
		/// </summary>
		private const int FieldWidth = 150;
		
		/// <summary>
		/// Space between Horizontal Fields. 
		/// </summary>
		private const int CellSpace = 4;

		/// <summary>
		/// Size of an Icon.
		/// </summary>
		private const int IconSize = 14;

		#endregion


		#region Static Fields
		
		/// <summary>
		/// Name of the data type where to activate the add menu.
		/// </summary>
		private static string _dataTypeAddMenu = string.Empty;
		
		/// <summary>
		/// Code of the new localization to add.
		/// </summary>
		private static string _codeToAdd = string.Empty;
		
		/// <summary>
		/// Code of the localization where to show edit menu.
		/// </summary>
		private static string _codeToEdit = string.Empty;
		
		/// <summary>
		/// Replacement for the code of the localization selected.
		/// </summary>
		private static string _codeReplacement = string.Empty;

		/// <summary>
		/// Value of the scroll bar.
		/// </summary>
		private static Vector2 _scroll;
		
		#endregion


		#region Override Methods

		/// <inheritdoc cref="OdinEditorWindow.OnEnable()"/>
		protected override void OnEnable ()
		{
			base.OnEnable ();
			titleContent = new GUIContent (
				"Localizations Editor",
				EditorIcons.Globe.Highlighted
			);

			if (LocalizationSettings.InstanceExist) return;
			LocalizationSettings.InitializeLocalization ();
		}

		/// <inheritdoc cref="OdinEditorWindow.OnGUI()"/>
		protected override void OnGUI ()
		{
			if (LocalizationSettings.CulturesNames.Length == 0)
			{
				SirenixEditorGUI.MessageBox (
					"Add at least one culture into the localization settings.",
					MessageType.Warning,
					wide: false
				);
				return;
			}
			
			if (LocalizationSettings.BooksCodes.Length == 0)
			{
				SirenixEditorGUI.MessageBox (
					"Add at least one book to start editing."
				);
				return;
			}
			
			if (LocalizationSettings.WindowCultureMask == 0)
			{
				SirenixEditorGUI.MessageBox (
					"Select at least one culture to edit.",
					MessageType.Warning
				);
				return;
			}

			DrawToolbar ();
			DrawContent ();
		}
		
		#endregion

		
		#region Class Implementation
		
		/// <summary>
		/// Draws method in the Unity menu.
		/// </summary>
		[MenuItem ("Tools/Bricks Bucket/Localization/Localizations Editor")]
		internal static void OpenWindow ()
		{
			LocalizationSettings.InitializeLocalization ();
			GetWindow<LocalizationsWindow> ().Show ();
		}

		internal void Import ()
		{
			this.ImportFromGoogleSheet (
				token: "AIzaSyBEH_oZtk8pxMJIRZx1W1TVjyeRKc3ja2c",
				spreadsheet: "15EAdMwZFYe_YAdHYPGxPZ4fY2IPN1a4u9viM1AAN8Z0",
				sheet: "Example",
				book => { Debug.Log (book);}
			);
		}

		/// <summary>
		/// Draws the header in the window.
		/// </summary>
		private static void DrawHeader ()
		{
			EditorGUILayout.BeginVertical ();
			EditorGUILayout.Space (20);

			EditorGUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			SirenixEditorGUI.BeginBox ();
			EditorGUILayout.BeginVertical ();

			//	Drawing Cultures selector.
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (
				"Culture(s)",
				GUILayout.Width (FieldWidth * 0.5f)
			);
			LocalizationSettings.WindowCultureMask = EditorGUILayout.MaskField (
				LocalizationSettings.WindowCultureMask,
				LocalizationSettings.CulturesNames,
				GUILayout.Width (FieldWidth)
			);
			EditorGUILayout.EndHorizontal ();

			//	Drawing Data Types selector.
			var types = new string[Book.GroupsNames.Count];
			Book.GroupsNames.Values.CopyTo (types, 0);
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (
				"Data Type(s)",
				GUILayout.Width (FieldWidth * 0.5f)
			);
			LocalizationSettings.WindowDataTypeMask =
				EditorGUILayout.MaskField (
					LocalizationSettings.WindowDataTypeMask,
					types,
					GUILayout.Width (FieldWidth)
				);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.EndVertical ();
			SirenixEditorGUI.EndBox ();
			EditorGUILayout.Space (20);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.EndVertical ();
		}

		/// <summary>
		/// Draws the tool bar with books.
		/// </summary>
		private void DrawToolbar ()
		{
			SirenixEditorGUI.BeginHorizontalToolbar ();
			for (int i = 0; i < LocalizationSettings.BooksNames.Length; i++)
			{
				if (SirenixEditorGUI.ToolbarToggle (
					LocalizationSettings.WindowActiveBook == i,
					LocalizationSettings.BooksNames[i]))
				{
					LocalizationSettings.WindowActiveBook = i;
				}
			}
			
			GUILayout.FlexibleSpace ();
			if (SirenixEditorGUI.ToolbarToggle (false, "Import"))
			{
				Import ();
			}
			
			SirenixEditorGUI.EndHorizontalToolbar ();
		}

		/// <summary>
		/// Draws the content of localizations.
		/// </summary>
		private static void DrawContent ()
		{
			//	Draw Books Info.
			var bookCodeIndex = LocalizationSettings.WindowActiveBook;
			var bookCode = LocalizationSettings.BooksCodes[bookCodeIndex];
			LocalizationSettings.GetBook (bookCode, out var book);

			EditorGUILayout.BeginHorizontal ();
			DrawBookInfo (book);
			DrawHeader ();
			EditorGUILayout.EndHorizontal ();
			
			// Initialize Scroll View.
			var currentWindowWidth = EditorGUILayout.GetControlRect ().width;
			var contentWidth =
				(FieldWidth + CellSpace) * LocalizationSettings.Cultures.Length;
			if (currentWindowWidth < contentWidth)
				_scroll = EditorGUILayout.BeginScrollView (_scroll, true, true);
			else
			{
				_scroll.x = 0;
				_scroll = EditorGUILayout.BeginScrollView (_scroll);
			}
			
			//	Draw groups.
			for (int i = 0; i < Book.GroupsNames.Count; i++)
			{
				if ((LocalizationSettings.WindowDataTypeMask & (1 << i)) <= 0)
					continue;
				DrawGroup (book, (LocalizationType) i);
			}
			EditorGUILayout.EndScrollView ();
		}

		/// <summary>
		/// Draws the info of a book.
		/// </summary>
		/// <param name="book">Book to draw its info.</param>
		private static void DrawBookInfo (Book book)
		{
			EditorGUILayout.BeginVertical ();
			EditorGUILayout.Space(10);
			EditorGUILayout.BeginHorizontal ();
			CellHorizontalSpace ();
			EditorGUILayout.LabelField (book.Name, EditorStyles.boldLabel);
			EditorGUILayout.EndHorizontal ();

			if (!string.IsNullOrWhiteSpace (book.Description))
			{
				EditorGUILayout.Space (1);
				EditorGUILayout.BeginHorizontal ();
				CellHorizontalSpace ();
				EditorGUILayout.LabelField (
					book.Description,
					SirenixGUIStyles.MultiLineLabel,
					GUILayout.Width (FieldWidth * 3),
					GUILayout.Height (40)
				);
				EditorGUILayout.EndHorizontal ();
			}

			//	Draws the Status.
			EditorGUILayout.BeginHorizontal ();
			CellHorizontalSpace ();
			GUIContent statusIcon = new GUIContent();
			GUIContent statusValue = new GUIContent (
				"Completed", "All localizations are setup."
			);
			if (book.IsCompleted)
			{
				statusIcon.image = EditorIcons.TestPassed;
				statusIcon.tooltip = "All localizations are setup.";
				statusValue.text = "Completed";
				statusValue.tooltip = "All localizations are setup.";
			}
			else
			{
				var statusTooltip =
					$"{book.UncompletedCount} localizations left to be completed.";
				statusIcon.image = EditorIcons.UnityWarningIcon;
				statusIcon.tooltip = statusTooltip;
				statusValue.text = "Uncompleted";
				statusValue.tooltip = statusTooltip;
			}
			EditorGUILayout.LabelField (
				"Status:",
				GUILayout.Width (FieldWidth * 0.5f));
			EditorGUILayout.LabelField (
				statusValue,
				SirenixGUIStyles.BoldLabel,
				GUILayout.Width (FieldWidth * 0.6f)
			);
			EditorGUILayout.LabelField (statusIcon, GUILayout.Width (IconSize));
			EditorGUILayout.EndHorizontal ();


			for (int i = 0; i < Book.GroupsNames.Count; i++)
			{
				var type = (LocalizationType) i;
				var group = book.GroupsDictionary[type];
				if(group.UncompletedCount == 0) continue;
				
				EditorGUILayout.BeginHorizontal ();
				CellHorizontalSpace ();
				EditorGUILayout.LabelField (
					string.Empty,
					GUILayout.Width (FieldWidth * 0.5f)
				);
				EditorGUILayout.LabelField (
					string.Concat (
						group.UncompletedCount.ToString (), " ",
						Book.GroupsNames[type],
						" localizations left."
					),
					GUILayout.Width (FieldWidth)
				);
				EditorGUILayout.EndHorizontal ();
			}
			EditorGUILayout.EndVertical ();
		}
		
		/// <summary>
		/// Draws a group for the specific type.
		/// </summary>
		/// <param name="book">Book the group belongs to.</param>
		/// <param name="type">Type of the group.</param>
		private static void DrawGroup (Book book, LocalizationType type)
		{
			var culturesCount = LocalizationSettings.CulturesNames.Length;
			var culturesMask = LocalizationSettings.WindowCultureMask;
			var groupTitle = Book.GroupsNames[type];
			var group = book.GroupsDictionary[type];
			
			//	Group Titles.
			EditorGUILayout.Space (1);
			EditorGUILayout.BeginHorizontal ();
			ColumnTitleHorizontalSpace ();
			ColumnTitle (groupTitle);
			ColumnTitleHorizontalSpace ();
			for (int i = 0; i < culturesCount; i++)
				if ((culturesMask & (1 << i)) > 0)
				{
					ColumnTitle (LocalizationSettings.CulturesNames[i]);
					ColumnTitleHorizontalSpace ();
				}
			EditorGUILayout.EndHorizontal ();

			
			SirenixEditorGUI.BeginBox ();
			var codes = group.Codes;
			for (int i = 0; i < codes.Length; i++)
			{
				EditorGUILayout.BeginHorizontal ();
				CellHorizontalSpace ();
				
				//	Draws Editable Localization Code.
				if (_codeToEdit == codes[i])
				{
					_codeReplacement = EditorGUILayout.DelayedTextField (
						_codeReplacement,
						GUILayout.Width (FieldWidth - IconSize * 2 - CellSpace)
					).ToCodeFormat ();

					if (DrawIconButton (EditorIcons.ArrowLeft))
					{
						_codeReplacement = string.Empty;
						_codeToEdit = string.Empty;
						return;
					}

					var isAvailable =
						!string.IsNullOrWhiteSpace (_codeReplacement) &&
						!book.ContainsLocalizedObject (_codeReplacement);

					GUI.enabled = isAvailable;
					if (DrawIconButton(EditorIcons.Checkmark))
					{
						group.UpdateLocalizationCode (
							codes[i],
							_codeReplacement
						);
						_codeReplacement = string.Empty;
						_codeToEdit = string.Empty;
						return;
					}
					GUI.enabled = true;
					
					CellHorizontalSpace ();
				}
				
				//	Draws Localization.
				else
				{
					EditorGUILayout.LabelField (
						codes[i],
						SirenixGUIStyles.LeftAlignedCenteredLabel,
						GUILayout.Width (FieldWidth - IconSize * 2 - CellSpace)
					);

					if (DrawIconButton (EditorIcons.Pen))
					{
						_codeReplacement = codes[i];
						_codeToEdit = codes[i];
					}

					if (DrawIconButton (EditorIcons.X))
					{
						group.Remove (codes[i]);
						_codeToEdit = string.Empty;
						return;
					}

					CellHorizontalSpace ();
					for (int j = 0; j < culturesCount; j++)
						if ((culturesMask & (1 << j)) > 0)
						{
							var cultureCode = LocalizationSettings
								.CulturesCodes[j];
							
							group.GetLocalized (codes[i]).DrawField (
								cultureCode,
								drawLabel: false,
								options: new[] {GUILayout.Width (FieldWidth)}
							);
							CellHorizontalSpace ();
						}
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space (2);
			}

			//	Draws Add Menu.
			EditorGUILayout.BeginHorizontal ();
			CellHorizontalSpace ();
			if (_dataTypeAddMenu == groupTitle)
				DrawAddMenu (book, group);
			else if (DrawIconButton (EditorIcons.Plus))
				_dataTypeAddMenu = groupTitle;
			EditorGUILayout.EndHorizontal ();
			
			SirenixEditorGUI.EndBox ();
		}

		/// <summary>
		/// Draws the add menu to add new empty localizations.
		/// </summary>
		/// <param name="book">Book to validate new code.</param>
		/// <param name="group">Group of the localization.</param>
		private static void DrawAddMenu (Book book,  ILocalizedGroup group)
		{
			_codeToAdd = EditorGUILayout.DelayedTextField (
				_codeToAdd,
				GUILayout.Width (FieldWidth - 2)
			).ToCodeFormat ();

			CellHorizontalSpace ();

			var isAvailable =
				!string.IsNullOrWhiteSpace (_codeToAdd) &&
				!book.ContainsLocalizedObject (_codeToAdd);

			GUI.enabled = isAvailable;
			if (GUILayout.Button ("Add",
				GUILayout.Width (FieldWidth * 0.5f - 1)))
			{
				group.AddEmpty (
					_codeToAdd,
					LocalizationSettings.CulturesCodes
				);
				_codeToAdd = string.Empty;
			}

			GUI.enabled = true;
			if (!GUILayout.Button ("Cancel",
				GUILayout.Width (FieldWidth * 0.5f)))
				return;
			_dataTypeAddMenu = string.Empty;
			_codeToAdd = string.Empty;
		}

		/// <summary>
		/// Horizontal space of a cell.
		/// </summary>
		private static void CellHorizontalSpace () =>
			EditorGUILayout.LabelField (
				string.Empty,
				GUILayout.Width (CellSpace)
			);

		/// <summary>
		/// Horizontal Space of a column title.
		/// </summary>
		private static void ColumnTitleHorizontalSpace () =>
			EditorGUILayout.LabelField (
				string.Empty,
				GUILayout.Width (CellSpace),
				GUILayout.Height (10)
			);

		/// <summary>
		/// Draws a column title.
		/// </summary>
		/// <param name="columnTitle"></param>
		private static void ColumnTitle (string columnTitle) =>
			EditorGUILayout.LabelField (
				columnTitle,
				SirenixGUIStyles.Subtitle,
				GUILayout.Width (FieldWidth),
				GUILayout.Height (10)
			);

		/// <summary>
		/// Draws a button icon.
		/// </summary>
		/// <param name="icon">Icon to draw.</param>
		/// <returns>True if the button is pressed.</returns>
		private static bool DrawIconButton (EditorIcon icon) =>
			SirenixEditorGUI.IconButton (icon, IconSize, IconSize);

		#endregion
	}
}
