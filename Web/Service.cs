using System.Collections.Generic;
using UnityEngine;
using BricksBucket.Core.Collections;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
#endif

namespace BricksBucket.Web
{
	[System.Serializable]
	public class Service
	{
		
		#region Fields

		/// <summary>
		/// Parent API where this service belongs to.
		/// </summary>
		[SerializeField, HideInInspector]
		internal WebAPI _api;

		/// <summary>
		/// HTTP Verb for the web service.
		/// </summary>
		[SerializeField]
		[Tooltip("HTTP Verb for the web service.")]
		private HTTPVerb _verb;

		/// <summary>
		/// Path of end point or service. Does not include the host. 
		/// </summary>
		[SerializeField]
		[Tooltip("Path of end point or service with out the host.")]
		private string _path;
		
		/// <summary>
		/// Collection of names of parameters that exists into the URL segment.
		/// </summary>
		[SerializeField]
		[Tooltip("Collection of parameters that exists into the URL segment.")]
		private List<string> _pathParams;

		/// <summary>
		/// Collection of parameters for the service.
		/// </summary>
		[SerializeField]
		[Tooltip("Collection of parameters for the service.")]
		private List<string> _queryParams;

		/// <summary>
		/// Dictionary of strings keys and string values for the headers for
		/// the service.
		/// </summary>
		[SerializeField]
		[Tooltip("Headers to add to the request.")]
		private Headers _headers;
		
		/// <summary>
		/// Method of authentication for the service.
		/// </summary>
		[SerializeField]
		[Tooltip(" Method of authentication for the service.")]
		private AuthMethod _authMethod;

		/// <summary>
		/// Whether the authentication is constant in this service or it is
		/// variable.
		/// </summary>
		[SerializeField]
		[Tooltip("Whether the auth values are constant in this service.")]
		private bool _isConstant;

		/// <summary>
		/// Empty Authentication.
		/// </summary>
		[SerializeField]
		[Tooltip("Empty Authentication.")]
		private EmptyAuth _emptyAuth;

		/// <summary>
		/// Basic authentication.
		/// </summary>
		[SerializeField]
		[Tooltip("Basic Authentication.")]
		private BasicAuth _basicAuth;

		/// <summary>
		/// Authentication with custom key and value.
		/// </summary>
		[SerializeField]
		[Tooltip("Authentication with custom key and value.")]
		private APIKeyAuth _apiKeyAuth;
		
		/// <summary>
		/// OAuth 2.0 Bearer token authentication schema.
		/// </summary>
		[SerializeField]
		[Tooltip("Bearer Token Authentication")]
		private BearerTokenAuth _bearerTokenAuth;
		
		#endregion


		#region Properties

		/// <summary>
		/// Parent API where this service belongs to and is assigned from
		/// the editor.
		/// </summary>
		/// <returns>API this service belongs to.</returns>
		// ReSharper disable once InconsistentNaming
		internal WebAPI API
		{
			get => _api;
			set => _api = value;
		}
		
		/// <summary>
		/// HTTP Verb of the service.
		/// </summary>
		/// <returns>HTTP Verb of the service.</returns>
		public HTTPVerb Verb
		{
			get => _verb;
			private set => _verb = value;
		}

		/// <summary>
		/// Path of end point or service. Does not include the host. 
		/// </summary>
		/// <returns>Path of the service with out the host.</returns>
		public string Path
		{
			get => _path;
			private set => _path = value;
		}

		/// <summary>
		/// Collection of names of parameters that exists into the URL segment.
		/// </summary>
		/// <returns>List of names of parameters into the <see cref="Path"/>.
		/// </returns>
		public List<string> PathParams
		{
			get => _pathParams;
			private set => _pathParams = value;
		}
		
		/// <summary>
		/// Collection of parameters for the service.
		/// </summary>
		/// <returns>List of names of query parameters to add to the <see cref=
		/// "Path"/>.</returns>
		public List<string> QueryParams
		{
			get => _queryParams;
			private set => _queryParams = value;
		}

		/// <summary>
		/// Dictionary of strings keys and string values for the headers for
		/// the service.
		/// </summary>
		/// <returns>Dictionary of headers into this service.</returns>
		public Headers Headers
		{
			get => _headers;
			private set => _headers = value;
		}

		/// <summary>
		/// Method of authentication for the service.
		/// </summary>
		/// <returns>Method for the authentication.</returns>
		public AuthMethod Method
		{
			get => _authMethod;
			private set => _authMethod = value;
		}

		/// <summary>
		/// Whether the authentication is constant in this service or it is
		/// variable.
		/// </summary>
		/// <returns><value>True</value> if the service has a constant
		/// authentication or <value>false</value> is it is variable.</returns>
		public bool IsConstant
		{
			get => _isConstant;
			private set => _isConstant = value;
		}
		
		/// <summary>
		/// Authentication of the service.
		/// </summary>
		/// <returns>Authentication of the service.</returns>
		public Auth Auth
		{
			get
			{
				switch (Method)
				{
					case AuthMethod.NONE:
						return _emptyAuth;
					
					case AuthMethod.INHERIT:
						return API != null ? API.Auth : _emptyAuth;

					case AuthMethod.BASIC:
						return _basicAuth;
					
					case AuthMethod.API_KEY:
						return _apiKeyAuth;
					
					case AuthMethod.BEARER_TOKEN:
						return _bearerTokenAuth;
					
					// TODO: Implement missing authentications.
					case AuthMethod.DIGEST:
					case AuthMethod.OAUTH1:
					case AuthMethod.OAUTH2:
						return _emptyAuth;
					
					default:
						return null;
				}
			}
		}

		#endregion
		
		


		#region Editor

#if UNITY_EDITOR

		/// <summary>
		/// Gets the URL to display on inspector.
		/// </summary>
		/// <returns></returns>
		// ReSharper disable once InconsistentNaming
		private string GetURL ()
		{
			var host = API != null ? API.Host : string.Empty;

			if (string.IsNullOrEmpty (Path)) return host + ".../";

			var segment = string.Empty;
			var split = Path.Split ('/');
			for (int i = 0; i < split.Length; i++)
			{
				var index = PathParams.FindIndex (v => v == split[i]);
				segment += index >= 0
					? "<color=#5555FF><" + split[i] + "></color>"
					: split[i];
				if (i < split.Length - 1) segment += "/";
			}

			var queryParams = string.Empty;

			if (Auth != null)
			{
				var pathParams = Auth.GetPathParams ();
				var isEmpty = string.IsNullOrWhiteSpace (pathParams);

				queryParams += isEmpty
					? QueryParams.Count > 0 ? "?" : string.Empty
					: IsConstant
						? "?<color=#55FF55>" + pathParams + "</color>" +
						(QueryParams.Count > 0 ? "&" : "")
						: "?<color=#55FF55>" + Auth.GetDisplayPathParams ()
						+ "</color>" + (QueryParams.Count > 0 ? "&" : "");
			}
			else
			{
				queryParams += QueryParams.Count > 0 ? "?" : string.Empty;
			}

			for (int i = 0; i < QueryParams.Count; i++)
			{
				queryParams += "<color=#55FF55>" + QueryParams[i] + "=<" +
					QueryParams[i] + "></color>";
				if (i < QueryParams.Count - 1) queryParams += "&";
			}

			var url = string.IsNullOrWhiteSpace (host)
				? ".../"
				: host + "/";
			url += "<b>" + segment + queryParams + "</b>";
			return "<size=12>" + url + "</size>";
		}

		/// <summary>
		/// <!-- Service.Drawer -->
		/// Custom drawer for the <see cref="Service"/> class.
		/// </summary>
		private class Drawer : OdinValueDrawer<Service>
		{
			
			/// <summary>
			/// Status of the dropdown that contains the <see cref=
			/// "Service.Auth"/> info.
			/// </summary>
			private bool _authDropDown;
			
			/// <summary>
			/// Position of the scroll of the URL display.
			/// </summary>
			private Vector2 _scroll;
			
			/// <inheritdoc cref="OdinDrawer.DrawPropertyLayout"/>
			protected override void DrawPropertyLayout (GUIContent label)
			{
				//	Accessors.
				var value = ValueEntry.SmartValue;
				var children = ValueEntry.Property.Children;
				
				//	Draws the label.
				if (label != null)
					SirenixEditorGUI.Title (
						label.text, "", TextAlignment.Left, false
					);
				
				//	Draws Display URL Service.
				SirenixEditorGUI.BeginBox ();
				EditorGUILayout.BeginHorizontal ();
				_scroll = EditorGUILayout.BeginScrollView (_scroll);
				GUILayout.Label (
					text: value.GetURL (),
					style: SirenixGUIStyles.RichTextLabel
				);
				EditorGUILayout.EndScrollView ();
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndBox ();
				
				//	Draws the verb and segment fields.
				EditorGUILayout.Space();
				
				SirenixEditorGUI.BeginLegendBox ();
				EditorGUILayout.BeginHorizontal ();
				value.Verb = (HTTPVerb) SirenixEditorFields.EnumDropdown (
					selected: value.Verb,
					options: GUILayout.Width (80)
				);
				children.Get ("_path").Draw(null);
				EditorGUILayout.EndHorizontal ();
				SirenixEditorGUI.EndLegendBox ();
				ValueEntry.SmartValue = value;
				
				//	Draws Parameters.
				EditorGUILayout.Space();
				children.Get ("_pathParams").Draw();
				EditorGUILayout.Space();
				children.Get ("_queryParams").Draw();
				EditorGUILayout.Space();
				children.Get ("_headers").Draw();
				EditorGUILayout.Space();

				SirenixEditorGUI.BeginLegendBox ();
				EditorGUILayout.BeginHorizontal ();
				var drawContent =
					value.Method != AuthMethod.NONE &&
					value.Method != AuthMethod.INHERIT;
				
				_authDropDown = EditorGUILayout.Foldout (
					_authDropDown, "Auth", drawContent
						? EditorStyles.foldout
						: EditorStyles.label
				);
				children.Get ("_authMethod").Draw(null);
				EditorGUILayout.EndHorizontal ();
				
				if (_authDropDown && drawContent)
				{
					EditorGUI.indentLevel++;
					children.Get ("_isConstant").Draw ();

					if (value.IsConstant)
					{
						switch (value.Method)
						{
							case AuthMethod.BASIC:
								children.Get ("_basicAuth").Draw (null);
								break;
							case AuthMethod.API_KEY:
								children.Get ("_apiKeyAuth").Draw (null);
								break;
							case AuthMethod.BEARER_TOKEN:
								children.Get ("_bearerTokenAuth").Draw (null);
								break;
							case AuthMethod.NONE:
							case AuthMethod.INHERIT:
							// TODO: Implement missing authentications.
							case AuthMethod.DIGEST:
							case AuthMethod.OAUTH1:
							case AuthMethod.OAUTH2:
								break;
							default:
								throw new System.ArgumentOutOfRangeException ();
						}
					}
					EditorGUI.indentLevel--;
				}
				SirenixEditorGUI.EndLegendBox ();
				
			}
		}
#endif
		
		#endregion

	}
	
	[System.Serializable]
	public class Headers : SerializableDictionary<string, string> { }
	
}