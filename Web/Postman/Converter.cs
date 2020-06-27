using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// TODO: Simplify converters using wrapper interfaces.

namespace Postman.v2
{
	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings =
			new JsonSerializerSettings
			{
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters =
				{
					DescriptionConverter.Singleton,
					BoolWrapperConverter.Singleton,
					IntegerWrapperConverter.Singleton,
					StringFloatWrapperConverter.Singleton,
					AuthTypeConverter.Singleton,
					HostConverter.Singleton,
					UrlConverter.Singleton,
					UrlPathConverter.Singleton,
					PathElementConverter.Singleton,
					VariableTypeConverter.Singleton,
					CollectionVersionConverter.Singleton,
					RequestUnionConverter.Singleton,
					SrcConverter.Singleton,
					FormParameterTypeConverter.Singleton,
					ModeConverter.Singleton,
					HeaderUnionConverter.Singleton,
					ResponseConverter.Singleton,
					HeadersConverter.Singleton,
					HeaderElementConverter.Singleton,
					new IsoDateTimeConverter
					{
						DateTimeStyles =
							DateTimeStyles.AssumeUniversal
					}
				}
			};
	}

	internal class BoolWrapperConverter : JsonConverter
	{
		public static readonly BoolWrapperConverter Singleton =
			new BoolWrapperConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (BoolWrapper);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Boolean)
				return new BoolWrapper
				{
					Bool = serializer.Deserialize<bool> (reader)
				};

			throw new Exception ("Cannot unmarshal type Description");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (BoolWrapper) untypedValue;
			serializer.Serialize (writer, value.Bool);
		}
	}

	internal class IntegerWrapperConverter : JsonConverter
	{
		public static readonly IntegerWrapperConverter Singleton =
			new IntegerWrapperConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (IntegerWrapper);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Integer)
				return new IntegerWrapper
				{
					Integer = serializer.Deserialize<int> (reader)
				};

			throw new Exception ("Cannot unmarshal type Description");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (IntegerWrapper) untypedValue;
			serializer.Serialize (writer, value.Integer);
		}
	}

	internal class StringFloatWrapperConverter : JsonConverter
	{
		public static readonly StringFloatWrapperConverter Singleton =
			new StringFloatWrapperConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (StringFloatWrapper);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Float:
					return new StringFloatWrapper
					{
						Float = serializer.Deserialize<float> (reader)
					};
				case JsonToken.Date:
				case JsonToken.String:
					return new StringFloatWrapper
					{
						String = serializer.Deserialize<string> (reader)
					};
				default:
					throw new Exception ("Cannot unmarshal type Description");
			}
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (StringFloatWrapper) untypedValue;
			if (value.DataType == DataType.STRING)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.DataType == DataType.FLOAT)
				serializer.Serialize (writer, value.Float);
		}
	}

	internal class DescriptionConverter : JsonConverter
	{
		public static readonly DescriptionConverter Singleton =
			new DescriptionConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Description);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return new Description ();
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Description {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<DescriptionObject> (reader);
					return new Description {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type Description");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Description) untypedValue;
			if (value.IsNull)
			{
				serializer.Serialize (writer, null);
				return;
			}

			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Description");
		}
	}


	internal class AuthTypeConverter : JsonConverter
	{
		public static readonly AuthTypeConverter Singleton =
			new AuthTypeConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (AuthType);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Null) return AuthType.NULL;
			var value = serializer.Deserialize<string> (reader);
			switch (value)
			{
				// ReSharper disable once StringLiteralTypo
				case "apikey":
					return AuthType.API_KEY;
				// ReSharper disable once StringLiteralTypo
				case "awsv4":
					return AuthType.AWS_V4;
				case "basic":
					return AuthType.BASIC;
				case "bearer":
					return AuthType.BEARER;
				case "digest":
					return AuthType.DIGEST;
				case "hawk":
					return AuthType.HAWK;
				// ReSharper disable once StringLiteralTypo
				case "noauth":
					return AuthType.NO_AUTH;
				// ReSharper disable once StringLiteralTypo
				case "ntlm":
					return AuthType.NTLM;
				case "oauth1":
					return AuthType.OAUTH1;
				case "oauth2":
					return AuthType.OAUTH2;
			}

			throw new Exception ("Cannot unmarshal type AuthType");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			if (untypedValue == null)
			{
				serializer.Serialize (writer, null);
				return;
			}

			var value = (AuthType) untypedValue;
			switch (value)
			{
				case AuthType.API_KEY:
					// ReSharper disable once StringLiteralTypo
					serializer.Serialize (writer, "apikey");
					return;
				case AuthType.AWS_V4:
					// ReSharper disable once StringLiteralTypo
					serializer.Serialize (writer, "awsv4");
					return;
				case AuthType.BASIC:
					serializer.Serialize (writer, "basic");
					return;
				case AuthType.BEARER:
					serializer.Serialize (writer, "bearer");
					return;
				case AuthType.DIGEST:
					serializer.Serialize (writer, "digest");
					return;
				case AuthType.HAWK:
					serializer.Serialize (writer, "hawk");
					return;
				case AuthType.NO_AUTH:
					// ReSharper disable once StringLiteralTypo
					serializer.Serialize (writer, "noauth");
					return;
				case AuthType.NTLM:
					// ReSharper disable once StringLiteralTypo
					serializer.Serialize (writer, "ntlm");
					return;
				case AuthType.OAUTH1:
					serializer.Serialize (writer, "oauth1");
					return;
				case AuthType.OAUTH2:
					serializer.Serialize (writer, "oauth2");
					return;
				case AuthType.NULL:
					serializer.Serialize (writer, null);
					return;
			}

			throw new Exception ("Cannot marshal type AuthType");
		}
	}

	internal class HostConverter : JsonConverter
	{
		public static readonly HostConverter Singleton = new HostConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Host);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Host {String = stringValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.Deserialize<List<string>> (reader);
					return new Host {Object = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type Host");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Host) untypedValue;
			if (value.DataType == DataType.STRING)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.DataType == DataType.OBJECT)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Host");
		}
	}

	internal class UrlConverter : JsonConverter
	{
		public static readonly UrlConverter Singleton = new UrlConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Url);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Url {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<UrlObject> (reader);
					return new Url {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type Url");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Url) untypedValue;
			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Url");
		}
	}

	internal class UrlPathConverter : JsonConverter
	{
		public static readonly UrlPathConverter Singleton =
			new UrlPathConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (PathList);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new PathList {String = stringValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.Deserialize<List<Path>> (reader);
					return new PathList {Object = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type PathList");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (PathList) untypedValue;
			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type PathList");
		}
	}

	internal class PathElementConverter : JsonConverter
	{
		public static readonly PathElementConverter Singleton =
			new PathElementConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Path);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Path {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<PathObject> (reader);
					return new Path {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type PathList");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Path) untypedValue;
			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type PathList");
		}
	}

	internal class VariableTypeConverter : JsonConverter
	{
		public static readonly VariableTypeConverter Singleton =
			new VariableTypeConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (VariableType) || t == typeof (VariableType?);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string> (reader);
			switch (value)
			{
				case "any":
					return VariableType.ANY;
				case "boolean":
					return VariableType.BOOLEAN;
				case "number":
					return VariableType.NUMBER;
				case "string":
					return VariableType.STRING;
			}

			throw new Exception ("Cannot unmarshal type VariableType");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			if (untypedValue == null)
			{
				serializer.Serialize (writer, null);
				return;
			}

			var value = (VariableType) untypedValue;
			if (value == VariableType.ANY) serializer.Serialize (writer, "any");

			switch (value)
			{
				case VariableType.BOOLEAN:
					serializer.Serialize (writer, "boolean");
					return;
				case VariableType.NUMBER:
					serializer.Serialize (writer, "number");
					return;
				case VariableType.STRING:
					serializer.Serialize (writer, "string");
					return;
			}

			throw new Exception ("Cannot marshal type VariableType");
		}
	}

	internal class CollectionVersionConverter : JsonConverter
	{
		public static readonly CollectionVersionConverter Singleton =
			new CollectionVersionConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Version);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Version {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<VersionObject> (reader);
					return new Version {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type Version");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Version) untypedValue;
			if (value.DataType == DataType.STRING)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.DataType == DataType.OBJECT)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Version");
		}
	}

	internal class MinMaxLengthCheckConverter : JsonConverter
	{
		public static readonly MinMaxLengthCheckConverter Singleton =
			new MinMaxLengthCheckConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (string);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			var value = serializer.Deserialize<string> (reader);
			if (value != null && value.Length <= 10) return value;
			throw new Exception ("Cannot unmarshal type string");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (string) untypedValue;
			if (value.Length <= 10)
			{
				serializer.Serialize (writer, value);
				return;
			}

			throw new Exception ("Cannot marshal type string");
		}
	}

	internal class RequestUnionConverter : JsonConverter
	{
		public static readonly RequestUnionConverter Singleton =
			new RequestUnionConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Request);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Request {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<RequestObject> (reader);
					return new Request {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type Request");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Request) untypedValue;
			if (value.DataType == DataType.STRING)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.DataType == DataType.OBJECT)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Request");
		}
	}

	internal class SrcConverter : JsonConverter
	{
		public static readonly SrcConverter Singleton = new SrcConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Source);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return new Source ();
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Source {String = stringValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.Deserialize<List<string>> (reader);
					return new Source {Object = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type Source");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Source) untypedValue;
			if (value.IsNull)
			{
				serializer.Serialize (writer, null);
				return;
			}

			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Source");
		}
	}

	internal class FormParameterTypeConverter : JsonConverter
	{
		public static readonly FormParameterTypeConverter Singleton =
			new FormParameterTypeConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (FormParameterType) ||
				t == typeof (FormParameterType?);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string> (reader);
			switch (value)
			{
				case "file":
					return FormParameterType.FILE;
				case "text":
					return FormParameterType.TEXT;
			}

			throw new Exception ("Cannot unmarshal type FormParameterType");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			if (untypedValue == null)
			{
				serializer.Serialize (writer, null);
				return;
			}

			var value = (FormParameterType) untypedValue;
			switch (value)
			{
				case FormParameterType.FILE:
					serializer.Serialize (writer, "file");
					return;
				case FormParameterType.TEXT:
					serializer.Serialize (writer, "text");
					return;
			}

			throw new Exception ("Cannot marshal type FormParameterType");
		}
	}

	internal class ModeConverter : JsonConverter
	{
		public static readonly ModeConverter Singleton = new ModeConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Mode);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType == JsonToken.Null) return Mode.NONE;
			var value = serializer.Deserialize<string> (reader);
			switch (value)
			{
				case "file":
					return Mode.FILE;
				// ReSharper disable once StringLiteralTypo
				case "formdata":
					return Mode.FORM_DATA;
				case "graphql":
					return Mode.GRAPHQL;
				case "raw":
					return Mode.RAW;
				case "urlencoded":
					return Mode.URLENCODED;
			}

			throw new Exception ("Cannot unmarshal type Mode");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			if (untypedValue == null)
			{
				serializer.Serialize (writer, null);
				return;
			}

			var value = (Mode) untypedValue;
			switch (value)
			{
				case Mode.FILE:
					serializer.Serialize (writer, "file");
					return;
				case Mode.FORM_DATA:
					// ReSharper disable once StringLiteralTypo
					serializer.Serialize (writer, "formdata");
					return;
				case Mode.GRAPHQL:
					serializer.Serialize (writer, "graphql");
					return;
				case Mode.RAW:
					serializer.Serialize (writer, "raw");
					return;
				case Mode.URLENCODED:
					serializer.Serialize (writer, "urlencoded");
					return;
				case Mode.NONE:
					serializer.Serialize (writer, null);
					return;
			}

			throw new Exception ("Cannot marshal type Mode");
		}
	}

	internal class HeaderUnionConverter : JsonConverter
	{
		public static readonly HeaderUnionConverter Singleton =
			new HeaderUnionConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Header);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Header {String = stringValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.Deserialize<List<HeaderObject>> (reader);
					return new Header {Object = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type Header");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Header) untypedValue;
			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Header");
		}
	}

	internal class ResponseConverter : JsonConverter
	{
		public static readonly ResponseConverter Singleton =
			new ResponseConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Response);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return new Response ();
				case JsonToken.Integer:
					var integerValue = serializer.Deserialize<int> (reader);
					return new Response {Integer = integerValue};
				case JsonToken.Float:
					var doubleValue = serializer.Deserialize<float> (reader);
					return new Response {Float = doubleValue};
				case JsonToken.Boolean:
					var boolValue = serializer.Deserialize<bool> (reader);
					return new Response {Bool = boolValue};
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Response {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<ResponseClass> (reader);
					return new Response {Object = objectValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.
							Deserialize<List<ResponseClass>> (
								reader);
					return new Response {Array = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type Response");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Response) untypedValue;
			if (value.IsNull)
			{
				serializer.Serialize (writer, null);
				return;
			}

			if (value.DataType == DataType.INTEGER)
			{
				serializer.Serialize (writer, value.Integer);
				return;
			}

			if (value.DataType == DataType.FLOAT)
			{
				serializer.Serialize (writer, value.Float);
				return;
			}

			if (value.DataType == DataType.BOOL)
			{
				serializer.Serialize (writer, value.Bool);
				return;
			}

			if (value.DataType == DataType.STRING)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.DataType == DataType.ARRAY)
			{
				serializer.Serialize (writer, value.Array);
				return;
			}

			if (value.DataType == DataType.OBJECT)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Response");
		}
	}

	internal class HeadersConverter : JsonConverter
	{
		public static readonly HeadersConverter Singleton =
			new HeadersConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (Headers);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null:
					return new Headers ();
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new Headers {String = stringValue};
				case JsonToken.StartArray:
					var arrayValue =
						serializer.Deserialize<List<HeaderElement>> (reader);
					return new Headers {Object = arrayValue};
			}

			throw new Exception ("Cannot unmarshal type Headers");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (Headers) untypedValue;
			if (value.IsNull)
			{
				serializer.Serialize (writer, null);
				return;
			}

			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type Headers");
		}
	}

	internal class HeaderElementConverter : JsonConverter
	{
		public static readonly HeaderElementConverter Singleton =
			new HeaderElementConverter ();

		public override bool CanConvert (Type t)
		{
			return t == typeof (HeaderElement);
		}

		public override object ReadJson (
			JsonReader reader, Type t, object existingValue,
			JsonSerializer serializer
		)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string> (reader);
					return new HeaderElement {String = stringValue};
				case JsonToken.StartObject:
					var objectValue =
						serializer.Deserialize<HeaderObject> (reader);
					return new HeaderElement {Object = objectValue};
			}

			throw new Exception ("Cannot unmarshal type HeaderElement");
		}

		public override void WriteJson (
			JsonWriter writer, object untypedValue, JsonSerializer serializer
		)
		{
			var value = (HeaderElement) untypedValue;
			if (value.String != null)
			{
				serializer.Serialize (writer, value.String);
				return;
			}

			if (value.Object != null)
			{
				serializer.Serialize (writer, value.Object);
				return;
			}

			throw new Exception ("Cannot marshal type HeaderElement");
		}
	}
}