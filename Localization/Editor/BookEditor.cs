using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace BricksBucket.Localization.Editor
{
	/// <summary>
	/// 
	/// Book Editor.
	///
	/// Draws the book nicely.
	/// </summary>
	[CustomEditor (typeof (Book))]
	public class BookEditor : OdinEditor
	{
		private bool _textVisible;
		private bool _textureVisible;
		private bool _spriteVisible;
		private bool _audioVisible;
		private bool _videoVisible;
		private bool _objectVisible;

		#region Override Methods

		public override void OnInspectorGUI ()
		{
			// Like this.serializedObject.
			var tree = this.Tree;
			var book = this.target as Book;

			InspectorUtilities.BeginDrawPropertyTree (tree, true);

			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_code").Draw ();

			EditorGUILayout.Space ();
			tree.GetPropertyAtPath ("_name").Draw ();
			tree.GetPropertyAtPath ("_description").Draw ();

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

			InspectorUtilities.EndDrawPropertyTree (tree);
			Tree.UpdateTree ();
		}

		#endregion


		#region List Drawers

		/// <summary>
		/// Boxed Width
		/// </summary>
		private static float Width => EditorGUIUtility.currentViewWidth - 23;

		/// <summary>
		/// GUI Layout option for code label fields.
		/// </summary>
		private static GUILayoutOption CodeLayout =>
			GUILayout.Width (((Width - 20) * 0.3f) - WarningIconWidth);

		/// <summary>
		/// GUI Layout option for object fields.
		/// </summary>
		private static GUILayoutOption FieldLayout =>
			GUILayout.Width ((Width - 20) * 0.4f);

		/// <summary>
		/// Warning Button Icon Width.
		/// </summary>
		private const int WarningIconWidth = 18;

		/// <summary>
		/// Remove Button Icon Width
		/// </summary>
		private const int RemoveIconWidth = 14;

		/// <summary>
		/// General Icon Height.
		/// </summary>
		private const int IconHeight = 20;

		private void DrawLocalizationGroup<T> (
			ref bool isActive, ILocalizationGroup<T> group, string groupName
		)
		{
			var book = this.target as Book;
			if (book == null) return;

			//	Draws the foldout list.
			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, groupName);
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
						GUILayout.Width (RemoveIconWidth),
						GUILayout.Height (IconHeight)
					))
					{
						group.Remove (code);
						continue;
					}

					EditorGUILayout.LabelField (
						new GUIContent (code, code),
						CodeLayout
					);
					EditorGUILayout.BeginVertical ();

					//	Draws each culture and its value for localization.
					foreach (string culture in group[code].Cultures)
					{
						EditorGUILayout.BeginHorizontal ();

						//	Culture Code Label.
						EditorGUILayout.LabelField (
							new GUIContent (culture, culture),
							CodeLayout
						);

						group[code].DrawField (culture,new []{FieldLayout});

						//	Drawing status mark.
						if (!group[code].IsComplete (culture))
						{
							/* TODO: Add tooltip for default culture.
								var isDefault = culture == 
									LocalizationSettings.Default;
							*/
							GUILayout.Button (
								EditorIcons.UnityWarningIcon,
								SirenixGUIStyles.Title,
								GUILayout.Width (WarningIconWidth),
								GUILayout.Height (IconHeight)
							);
						}

						EditorGUILayout.EndHorizontal ();
					}

					EditorGUILayout.EndVertical ();
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Space ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
			
			if (GUI.changed)
			{
				EditorUtility.SetDirty(book);
			}
		}

		#endregion
	}
}

