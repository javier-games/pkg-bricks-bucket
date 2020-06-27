using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using BricksBucket.Collections;

namespace Postman.v2
{
	/// <summary>
	/// A response represents an HTTP response.
	/// </summary>
	[System.Serializable]
	public class Response : GenericWrapper<ResponseClass>
	{
		#region Methods

		public static implicit operator Response (bool value) =>
			new Response {Bool = value};

		public static implicit operator Response (int value) =>
			new Response {Integer = value};

		public static implicit operator Response (float value) =>
			new Response {Float = value};

		public static implicit operator Response (string value) =>
			new Response {String = value};

		public static implicit operator Response (ResponseClass value) =>
			new Response {Object = value};
		
		public static implicit operator Response (
			List<ResponseClass> list
		) =>
			new Response {Array = list};

		#endregion
	}

	[System.Serializable]
	public class ResponseClass
	{
		#region Fields

		[SerializeField]
		private string m_id;

		[SerializeField]
		private Request m_originalRequest;
		
		[SerializeField]
		private StringFloatWrapper m_responseTime;

		[SerializeField]
		private Timings m_timings;

		[SerializeField]
		private Headers m_header;

		[SerializeField]
		private List<Cookie> m_cookie;

		[SerializeField]
		private string m_body;
		
		[SerializeField]
		private string m_status;

		[SerializeField]
		private int m_code;

		#endregion

		#region Properties

		/// <summary>
		/// The raw text of the response.
		/// </summary>
		[JsonProperty ("body")]
		public string Body
		{
			get => m_body;
			set => m_body = value;
		}

		/// <summary>
		/// The numerical response code, example: 200, 201, 404, etc.
		/// </summary>
		[JsonProperty ("code", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public int Code
		{
			get => m_code;
			set => m_code = value;
		}

		[JsonProperty ("cookie", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Cookie> Cookie
		{
			get => m_cookie;
			set => m_cookie = value;
		}

		[JsonProperty ("header")]
		public Headers Header
		{
			get => m_header;
			set => m_header = value;
		}

		/// <summary>
		/// A unique, user defined identifier that can  be used to refer to
		/// this response from requests.
		/// </summary>
		[JsonProperty ("id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get => m_id;
			set => m_id = value;
		}

		[JsonProperty ("originalRequest", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Request OriginalRequest
		{
			get => m_originalRequest;
			set => m_originalRequest = value;
		}

		/// <summary>
		/// The time taken by the request to complete. If a number, the unit
		/// is milliseconds. If the response is manually created, this can
		/// be set to `null`.
		/// </summary>
		[JsonProperty ("responseTime")]
		public StringFloatWrapper ResponseTime
		{
			get => m_responseTime;
			set => m_responseTime = value;
		}

		/// <summary>
		/// The response status, e.g: '200 OK'
		/// </summary>
		[JsonProperty ("status", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Status
		{
			get => m_status;
			set => m_status = value;
		}

		/// <summary>
		/// Set of timing information related to request and response
		/// in milliseconds.
		/// </summary>
		[JsonProperty ("timings")]
		public Timings Timings
		{
			get => m_timings;
			set => m_timings = value;
		}

		#endregion
	}
	
	/// <summary>
	/// A Cookie, that follows the [Google Chrome
	/// format](https://developer.chrome.com/extensions/cookies).
	/// </summary>
	[System.Serializable]
	public class Cookie
	{
		#region Fields

		[SerializeField]
		private string m_domain;

		[SerializeField]
		private StringFloatWrapper m_expires;

		[SerializeField]
		private string m_maxAge;

		[SerializeField]
		private BoolWrapper m_hostOnly;

		[SerializeField]
		private BoolWrapper m_httpOnly;

		[SerializeField]
		private string m_name;

		[SerializeField]
		private string m_path;

		[SerializeField]
		private BoolWrapper m_secure;

		[SerializeField]
		private BoolWrapper m_session;

		[SerializeField]
		private string m_value;

		[SerializeField]
		private List<string> m_extensions;

		#endregion

		#region Properties

		/// <summary>
		/// This is the name of the Cookie.
		/// </summary>
		[JsonProperty ("name", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Name
		{
			get => m_name;
			set => m_name = value;
		}

		/// <summary>
		/// The path associated with the Cookie.
		/// </summary>
		[JsonProperty ("path", Required = Required.Always)]
		public string Path
		{
			get => m_path;
			set => m_path = value;
		}

		/// <summary>
		/// The domain for which this cookie is valid.
		/// </summary>
		[JsonProperty ("domain", Required = Required.Always)]
		public string Domain
		{
			get => m_domain;
			set => m_domain = value;
		}

		/// <summary>
		/// The value of the Cookie.
		/// </summary>
		[JsonProperty ("value", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Value
		{
			get => m_value;
			set => m_value = value;
		}

		/// <summary>
		/// When the cookie expires.
		/// </summary>
		[JsonProperty ("expires", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public StringFloatWrapper Expires
		{
			get => m_expires;
			set => m_expires = value;
		}

		/// <summary>
		/// Custom attributes for a cookie go here, such as the [Priority
		/// Field](https://code.google.com/p/chromium/issues/detail?id=232693)
		/// </summary>
		[JsonProperty ("extensions", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Extensions
		{
			get => m_extensions;
			set => m_extensions = value;
		}

		/// <summary>
		/// True if the cookie is a host-only cookie. (i.e. a requests URL
		/// domain must exactly match the domain of the cookie).
		/// </summary>
		[JsonProperty ("hostOnly", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper HostOnly
		{
			get => m_hostOnly;
			set => m_hostOnly = value;
		}

		/// <summary>
		/// Indicates if this cookie is HTTP Only. (if True, the cookie is
		/// inaccessible to client-side scripts).
		/// </summary>
		[JsonProperty ("httpOnly", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper HttpOnly
		{
			get => m_httpOnly;
			set => m_httpOnly = value;
		}

		[JsonProperty ("maxAge", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string MaxAge
		{
			get => m_maxAge;
			set => m_maxAge = value;
		}

		/// <summary>
		/// Indicates if the 'secure' flag is set on the Cookie, meaning that
		/// it is transmitted over secure connections only (typically HTTPS).
		/// </summary>
		[JsonProperty ("secure", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Secure
		{
			get => m_secure;
			set => m_secure = value;
		}

		/// <summary>
		/// True if the cookie is a session cookie.
		/// </summary>
		[JsonProperty ("session", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Session
		{
			get => m_session;
			set => m_session = value;
		}

		#endregion
	}

	[System.Serializable]
	public class Headers : StringObjectWrapper<List<HeaderElement>>
	{
		#region Methods

		public static implicit operator Headers (
			List<HeaderElement> anythingArray
		) =>
			new Headers {Object = anythingArray};

		public static implicit operator Headers (string s) =>
			new Headers {String = s};

		#endregion
	}
	
	/// <summary>
	/// No HTTP request is complete without its headers, and the same is
	/// true for a Postman request. This field is an array containing all
	/// the headers.
	/// </summary>
	[System.Serializable]
	public class HeaderElement : StringObjectWrapper<HeaderObject>
	{
		#region Method

		public static implicit operator HeaderElement (HeaderObject obj) =>
			new HeaderElement {Object = obj};

		public static implicit operator HeaderElement (string s) =>
			new HeaderElement {String = s};

		#endregion
	}
	
	[System.Serializable]
	public class Timings : SerializableDictionary<string, SerializedObject> { }

}