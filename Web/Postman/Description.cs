using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	/// <summary>
	/// A Description can be a raw text, or be an object, which holds the
	/// description along with its format.
	/// </summary>
	[System.Serializable]
	public class Description : StringObjectWrapper<DescriptionObject>
	{
		public static implicit operator Description (DescriptionObject obj) =>
			new Description {Object = obj};

		public static implicit operator Description (string s) =>
			new Description {String = s};
	}

	[System.Serializable]
	public class DescriptionObject
	{
		#region Fields

		[SerializeField]
		private string m_content;

		[SerializeField]
		private string m_type;

		[SerializeField]
		private SerializedObject m_version;

		#endregion

		#region Properties

		/// <summary>
		/// Description can have versions associated with it, which should be
		/// put in this property.
		/// </summary>
		[JsonProperty ("version")]
		public SerializedObject Version
		{
			get => m_version;
			set => m_version = value;
		}

		/// <summary>
		/// Holds the mime type of the raw description content. E.g:
		/// 'text/markdown' or 'text/html'. The type is used to correctly
		/// render the description when generating documentation, or in the
		/// Postman app.
		/// </summary>
		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Type
		{
			get => m_type;
			set => m_type = value;
		}

		/// <summary>
		/// The content of the description goes here, as a raw string.
		/// </summary>
		[JsonProperty ("content", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Content
		{
			get => m_content;
			set => m_content = value;
		}

		#endregion
	}
}