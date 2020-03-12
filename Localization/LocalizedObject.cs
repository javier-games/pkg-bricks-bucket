using UnityEngine;
using UnityEngine.Video;
using BricksBucket.Collections;

namespace BricksBucket.Localization
{

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for texts.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedText :
		SerializableDictionary<string, string>
	{
		/// <summary>
		/// Defines if the localized object has a localization for
		/// each culture.
		/// </summary>
		/// <returns>True if the localized object is complete.</returns>
		public bool IsComplete ()
		{
			return !ContainsValue (string.Empty) && !ContainsValue (null);
		}
	};

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedTexture :
		SerializableDictionary<string, Texture> { };

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedSprite :
		SerializableDictionary<string, Sprite> { };

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedAudio :
		SerializableDictionary<string, AudioClip> { };

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedVideo :
		SerializableDictionary<string, VideoClip> { };

	/// <summary>
	/// 
	/// Localized Unity Object.
	/// 
	/// <para>
	/// Dictionary with localizations for Unity Objects.
	/// </para>
	/// 
	/// <para> By Javier García | @jvrgms | 2020 </para>
	/// 
	/// </summary>
	[System.Serializable]
	internal class LocalizedUnityObject :
		SerializableDictionary<string, Object> { };

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
	internal class TextLocalizations :
		SerializableDictionary<string, LocalizedText>
	{
		public bool IsComplete ()
		{
			bool isComplete = true;
			foreach (var keyValuePair in this)
			{
				if (!keyValuePair.Value.IsComplete ())
					isComplete = false;
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
		SerializableDictionary<string, LocalizedTexture> { }

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
		SerializableDictionary<string, LocalizedSprite> { }

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
		SerializableDictionary<string, LocalizedAudio> { }

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
		SerializableDictionary<string, LocalizedVideo> { }

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
		SerializableDictionary<string, LocalizedUnityObject> { }

}