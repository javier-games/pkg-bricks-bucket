using UnityEngine;
using Sirenix.OdinInspector;


#if UNITY_EDITOR

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif


namespace BricksBucket.Localization
{
	/// <summary>
	/// 
	/// Book
	///
	/// <para>
	/// Struct with information related to books of localization.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	public struct Book
	{


		
		#region Fields

		/// <summary>
		/// Name of the book.
		/// </summary>
		[SerializeField]
		[Tooltip ("Name of the book.")]
		[OnValueChanged("OnNameChanged")]
		private string _name;

		/// <summary>
		/// What does this book is for.
		/// </summary>
		[SerializeField]
		[Tooltip ("What does this book is for.")]
		private string _description;

		/// <summary>
		/// Code to identify this book.
		/// </summary>
		[SerializeField]
		[Tooltip ("Code to identify this book.")]
		private string _code;

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

		#endregion


		
		#region Editor

#if UNITY_EDITOR

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
		
		/// <summary>
		/// Book Drawer.
		///
		/// Draws the book nicely.
		/// </summary>
		private class BookDrawer : OdinValueDrawer<Book>
		{
			
			
			#region Fields

			/// <summary>
			/// Whether the foldout is visible.
			/// </summary>
			private bool _isVisible;

			#endregion

			
			#region Override Methods

			/// <summary>
			/// Draws the property.
			/// </summary>
			/// <param name="label">Label of the property.</param>
			protected override void DrawPropertyLayout (GUIContent label)
			{
				var value = ValueEntry.SmartValue;

				//  Draws the label on Foldout.
				if (label != null)
				{
					label.text = string.IsNullOrEmpty (label.text)
						? StringUtils.Concat (
							value.Code,
							" [", value.Name, "]"
						)
						: label.text;
				}
				else
				{
					label = new GUIContent (
						value.Name,
						tooltip: "Book of localizations."
					);
				}
				
				_isVisible = SirenixEditorGUI.Foldout(
					_isVisible,
					label,
					SirenixGUIStyles.Foldout
				);

				if (!_isVisible) return;
				
				EditorGUI.indentLevel++;
					
				var children = ValueEntry.Property.Children;
					
				EditorGUILayout.LabelField(
					new GUIContent("Code", "Code to identify this Book"),
					new GUIContent(value.Code)
				);
					
				children.Get("_name").Draw();
				children.Get("_description").Draw();
					
				EditorGUI.indentLevel--;

			}

			#endregion
			
		}
		
#endif

		#endregion
	}
}