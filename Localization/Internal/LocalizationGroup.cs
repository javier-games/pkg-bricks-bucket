using System.Collections.Generic;
using System.Linq;
using BricksBucket.Core.Collections;


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
	
	/// <!-- ILocalizedGroup -->
	/// 
	/// <summary>
	/// Interface with fundamental methods for a group of Localized Objects
	/// stored by its code.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	internal interface ILocalizedGroup
	{

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
		/// Returns a localized object for the specific code.
		/// </summary>
		/// <param name="code">Code of the localized object.</param>
		/// <returns>Localized object at the specified code.</returns>
		ILocalizedObject GetLocalized (string code);

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
		// ReSharper disable once UnusedMethodReturnValue.Global
		bool Remove (string code);

		/// <summary>
		/// Updates the code for a localized in the localization group.
		/// </summary>
		/// <param name="oldCode">Old code to replace.</param>
		/// <param name="newCode">New code for de localized object.</param>
		void UpdateLocalizationCode (string oldCode, string newCode);

		/// <summary>
		/// Adds a new culture to each localized object in this group.
		/// </summary>
		/// <param name="code">Code of the culture to add.</param>
		void AddNewCulture (string code);

		/// <summary>
		/// Removes a culture for all the localized objects in the group.
		/// </summary>
		/// <param name="code"></param>
		void RemoveCulture (string code);

		/// <summary>
		/// Updates the culture to each localized object in the group.
		/// </summary>
		/// <param name="oldCode">Old code to change.</param>
		/// <param name="newCode">New code for de value.</param>
		void UpdateCulture (string oldCode, string newCode);
	}

	/// <!-- TextGroup -->
	/// 
	/// <summary>
	/// Group of localized string objects.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class TextGroup :
		SerializableDictionary<string, LocalizedText>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);

		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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

		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}

		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}

		/// <inheritdoc cref="object.ToString()"/>
		public override string ToString ()
		{
			var builder = new System.Text.StringBuilder ();
			builder.Append ("Text Group:\n");
			foreach (var localized in Keys)
			{
				builder.Append ("- ");
				builder.Append (localized);
				builder.Append (":\n");
				foreach (var culture in this[localized].Keys)
				{
					builder.Append (" - ");
					builder.Append (culture);
					builder.Append (": ");
					builder.Append (this[localized][culture]);
					builder.Append ("\n");
				}
			}

			return builder.ToString ();
		}
	}

	/// <!-- TextureGroup -->
	/// 
	/// <summary>
	/// Dictionary of localized textures.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class TextureGroup :
		SerializableDictionary<string, LocalizedTexture>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);
		
		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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

		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}

		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}
	}

	/// <!-- SpriteGroup -->
	/// 
	/// <summary>
	/// Dictionary of localized sprites.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class SpriteGroup :
		SerializableDictionary<string, LocalizedSprite>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);
		
		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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

		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}

		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}
	}

	/// <!-- AudioGroup -->
	/// 
	/// <summary>
	/// Dictionary of localized Audio Clips.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class AudioGroup :
		SerializableDictionary<string, LocalizedAudio>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);
		
		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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
		
		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}
		
		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}
	}

	/// <!-- VideoGroup -->
	/// 
	/// <summary>
	/// Dictionary of localized video clips.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class VideoGroup :
		SerializableDictionary<string, LocalizedVideo>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}
		
		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);
		
		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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

		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}

		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}
	}

	/// <!-- UnityObjectGroup -->
	/// 
	/// <summary>
	/// Dictionary of localized Unity Objects.
	/// </summary>
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	[System.Serializable]
	internal class UnityObjectGroup :
		SerializableDictionary<string, LocalizedUnityObject>, ILocalizedGroup
	{
		/// <inheritdoc cref="ILocalizedGroup.UncompletedCount"/>
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

		/// <inheritdoc cref="ILocalizedGroup.Codes"/>
		public string[] Codes => Keys.ToArray ();

		/// <inheritdoc cref="ILocalizedGroup.IsCompleted"/>
		public bool IsCompleted ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
				if (!keyValuePair.Value.IsCompleted ())
					isComplete = false;
			return isComplete;
		}

		/// <inheritdoc cref="ILocalizedGroup.ContainsLocalizedObject"/>
		public bool ContainsLocalizedObject (string code) => ContainsKey (code);
		
		/// <inheritdoc cref="ILocalizedGroup.GetLocalized"/>
		public ILocalizedObject GetLocalized (string code) =>
			ContainsLocalizedObject (code) ? this[code] : null;

		/// <inheritdoc cref="ILocalizedGroup.AddEmpty"/>
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

		///<inheritdoc cref="ILocalizedGroup.UpdateLocalizationCode"/>
		public void UpdateLocalizationCode (string oldCode, string newCode)
		{
			var localizedObject = this[oldCode];
			Remove (oldCode);
			Add (newCode, localizedObject);
		}

		/// <inheritdoc cref="ILocalizedGroup.AddNewCulture"/>
		public void AddNewCulture (string code)
		{
			foreach (var localizedObject in Values)
				localizedObject.AddEmpty (code);
		}

		///<inheritdoc cref="ILocalizedGroup.RemoveCulture"/>
		public void RemoveCulture (string code)
		{
			foreach (var localizedObject in Values)
				if (localizedObject.ContainsKey (code))
					localizedObject.Remove (code);
		}
		
		///<inheritdoc cref="ILocalizedGroup.UpdateCulture"/>
		public void UpdateCulture (string oldCode, string newCode)
		{
			foreach (var localizedObject in Values)
			{
				if (!localizedObject.ContainsCulture (oldCode)) continue;
				localizedObject.Add (newCode, localizedObject[oldCode]);
				localizedObject.Remove (oldCode);
			}
		}
	}
}