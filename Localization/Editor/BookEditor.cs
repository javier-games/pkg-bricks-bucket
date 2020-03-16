using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace BricksBucket.Localization.Editor
{
	/// <summary>
	/// 
	/// BookEditor.
	/// 
	/// <para>
	/// Custom editor for the scriptable object book.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[CustomEditor (typeof (Book))]
	public class BookEditor : OdinEditor
	{



		#region Add Menu

		/// <summary>
		/// Whether to show the Add Menu.
		/// </summary>
		private static bool _showAddMenu;

		/// <summary>
		/// Code of the new localization to add.
		/// </summary>
		private string _localizationToAddCode;

		/// <summary>
		/// Type of the new localization to add.
		/// </summary>
		private LocalizationType _localizationToAddType;

		#endregion


		#region MyRegion
		
		/// <summary>
		/// Code to modify from localized object.
		/// </summary>
		private static string _codeToModify;
		
		/// <summary>
		/// Name of the new localization object to add.
		/// </summary>
		private static string _newCodeName;

		#endregion

		
		
		#region GUI Content
		
		/// <summary>
		/// Value of the status completed.
		/// </summary>
		private const string StatusCompleted = "Completed";
		
		/// <summary>
		/// Value of the status uncompleted.
		/// </summary>
		private const string StatusUncompleted = "Uncompleted";
		
		/// <summary>
		/// Status Completed Tooltip message.
		/// </summary>
		private const string StatusCompletedTooltip =
			"All localizations are setup.";

		/// <summary>
		/// Status uncompleted Tooltip message.
		/// </summary>
		private const string StatusUncompletedTooltip =
			"{0} localizations left to be completed.";
		
		/// <summary>
		/// GUI Content status label.
		/// </summary>
		private readonly GUIContent _statusLabel = new GUIContent (
			"Status", "Whether there is book is completed."
		);
		
		/// <summary>
		/// GUI Content status value.
		/// </summary>
		private readonly GUIContent _statusValue = new GUIContent (
			"Completed", "All localizations are setup."
		);
		
		/// <summary>
		/// GUI Content status icon.
		/// </summary>
		private readonly GUIContent _statusIcon = new GUIContent ();

		#endregion


		
		#region Override Methods

		/// <summary>
		/// Called on inspector GUI.
		/// </summary>
		public override void OnInspectorGUI ()
		{
			// Getting the target.
			var tree = this.Tree;
			var book = this.target as Book;
			if (book == null) return;
			InspectorUtilities.BeginDrawPropertyTree (tree, true);


			//	Draws the Book Info.
			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_code").Draw ();
			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_name").Draw ();
			tree.GetPropertyAtPath ("_description").Draw ();
			EditorGUILayout.Space ();


			//	Draws the Status.
			EditorGUILayout.BeginHorizontal ();
			if (book.IsCompleted ())
			{
				_statusIcon.image = EditorIcons.TestPassed;
				_statusIcon.tooltip = StatusCompletedTooltip;
				_statusValue.text = StatusCompleted;
				_statusValue.tooltip = StatusCompletedTooltip;
			}
			else
			{
				var statusTooltip = StringUtils.ConcatFormat (
					StatusUncompletedTooltip, book.UncompletedCount
				);
				_statusIcon.image = EditorIcons.UnityWarningIcon;
				_statusIcon.tooltip = statusTooltip;
				_statusValue.text = StatusUncompleted;
				_statusValue.tooltip = statusTooltip;
			}

			EditorGUILayout.LabelField (
				_statusLabel, _statusValue,
				SirenixGUIStyles.BoldLabel,
				GUILayout.Width (EditorGUIUtility.currentViewWidth - 50)
			);
			EditorGUILayout.LabelField (_statusIcon, GUILayout.Width (18));
			EditorGUILayout.EndHorizontal ();


			//	Draws the title.
			EditorGUILayout.Space ();
			SirenixEditorGUI.Title (
				title: "Localizations",
				subtitle: string.Empty,
				TextAlignment.Left,
				horizontalLine: true
			);


			//	Draws the Add Menu.
			if (_showAddMenu)
			{
				EditorGUILayout.Space ();
				_localizationToAddCode = SirenixEditorFields.
					TextField ("Code", _localizationToAddCode).ToCodeFormat ();
				_localizationToAddType =
					(LocalizationType) SirenixEditorFields.EnumDropdown (
						"Type", _localizationToAddType
					);
				EditorGUILayout.BeginHorizontal ();
				GUI.enabled =
					!book.ContainsLocalizedObject (_localizationToAddCode) &&
					!string.IsNullOrWhiteSpace (_localizationToAddCode);
				if (GUILayout.Button ("Add"))
				{
					switch (_localizationToAddType)
					{
						case LocalizationType.TEXT:
							book.TextLocalizations.AddEmpty (
								_localizationToAddCode);
							break;
						case LocalizationType.TEXTURE:
							book.TextureLocalization.AddEmpty (
								_localizationToAddCode);
							break;
						case LocalizationType.SPRITE:
							book.SpriteLocalizations.AddEmpty (
								_localizationToAddCode);
							break;
						case LocalizationType.AUDIO:
							book.AudioLocalizations.AddEmpty (
								_localizationToAddCode);
							break;
						case LocalizationType.VIDEO:
							book.VideoLocalizations.AddEmpty (
								_localizationToAddCode);
							break;
						case LocalizationType.OBJECT:
							book.UnityObjectLocalizations.AddEmpty (
								_localizationToAddCode);
							break;
					}

					_localizationToAddCode = string.Empty;
				}

				GUI.enabled = true;
				if (GUILayout.Button ("Cancel"))
				{
					_localizationToAddCode = string.Empty;
					_showAddMenu = false;
				}

				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
				SirenixEditorGUI.HorizontalLineSeparator (
					SirenixGUIStyles.LightBorderColor
				);
			}
			
			
			//	Draws icon to add.
			else
			{
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space (0f, true);
				_showAddMenu = SirenixEditorGUI.IconButton (
					EditorIcons.Plus, 14, 14
				);
				EditorGUILayout.EndHorizontal ();
			}


			//	Draws the lists.
			EditorGUILayout.Space ();
			if (book != null)
			{
				if (book.TextLocalizations.Count > 0)
					DrawLocalizationGroup (book.TextLocalizations, "Texts");

				if (book.TextureLocalization.Count > 0)
					DrawLocalizationGroup (book.TextureLocalization,
						"Textures");

				if (book.SpriteLocalizations.Count > 0)
					DrawLocalizationGroup (book.SpriteLocalizations, "Sprites");

				if (book.AudioLocalizations.Count > 0)
					DrawLocalizationGroup (book.AudioLocalizations,
						"Audio Clips");

				if (book.VideoLocalizations.Count > 0)
					DrawLocalizationGroup (book.VideoLocalizations,
						"Video Clips");

				if (book.UnityObjectLocalizations.Count > 0)
					DrawLocalizationGroup (book.UnityObjectLocalizations,
						"Objects");
			}

			
			//	End drawing.
			if (GUI.changed) EditorUtility.SetDirty (book);
			InspectorUtilities.EndDrawPropertyTree (tree);
			Tree.UpdateTree ();

			EditorGUILayout.Space ();
		}

		#endregion


		#region Class Implementation

		

		private void DrawLocalizationGroup<T> (
			ILocalizationGroup<T> group, string groupName
		)
		{
			//	Definition of layout.
			const int codeIconWidth = 14;
			const int iconHeight = 20;

			var boxWidth = EditorGUIUtility.currentViewWidth - 24;
			var width = boxWidth - 20;
			var codeWidth = width - 2 * codeIconWidth;

			//	Getting the reference from the target.
			var book = this.target as Book;
			if (book == null) return;

			//	Draws the foldout list.
			SirenixEditorGUI.BeginLegendBox (GUILayout.Width (boxWidth));


			//	List Title and Status.
			EditorGUILayout.BeginHorizontal ();
			var completedText = group.UncompletedCount > 0
				? StringUtils.Concat (group.UncompletedCount, " Left")
				: "Completed";
			EditorGUILayout.LabelField (
				groupName, SirenixGUIStyles.Title,
				GUILayout.Width (width * 0.5f)
			);
			EditorGUILayout.LabelField (
				completedText,
				SirenixGUIStyles.RightAlignedGreyMiniLabel,
				GUILayout.Width (width * 0.5f)
			);
			EditorGUILayout.EndHorizontal ();
			SirenixEditorGUI.HorizontalLineSeparator (
				SirenixGUIStyles.BorderColor
			);


			EditorGUI.indentLevel++;
			//	Drawing codes.
			foreach (var code in group.Codes)
			{
				//	Unmodifiable Code Label.
				if (_codeToModify != code)
				{
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (
						code,
						GUILayout.Width (codeWidth)
					);
					if (SirenixEditorGUI.IconButton (
						EditorIcons.SettingsCog, codeIconWidth
					))
					{
						_codeToModify = _newCodeName = code;
					}

					if (SirenixEditorGUI.IconButton (
						EditorIcons.X, codeIconWidth
					))
					{
						group.Remove (code);
						return;
					}

					EditorGUILayout.EndHorizontal ();
				}

				//	Modifiable Code Label.
				else
				{
					EditorGUILayout.BeginHorizontal ();
					_newCodeName = SirenixEditorFields.TextField (
						_newCodeName,
						GUILayout.Width (codeWidth),
						GUILayout.Height (iconHeight - 4)
					).ToCodeFormat ();

					var existingCode =
						_newCodeName != code &&
						book.ContainsLocalizedObject (_newCodeName);

					var newCodeIsNull =
						string.IsNullOrWhiteSpace (_newCodeName);

					if (!existingCode && !newCodeIsNull)
					{
						if (SirenixEditorGUI.IconButton (
							EditorIcons.Checkmark, codeIconWidth - 4
						))
						{
							var localizedObject = group[code];
							group.Remove (code);
							group.Add (_newCodeName, localizedObject);
							_codeToModify = _newCodeName = string.Empty;
							return;
						}
					}
					else
					{
						if (SirenixEditorGUI.IconButton (
							EditorIcons.Undo, codeIconWidth
						))
						{
							_newCodeName = code;
						}
					}

					EditorGUILayout.EndHorizontal ();
					if (existingCode)
					{
						SirenixEditorGUI.MessageBox (
							"This code already exists for a localization.",
							MessageType.Error
						);
					}
				}


				//	Draws each culture and its value for localization.
				foreach (string culture in group[code].Cultures)
				{
					EditorGUILayout.BeginHorizontal ();

					EditorGUI.indentLevel++;
					group[code].DrawField (culture, null);
					EditorGUI.indentLevel--;
					EditorGUILayout.EndHorizontal ();

				}
			}

			EditorGUI.indentLevel--;
			SirenixEditorGUI.EndLegendBox ();
			EditorGUILayout.Space ();
		}

		#endregion
	}
}

