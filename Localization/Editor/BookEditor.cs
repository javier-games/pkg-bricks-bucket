using System;
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
		private static bool _textVisible;
		private static bool _textureVisible;
		private static bool _spriteVisible;
		private static bool _audioVisible;
		private static bool _videoVisible;
		private static bool _objectVisible;

		private static bool _showAddMenu;
		private LocalizationType _type;
		private string _code;

		#region Override Methods

		/// <summary>
		/// Called on inspector GUI.
		/// </summary>
		public override void OnInspectorGUI ()
		{
			// Like this.serializedObject.
			var tree = this.Tree;
			var book = this.target as Book;
			if (book == null) return;

			InspectorUtilities.BeginDrawPropertyTree (tree, true);

			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_code").Draw ();

			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_name").Draw ();
			tree.GetPropertyAtPath ("_description").Draw ();


			EditorGUILayout.Space ();

			EditorGUILayout.BeginHorizontal ();

			var isCompleted =
				book.TextLocalizations.UncompletedCount +
				book.TextureLocalization.UncompletedCount +
				book.SpriteLocalizations.UncompletedCount +
				book.AudioLocalizations.UncompletedCount +
				book.VideoLocalizations.UncompletedCount +
				book.UnityObjectLocalizations.UncompletedCount;

			var status = "Completed";
			var statusIcon = EditorIcons.TestPassed;
			var statusTooltip = "All localizations are setup";
			if (isCompleted > 0)
			{
				status = "Uncompleted";
				statusIcon = EditorIcons.UnityWarningIcon;
				statusTooltip = StringUtils.Concat (
					isCompleted, " localizations left."
				);
			}

			EditorGUILayout.LabelField (
				"Status", status,
				SirenixGUIStyles.BoldLabel,
				GUILayout.Width (EditorGUIUtility.currentViewWidth - 50)
			);
			EditorGUILayout.LabelField (
				new GUIContent (statusIcon, statusTooltip),
				GUILayout.Width (18)
			);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
			SirenixEditorGUI.Title (
				title: "Localizations",
				subtitle: string.Empty,
				TextAlignment.Left,
				horizontalLine: true
			);
			
			
			if (_showAddMenu)
			{
				EditorGUILayout.Space ();
				_code = SirenixEditorFields.TextField ("Code", _code).ToCodeFormat ();
				_type = (LocalizationType) SirenixEditorFields.EnumDropdown (
					"Type", _type
				);

				EditorGUILayout.BeginHorizontal ();
				GUI.enabled =
					!book.ContainsLocalizedObject (_code) &&
					!string.IsNullOrWhiteSpace (_code);
				var cultures = LocalizationSettings.LanguagesCodes;
				if (GUILayout.Button ("Add"))
				{
					switch (_type)
					{
						case LocalizationType.TEXT:
							var localizedText = new LocalizedText ();
							foreach (var culture in cultures)
								localizedText.Add (culture, string.Empty);
							book.TextLocalizations.Add (_code, localizedText);
							break;
						case LocalizationType.TEXTURE:
							var localizedTexture = new LocalizedTexture ();
							foreach (var culture in cultures)
								localizedTexture.Add (culture, null);
							book.TextureLocalization.Add (_code, localizedTexture);
							break;
						case LocalizationType.SPRITE:
							var localizedSprite = new LocalizedSprite ();
							foreach (var culture in cultures)
								localizedSprite.Add (culture, null);
							book.SpriteLocalizations.Add (_code, localizedSprite);
							break;
						case LocalizationType.AUDIO:
							var localizedAudio = new LocalizedAudio ();
							foreach (var culture in cultures)
								localizedAudio.Add (culture, null);
							book.AudioLocalizations.Add (_code, localizedAudio);
							break;
						case LocalizationType.VIDEO:
							var localizedVideo = new LocalizedVideo ();
							foreach (var culture in cultures)
								localizedVideo.Add (culture, null);
							book.VideoLocalizations.Add (_code, localizedVideo);
							break;
						case LocalizationType.OBJECT:
							var localizedObject = new LocalizedUnityObject ();
							foreach (var culture in cultures)
								localizedObject.Add (culture, null);
							book.UnityObjectLocalizations.Add (_code, localizedObject);
							break;
					}

					_code = string.Empty;
				}
				GUI.enabled = true;
				
				if (GUILayout.Button ("Cancel"))
				{
					_code = string.Empty;
					_showAddMenu = false;
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
				SirenixEditorGUI.HorizontalLineSeparator (
					SirenixGUIStyles.LightBorderColor
				);
			}
			else
			{
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space (0f, true);
				_showAddMenu = SirenixEditorGUI.IconButton (
					EditorIcons.Plus, 14, 14
				);
				EditorGUILayout.EndHorizontal ();
			}
			
			EditorGUILayout.Space ();

			if (book != null)
			{
				if (book.TextLocalizations.Count > 0)
					DrawLocalizationGroup (ref _textVisible,
						book.TextLocalizations, "Texts");

				if (book.TextureLocalization.Count > 0)
					DrawLocalizationGroup (ref _textureVisible,
						book.TextureLocalization, "Textures");

				if (book.SpriteLocalizations.Count > 0)
					DrawLocalizationGroup (ref _spriteVisible,
						book.SpriteLocalizations, "Sprites");

				if (book.AudioLocalizations.Count > 0)
					DrawLocalizationGroup (ref _audioVisible,
						book.AudioLocalizations, "Audio Clips");

				if (book.VideoLocalizations.Count > 0)
					DrawLocalizationGroup (ref _videoVisible,
						book.VideoLocalizations, "Video Clips");

				if (book.UnityObjectLocalizations.Count > 0)
					DrawLocalizationGroup (ref _objectVisible,
						book.UnityObjectLocalizations, "Objects");
			}

			if (GUI.changed) EditorUtility.SetDirty (book);

			InspectorUtilities.EndDrawPropertyTree (tree);
			Tree.UpdateTree ();

			EditorGUILayout.Space ();
		}

		#endregion


		#region Class Implementation

		private static string _codeToModify;
		private static string _newCodeName;

		private void DrawLocalizationGroup<T> (
			ref bool isActive, ILocalizationGroup<T> group, string groupName
		)
		{
			//	Definition of layout.
			const int warningIconWidth = 18;
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

					/*
					//	Drawing status mark.
					if (!group[code].IsComplete (culture))
					{
						// TODO: Add tooltip for default culture.
							var isDefault = culture == 
								LocalizationSettings.Default;
						/

						var warningButtonContent = new GUIContent (
							image: EditorIcons.UnityWarningIcon,
							tooltip: string.Empty
						);

						GUILayout.Button (
							warningButtonContent,
							SirenixGUIStyles.Title,
							GUILayout.Width (warningIconWidth),
							GUILayout.Height (iconHeight)
						);
					}
					*/

					EditorGUI.indentLevel--;
					EditorGUILayout.EndHorizontal ();
					
				}
			}
			EditorGUI.indentLevel--;
			SirenixEditorGUI.EndLegendBox ();
			EditorGUILayout.Space();

		}

		#endregion
	}
}

