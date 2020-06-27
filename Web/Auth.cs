using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
#endif

namespace BricksBucket.Web
{
	
	#region Auth
	
	/// <summary>
	/// 
	/// <!-- Auth -->
	///
	/// Abstract class for an authentication with useful methods to add
	/// authentication to a service.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	[System.Serializable]
	public abstract class Auth
	{
		
		#region Properties
		
		/// <summary>
		/// Type of the authorization.
		/// </summary>
		public virtual AuthMethod Method { get; private set; }

		#endregion
		
		
		#region Methods

		/// <summary>
		/// Gets headers with authentication.
		/// </summary>
		/// <returns>Headers with authentication.</returns>
		public virtual Headers GetHeaders () => null;

		/// <summary>
		/// Gets authentication in the query params form for a service.
		/// </summary>
		/// <returns>String with params.</returns>
		public virtual string GetPathParams () => string.Empty;

		/// <summary>
		/// Gets authentication in the query params form with out any value.
		/// </summary>
		/// <returns></returns>
		internal virtual string GetDisplayPathParams () => string.Empty;

		#endregion
	}

	#endregion

	
	#region Empty Auth

	/// <summary>
	/// 
	/// <!-- EmptyAuth -->
	///
	/// Class for a non authentication required.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	// ReSharper disable once ClassNeverInstantiated.Global
	public class EmptyAuth : Auth
	{
		
		#region Properties

		/// <inheritdoc cref="Auth.Method"/>
		public override AuthMethod Method => AuthMethod.NONE;

		#endregion
	}
	
	#endregion

	
	#region Basic Auth
	
	/// <summary>
	/// 
	/// <!-- BasicAuth -->
	///
	/// Basic authentication schema <see href=
	/// "https://tools.ietf.org/html/rfc7617">RFC-7617</see>.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	[System.Serializable]
	public class BasicAuth : Auth
	{
		
		#region Fields
		
		/// <summary>
		/// E-mail, gamer tag or alias of the user.
		/// </summary>
		[SerializeField]
		[Tooltip("E-mail, gamer tag or alias of the user.")]
		private string _username;
		
		/// <summary>
		/// Password of the user.
		/// </summary>
		[SerializeField]
		[Tooltip("Password of the user.")]
		private string _password;
		
		#endregion

		
		#region Properties
		
		/// <inheritdoc cref="Auth.Method"/>
		public override AuthMethod Method => AuthMethod.BASIC;

		#endregion

		
		#region Methods
		
		/// <inheritdoc cref="Auth.GetHeaders"/>
		public override Headers GetHeaders ()
		{
			if(string.IsNullOrWhiteSpace (_username))
				Debug.LogWarning ("User name is empty in Basic Auth");
			
			if(string.IsNullOrWhiteSpace (_password))
				Debug.LogWarning ("Password is empty in Basic Auth");
			
			string auth = _username + ":" + _password;
			var encodedAuth = System.Text.Encoding.GetEncoding (
				"ISO-8859-1"
			).GetBytes (auth);
			auth = System.Convert.ToBase64String (encodedAuth);
			auth = "Basic " + auth;
			return new Headers () {{"Authorization", auth}};
		}

		#endregion


		#region Editor

#if UNITY_EDITOR
		private class Drawer : OdinValueDrawer<BasicAuth>
		{
			protected override void DrawPropertyLayout (GUIContent label)
			{
				if (label != null)
				{
					EditorGUILayout.LabelField (label);
					EditorGUI.indentLevel++;
				}

				var children = ValueEntry.Property.Children;
				children.Get ("_username").Draw ();
				children.Get ("_password").Draw ();
				
				if (label != null)
					EditorGUI.indentLevel--;
			}
		}
#endif

		#endregion
	}
	
	#endregion

	
	#region API Key Auth
	
	/// <summary>
	/// 
	/// <!-- APIKeyAuth -->
	///
	/// Custom name key and its value in the header or query params.
	/// 
	/// <!-- By Javier García | @jvrgms | 2020 -->
	/// 
	/// </summary>
	[System.Serializable]
	// ReSharper disable once InconsistentNaming
	public class APIKeyAuth : Auth
	{
		
		#region Fields

		/// <summary>
		/// Key of the authentication.
		/// </summary>
		[SerializeField]
		private string _key;

		/// <summary>
		/// Value for the authentication.
		/// </summary>
		[SerializeField]
		private string _value;

		/// <summary>
		/// Where to put the authentication on a service.
		/// </summary>
		[SerializeField]
		private Type _type;
		
		#endregion

		
		#region Methods

		/// <inheritdoc cref="Auth.GetHeaders"/>
		public override Headers GetHeaders () =>
			_type != Type.HEADER ? null : new Headers () {{_key, _value}};

		/// <inheritdoc cref="Auth.GetPathParams"/>
		public override string GetPathParams () => _type != Type.QUERY_PARAM
			? string.Empty : _key + "=" + _value;

		/// <inheritdoc cref="Auth.GetDisplayPathParams"/>
		internal override string GetDisplayPathParams () => "<key>=<value>";

		#endregion
		
		
		#region Enums
		
		/// <summary>
		/// Types of API key that defines where to put the auth.
		/// </summary>
		private enum Type
		{
			HEADER,
			QUERY_PARAM
		}
		
		#endregion


		#region Editor

#if UNITY_EDITOR
		private class Drawer : OdinValueDrawer<APIKeyAuth>
		{
			protected override void DrawPropertyLayout (GUIContent label)
			{
				if (label != null)
				{
					EditorGUILayout.LabelField (label);
					EditorGUI.indentLevel++;
				}

				var children = ValueEntry.Property.Children;
				children.Get ("_key").Draw ();
				children.Get ("_value").Draw ();
				children.Get ("_type").Draw ();
				
				if (label != null)
					EditorGUI.indentLevel--;
			}
		}
#endif

		#endregion
	}
	
	#endregion


	#region Bearer Token Auth

	/// <summary>
	/// OAuth 2.0 Bearer token authentication schema <see href=
	/// "https://tools.ietf.org/html/rfc6750">RFC 6750</see>.
	/// </summary>
	[System.Serializable]
	public class BearerTokenAuth : Auth
	{
		
		#region Fields
		
		/// <summary>
		/// Access Token of the user.
		/// </summary>
		[SerializeField]
		private string _token;
		
		#endregion

		#region Methods
		
		/// <inheritdoc cref="Auth.GetHeaders"/>
		public override Headers GetHeaders () =>
			new Headers () {{"Authorization", "Bearer " + _token}};
		
		#endregion


		#region Editor

#if UNITY_EDITOR
		private class Drawer : OdinValueDrawer<APIKeyAuth>
		{
			protected override void DrawPropertyLayout (GUIContent label)
			{
				if (label != null)
				{
					EditorGUILayout.LabelField (label);
					EditorGUI.indentLevel++;
				}

				var children = ValueEntry.Property.Children;
				children.Get ("_token").Draw ();
				
				if (label != null)
					EditorGUI.indentLevel--;
			}
		}
#endif

		#endregion
	}
	
	#endregion
}