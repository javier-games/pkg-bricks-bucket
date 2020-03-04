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
	public struct Book : System.IComparable
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
		[TextArea]
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



		#region Methods
		
		/// <summary>
		/// Compares to an object.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>Value of comparision.</returns>
		public int CompareTo (object obj)
		{
			var book = (Book) obj;
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
						? value.Code
						: label.text;
				}
				else
				{
					label = new GUIContent (value.Name, value.Code);
				}
				
				_isVisible = SirenixEditorGUI.Foldout(
					_isVisible,
					label,
					SirenixGUIStyles.Foldout
				);

				if (_isVisible)
				{
					EditorGUI.indentLevel++;

					var children = ValueEntry.Property.Children;

					GUI.enabled = false;
					EditorGUILayout.LabelField (
						new GUIContent(
							"Code", "Code to Identify language category."
						),
						new GUIContent (value.Code),
						EditorStyles.textField
					);
					GUI.enabled = true;
					children.Get ("_name").Draw ();
					children.Get ("_description").Draw ();
					EditorGUI.indentLevel--;
					EditorGUILayout.Space ();
				}

				ValueEntry.SmartValue = value;
			}

			#endregion
			
		}
		
		#endregion
		
#endif

		#endregion
	}
}