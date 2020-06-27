using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	/// <summary>
	/// A request represents an HTTP request. If a string, the string is
	/// assumed to be the request URL and the method is assumed to be 'GET'.
	/// </summary>
	[System.Serializable]
	public class Request : StringObjectWrapper<RequestObject>
	{
		public static implicit operator Request (RequestObject obj) =>
			new Request {Object = obj};

		public static implicit operator Request (string s) =>
			new Request {String = s};
	}

	[System.Serializable]
	public class RequestObject
	{
		#region Fields

		[SerializeField]
		private Url m_url;

		[SerializeField]
		private Auth m_auth;

		[SerializeField]
		private ProxyConfig m_proxy;

		[SerializeField]
		private Certificate m_certificate;

		[SerializeField]
		private string m_method;

		[SerializeField]
		private Description m_description;

		[SerializeField]
		private Header m_header;

		[SerializeField]
		private Body m_body;

		#endregion

		#region Properties

		[JsonProperty ("description")]
		public Description Description
		{
			get => m_description;
			set => m_description = value;
		}

		[JsonProperty ("method", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Method
		{
			get => m_method;
			set => m_method = value;
		}

		[JsonProperty ("url", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Url Url
		{
			get => m_url;
			set => m_url = value;
		}

		[JsonProperty ("auth")]
		public Auth Auth
		{
			get => m_auth;
			set => m_auth = value;
		}

		[JsonProperty ("body")]
		public Body Body
		{
			get => m_body;
			set => m_body = value;
		}

		[JsonProperty ("header", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Header Header
		{
			get => m_header;
			set => m_header = value;
		}

		[JsonProperty ("certificate", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Certificate Certificate
		{
			get => m_certificate;
			set => m_certificate = value;
		}

		[JsonProperty ("proxy", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public ProxyConfig Proxy
		{
			get => m_proxy;
			set => m_proxy = value;
		}

		#endregion
	}
	
	/// <summary>
	/// A representation of an ssl certificate
	/// </summary>
	[System.Serializable]
	public class Certificate
	{

		#region Fields

		[SerializeField]
		private string m_name;

		[SerializeField]
		private List<string> m_matches;

		[SerializeField]
		private Key m_key;

		[SerializeField]
		private CertificateSource m_certificateSource;

		[SerializeField]
		private string m_passphrase;

		#endregion

		#region Properties

		/// <summary>
		/// A name for the certificate for user reference
		/// </summary>
		[JsonProperty ("name", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Name
		{
			get => m_name;
			set => m_name = value;
		}

		/// <summary>
		/// An object containing path to file certificate, on the file system
		/// </summary>
		[JsonProperty ("cert", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public CertificateSource CertificateSource
		{
			get => m_certificateSource;
			set => m_certificateSource = value;
		}

		/// <summary>
		/// An object containing path to file containing private key, on the
		/// file system
		/// </summary>
		[JsonProperty ("key", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Key Key
		{
			get => m_key;
			set => m_key = value;
		}

		/// <summary>
		/// A list of Url match pattern strings, to identify Urls this
		/// certificate can be used for.
		/// </summary>
		[JsonProperty ("matches", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Matches
		{
			get => m_matches;
			set => m_matches = value;
		}

		/// <summary>
		/// The passphrase for the certificate
		/// </summary>
		[JsonProperty ("passphrase", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Passphrase
		{
			get => m_passphrase;
			set => m_passphrase = value;
		}

		#endregion
	}

	/// <summary>
	/// An object containing path to file certificate, on the file system
	/// </summary>
	[System.Serializable]
	public class CertificateSource
	{
		#region Fields

		[SerializeField]
		private string m_source;

		#endregion

		#region Properties

		/// <summary>
		/// The path to file containing key for certificate, on the file system
		/// </summary>
		[JsonProperty ("src")]
		public string Source
		{
			get => m_source;
			set => m_source = value;
		}

		#endregion
	}

	/// <summary>
	/// An object containing path to file containing private key, on the
	/// file system
	/// </summary>
	[System.Serializable]
	public class Key
	{
		#region Fields

		[SerializeField]
		private string m_source;

		#endregion

		#region Properties

		/// <summary>
		/// The path to file containing key for certificate, on the file
		/// system.
		/// </summary>
		[JsonProperty ("src")]
		public string Source
		{
			get => m_source;
			set => m_source = value;
		}

		#endregion
	}

	/// <summary>
	/// Using the Proxy, you can configure your custom proxy into the postman
	/// for particular url match.
	/// </summary>
	[System.Serializable]
	public class ProxyConfig
	{
		#region Fields

		[SerializeField]
		private string m_host;

		[SerializeField]
		private string m_match;

		[SerializeField]
		private IntegerWrapper m_port;

		[SerializeField]
		private BoolWrapper m_tunnel;

		[SerializeField]
		private BoolWrapper m_disabled;

		#endregion

		#region Properties

		/// <summary>
		/// The proxy server host.
		/// </summary>
		[JsonProperty ("host", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Host
		{
			get => m_host;
			set => m_host = value;
		}

		/// <summary>
		/// The Url match for which the proxy config is defined.
		/// </summary>
		[JsonProperty ("match", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Match
		{
			get => m_match;
			set => m_match = value;
		}

		/// <summary>
		/// The proxy server port.
		/// </summary>
		[JsonProperty ("port", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public IntegerWrapper Port
		{
			get => m_port;
			set => m_port = value;
		}

		/// <summary>
		/// The tunneling details for the proxy config.
		/// </summary>
		[JsonProperty ("tunnel", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Tunnel
		{
			get => m_tunnel;
			set => m_tunnel = value;
		}

		/// <summary>
		/// When set to true, ignores this proxy configuration entity.
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