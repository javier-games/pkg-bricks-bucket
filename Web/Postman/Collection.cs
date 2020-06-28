using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using BricksBucket.Core.Collections;

namespace Postman.v2
{
	[System.Serializable]
	public class Collection
	{
		#region Fields

		[SerializeField]
		private Information m_info;

		[SerializeField]
		private List<Items> m_item;

		[SerializeField]
		private List<Event> m_event;
		
		[SerializeField]
		private List<Variable> m_variable;

		[SerializeField]
		private Auth m_auth;

		[SerializeField]
		public ProtocolProfileBehavior m_protocolProfileBehavior;

		#endregion

		#region Properties

		[JsonProperty ("info", Required = Required.Always)]
		public Information Info
		{
			get => m_info;
			set => m_info = value;
		}

		/// <summary>
		/// Items are the basic unit for a Postman collection. You can think of
		/// them as corresponding to a single API endpoint. Each Item has one
		/// request and may have multiple API responses associated with it.
		/// </summary>
		[JsonProperty ("item", Required = Required.Always)]
		public List<Items> Item
		{
			get => m_item;
			set => m_item = value;
		}

		[JsonProperty ("auth")]
		public Auth Auth
		{
			get => m_auth;
			set => m_auth = value;
		}

		[JsonProperty ("variable", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public List<Variable> Variable
		{
			get => m_variable;
			set => m_variable = value;
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

		#endregion

		#region Methods

		public static Collection FromJson (string json)
		{
			return JsonConvert.DeserializeObject<Collection> (json,
				Converter.Settings);
		}

		#endregion
	}
	
	/// <summary>
	/// Detailed description of the info block
	/// </summary>
	[System.Serializable]
	public class Information
	{
		#region Fields

		[SerializeField]
		private string m_name;

		[SerializeField]
		private string m_postmanId;

		[SerializeField]
		private Description m_description;

		[SerializeField]
		private Version m_version;

		[SerializeField]
		private string m_schema;

		#endregion

		#region Properties

		/// <summary>
		/// This should ideally hold a link to the Postman schema that is
		/// used to validate this collection. E.g:
		/// https://schema.getpostman.com/collection/v1
		/// </summary>
		[JsonProperty ("schema", Required = Required.Always)]
		public string Schema
		{
			get => m_schema;
			set => m_schema = value;
		}

		/// <summary>
		/// Every collection is identified by the unique value of this field.
		/// The value of this field is usually easiest to generate using a UID
		/// generator function. If you already have a collection, it is
		/// recommended that you maintain the same id since changing the id
		/// usually implies that is a different collection than it was
		/// originally. *Note: This field exists for compatibility reasons with
		/// Collection Format V1.*
		/// </summary>
		[JsonProperty ("_postman_id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string PostmanId
		{
			get => m_postmanId;
			set => m_postmanId = value;
		}

		/// <summary>
		/// A collection's friendly name is defined by this field. You would
		/// want to set this field to a value that would allow you to easily
		/// identify this collection among a bunch of other collections, as
		/// such outlining its usage or content.
		/// </summary>
		[JsonProperty ("name", Required = Required.Always)]
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

		[JsonProperty ("version", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public Version Version
		{
			get => m_version;
			set => m_version = value;
		}

		#endregion
	}
	
	/// <summary>
	/// Postman allows you to version your collections as they grow, and this
	/// field holds the version number. While optional, it is recommended that
	/// you use this field to its fullest extent!
	/// </summary>
	[System.Serializable]
	public class Version : StringObjectWrapper<VersionObject>
	{
		public static implicit operator Version (VersionObject obj) =>
			new Version {Object = obj};

		public static implicit operator Version (string s) =>
			new Version {String = s};
	}
	
	[System.Serializable]
	public class VersionObject
	{
		#region Fields

		[SerializeField]
		private int m_major;

		[SerializeField]
		private int m_minor;

		[SerializeField]
		private int m_patch;

		[SerializeField]
		private string m_identifier;

		[SerializeField]
		private SerializedObject m_meta;

		#endregion

		#region Properties

		/// <summary>
		/// Increment this number if you make changes to the collection that
		/// changes its behaviour. E.g: Removing or adding new test scripts.
		/// (partly or completely).
		/// </summary>
		[JsonProperty ("major", Required = Required.Always)]
		public int Major
		{
			get => m_major;
			set => m_major = value;
		}

		/// <summary>
		/// You should increment this number if you make changes that will
		/// not break anything that uses the collection. E.g: removing a folder.
		/// </summary>
		[JsonProperty ("minor", Required = Required.Always)]
		public int Minor
		{
			get => m_minor;
			set => m_minor = value;
		}

		/// <summary>
		/// Ideally, minor changes to a collection should result in the
		/// increment of this number.
		/// </summary>
		[JsonProperty ("patch", Required = Required.Always)]
		public int Patch
		{
			get => m_patch;
			set => m_patch = value;
		}

		/// <summary>
		/// A human friendly identifier to make sense of the version numbers.
		/// E.g: 'beta-3'
		/// </summary>
		[JsonProperty ("identifier", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore),
		JsonConverter (typeof (MinMaxLengthCheckConverter))]
		public string Identifier
		{
			get => m_identifier;
			set => m_identifier = value;
		}

		[JsonProperty ("meta")]
		public SerializedObject Meta
		{
			get => m_meta;
			set => m_meta = value;
		}

		#endregion
	}
	
	[System.Serializable]
	// ReSharper disable once ClassNeverInstantiated.Global
	public class ProtocolProfileBehavior :
		SerializableDictionary<string, SerializedObject> { }
	
}