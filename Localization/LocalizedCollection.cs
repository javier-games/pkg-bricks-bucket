using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Collections;

namespace BricksBucket.Localization
{
	
	internal interface ILocalizedCollection<TLocalized, TValue> where
	TLocalized : ILocalizedObject<TValue>
	{
		void Add (string key, TLocalized value);
		bool Remove (string key);
		bool IsComplete ();
		bool TryGetValue (string key, out TLocalized value);
	}
	
	/// <summary>
	/// 
	/// Text Localizations.
	/// 
	/// <para>
	/// Dictionary of localized texts.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedTextCollection :
		SerializableDictionary<string, LocalizedText>,
		ILocalizedCollection<LocalizedText, string>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
		}
	}

	/// <summary>
	/// 
	/// Texture Localizations.
	/// 
	/// <para>
	/// Dictionary of localized textures.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class TextureLocalizations :
		SerializableDictionary<string, LocalizedTexture>,
		ILocalizedCollection<LocalizedTexture, Texture>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
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
	internal class SpriteLocalizations :
		SerializableDictionary<string, LocalizedSprite>,
		ILocalizedCollection<LocalizedSprite, Sprite>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
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
		ILocalizedCollection<LocalizedAudio, AudioClip>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
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
		ILocalizedCollection<LocalizedVideo, VideoClip>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
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
		ILocalizedCollection<LocalizedUnityObject, Object>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ()) isComplete = false;
			}

			return isComplete;
		}
	}
}