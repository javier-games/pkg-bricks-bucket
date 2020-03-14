using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
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

			if (book != null)
			{
				if (book.TextLocalizations.Count > 0)
					DrawTextLocalization (ref _textVisible);

				if (book.TextureLocalization.Count > 0)
					DrawTextureLocalization (ref _textureVisible);

				if (book.SpriteLocalizations.Count > 0)
					DrawSpriteLocalization (ref _spriteVisible);

				if (book.AudioLocalizations.Count > 0)
					DrawAudioLocalization (ref _audioVisible);

				if (book.VideoLocalizations.Count > 0)
					DrawVideoLocalization (ref _videoVisible);

				if (book.UnityObjectLocalizations.Count > 0)
					DrawUnityObjectLocalization (ref _objectVisible);
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

		private static void DrawSeparator ()
		{
			//	Draws an horizontal line separator.
			SirenixEditorGUI.HorizontalLineSeparator (
				SirenixGUIStyles.LightBorderColor
			);
		}

		private static bool BeginDrawCode<TLocalized, TValue> (
			ILocalizedCollection<TLocalized, TValue> collection,
			string code
		) where TLocalized : ILocalizedObject<TValue>
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
				collection.Remove (code);
				return false;
			}

			EditorGUILayout.LabelField (
				new GUIContent (code, code),
				CodeLayout
			);
			EditorGUILayout.BeginVertical ();

			return true;
		}

		private static void BeginDrawCulture (string culture)
		{

			EditorGUILayout.BeginHorizontal ();

			//	Culture Code Label.
			EditorGUILayout.LabelField (
				new GUIContent (culture, culture),
				CodeLayout
			);
		}

		private static void DrawWarning (string culture)
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

		private static void EndDrawCulture ()
		{
			EditorGUILayout.EndHorizontal ();
		}

		private static void EndDrawCode ()
		{
			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();
		}

		/// <summary>
		/// Draws a list of texts localizations.
		/// </summary>
		/// <param name="isActive">Foldout control.</param>
		private void DrawTextLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.TextLocalizations;

			//	Draws the foldout list.
			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Texts");
			if (isActive)
			{
				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{

					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							EditorGUILayout.DelayedTextField (
								value,
								FieldLayout
							);

						//	Drawing status mark.
						if (string.IsNullOrWhiteSpace (value))
							DrawWarning (culture);

						EndDrawCulture ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		private void DrawTextureLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.TextureLocalization;

			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Textures");
			if (isActive)
			{
				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{

					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							SirenixEditorFields.UnityObjectField (
								value,
								typeof (Texture),
								allowSceneObjects: false,
								options: FieldLayout
							) as Texture;

						//	Drawing status mark.
						if (localization[code][culture] == null)
							DrawWarning (culture);

						EndDrawCulture ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		private void DrawSpriteLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.SpriteLocalizations;

			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Sprites");
			if (isActive)
			{

				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{
					//	Code Label.
					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							SirenixEditorFields.UnityObjectField (
								value,
								typeof (Sprite),
								allowSceneObjects: false,
								options: FieldLayout
							) as Sprite;

						//	Drawing status mark.
						if (localization[code][culture] == null)
							DrawWarning (culture);

						EndDrawCulture ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		private void DrawAudioLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.AudioLocalizations;

			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Audio Clips");
			if (isActive)
			{

				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{
					//	Code Label.
					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							SirenixEditorFields.UnityObjectField (
								value,
								typeof (AudioClip),
								allowSceneObjects: false,
								options: FieldLayout
							) as AudioClip;

						//	Drawing status mark.
						if (localization[code][culture] == null)
							DrawWarning (culture);

						EndDrawCulture ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		private void DrawVideoLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.VideoLocalizations;

			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Video Clips");
			if (isActive)
			{

				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{
					//	Code Label.
					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							SirenixEditorFields.UnityObjectField (
								value,
								typeof (VideoClip),
								allowSceneObjects: false,
								options: FieldLayout
							) as VideoClip;

						//	Drawing status mark.
						if (localization[code][culture] == null)
							DrawWarning (culture);

						EndDrawCulture ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		private void DrawUnityObjectLocalization (ref bool isActive)
		{
			var book = this.target as Book;
			if (book == null) return;
			var localization = book.UnityObjectLocalizations;

			SirenixEditorGUI.BeginBox (GUILayout.Width (Width));
			isActive = EditorGUILayout.Foldout (isActive, "Unity Objects");
			if (isActive)
			{

				DrawSeparator ();

				//	Draws for each localized object.
				var codes = localization.Keys.ToArray ();
				foreach (string code in codes)
				{
					//	Code Label.
					if (!BeginDrawCode (localization, code)) continue;

					//	Draws each culture and its value for localization.
					var cultures = localization[code].Keys.ToArray ();
					foreach (string culture in cultures)
					{
						BeginDrawCulture (culture);

						//	Value Field.
						var value = localization[code][culture];
						localization[code][culture] =
							SirenixEditorFields.UnityObjectField (
								value,
								typeof (Object),
								allowSceneObjects: false,
								options: FieldLayout
							);

						//	Drawing status mark.
						if (localization[code][culture] == null)
							DrawWarning (culture);

						EditorGUILayout.EndHorizontal ();
					}

					EndDrawCode ();
				}

				EditorGUILayout.Space ();
			}

			SirenixEditorGUI.EndBox ();
		}

		#endregion
	}
}

