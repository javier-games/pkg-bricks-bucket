namespace BricksBucket.Web
{

	#region Enviroment Types
	
	/// <summary>
	/// 
	/// <!-- EnvironmentType -->
	/// 
	/// Different type of environments.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// <!--
	/// 
	/// Notes: 
	/// Add extra string fields in <see cref="WebAPI"/> for each new
	/// environment type.
	///
	/// -->
	/// </summary>
	public enum EnvironmentType
	{
		/// <summary>
		/// Production Environment.
		/// </summary>
		PRODUCTION,
		
		/// <summary>
		/// Development Environment.
		/// </summary>
		DEVELOPMENT,
		
		/// <summary>
		/// Overrides the development and production host.
		/// </summary>
		OVERRIDE
	}
	
	#endregion
	
	
	#region Auth Methods
	
	/// <summary>
	/// 
	/// <!-- AuthMethod -->
	///
	/// Enumerates the types of authentications supported by the
	/// <see cref="Auth"/> structure.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	public enum AuthMethod
	{
		/// <summary>
		/// No authentication required.
		/// </summary>
		NONE,
			
		/// <summary>
		/// Inherits the authentication from the APIs global
		/// authentication.
		/// </summary>
		INHERIT,

		/// <summary>
		/// Basic authentication schema <see href=
		/// "https://tools.ietf.org/html/rfc7617">RFC-7617</see>.
		/// </summary>
		BASIC,

		/// <summary>
		/// Custom name key and its value in the header or query params.
		/// </summary>
		API_KEY,

		/// <summary>
		/// OAuth 2.0 Bearer token authentication schema <see href=
		/// "https://tools.ietf.org/html/rfc6750">RFC 6750</see>.
		/// </summary>
		BEARER_TOKEN,

		/// <summary>
		/// Digest authentication schema <see href=
		/// "https://tools.ietf.org/html/rfc2069">RFC-2069</see>.
		/// </summary>
		DIGEST,

		/// <summary>
		/// OAuth 1.0 authentication schema <see href=
		/// "https://tools.ietf.org/html/rfc5849">RFC-5849</see>.
		/// </summary>
		OAUTH1,

		/// <summary>
		/// OAuth 2.0 authentication schema <see href=
		/// "https://tools.ietf.org/html/rfc6749">RFC-6749</see>.
		/// </summary>
		OAUTH2
	}
	
	#endregion
	
	
	#region HTTP Verbs
	
	/// <summary>
	/// 
	/// <!-- HTTPVerbs -->
	///
	/// Enumerates the types of authentications supported by the
	/// <see cref="Service"/> class according to the <see href=
	/// "https://www.ietf.org/rfc/rfc2616.txt">RFC-2616</see>.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	// ReSharper disable once InconsistentNaming
	public enum HTTPVerb
	{
		GET,
		HEAD,
		POST,
		PUT,
		DELETE,
		PATCH,
		COPY,
		OPTIONS,
		LINK,
		UNLINK,
		PURGE,
		LOCK,
		UNLOCK,
		PROPFIND,
		VIEW
	}
	
	#endregion
}