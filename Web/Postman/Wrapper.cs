using System.Collections.Generic;
using UnityEngine;
// ReSharper disable RedundantAssignment

namespace Postman.v2
{
	public enum DataType
	{
		NULL,
		BOOL,
		INTEGER,
		FLOAT,
		LONG,
		STRING,
		OBJECT,
		ARRAY
	}

	public interface IWrapper
	{
		DataType DataType { get; }
		bool IsNull { get; }
	}

	public interface IBoolWrapper : IWrapper
	{
		bool? Bool { get; set; }
	}

	public interface IIntegerWrapper : IWrapper
	{
		int? Integer { get; set; }
	}

	public interface IFloatWrapper : IWrapper
	{
		float? Float { get; set; }
	}

	public interface IStringWrapper : IWrapper
	{
		string String { get; set; }
	}

	public interface IObjectWrapper<T> : IWrapper
	{
		T Object { get; set; }
	}

	public interface IArrayWrapper<T> : IWrapper
	{
		List<T> Array { get; set; }
	}

	public abstract class Wrapper : IWrapper
	{
		#region Properties

		public virtual DataType DataType { get; protected set; }
		public virtual bool IsNull => DataType == DataType.NULL;

		#endregion

		#region Methods

		protected T? GetValue<T> (ref T value) where T : struct
		{
			if (DataType == DataType.NULL) return null;
			return value;
		}

		protected T GetValue<T> (T value) where T : class =>
			DataType == DataType.NULL ? null : value;

		protected void SetValue<T> (ref T variable, ref T? value, DataType type)
		where T : struct
		{
			if (value == null)
			{
				DataType = DataType.NULL;
				variable = default;
			}
			else
			{
				DataType = type;
				variable = value.Value;
			}
		}

		protected void SetValue<T> (ref T variable, T value, DataType type)
		where T : class
		{
			DataType = value == null ? DataType.NULL : type;
			variable = value;
		}

		#endregion
	}

	[System.Serializable]
	public class BoolWrapper : Wrapper, IBoolWrapper
	{
		#region Fields

		[SerializeField]
		private DataType m_dataType;

		[SerializeField]
		private bool m_bool;

		#endregion

		#region Properties

		public override DataType DataType
		{
			get => m_dataType;
			protected set => m_dataType = value;
		}

		public bool? Bool
		{
			get => GetValue (ref m_bool);
			set => SetValue (ref m_bool, ref value, DataType.BOOL);
		}

		#endregion
	}

	[System.Serializable]
	public class IntegerWrapper : Wrapper, IIntegerWrapper
	{
		#region Fields

		[SerializeField]
		private DataType m_dataType;

		[SerializeField]
		private int m_integer;

		#endregion

		#region Properties

		public int? Integer
		{
			get => GetValue (ref m_integer);
			set => SetValue (ref m_integer, ref value, DataType.INTEGER);
		}

		public override DataType DataType
		{
			get => m_dataType;
			protected set => m_dataType = value;
		}

		#endregion
	}

	[System.Serializable]
	public class StringFloatWrapper :
		Wrapper, IStringWrapper, IFloatWrapper
	{
		#region Fields

		[SerializeField]
		protected DataType m_dataType;

		[SerializeField]
		protected string m_string;

		[SerializeField]
		protected float m_float;

		#endregion

		#region Properties

		public override DataType DataType
		{
			get => m_dataType;
			protected set => m_dataType = value;
		}

		public float? Float
		{
			get => GetValue (ref m_float);
			set => SetValue (ref m_float, ref value, DataType.FLOAT);
		}

		public string String
		{
			get => GetValue (m_string);
			set => SetValue (ref m_string, value, DataType.STRING);
		}

		#endregion

		#region Methods

		public static implicit operator StringFloatWrapper (float f) =>
			new StringFloatWrapper {Float = f};

		public static implicit operator StringFloatWrapper (string s) =>
			new StringFloatWrapper {String = s};

		#endregion
	}

	[System.Serializable]
	public abstract class StringObjectWrapper<T> :
		Wrapper, IStringWrapper, IObjectWrapper<T>
	where T : class
	{
		#region Fields

		[SerializeField]
		protected DataType m_dataType;

		[SerializeField]
		protected string m_string;

		[SerializeField]
		protected T m_object;

		#endregion

		#region Properties

		public override DataType DataType
		{
			get => m_dataType;
			protected set => m_dataType = value;
		}

		public string String
		{
			get => GetValue (m_string);
			set => SetValue (ref m_string, value, DataType.STRING);
		}

		public T Object
		{
			get => GetValue (m_object);
			set => SetValue (ref m_object, value, DataType.OBJECT);
		}

		#endregion
	}

	[System.Serializable]
	public abstract class GenericWrapper<T> :
		Wrapper, IBoolWrapper, IIntegerWrapper, IFloatWrapper, IStringWrapper,
		IObjectWrapper<T>, IArrayWrapper<T>
	where T : class
	{

		[SerializeField]
		protected DataType m_dataType;

		[SerializeField]
		protected bool m_bool;

		[SerializeField]
		protected int m_integer;

		[SerializeField]
		protected float m_float;

		[SerializeField]
		protected string m_string;

		[SerializeField]
		protected T m_object;

		[SerializeField]
		protected List<T> m_list;

		public override DataType DataType
		{
			get => m_dataType;
			protected set => m_dataType = value;
		}

		public bool? Bool
		{
			get => GetValue (ref m_bool);
			set => SetValue (ref m_bool, ref value, DataType.BOOL);
		}

		public int? Integer
		{
			get => GetValue (ref m_integer);
			set => SetValue (ref m_integer, ref value, DataType.INTEGER);
		}

		public float? Float
		{
			get => GetValue (ref m_float);
			set => SetValue (ref m_float, ref value, DataType.FLOAT);
		}

		public string String
		{
			get => GetValue (m_string);
			set => SetValue (ref m_string, value, DataType.STRING);
		}

		public T Object
		{
			get => GetValue (m_object);
			set => SetValue (ref m_object, value, DataType.OBJECT);
		}

		public List<T> Array
		{
			get => GetValue (m_list);
			set => SetValue (ref m_list, value, DataType.ARRAY);
		}
	}
}