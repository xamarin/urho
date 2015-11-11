namespace Urho
{
	public interface IComponentSerializer
	{
		void Serialize(string key, object value);

		bool Deserialize<T>(string key, out T value);
	}

	/// <summary>
	/// XMLElement based impl of IComponentSerializer
	/// </summary>
	public class XmlComponentSerializer : IComponentSerializer
	{
		readonly XMLElement xmlElement;

		public XmlComponentSerializer(XMLElement xmlElement)
		{
			this.xmlElement = xmlElement;
		}

		public void Serialize(string key, object value)
		{
			//TODO: handle types of value, e.g.:
			//xmlElement.SetFloat(key, (float) value);
		}

		public bool Deserialize<T>(string key, out T value)
		{
			//TODO: handle types of value
			value = default(T);
			return false;
		}
	}

	//TODO: binary serializer
}
