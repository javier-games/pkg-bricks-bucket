using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

namespace BricksBucket.Localization
{
	public enum LocalizationType
	{
		TEXT,
		TEXTURE,
		SPRITE,
		VIDEO,
		AUDIO
	}
	
	[System.Serializable]
	public abstract class Localization <TValue>
	{

		[SerializeField]
		private string _code;

		[SerializeField]
		private string _book;

		[SerializeField]
		private LocalizationType _type;

		[SerializeField]
		[OnValueChanged("ValuesChanged")]
		private Dictionary<string, TValue> _values;

		[SerializeField]
		private bool _isComplete;

		public string Code
		{
			get => _code;
			protected set => _code = value;
		}

		public string Book
		{
			get => _book;
			protected set => _book = value;
		}

		public LocalizationType Type
		{
			get => _type;
			protected set => _type = value;
		}

		public Dictionary<string, TValue> Values
		{
			get => _values;
			protected set => _values = value;
		}

		public bool IsComplete
		{
			get => _isComplete;
			protected set => _isComplete = value;
		}

		protected void ValuesChanged ()
		{
			//IsComplete = _values.Count ==
			//	LocalizationSettings.BookSettings.Books.Length;
		}
	}
	
	[System.Serializable]
	public class TextLocalization : Localization<string>
	{
		private TextLocalization (string code, string book)
		{
			Type = LocalizationType.TEXT;
			Code = code;
			Book = book;
			Values = new Dictionary<string, string> ();
		}
	}
	
	[System.Serializable]
	public class TextureLocalization : Localization<Texture>
	{
		private TextureLocalization (string code, string book)
		{
			Type = LocalizationType.TEXTURE;
			Code = code;
			Book = book;
			Values = new Dictionary<string, Texture> ();
		}
	}
	
	[System.Serializable]
	public class SpriteLocalization : Localization<Sprite>
	{
		private SpriteLocalization (string code, string book)
		{
			Type = LocalizationType.SPRITE;
			Code = code;
			Book = book;
			Values = new Dictionary<string, Sprite> ();
		}
	}
	
	[System.Serializable]
	public class VideoLocalization : Localization<VideoClip>
	{
		private VideoLocalization (string code, string book)
		{
			Type = LocalizationType.VIDEO;
			Code = code;
			Book = book;
			Values = new Dictionary<string, VideoClip> ();
		}
	}
	
	[System.Serializable]
	public class AudioLocalization : Localization<AudioClip>
	{
		private AudioLocalization (string code, string book)
		{
			Type = LocalizationType.AUDIO;
			Code = code;
			Book = book;
			Values = new Dictionary<string, AudioClip> ();
		}
	}
}