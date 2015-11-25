using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Urho;
using Type = System.Type;

namespace StructsValidator
{
	class Program
	{
		static string codeContent;
		const string Header = 

@"using namespace Urho3D;

void check_size(int actual, int expected, const char * typeName)
{
	if (actual != expected)
		printf(""sizeof(%s) is %d but %d is expected"", typeName, actual, expected);
}

void check_offset(int actual, int expected, const char* typeName, const char* fieldName)
{
	if (actual != expected)
		printf(""offset(%s, %s) is %d but %d is expected"", typeName, fieldName, actual, expected);
}

// TESTS ARE GENERATED FOR %ARCH%. MAKE SURE YOU USE THE SAME ARCHITECTURE WHILE RUNNING THESE TESTS!
void check_bindings_offsets()
{";


		static void Main(string[] args)
		{
			//get all structs from Urho.dll
			var structs = typeof (UrhoObject).Assembly.GetTypes().Where(t => t.IsValueType && !t.IsPrimitive && !t.IsEnum).ToArray();
			
			// only those with Sequential layout and Sizeof > 1 (not empty)
			var notEmptyStructs = structs.Where(t => t.IsLayoutSequential && Marshal.SizeOf(t) > 1).ToArray();

			AppendC(Header.Replace("%ARCH%", IntPtr.Size == 8 ? "64bit" : "32bit"));
			foreach (var type in notEmptyStructs)
				AddTest(type);
			AppendC("}");
			AppendC($"\n/* Empty structs (stubs?):\n\n  {string.Join("\n  ", structs.Where(t => Marshal.SizeOf(t) <= 1).Select(t => t.Name).ToArray())}\n\n*/");
			File.WriteAllText("../../../../bindings/src/asserts.h", codeContent);
		}

		static void AddTest(Type type)
		{
			var managedName = type.Name;
			var nativeName = ResolveUrhoType(managedName);
			if (nativeName == null)
				return;

			var size = Marshal.SizeOf(type);
			AppendC($"\n\t// {managedName}:");
			AppendC($"\tcheck_size(sizeof({nativeName}), {size}, \"{managedName}\");");
			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				var managedFieldName = field.Name;
				var nativeFieldName = ResolveUrhoTypeField(managedName, managedFieldName);
				var offset = Marshal.OffsetOf(type, managedFieldName);
				AppendC($"\tcheck_offset(offsetof({nativeName}, {nativeFieldName}), {offset}, \"{managedName}\", \"{managedFieldName}\");");
			}
		}

		static string ResolveUrhoTypeField(string typeName, string fieldName)
		{
			fieldName = fieldName.Trim('_');
			fieldName = char.ToLowerInvariant(fieldName[0]) + fieldName.Substring(1);

			if (fieldName.EndsWith("Ptr"))
				fieldName = fieldName.Remove(fieldName.Length - 3);

			if (fieldName.EndsWith("Id"))
				fieldName = fieldName.Remove(fieldName.Length - 2) + "ID";

			if (typeName == "BiasParameters" && fieldName == "slopeScaleBias") return "slopeScaledBias_";
			if (typeName == "AnimationTriggerPoint" && fieldName == "variant") return "data_";

			if (typeName != "CrowdObstacleAvoidanceParams")
				fieldName += "_";
			return fieldName;
		}

		static string ResolveUrhoType(string name)
		{
			string[] ignoredTypes = {
				//TODO: handle these types
				"nuint",
				"nint",
				"BezierConfig",
				"CollisionData",
				"StringHashRef",
				"StringHash",
				"Iterator",
				"AttributeInfo",
				"ProfilerBlock",
				"WeakPtr",
				"VectorBase",
				"RandomAccessIterator",
				"Matrix3",
				"Matrix4",
				"Matrix4d",
				"Quaterniond",
				"Quaternion",
				"Vector2d",
				"Vector3d",
				"Vector4d",
				"UrhoString",
				"CascadeParameters",
			};

			if (name.EndsWith("EventArgs") || ignoredTypes.Contains(name))
				return null;

			if (name == "UrhoString")
				return "String";

			return name;
		}

		static void AppendC(string line)
		{
			codeContent += line + "\n";
		}
	}
}
