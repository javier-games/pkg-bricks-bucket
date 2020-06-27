using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	
	public enum AuthType
	{
		NULL,
		NO_AUTH,
		API_KEY,
		AWS_V4,
		BASIC,
		BEARER,
		DIGEST,
		HAWK,
		// ReSharper disable once IdentifierTypo
		NTLM,
		OAUTH1,
		OAUTH2
	}
	
	/// <summary>
	/// Represents authentication helpers provided by Postman
	/// </summary>
	[System.Serializable]
	public class Auth
	{
		#region Fields

		[SerializeField]
		private AuthType m_type;

		[SerializeField]
		private SerializedObject m_noAuth;

		[SerializeField]
		private List<ApiKeyElement> m_apiKey;

		[SerializeField]
		private List<ApiKeyElement> m_basic;

		[SerializeField]
		private List<ApiKeyElement> m_bearer;

		[SerializeField]
		private List<ApiKeyElement> m_awsV4;

		[SerializeField]
		private List<ApiKeyElement> m_digest;

		[SerializeField]
		private List<ApiKeyElement> m_hawk;

		[SerializeField]
		// ReSharper disable once IdentifierTypo
		private List<ApiKeyElement> m_ntlm;

		[SerializeField]
		private List<ApiKeyElement> m_oauth1;

		[SerializeField]
		private List<ApiKeyElement> m_oauth2;

		#endregion

		#region Properties

		[JsonProperty ("type", Required = Required.Always)]
		public AuthType Type
		{
			get => m_type;
			set => m_type = value;
		}

		// ReSharper disable once StringLiteralTypo
		[JsonProperty ("noauth")]
		public SerializedObject NoAuth
		{
			get => m_noAuth;
			set => m_noAuth = value;
		}

		/// <summary>
		/// The attributes for API Key Authentication.
		/// </summary>
		// ReSharper disable once StringLiteralTypo
		[JsonProperty ("apikey", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> ApiKey
		{
			get => m_apiKey;
			set => m_apiKey = value;
		}

		/// <summary>
		/// The attributes for [Basic Authentication]
		/// https://en.wikipedia.org/wiki/Basic_access_authentication.
		/// </summary>
		[JsonProperty ("basic", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Basic
		{
			get => m_basic;
			set => m_basic = value;
		}

		/// <summary>
		/// The helper attributes for [Bearer Token Authentication]
		/// https://tools.ietf.org/html/rfc6750
		/// </summary>
		[JsonProperty ("bearer", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Bearer
		{
			get => m_bearer;
			set => m_bearer = value;
		}

		/// <summary>
		/// The attributes for [AWS Auth]
		/// (http://docs.aws.amazon.com/AmazonS3/latest/dev/RESTAuthentication).
		/// </summary>
		// ReSharper disable once StringLiteralTypo
		[JsonProperty ("awsv4", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> AwsV4
		{
			get => m_awsV4;
			set => m_awsV4 = value;
		}

		/// <summary>
		/// The attributes for [Digest Authentication]
		/// (https://en.wikipedia.org/wiki/Digest_access_authentication).
		/// </summary>
		[JsonProperty ("digest", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Digest
		{
			get => m_digest;
			set => m_digest = value;
		}

		/// <summary>
		/// The attributes for [Hawk Authentication]
		/// (https://github.com/hueniverse/hawk).
		/// </summary>
		[JsonProperty ("hawk", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Hawk
		{
			get => m_hawk;
			set => m_hawk = value;
		}

		// ReSharper disable once CommentTypo
		/// <summary>
		/// The attributes for [NTLM Authentication]
		/// (https://msdn.microsoft.com/en-us/library/cc237488.aspx).
		/// </summary>
		// ReSharper disable once StringLiteralTypo
		[JsonProperty ("ntlm", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		// ReSharper disable once IdentifierTypo
		public List<ApiKeyElement> Ntlm
		{
			get => m_ntlm;
			set => m_ntlm = value;
		}

		/// <summary>
		/// The attributes for [OAuth2](https://oauth.net/1/).
		/// </summary>
		[JsonProperty ("oauth1", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Oauth1
		{
			get => m_oauth1;
			set => m_oauth1 = value;
		}

		/// <summary>
		/// Helper attributes for [OAuth2](https://oauth.net/2/).
		/// </summary>
		[JsonProperty ("oauth2", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<ApiKeyElement> Oauth2
		{
			get => m_oauth2;
			set => m_oauth2 = value;
		}

		#endregion
	}
	
	/// <summary>
	/// Represents an attribute for any authorization method provided by
	/// Postman. For example `username` and `password` are set as auth
	/// attributes for Basic Authentication method.
	/// </summary>
	[System.Serializable]
	public class ApiKeyElement
	{

		#region Fields

		[SerializeField]
		private string m_type;

		[SerializeField]
		private string m_key;

		[SerializeField]
		private SerializedObject m_value;

		#endregion

		#region Properties

		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Type
		{
			get => m_type;
			set => m_type = value;
		}

		[JsonProperty ("key", Required = Required.Always)]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		[JsonProperty ("value")]
		public SerializedObject Value
		{
			get => m_value;
			set => m_value = value;
		}

		#endregion
	}
}