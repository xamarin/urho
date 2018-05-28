using System;

namespace Urho.Resources
{
	public interface IComponentSerializer
	{
		void Serialize(string key, object value);
	}

	public interface IComponentDeserializer
	{
		T Deserialize<T>(string key);
	}

	/// <summary>
	/// XMLElement based implementation of IComponentSerializer
	/// </summary>
	public class XmlComponentSerializer : IComponentSerializer, IComponentDeserializer
	{
		readonly XmlElement xmlElement;

		public XmlComponentSerializer(XmlElement xmlElement)
		{
			this.xmlElement = xmlElement;
		}

		public void Serialize(string key, object value)
		{
			if (value == null)
				return;

			if (value is string)
				xmlElement.SetString(key, (string)value);
			else if (value is Vector2)
				xmlElement.SetVector2(key, (Vector2)value);
			else if (value is Vector3)
				xmlElement.SetVector3(key, (Vector3)value);
			else if (value is Vector4)
				xmlElement.SetVector4(key, (Vector4)value);
			else if (value is IntRect)
				xmlElement.SetIntRect(key, (IntRect)value);
			else if (value is Quaternion)
				xmlElement.SetQuaternion(key, (Quaternion)value);
			else if (value is Color)
				xmlElement.SetColor(key, (Color)value);
			else if (value is float)
				xmlElement.SetFloat(key, (float)value);
			else if (value is int)
				xmlElement.SetInt(key, (int)value);
			else if (value is uint)
				xmlElement.SetUInt(key, (uint)value);
			else if (value is bool)
				xmlElement.SetBool(key, (bool)value);
			else if (value is double)
				xmlElement.SetDouble(key, (double)value);
			else throw new NotSupportedException($"{value.GetType().Name} is not supported."); 
		}

		public T Deserialize<T>(string key)
		{
			var type = typeof (T);
			if (type == typeof (string))
				return (T)(object) xmlElement.GetAttribute(key);
			else if (type == typeof (Vector2))
				return (T) (object) xmlElement.GetVector2(key);
			else if (type == typeof (Vector3))
				return (T) (object) xmlElement.GetVector3(key);
			else if (type == typeof (Vector4))
				return (T) (object) xmlElement.GetVector4(key);
			else if (type == typeof (Quaternion))
				return (T) (object) xmlElement.GetQuaternion(key);
			else if (type == typeof (Color))
				return (T) (object) xmlElement.GetColor(key);
			else if (type == typeof (float))
				return (T) (object) xmlElement.GetFloat(key);
			else if (type == typeof (double))
				return (T) (object) xmlElement.GetDouble(key);
			else if (type == typeof (int))
				return (T) (object) xmlElement.GetInt(key);
			else if (type == typeof (uint))
				return (T) (object) xmlElement.GetUInt(key);
			else if (type == typeof (bool))
				return (T) (object) xmlElement.GetBool(key);
			else throw new NotSupportedException($"{type.Name} is not supported."); 
		}
	}

	//TODO: binary serializer
}
