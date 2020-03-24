using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Collections;


// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMemberInSuper.Global
namespace BricksBucket.Localization.Internal
{
	/// <summary>
	/// 
	/// ILocalizationGroup.
	/// 
	/// <para>
	/// Interface with fundamental methods for a group of Localized Objects
	/// stored by its code.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	/// <typeparam name="T">Type of value of the localization.</typeparam>
	internal interface ILocalizationGroup<T>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		ILocalizedObject<T> this [string code] { get; }

		/// <summary>
		/// Total count of localized objects in the group.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		int UncompletedCount { get; }

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		string[] Codes { get; }

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		bool IsCompleted ();

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		bool ContainsLocalizedObject (string code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		void Add (string code, ILocalizedObject<T> localizedObject);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		string[] AddEmpty (string code, params string[] cultures);

		/// <summary>
		/// Removes an existing localized object.
		/// </summary>
		/// <param name="code">Localized Object Code to remove.</param>
		/// <returns><value>True</value> if the element is successfully found
		/// and removed; otherwise, <value>False</value>.</returns>
		bool Remove (string code);
	}

	/// <summary>
	/// 
	/// TextGroup.
	/// 
	/// <para>
	/// Group of localized string objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class TextGroup :
		SerializableDictionary<string, LocalizedText>,
		ILocalizationGroup<string>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<string> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void
			Add (string code, ILocalizedObject<string> localizedObject) =>
			base.Add (code, localizedObject as LocalizedText);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedText ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}

	/// <summary>
	/// 
	/// TextureGroup.
	/// 
	/// <para>
	/// Dictionary of localized textures.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class TextureGroup :
		SerializableDictionary<string, LocalizedTexture>,
		ILocalizationGroup<Texture>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<Texture> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void
			Add (string code, ILocalizedObject<Texture> localizedObject) =>
			base.Add (code, localizedObject as LocalizedTexture);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedTexture ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}

	/// <summary>
	/// 
	/// Sprite Localizations.
	/// 
	/// <para>
	/// Dictionary of localized sprites.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class SpriteGroup :
		SerializableDictionary<string, LocalizedSprite>,
		ILocalizationGroup<Sprite>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<Sprite> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void
			Add (string code, ILocalizedObject<Sprite> localizedObject) =>
			base.Add (code, localizedObject as LocalizedSprite);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedSprite ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}

	/// <summary>
	/// 
	/// Audio Localizations.
	/// 
	/// <para>
	/// Dictionary of localized Audio Clips.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class AudioLocalizations :
		SerializableDictionary<string, LocalizedAudio>,
		ILocalizationGroup<AudioClip>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<AudioClip> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void Add (
			string code, ILocalizedObject<AudioClip> localizedObject
		) =>
			base.Add (code, localizedObject as LocalizedAudio);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedAudio ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}

	/// <summary>
	/// 
	/// Video Localizations.
	/// 
	/// <para>
	/// Dictionary of localized video clips.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class VideoLocalizations :
		SerializableDictionary<string, LocalizedVideo>,
		ILocalizationGroup<VideoClip>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<VideoClip> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void Add (
			string code, ILocalizedObject<VideoClip> localizedObject
		) =>
			base.Add (code, localizedObject as LocalizedVideo);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedVideo ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}

	/// <summary>
	/// 
	/// Unity Object Localizations.
	/// 
	/// <para>
	/// Dictionary of localized Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class UnityObjectLocalizations :
		SerializableDictionary<string, LocalizedUnityObject>,
		ILocalizationGroup<Object>
	{
		/// <summary>
		/// Gets the localization object for the given code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		public new ILocalizedObject<Object> this [string code] => base[code];

		/// <summary>
		/// Total count of localized object that are incomplete.
		/// </summary>
		public int UncompletedCount
		{
			get
			{
				int incompleteCount = 0;
				var codes = Codes;
				for (int i = 0; i < codes.Length; i++)
					incompleteCount += this[codes[i]].UncompletedCount;
				return incompleteCount;
			}
		}

		/// <summary>
		/// Array of all codes in this collection.
		/// </summary>
		public string[] Codes => Keys.ToArray ();

		/// <summary>
		/// Defines whether all localized objects are complete.
		/// </summary>
		/// <returns><value>True</value> if is complete.</returns>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <summary>
		/// Determines whether this group has a localized object for the
		/// given culture. 
		/// </summary>
		/// <param name="code">Localized Object Code to look for.</param>
		/// <returns><value>True</value> if has the given code.</returns>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <summary>
		/// Adds a new localized object with the given code.
		/// </summary>
		/// <param name="code">Localized Object Code to use.</param>
		/// <param name="localizedObject">Localized Object to add.</param>
		public void
			Add (string code, ILocalizedObject<Object> localizedObject) =>
			base.Add (code, localizedObject as LocalizedUnityObject);

		/// <summary>
		/// Adds a new localized object with empty localizations to
		/// the given codes.
		/// </summary>
		/// <param name="code">Code of the localized object to add.</param>
		/// <param name="cultures">Cultures to add.</param>
		public string[] AddEmpty (string code, params string[] cultures)
		{
			List<string> missingCultures = new List<string> ();
			var localizedObject = new LocalizedUnityObject ();
			foreach (var culture in cultures)
				if (!localizedObject.AddEmpty (culture))
					missingCultures.Add (culture);
			Add (code, localizedObject);
			return missingCultures.ToArray ();
		}
	}
}