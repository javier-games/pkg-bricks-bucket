using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	[System.Serializable]
	public class Header : StringObjectWrapper<List<HeaderObject>>
	{
		#region Methods
		public static implicit operator Header (List<HeaderObject> obj) =>
			new Header {Object = obj};

		public static implicit operator Header (string s) =>
			new Header {String = s};
		#endregion
	}
	
	/// <summary>
	/// A representation for a list of headers
	/// Represents a single HTTP Header
	/// </summary>
	[System.Serializable]
	public class HeaderObject
	{
		#region Fields

		[SerializeField]
		private string m_key;

		[SerializeField]
		private string m_value;

		[SerializeField]
		private Description m_description;

		[SerializeField]
		private BoolWrapper m_disabled;

		#endregion

		#region Properties

		/// <summary>
		/// This holds the LHS of the HTTP Header, e.g ``Content-Type`` or
		/// ``X-Custom-Header``
		/// </summary>
		[JsonProperty ("key", Required = Required.Always)]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		/// <summary>
		/// The value (or the RHS) of the Header is stored in this field.
		/// </summary>
		[JsonProperty ("value", Required = Required.Always)]
		public string Value
		{
			get => m_value;
			set => m_value = value;
		}

		[JsonProperty ("description")]
		public Description Description
		{
			get => m_description;
			set => m_description = value;
		}

		/// <summary>
		/// If set to true, the current header will not be sent with requests.
		/// </summary>
		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		#endregion
	}
}