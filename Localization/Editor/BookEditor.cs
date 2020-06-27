using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using BricksBucket.Localization.Internal;

namespace BricksBucket.Localization.Editor
{
	/// <!-- BookEditor -->
	/// 
	/// <summary>
	/// Custom editor for the scriptable object <see cref="Book"/>.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[CustomEditor (typeof (Book))]
	internal class BookEditor : OdinEditor
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


		#region Static Properties

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
		/// GUI content label for Name of the book.
		/// </summary>
		private readonly GUIContent _nameLabel = new GUIContent (
			"Name", "Name of the book."
		);

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

		/// <inheritdoc cref="Editor.OnInspectorGUI"/>
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
			EditorGUI.BeginChangeCheck ();
			var nameAttempt = SirenixEditorFields.DelayedTextField (
				label: _nameLabel,
				value: book.Name
			);
			var codeAttempt = nameAttempt.ToCodeFormat ();
			var isValidName =
				!string.IsNullOrWhiteSpace (codeAttempt) &&
				!LocalizationSettings.Instance.ContainsBook (codeAttempt);
			if (isValidName) book.Name = nameAttempt;
			if (EditorGUI.EndChangeCheck () && isValidName)
			{
				var oldReference = book.Code;
				book.name = nameAttempt;
				book.Name = nameAttempt;
				book.Code = codeAttempt;
				LocalizationSettings.Instance.AddBook (book.Code, book);
				LocalizationSettings.Instance.RemoveBook (oldReference);

				if (GUI.changed) EditorUtility.SetDirty (book);
				InspectorUtilities.EndDrawPropertyTree (tree);
				Tree.UpdateTree ();

				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();
				return;
			}

			tree.GetPropertyAtPath ("_description").Draw ();
			EditorGUILayout.Space ();
			
			//	Draws the Status.
			EditorGUILayout.BeginHorizontal ();
			if (book.IsCompleted)
			{
				_statusIcon.image = EditorIcons.TestPassed;
				_statusIcon.tooltip = StatusCompletedTooltip;
				_statusValue.text = StatusCompleted;
				_statusValue.tooltip = StatusCompletedTooltip;
			}
			else
			{
				var statusTooltip = string.Format (
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
			
			//	Localization Button.
			EditorGUILayout.Space ();
			if (GUILayout.Button ("Edit Localization"))
			{
				var bookIndex = 0;
				for (int i = 0; i < LocalizationSettings.Books.Length; i++)
					if (LocalizationSettings.Books[i] == book)
						bookIndex = i;
				LocalizationSettings.WindowActiveBook = bookIndex;
				LocalizationsWindow.OpenWindow ();
			}
			
			//	End drawing.
			if (GUI.changed) EditorUtility.SetDirty (book);
			InspectorUtilities.EndDrawPropertyTree (tree);
			Tree.UpdateTree ();

			EditorGUILayout.Space ();
		}

		#endregion
	}
}

