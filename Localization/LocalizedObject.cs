using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Collections;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
#endif

// ReSharper disable UnusedMemberInSuper.Global
namespace BricksBucket.Localization
{
	/// <summary>
	/// 
	/// ILocalizedObject.
	/// 
	/// <para>
	/// Interface with fundamental methods for a Localized Object with a
	/// localized value for each different culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	/// <typeparam name="TValue">Type of value of the localization.</typeparam>
	internal interface ILocalizedObject<TValue>
	{
		/// <summary>
		/// Gets the localization value for the given culture.
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		TValue this [string culture] { get; }

		/// <summary>
		/// Total count of localizations on this localized object.
		/// </summary>
		int Count { get; }
		
		/// <summary>
		/// Array of all cultures in this localized object.
		/// </summary>
		string[] Cultures { get; }

		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the default value.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		bool IsComplete ();
		
		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		bool IsComplete (string culture);

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		bool ContainsCulture (string culture);

		/// <summary>
		/// Adds a new localization for the given culture to the localized
		/// object.
		/// </summary>
		/// <param name="culture">Name of the culture.</param>
		/// <param name="localization">Value of the localization.</param>
		void Add (string culture, TValue localization);

		/// <summary>
		/// Removes an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to remove.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and removed; otherwise, <value>False</value>.</returns>
		bool Remove (string culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, default value.</returns>
		TValue Get (string culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		bool TryGet (string culture, out TValue localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		bool Set (string culture, TValue localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		bool TrySet (string culture, object localization);

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		void DrawField (string culture, GUILayoutOption [] options);
#endif
	}

	/// <summary>
	/// 
	/// LocalizedText.
	/// 
	/// <para>
	/// Localized Object with a localized string values for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedText :
		SerializableDictionary<string, string>,
		ILocalizedObject<string>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from string empty or null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () =>
			!ContainsValue (string.Empty) &&
			!ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) =>
			!string.IsNullOrEmpty (this[culture]);

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise an empty string.</returns>
		public string Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: string.Empty;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out string localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, string localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			string value;
			try
			{
				value = (string) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				EditorGUILayout.DelayedTextField (this[culture], options)
			);
		}
#endif
	}

	/// <summary>
	/// 
	/// LocalizedTexture.
	/// 
	/// <para>
	/// Localized Object with a localized texture values for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedTexture :
		SerializableDictionary<string, Texture>,
		ILocalizedObject<Texture>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () => !ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) => this[culture] != null;

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, null.</returns>
		public Texture Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: null;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out Texture localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, Texture localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			Texture value;
			try
			{
				value = (Texture) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Texture),
					allowSceneObjects: false,
					options
				) as Texture
			);
		}
#endif
	}

	/// <summary>
	/// 
	/// LocalizedSprite.
	/// 
	/// <para>
	/// Localized Object with a localized sprite values for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedSprite :
		SerializableDictionary<string, Sprite>,
		ILocalizedObject<Sprite>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () => !ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) => this[culture] != null;

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, null.</returns>
		public Sprite Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: null;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out Sprite localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, Sprite localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			Sprite value;
			try
			{
				value = (Sprite) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Sprite),
					allowSceneObjects: false,
					options
				) as Sprite
			);
		}
#endif
	}

	/// <summary>
	/// 
	/// LocalizedAudio.
	/// 
	/// <para>
	/// Localized Object with a localized audio clip values for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedAudio :
		SerializableDictionary<string, AudioClip>,
		ILocalizedObject<AudioClip>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () => !ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) => this[culture] != null;

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, null.</returns>
		public AudioClip Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: null;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out AudioClip localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, AudioClip localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			AudioClip value;
			try
			{
				value = (AudioClip) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (AudioClip),
					allowSceneObjects: false,
					options
				) as AudioClip
			);
		}
#endif
	}

	/// <summary>
	/// 
	/// LocalizedVideo.
	/// 
	/// <para>
	/// Localized Object with a localized video clip values for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedVideo :
		SerializableDictionary<string, VideoClip>,
		ILocalizedObject<VideoClip>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () => !ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) => this[culture] != null;

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, null.</returns>
		public VideoClip Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: null;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out VideoClip localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, VideoClip localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			VideoClip value;
			try
			{
				value = (VideoClip) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (VideoClip),
					allowSceneObjects: false,
					options
				) as VideoClip
			);
		}
#endif
	}

	/// <summary>
	/// 
	/// LocalizedObject.
	/// 
	/// <para>
	/// Localized Object with a localized Unity.Object value for each different
	/// culture in the object.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedUnityObject :
		SerializableDictionary<string, Object>,
		ILocalizedObject<Object>
	{
		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Cultures => Keys.ToArray ();
		
		/// <summary>
		/// Defines whether for all this localized object cultures has a
		/// localization different from the null.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete () => !ContainsValue (null);

		/// <summary>
		/// Defines whether the localization for the specified culture
		/// is complete.
		/// </summary>
		/// <param name="culture">Culture to evaluate its localization.</param>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsComplete (string culture) => this[culture] != null;

		/// <summary>
		/// Defines whether this localized object has a localization for the
		/// given culture. 
		/// </summary>
		/// <param name="culture">Culture to look for.</param>
		/// <returns><value>True</value> if has the given culture.</returns>
		public bool ContainsCulture (string culture) => ContainsKey (culture);

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <returns>Value of the localization if the element is successfully
		/// found otherwise, null.</returns>
		public Object Get (string culture) =>
			ContainsKey (culture)
				? this[culture]
				: null;

		/// <summary>
		/// Gets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to look for.</param>
		/// <param name="localization">Value of the localization.</param>
		/// <returns><value>True</value> if the element is successfully found;
		/// otherwise, <value>False</value>.</returns>
		public bool TryGet (string culture, out Object localization) =>
			TryGetValue (culture, out localization);

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool Set (string culture, Object localization)
		{
			if (!ContainsKey (culture)) return false;
			this[culture] = localization;
			return true;
		}

		/// <summary>
		/// Sets the localization value in an existing culture.
		/// </summary>
		/// <param name="culture">Name of the culture to set its value.</param>
		/// <param name="localization">The new value for localization.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and set; otherwise, <value>False</value>.</returns>
		public bool TrySet (string culture, object localization)
		{
			if (!ContainsKey (culture)) return false;

			Object value;
			try
			{
				value = (Object) localization;
			}
			catch
			{
				return false;
			}

			this[culture] = value;
			return true;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Draws the field for the specified culture.
		/// </summary>
		/// <param name="culture">Culture Localization to draw.</param>
		/// <param name="options">Options for GUI.</param>
		public void DrawField (string culture, GUILayoutOption[] options)
		{
			Set (
				culture,
				SirenixEditorFields.UnityObjectField (
					this[culture],
					typeof (Object),
					allowSceneObjects: false,
					options
				)
			);
		}
#endif
	}
}