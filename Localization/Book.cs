using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine.Video;

#endif

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
		private TextLocalizations _textLocalizations;

		[SerializeField]
		private TextureLocalizations _textureLocalizations;

		[SerializeField]
		private SpriteLocalizations _spriteLocalizations;

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

		private TextLocalizations TextLocalizations
		{
			get => _textLocalizations;
			set => _textLocalizations = value;
		}

		private TextureLocalizations TextureLocalization
		{
			get => _textureLocalizations;
			set => _textureLocalizations = value;
		}

		private SpriteLocalizations SpriteLocalizations
		{
			get => _spriteLocalizations;
			set => _spriteLocalizations = value;
		}

		private AudioLocalizations AudioLocalizations
		{
			get => _audioLocalizations;
			set => _audioLocalizations = value;
		}

		private VideoLocalizations VideoLocalizations
		{
			get => _videoLocalizations;
			set => _videoLocalizations = value;
		}

		private UnityObjectLocalizations UnityObjectLocalizations
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

		#endregion



		#region Editor

#if UNITY_EDITOR

		#region Editor Methods

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

		#endregion

		#region Drawer

		/// <summary>
		/// 
		/// Book Editor.
		///
		/// Draws the book nicely.
		/// </summary>
		[CustomEditor (typeof (Book))]
		private class BookEditor : OdinEditor
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

					if (book.TextLocalizations.Count > 0)
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
			
			

			private static float Width =>
				EditorGUIUtility.currentViewWidth - 23;

			private static GUILayoutOption CodeLayout =>
				GUILayout.Width (((Width - 20) * 0.3f) - IconWidth );

			private static GUILayoutOption FieldLayout =>
				GUILayout.Width ((Width - 20) * 0.4f);

			private const int IconWidth = 18;
			private const int Space = 14;
			private const int IconHeight = 20;

			private static void DrawColumnTitles ()
			{
			}

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
					//	Draws an horizontal line separator.
					SirenixEditorGUI.HorizontalLineSeparator (
						SirenixGUIStyles.LightBorderColor
					);
					
					//	Draws for each localized object.
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Button (
							EditorIcons.X.Inactive,
							SirenixGUIStyles.Title,
							GUILayout.Width (Space),
							GUILayout.Height (IconHeight)
						);
						EditorGUILayout.LabelField (
							new GUIContent (code, code),
							CodeLayout
						);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (
								new GUIContent (culture, culture),
								CodeLayout
							);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								EditorGUILayout.DelayedTextField (
									value,
									FieldLayout
								);

							//	Drawing status mark.
							if (string.IsNullOrWhiteSpace (value))
							{
								/* TODO: Add tooltip for default culture.
									var isDefault = culture == 
										LocalizationSettings.Default;
								*/
								GUILayout.Button (
									EditorIcons.UnityWarningIcon,
									SirenixGUIStyles.Title,
									GUILayout.Width (IconWidth),
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
					DrawColumnTitles ();
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (code, CodeLayout);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (culture, CodeLayout);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								SirenixEditorFields.UnityObjectField (
									value,
									typeof (Texture),
									allowSceneObjects: false,
									options: FieldLayout
								) as Texture;

							EditorGUILayout.EndHorizontal ();
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
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
					DrawColumnTitles ();
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (code, CodeLayout);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (culture, CodeLayout);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								SirenixEditorFields.UnityObjectField (
									value,
									typeof (Sprite),
									allowSceneObjects: false,
									options: FieldLayout
								) as Sprite;

							EditorGUILayout.EndHorizontal ();
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
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
					DrawColumnTitles ();
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (code, CodeLayout);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (culture, CodeLayout);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								SirenixEditorFields.UnityObjectField (
									value,
									typeof (AudioClip),
									allowSceneObjects: false,
									options: FieldLayout
								) as AudioClip;

							EditorGUILayout.EndHorizontal ();
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
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
					DrawColumnTitles ();
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (code, CodeLayout);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (culture, CodeLayout);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								SirenixEditorFields.UnityObjectField (
									value,
									typeof (VideoClip),
									allowSceneObjects: false,
									options: FieldLayout
								) as VideoClip;

							EditorGUILayout.EndHorizontal ();
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
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
					DrawColumnTitles ();
					var codes = localization.Keys.ToArray ();
					foreach (string code in codes)
					{
						//	Code Label.
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (code, CodeLayout);
						EditorGUILayout.BeginVertical ();

						//	Draws each culture and its value for localization.
						var cultures = localization[code].Keys.ToArray ();
						foreach (string culture in cultures)
						{
							EditorGUILayout.BeginHorizontal ();

							//	Culture Code Label.
							EditorGUILayout.LabelField (culture, CodeLayout);

							//	Value Field.
							var value = localization[code][culture];
							localization[code][culture] =
								SirenixEditorFields.UnityObjectField (
									value,
									typeof (Object),
									allowSceneObjects: false,
									options: FieldLayout
								);

							EditorGUILayout.EndHorizontal ();
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
					}

					EditorGUILayout.Space ();
				}

				SirenixEditorGUI.EndBox ();
			}
		}

		#endregion

#endif

		#endregion

	}
}