using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	//	TODO: Split Items and Folders.
	/// <summary>
	/// Items are entities which contain an actual HTTP request, and sample
	/// responses attached to it. One of the primary goals of Postman is to
	/// organize the development of APIs. To this end, it is necessary to be
	/// able to group requests together. This can be achieved using 'Folders'.
	/// A folder just is an ordered set of requests.
	/// </summary>
	[System.Serializable]
	public class Items
	{
		#region Fields

		[SerializeField]
		private string m_id;

		[SerializeField]
		private string m_name;

		[SerializeField]
		private Description m_description;

		[SerializeField]
		private List<Variable> m_variable;

		[SerializeField]
		private List<Items> m_item;

		[SerializeField]
		private List<Event> m_event;

		[SerializeField]
		private Request m_request;

		[SerializeField]
		private List<Response> m_response;

		[SerializeField]
		private Auth m_auth;

		[SerializeField]
		private ProtocolProfileBehavior m_protocolProfileBehavior;

		#endregion

		#region Properties

		/// <summary>
		/// A unique ID that is used to identify collections internally
		/// </summary>
		[JsonProperty ("id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get => m_id;
			set => m_id = value;
		}

		/// <summary>
		/// A human readable identifier for the current item. A folder's
		/// friendly name is defined by this field. You would want to set this
		/// field to a value that would allow you to easily identify this
		/// folder.
		/// </summary>
		[JsonProperty ("name", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Name
		{
			get => m_name;
			set => m_name = value;
		}

		[JsonProperty ("description")]
		public Description Description
		{
			get => m_description;
			set => m_description = value;
		}

		[JsonProperty ("variable", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Variable> Variable
		{
			get => m_variable;
			set => m_variable = value;
		}

		[JsonProperty ("request", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Request Request
		{
			get => m_request;
			set => m_request = value;
		}

		[JsonProperty ("response", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Response> Response
		{
			get => m_response;
			set => m_response = value;
		}

		[JsonProperty ("auth")]
		public Auth Auth
		{
			get => m_auth;
			set => m_auth = value;
		}

		[JsonProperty ("event", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Event> Event
		{
			get => m_event;
			set => m_event = value;
		}

		[JsonProperty ("protocolProfileBehavior",
			Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public ProtocolProfileBehavior ProtocolProfileBehavior
		{
			get => m_protocolProfileBehavior;
			set => m_protocolProfileBehavior = value;
		}

		/// <summary>
		/// Items are entities which contain an actual HTTP request, and sample
		/// responses attached to it. Folders may contain many items.
		/// </summary>
		[JsonProperty ("item", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Items> Item
		{
			get => m_item;
			set => m_item = value;
		}

		#endregion
	}
	
	/// <summary>
	/// Postman allows you to configure scripts to run when specific events
	/// occur. These scripts are stored here, and can be referenced in the
	/// collection by their ID. Defines a script associated with an associated
	/// event name
	/// </summary>
	[System.Serializable]
	public class Event
	{

		#region Fields

		[SerializeField]
		private string m_id;

		[SerializeField]
		private string m_listen;

		[SerializeField]
		private Script m_script;

		[SerializeField]
		private BoolWrapper m_disabled;

		#endregion

		#region Properties


		/// <summary>
		/// Indicates whether the event is disabled. If absent, the event is
		/// assumed to be enabled.
		/// </summary>
		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		/// <summary>
		/// A unique identifier for the enclosing event.
		/// </summary>
		[JsonProperty ("id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get => m_id;
			set => m_id = value;
		}

		// ReSharper disable once CommentTypo
		/// <summary>
		/// Can be set to `test` or `prerequest` for test scripts or
		/// pre-request scripts respectively.
		/// </summary>
		[JsonProperty ("listen", Required = Required.Always)]
		public string Listen
		{
			get => m_listen;
			set => m_listen = value;
		}

		[JsonProperty ("script", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Script Script
		{
			get => m_script;
			set => m_script = value;
		}

		#endregion

	}
	
	/// <summary>
	/// A script is a snippet of Javascript code that can be used to to
	/// perform setup or teardown operations on a particular response.
	/// </summary>
	[System.Serializable]
	public class Script
	{

		#region Fields

		[SerializeField]
		private string m_id;

		[SerializeField]
		private string m_type;

		[SerializeField]
		private Host m_exec;

		[SerializeField]
		private Url m_src;

		[SerializeField]
		private string m_name;


		#endregion

		#region Properties


		[JsonProperty ("exec", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Host Exec
		{
			get => m_exec;
			set => m_exec = value;
		}

		/// <summary>
		/// A unique, user defined identifier that can  be used to refer to
		/// this script from requests.
		/// </summary>
		[JsonProperty ("id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get => m_id;
			set => m_id = value;
		}

		/// <summary>
		/// Script name
		/// </summary>
		[JsonProperty ("name", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Name
		{
			get => m_name;
			set => m_name = value;
		}

		[JsonProperty ("src", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Url Src
		{
			get => m_src;
			set => m_src = value;
		}

		/// <summary>
		/// Type of the script. E.g: 'text/javascript'
		/// </summary>
		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Type
		{
			get => m_type;
			set => m_type = value;
		}


		#endregion
	}
}