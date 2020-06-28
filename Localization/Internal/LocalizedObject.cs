using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Core.Collections;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif


// ReSharper disable UnusedMemberInSuper.Global
namespace BricksBucket.Localization.Internal
{
	
	/*
	 * WARNING:
	 * 
	 * Technically should be better to create an abstract class to implement
	 * some methods that are repeated in this script but the Unity
	 * serialization system avoids to show on inspector serialized fields in
	 * a class that inherits from an abstract class that inherits from a
	 * dictionary.
	 * 
	 */

	/// <summary>
	/// 
	/// <!-- ILocalizedObject -->
	/// 
	/// Interface with fundamental methods for a Localized Object with a
	/// localized value for each different culture in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	internal interface ILocalizedObject
	{
		/// <summary>
		/// Total count of localizations on this localized object.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Total count of cultures with a default value for it's localization.
		/// </summary>
		int UncompletedCount { get; }

		/// <summary>
		/// Array of all cultures in this localized object.
		/// </summary>
		string[] Cultures { get; }

		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the default value.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		bool IsCompleted ();

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		bool IsCompleted (string culture);

		/// <summary>
		/// Gets a localization for the requested culture.
		/// </summary>
		/// <param name="culture">Code of the culture to retrieve.</param>
		/// <returns>Localization value for the requested culture.</returns>
		object GetLocalization (string culture);

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		bool ContainsCulture (string culture);

		/// <summary>
		/// Adds a new localization to the localized object. 
		/// </summary>
		/// <param name="culture">Culture to add.</param>
		/// <returns><value>True</value> if the culture has
		/// been added.</returns>
		bool AddEmpty (string culture);

		/// <summary>
		/// Removes an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to remove.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and removed; otherwise, <value>False</value>.</returns>
		bool Remove (string culture);

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="drawLabel">Whether to draw label.</param>
		/// <param name="options">Options for GUI.</param>
		void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		);
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedText -->
	/// 
	/// Localized Object with string values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedText :
		SerializableDictionary<string, string>,
		ILocalizedObject
	{
		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () =>
			!ContainsValue (string.Empty) &&
			!ContainsValue (null);


		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) =>
			!string.IsNullOrEmpty (this[culture]);

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, string.Empty);
			return true;
		}

#if UNITY_EDITOR
		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.TextField (
					culture,
					this[culture],
					options
				);
			else
				this[culture] = SirenixEditorFields.TextField (
					this[culture],
					options
				);
		}
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedTexture -->
	/// 
	/// Localized Object with Texture values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedTexture :
		SerializableDictionary<string, Texture>,
		ILocalizedObject
	{

		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () => !ContainsValue (null);

		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) => this[culture] != null;

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, null);
			return true;
		}

#if UNITY_EDITOR
		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.UnityObjectField (
					culture,
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as Texture;
			else
				this[culture] = SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as Texture;
		}
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedSprite -->
	/// 
	/// Localized Object with Sprite values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedSprite :
		SerializableDictionary<string, Sprite>,
		ILocalizedObject
	{

		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () => !ContainsValue (null);

		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) => this[culture] != null;

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, null);
			return true;
		}

#if UNITY_EDITOR
		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.UnityObjectField (
					culture,
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as Sprite;
			else
				this[culture] = SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as Sprite;
				
		}
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedAudio -->
	/// 
	/// Localized Object with Audio Clip values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedAudio :
		SerializableDictionary<string, AudioClip>,
		ILocalizedObject
	{

		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () => !ContainsValue (null);

		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) => this[culture] != null;

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, null);
			return true;
		}

#if UNITY_EDITOR
		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.UnityObjectField (
					culture,
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as AudioClip;
			else
				this[culture] = SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as AudioClip;
		}
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedVideo -->
	/// 
	/// Localized Object with Video Clip values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedVideo :
		SerializableDictionary<string, VideoClip>,
		ILocalizedObject
	{

		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () => !ContainsValue (null);

		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) => this[culture] != null;

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, null);
			return true;
		}

#if UNITY_EDITOR
		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.UnityObjectField (
					culture,
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as VideoClip;
			else
				this[culture] = SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as VideoClip;
		}
#endif
	}

	/// <summary>
	/// 
	/// <!-- LocalizedObject -->
	/// 
	/// Localized Object with Object values for each different culture
	/// in the object.
	/// 
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class LocalizedUnityObject :
		SerializableDictionary<string, Object>,
		ILocalizedObject
	{

		/// <inheritdoc cref="ILocalizedObject.UncompletedCount"/>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var cultures = Cultures;
				for (int i = 0; i < cultures.Length; i++)
					if (!IsCompleted (cultures[i]))
						incompleteCount++;
				return incompleteCount;
			}
		}

		/// <inheritdoc cref="ILocalizedObject.Cultures"/>
		public string[] Cultures => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedObject.IsCompleted()"/>
		public bool IsCompleted () => !ContainsValue (null);

		/// <inheritdoc cref="ILocalizedObject.IsCompleted(string)"/>
		public bool IsCompleted (string culture) => this[culture] != null;

		/// <inheritdoc cref="ILocalizedObject.ContainsCulture(string)"/>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		///<inheritdoc cref="ILocalizedObject.GetLocalization"/>
		public object GetLocalization (string culture) =>
			!ContainsCulture (culture) ? null : this[culture];

		/// <inheritdoc cref="ILocalizedObject.AddEmpty(string)"/>
		public bool AddEmpty (string culture)
		{
			if (ContainsKey (culture)) return false;
			Add (culture, null);
			return true;
		}

#if UNITY_EDITOR

		/// <inheritdoc cref=
		/// "ILocalizedObject.DrawField(string, bool, GUILayoutOption[])"/>
		public void DrawField (
			string culture, bool drawLabel = true,
			GUILayoutOption[] options = null
		)
		{
			if (!ContainsKey (culture)) return;
			if (drawLabel)
				this[culture] = SirenixEditorFields.UnityObjectField (
					culture,
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				);
			else
				this[culture] = SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				);
		}
#endif
	}
}