using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	/// <summary>
	/// If object, contains the complete broken-down URL for this request.
	/// If string, contains the literal request URL.
	/// </summary>
	[System.Serializable]
	public class Url : StringObjectWrapper<UrlObject>
	{
		public static implicit operator Url (UrlObject urlObject) =>
			new Url {Object = urlObject};
		public static implicit operator Url (string s) =>
			new Url {String = s};
	}

	[System.Serializable]
	public class UrlObject
	{

		#region Fields

		[SerializeField]
		private string m_raw;

		[SerializeField]
		private string m_hash;

		[SerializeField]
		private string m_protocol;

		[SerializeField]
		private Host m_host;

		[SerializeField]
		private PathList m_pathList;

		[SerializeField]
		private string m_port;

		[SerializeField]
		private List<QueryParam> m_query;

		[SerializeField]
		private List<Variable> m_variable;

		#endregion

		#region Properties

		/// <summary>
		/// The string representation of the request URL, including the
		/// protocol, host, path, hash, query parameter(s) and path variable(s).
		/// </summary>
		[JsonProperty ("raw", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Raw
		{
			get => m_raw;
			set => m_raw = value;
		}

		/// <summary>
		/// Contains the URL fragment (if any). Usually this is not transmitted
		/// over the network, but it could be useful to store this in some
		/// cases.
		/// </summary>
		[JsonProperty ("hash", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Hash
		{
			get => m_hash;
			set => m_hash = value;
		}

		/// <summary>
		/// The protocol associated with the request, E.g: 'http'
		/// </summary>
		[JsonProperty ("protocol", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Protocol
		{
			get => m_protocol;
			set => m_protocol = value;
		}

		/// <summary>
		/// The host for the URL, E.g: api.your-domain.com. Can be stored as a
		/// string or as an array of strings.
		/// </summary>
		[JsonProperty ("host", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Host Host
		{
			get => m_host;
			set => m_host = value;
		}

		[JsonProperty ("path", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public PathList PathList
		{
			get => m_pathList;
			set => m_pathList = value;
		}

		/// <summary>
		/// The port number present in this URL. An empty value implies 80/443
		/// depending on whether the protocol field contains http/https.
		/// </summary>
		[JsonProperty ("port", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Port
		{
			get => m_port;
			set => m_port = value;
		}

		/// <summary>
		/// An array of QueryParams, which is basically the query string part
		/// of the URL, parsed into separate variables
		/// </summary>
		[JsonProperty ("query", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<QueryParam> Query
		{
			get => m_query;
			set => m_query = value;
		}

		/// <summary>
		/// Postman supports path variables with the syntax
		/// `/path/:variableName/to/somewhere`. These variables are stored in
		/// this field.
		/// </summary>
		[JsonProperty ("variable", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Variable> Variable
		{
			get => m_variable;
			set => m_variable = value;
		}

		#endregion
	}
	
	[System.Serializable]
	public class QueryParam
	{

		#region Fields

		[SerializeField]
		private string m_key;

		[SerializeField]
		private string m_value;

		[SerializeField]
		private BoolWrapper m_disabled;

		[SerializeField]
		private Description m_description;

		#endregion

		#region Properties

		[JsonProperty ("description")]
		public Description Description
		{
			get => m_description;
			set => m_description = value;
		}

		/// <summary>
		/// If set to true, the current query parameter will not be sent with
		/// the request.
		/// </summary>
		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		[JsonProperty ("key")]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		[JsonProperty ("value")]
		public string Value
		{
			get => m_value;
			set => m_value = value;
		}

		#endregion
	}

	/// <summary>
	/// The host for the URL, E.g: api.your-domain.com. Can be stored as a
	/// string or as an array of strings.
	/// </summary>
	[System.Serializable]
	public class Host : StringObjectWrapper<List<string>>
	{
		public static implicit operator Host (string s) =>
			new Host {String = s};

		public static implicit operator Host (List<string> stringArray) =>
			new Host {Object = stringArray};
	}

	[System.Serializable]
	public class PathList : StringObjectWrapper<List<Path>>
	{
		public static implicit operator PathList (List<Path> obj) =>
			new PathList {Object = obj};

		public static implicit operator PathList (string s) =>
			new PathList {String = s};
	}

	/// <summary>
	/// The complete path of the current url, broken down into segments. A
	/// segment could be a string, or a path variable.
	/// </summary>
	[System.Serializable]
	public class Path : StringObjectWrapper<PathObject>
	{
		public static implicit operator Path (PathObject obj) =>
			new Path {Object = obj};

		public static implicit operator Path (string s) =>
			new Path {String = s};
	}

	[System.Serializable]
	public class PathObject
	{

		#region Fields

		[SerializeField]
		private string m_type;

		[SerializeField]
		private string m_value;

		#endregion

		#region Methods

		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Type
		{
			get => m_type;
			set => m_type = value;
		}

		[JsonProperty ("value", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Value
		{
			get => m_value;
			set => m_value = value;
		}

		#endregion
	}
}