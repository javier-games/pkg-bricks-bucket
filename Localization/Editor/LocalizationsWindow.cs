using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace BricksBucket.Localization.Editor
{
	internal class LocalizationsWindow: OdinEditorWindow
	{
		private LocalizationType _dataType;

		private int _cultureMask;

		private int _activeTab;
		
		[SerializeField]
		private List<string> _cultureCodes
			= new List<string> ();

		[SerializeField]
		private List<string> _localizationCodes
			= new List<string> ();



		[MenuItem("Tools/Bricks Bucket/Localization/Localizations Editor")]
		private static void OpenWindow()
		{
			LocalizationSettings.InitializeLocalization ();
			GetWindow<LocalizationsWindow>().Show();
		}

		protected override void OnGUI ()
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			
			//	Drawing Cultures selector.
			SirenixEditorGUI.BeginBox ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Culture(s)", GUILayout.Width (100));
			_cultureMask = EditorGUILayout.MaskField (
				_cultureMask,
				LocalizationSettings.CulturesNames,
				GUILayout.Width (150)
			);
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Data Type", GUILayout.Width (100));
			_dataType = (LocalizationType) SirenixEditorFields.EnumDropdown (
				_dataType,
				GUILayout.Width (150)
			);
			EditorGUILayout.EndHorizontal ();
			SirenixEditorGUI.EndBox ();
			
			EditorGUILayout.Space(20);
			EditorGUILayout.EndHorizontal ();

			//	Drawing the toolbar.
			if (_cultureMask == 0)
			{
				SirenixEditorGUI.MessageBox (
					"Select at least one book and culture to edit.",
					MessageType.Warning
				);
				return;
			}
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			SirenixEditorGUI.BeginHorizontalToolbar ();
			for (int i = 0; i < LocalizationSettings.BooksNames.Length; i++)
			{
				if (SirenixEditorGUI.ToolbarToggle (_activeTab == i,
					LocalizationSettings.BooksNames[i]))
				{
					_activeTab = i;
				}
			}
			SirenixEditorGUI.EndHorizontalToolbar ();
			
			
			_cultureCodes.Clear ();
			for (int i = 0; i < LocalizationSettings.CulturesNames.Length; i++)
				if((_cultureMask & (1 << i)) > 0)
					_cultureCodes.Add (LocalizationSettings.CulturesCodes[i]);
			
			_localizationCodes.Clear ();
			var bookCode = LocalizationSettings.BooksCodes[_activeTab];
			var book = LocalizationSettings.GetBook (bookCode);
			string[] keys;
			switch (_dataType)
			{
				case LocalizationType.TEXT:
					keys = new string[book.TextGroup.Count];
					book.TextGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				case LocalizationType.TEXTURE:
					keys = new string[book.TextureGroup.Count];
					book.TextureGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				case LocalizationType.SPRITE:
					keys = new string[book.SpriteGroup.Count];
					book.SpriteGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				case LocalizationType.AUDIO:
					keys = new string[book.AudioGroup.Count];
					book.AudioGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				case LocalizationType.VIDEO:
					keys = new string[book.VideoGroup.Count];
					book.VideoGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				case LocalizationType.OBJECT:
					keys = new string[book.UnityObjectGroup.Count];
					book.UnityObjectGroup.Keys.CopyTo (keys, 0);
					for (int i = 0; i < keys.Length; i++)
						_localizationCodes.Add (keys[i]);
					break;
				default:
					throw new ArgumentOutOfRangeException ();
			}

			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (string.Empty, GUILayout.Width (150));
			for (int i = 0; i < _cultureCodes.Count; i++)
			{
				EditorGUILayout.LabelField (_cultureCodes[i], SirenixGUIStyles.Title , GUILayout.Width (150));
			}
			EditorGUILayout.EndHorizontal ();
			
			for (int i = 0; i < _localizationCodes.Count; i++)
			{
				
				EditorGUILayout.BeginHorizontal ();
				var localizationCode = _localizationCodes[i];
				EditorGUILayout.LabelField (_localizationCodes[i], SirenixGUIStyles.Title, GUILayout.Width (150));
				for (int j = 0; j < _cultureCodes.Count; j++)
				{
					var cultureCode = _cultureCodes[j];
					switch (_dataType)
					{
						case LocalizationType.TEXT:
							if(book.TextGroup.ContainsKey (localizationCode))
								book.TextGroup[localizationCode].Set (
									cultureCode,
									EditorGUILayout.TextField (
										book.TextGroup[localizationCode][cultureCode], GUILayout.Width (150)
									)
								);
							break;
						case LocalizationType.TEXTURE:
							if(book.TextureGroup.ContainsKey (localizationCode))
								book.TextureGroup[localizationCode].Set (
									cultureCode,
									SirenixEditorFields.UnityObjectField(
										book.TextureGroup[localizationCode][cultureCode],
										typeof(Texture),
										false,
										GUILayout.Width (150)
									) as Texture
								);
							break;
						case LocalizationType.SPRITE:
							break;
						case LocalizationType.AUDIO:
							break;
						case LocalizationType.VIDEO:
							break;
						case LocalizationType.OBJECT:
							break;
					}
				}
				EditorGUILayout.EndHorizontal ();
			}

			
			
			
			
		}
		
	}
}