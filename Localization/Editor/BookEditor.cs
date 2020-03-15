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
				book.TextLocalizations.IncompleteCount +
				book.TextureLocalization.IncompleteCount +
				book.SpriteLocalizations.IncompleteCount +
				book.AudioLocalizations.IncompleteCount +
				book.VideoLocalizations.IncompleteCount +
				book.UnityObjectLocalizations.IncompleteCount;

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
				new GUIContent(statusIcon, statusTooltip),
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

			/*
			if (GUILayout.Button (".."))
			{
				book.AudioLocalizations.Add ("APP", new LocalizedAudio ());
				book.AudioLocalizations["APP"].Add ("A", null);;
				book.AudioLocalizations["APP"].Add ("B", null);
			}
			*/

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
		
		private void DrawLocalizationGroup<T> (
			ref bool isActive, ILocalizationGroup<T> group, string groupName
		)
		{
			//	Definition of layout.
			const int warningIconWidth = 18;
			const int removeIconWidth = 14;
			const int iconHeight = 20;
			
			var boxWidth = EditorGUIUtility.currentViewWidth - 24;
			var width = boxWidth - warningIconWidth - removeIconWidth - 28;
			var codeLayout = GUILayout.Width (width * 0.4f);
			var cultureLayout = GUILayout.Width (width * 0.2f);
			var fieldLayout = GUILayout.Width (width * 0.4f);

			//	Getting the reference from the target.
			var book = this.target as Book;
			if (book == null) return;
			
			//	Draws the foldout list.
			SirenixEditorGUI.BeginBox (GUILayout.Width (boxWidth));
			
			EditorGUILayout.BeginHorizontal ();
			isActive = EditorGUILayout.Foldout (isActive, groupName);

			
			var completedText = group.IncompleteCount > 0
				? StringUtils.Concat (group.IncompleteCount, " Left")
				: "Complete";
			
			EditorGUILayout.LabelField (
				new GUIContent(completedText),
				SirenixGUIStyles.RightAlignedGreyMiniLabel
			);
			EditorGUILayout.EndHorizontal ();
			
			
			//	If is true draws the list.
			if (isActive)
			{
				//	Draws an horizontal line separator.
				SirenixEditorGUI.HorizontalLineSeparator (
					SirenixGUIStyles.LightBorderColor
				);

				//	Draws for each localized object.
				foreach (string code in group.Codes)
				{

					//	Code Label.
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (
						EditorIcons.X.Inactive,
						SirenixGUIStyles.Title,
						GUILayout.Width (removeIconWidth),
						GUILayout.Height (iconHeight)
					))
					{
						group.Remove (code);
						continue;
					}

					EditorGUILayout.LabelField (
						new GUIContent (code, code),
						codeLayout
					);
					EditorGUILayout.BeginVertical ();

					//	Draws each culture and its value for localization.
					foreach (string culture in group[code].Cultures)
					{
						EditorGUILayout.BeginHorizontal ();

						//	Culture Code Label.
						EditorGUILayout.LabelField (
							new GUIContent (culture, culture),
							cultureLayout
						);

						group[code].DrawField (culture, new[] {fieldLayout});

						//	Drawing status mark.
						if (!group[code].IsComplete (culture))
						{
							/* TODO: Add tooltip for default culture.
								var isDefault = culture == 
									LocalizationSettings.Default;
							*/
							
							var warningButtonContent = new GUIContent(
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

						EditorGUILayout.EndHorizontal ();
					}

					EditorGUILayout.EndVertical ();
					EditorGUILayout.EndHorizontal ();
				}

				EditorGUILayout.Space ();
			}


			SirenixEditorGUI.EndBox ();
		}

		#endregion
	}
}

