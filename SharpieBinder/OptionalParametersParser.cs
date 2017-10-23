using System;
using System.Globalization;
using System.Linq;
using Clang.Ast;
using ICSharpCode.NRefactory.CSharp;

namespace SharpieBinder
{
	public static class OptionalParametersParser
	{
		static readonly string[] MethodsToIgnore = {
			"SetTriangleMesh",
			"SetLayout",
			"DefineSprite",
			"SetConvexHull",
			"SetGImpactMesh"
		};

		public static Expression Parse(ParmVarDecl param, CSharpParser parser)
		{
			string dump = param.DumpToString();
			var lines = dump.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length < 2 || MethodsToIgnore.Contains(((CXXMethodDecl)param.DeclContext).Name))
				return null;

			var defaultValueLine = lines.Last().TrimStart(' ', '-', '\t', '`');
			var words = defaultValueLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			var expressionType = words[0];

			string defaultValue;
			Expression expression = null;

			if (expressionType.Contains("Literal")) //e.g. CXXBoolLiteralExpr, IntegerLiteral, etc
			{
				defaultValue = words.Last();
				if (expressionType.StartsWith("Floating")) //numbers come in a format like '1.000000e-01'
					defaultValue = float.Parse(defaultValue, CultureInfo.InvariantCulture).ToString() + "f";
				if (defaultValue == "0" && dump.Contains("NullToPointer"))
					defaultValue = "null";
				if (defaultValue == "0f" && dump.Contains("Urho3D::Color"))
					defaultValue = "default(Urho.Color)";
				if (defaultValue == "'nullptr_t'")
					defaultValue = "null";

				expression = parser.ParseExpression(defaultValue);
			}
			else if (expressionType == "DeclRefExpr")
			{
				var items = defaultValueLine
					.Split(new[] { "'" }, StringSplitOptions.RemoveEmptyEntries)
					.Where(i => !string.IsNullOrWhiteSpace(i))
					.ToArray();
				defaultValue = $"{items[items.Length - 2]}";

				var type = items.Last();
				var clearType = type.DropConst().DropClassOrStructPrefix().DropUrhoNamespace();
				bool isEnum = type.Contains("enum ");

				expression = parser.ParseExpression(RemapValue(clearType, isEnum, defaultValue));
			}

			return expression;
		}

		static string RemapValue(string type, bool isEnum, string value)
		{
			if (value == "M_MAX_UNSIGNED") return "uint.MaxValue";
			if (value == "M_MIN_UNSIGNED") return "0";
			if (type == "String" && value == "EMPTY") return "\"\"";
			if (!isEnum)
				return "";

			return type + "." + StringUtil.RemapEnumName(type, value);
		}
	}
}
