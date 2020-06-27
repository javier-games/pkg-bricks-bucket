using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Postman.v2
{
	/// <summary>
	/// A variable may have multiple types. This field specifies the type of
	/// the variable.
	/// </summary>
	[System.Flags]
	public enum VariableType
	{
		BOOLEAN,
		NUMBER,
		STRING,
		ANY = BOOLEAN | NUMBER | STRING
	}
	
	/// <summary>
	/// Collection variables allow you to define a set of variables, that are
	/// a *part of the collection*, as opposed to environments, which are
	/// separate entities. *Note: Collection variables must not contain any
	/// sensitive information.* Using variables in your Postman requests
	/// eliminates the need to duplicate requests, which can save a lot of
	/// time. Variables can be defined, and referenced to from any part of a
	/// request.
	/// </summary>
	[System.Serializable]
	public class Variable
	{

		#region Fields

		[SerializeField]
		private string m_id;

		[SerializeField]
		private string m_key;

		[SerializeField]
		private SerializedObject m_value;

		[SerializeField]
		private VariableType m_type;

		[SerializeField]
		private string m_name;

		[SerializeField]
		private Description m_description;

		[SerializeField]
		private BoolWrapper m_system;

		[SerializeField]
		private BoolWrapper m_disabled;

		#endregion

		#region Properties

		/// <summary>
		/// A variable ID is a unique user-defined value that identifies the
		/// variable within a collection. In traditional terms, this would be
		/// a variable name.
		/// </summary>
		[JsonProperty ("id", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Id
		{
			get => m_id;
			set => m_id = value;
		}

		/// <summary>
		/// Variable name
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

		/// <summary>
		/// A variable may have multiple types. This field specifies the type
		/// of the variable.
		/// </summary>
		[JsonProperty ("type", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public VariableType Type
		{
			get => m_type;
			set => m_type = value;
		}

		/// <summary>
		/// A variable key is a human friendly value that identifies the
		/// variable within a collection. In traditional terms, this would be a
		/// variable name.
		/// </summary>
		[JsonProperty ("key", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public string Key
		{
			get => m_key;
			set => m_key = value;
		}

		/// <summary>
		/// The value that a variable holds in this collection. Ultimately,
		/// the variables will be replaced by this value, when say running a
		/// set of requests from a collection
		/// </summary>
		[JsonProperty ("value")]
		public SerializedObject Value
		{
			get => m_value;
			set => m_value = value;
		}

		[JsonProperty ("disabled", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper Disabled
		{
			get => m_disabled;
			set => m_disabled = value;
		}

		/// <summary>
		/// When set to true, indicates that this variable has been set by
		/// Postman
		/// </summary>
		[JsonProperty ("system", Required = Required.DisallowNull,
			NullValueHandling = NullValueHandling.Ignore)]
		public BoolWrapper System
		{
			get => m_system;
			set => m_system = value;
		}

		#endregion
	}
	
	[System.Serializable]
	public class SerializedObject : GenericWrapper<SerializedObject>
	{
		public static implicit operator SerializedObject (bool value) =>
			new SerializedObject {Bool = value};

		public static implicit operator SerializedObject (int value) =>
			new SerializedObject {Integer = value};

		public static implicit operator SerializedObject (float value) =>
			new SerializedObject {Float = value};

		public static implicit operator SerializedObject (string value) =>
			new SerializedObject {String = value};
		
		public static implicit operator SerializedObject (
			List<SerializedObject> list
		) =>
			new SerializedObject {Array = list};
	}
}