using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using BricksBucket.Core.Collections;

namespace Postman.v2
{
	/// <summary>
	/// Postman stores the type of data associated with this request in
	/// this field.
	/// </summary>
	public enum Mode
	{
		NONE,
		FILE,
		FORM_DATA,
		GRAPHQL,
		RAW,
		URLENCODED
	}

	public enum FormParameterType
	{
		TEXT,
		FILE
	}

	/// <summary>
	/// This field contains the data usually contained in the request body.
	/// </summary>
	[System.Serializable]
	public class Body
	{

		#region Fields

		[SerializeField]
		private Mode m_mode;

		[SerializeField]
		private string m_raw;

		[SerializeField]
		private GraphQl m_graphql;

		[SerializeField]
		private List<UrlEncodedParameter> m_urlencoded;

		[SerializeField]
		private List<FormParameter> m_formData;

		[SerializeField]
		private File m_file;

		[SerializeField]
		private BoolWrapper m_disabled;

		#endregion

		#region Properties

		/// <summary>
		/// Postman stores the type of data associated with this request in
		/// this field.
		/// </summary>
		[JsonProperty ("mode", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Mode Mode
		{
			get => m_mode;
			set => m_mode = value;
		}

		/// <summary>
		/// When set to true, prevents request body from being sent.
		/// </summary>
		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		[JsonProperty ("raw", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Raw
		{
			get => m_raw;
			set => m_raw = value;
		}

		[JsonProperty ("graphql", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public GraphQl Graphql
		{
			get => m_graphql;
			set => m_graphql = value;
		}

		[JsonProperty ("file", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public File File
		{
			get => m_file;
			set => m_file = value;
		}

		// ReSharper disable once StringLiteralTypo
		[JsonProperty ("formdata", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<FormParameter> FormData
		{
			get => m_formData;
			set => m_formData = value;
		}

		[JsonProperty ("urlencoded", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<UrlEncodedParameter> Urlencoded
		{
			get => m_urlencoded;
			set => m_urlencoded = value;
		}

		#endregion
	}
	
	[System.Serializable]
	public class File
	{
		#region Fields

		[SerializeField]
		private string m_content;

		[SerializeField]
		private string m_source;

		#endregion

		#region Properties

		[JsonProperty ("content", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Content
		{
			get => m_content;
			set => m_content = value;
		}

		[JsonProperty ("src")]
		public string Source
		{
			get => m_source;
			set => m_source = value;
		}

		#endregion
	}
	
	[System.Serializable]
	public class FormParameter
	{

		#region Fields

		[SerializeField]
		private string m_key;

		[SerializeField]
		private string m_value;

		[SerializeField]
		private Source m_source;

		[SerializeField]
		private BoolWrapper m_disabled;

		[SerializeField]
		private FormParameterType m_type;

		[SerializeField]
		private string m_contentType;

		[SerializeField]
		private Description m_description;

		#endregion

		#region Properties

		/// <summary>
		/// Override Content-Type header of this form data entity.
		/// </summary>
		[JsonProperty ("contentType", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string ContentType
		{
			get => m_contentType;
			set => m_contentType = value;
		}

		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public FormParameterType Type
		{
			get => m_type;
			set => m_type = value;
		}

		[JsonProperty ("description")]
		public Description Description
		{
			get => m_description;
			set => m_description = value;
		}

		[JsonProperty ("key", Required = Required.Always)]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		[JsonProperty ("value", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Value
		{
			get => m_value;
			set => m_value = value;
		}

		[JsonProperty ("src")]
		public Source Source
		{
			get => m_source;
			set => m_source = value;
		}

		/// <summary>
		/// When set to true, prevents this form data entity from being sent.
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
	
	[System.Serializable]
	public class Source : StringObjectWrapper<List<string>>
	{
		#region Methods
		public static implicit operator Source (List<string> obj) =>
			new Source {Object = obj};

		public static implicit operator Source (string s) =>
			new Source {String = s};
		#endregion
	}
	
	[System.Serializable]
	public class UrlEncodedParameter
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

		[JsonProperty ("key", Required = Required.Always)]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		[JsonProperty ("value", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
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

		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		#endregion
	}
	
	[System.Serializable]
	public class GraphQl : SerializableDictionary<string, SerializedObject> { }
}