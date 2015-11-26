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

		static void Main(string[] args)
		{
			//get all structs from Urho.dll
			var structs = typeof (UrhoObject).Assembly.GetTypes().Where(t => t.IsValueType && !t.IsPrimitive && !t.IsEnum).ToArray();
			
			// only those with Sequential layout and Sizeof > 1 (not empty)
			var notEmptyStructs = structs.Where(t => t.IsLayoutSequential && Marshal.SizeOf(t) > 1).ToArray();

			AppendC("using namespace Urho3D;\n\n");
			AppendC($"// TESTS ARE GENERATED FOR {(IntPtr.Size == 8 ? "64bit" : "32bit")}. MAKE SURE YOU USE THE SAME ARCHITECTURE WHILE RUNNING THESE TESTS!");
			AppendC( "// MAKE SURE YOU USE THE SAME ARCHITECTURE WHILE RUNNING THESE TESTS!");
			AppendC("void check_bindings_offsets()\n{");

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
			AppendC($"\tstatic_assert(sizeof({nativeName}) == {size}, \"{managedName} has wrong size ({size})\");");
			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				var managedFieldName = field.Name;
				var nativeFieldName = ResolveUrhoTypeField(managedName, managedFieldName);
				var offset = Marshal.OffsetOf(type, managedFieldName);
				AppendC($"\tstatic_assert(offsetof({nativeName}, {nativeFieldName}) == {offset}, \"{managedName}.{managedFieldName} has wrong offset ({offset})\");");
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
