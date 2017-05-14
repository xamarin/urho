using System;
using System.Collections.Generic;
using System.Linq;
using Clang.Ast;

namespace SharpieBinder
{
	public static class StringUtil
	{
		public static string DropPrefix (this string w)
		{
			var j = w.IndexOf ("_", StringComparison.Ordinal);
			if (j < 0 || j == w.Length - 1)
				return w;
			return w.Substring(j + 1);
		}

		public static string SafeParamName(string paramName)
		{
			//some C#'s keywords which can be potentially used as arguments
			string[] csharpKeywords = { "object", "event", "string", "operator", "fixed", "ref", "case", "default", "lock", "unchecked" };
			if (csharpKeywords.Contains(paramName))
				return "@" + paramName;
			return paramName;
		}

		/// <summary>
		/// Removes the "const" and "&" from a typename string definition 
		/// </summary>
		public static string DropConstAndReference(this string tname)
		{
			tname = DropConst(tname);
			// strip the &
			if (tname.EndsWith("&"))
				tname = tname.Substring(0, tname.Length - 1);
			return tname.Trim();
		}

		public static string DropConst(this string str)
		{
			return str.Replace("const ", "");
		}

		public static string ExtractGenericParameter(this string str)
		{
			var open = str.IndexOf("<");
			var close = str.LastIndexOf(">");
			if (open < 0 || close < 0 || close <= open)
				return str;
			return str.Substring(open + 1, close - open - 1);
		}

		public static string DropClassOrStructPrefix(this string str)
		{
			if (str.StartsWith("class "))
				return str.Substring("class ".Length);
			if (str.StartsWith("enum "))
				return str.Substring("enum ".Length);
			if (str.StartsWith("struct "))
				return str.Substring("struct ".Length);
			return str;
		}

		public static string DropUrhoNamespace(this string str)
		{
			if (str.StartsWith("Urho3D::"))
				return str.Substring("Urho3D::".Length);
			return str;
		}

		public static string Remap (string source)
		{
			switch (source) {
				case "TraversingLink":
					return "TraversingLink";
				case "Conetwist":
					return "ConeTwist";
				case "Waitingforqueue":
					return "WaitingForQueue";
				case "Waitingforpath":
					return "WaitingForPath";
				case "Lookat":
					return "LookAt";
				case "Readwrite":
					return "ReadWrite";
				case "Notfocusable":
					return "NotFocusable";
				case "Resetfocus":
					return "ResetFocus";
				case "Premulalpha":
					return "PremultipliedAlpha";
				case "Subtractalpha":
					return "SubtractAlpha";
				case "Invdestalpha":
					return "InvDestAlpha";
				case "Notequal":
					return "NotEqual";
				case "Lessequal":
					return "LessEqual";
				case "Greaterequal":
					return "GreaterEqual";
				case "Bottomleft":
					return "BottomLeft";
				case "Topleft":
					return "TopLeft";
				case "Topright":
					return "Topright";
				case "Bottomright":
					return "BottomRight";
				case "Horizontalnvidia":
					return "HorizontalNvidia";
				case "Horizontalcross":
					return "HorizontalCross";
				case "Verticalcross":
					return "VerticalCross";
			}
			return source;
		}


		public static string RemapEnumName(string type, string value)
		{
			if (value.StartsWith("MAX_"))
				return value.PascalCase();

			switch (type)
			{
				case "InterpolationMode":
					return value.PascalCase();
				case "PrimitiveType":
					return value.Replace("Prim_", "").PascalCase(); //there are more than one enum with this name
				case "Orientation2D":
				case "Orientation":
					return value.Replace("O_", "").PascalCase(); //there are more than one enum with this name
				case "EmitterTypeGravity":
				case "EmitterType2D":
				case "CrowdAgentTargetState":
				case "NavmeshPartitionType":
					return value.DropPrefix().DropPrefix().PascalCase();
				case "ShaderType":
					if (value.Length < 3)
						return value.ToUpper();
					goto default;
				default:
					return value.DropPrefix().PascalCase();
			}
		}

		public static string RemapAcronyms(this string source)
		{
			if (string.IsNullOrEmpty(source))
				return source;
			var map = new Dictionary<string, string> { {"XML", "Xml"}, {"JSON", "Json"} };
			return map.Aggregate(source, (current, mapItem) => current.Replace(mapItem.Key, mapItem.Value));
		}

		public static string PascalCase (this string w)
		{
			return string.Join ("", w.Split ('_').Select (x => Remap (Capitalize (x))));
		}

		public static string Capitalize (this string word, bool restLower = true)
		{
			if (string.IsNullOrEmpty(word))
				return string.Empty;

			if (word.Length > 1)
			{
				if (char.IsDigit(word[0]))
				{
					string digits = "";
					foreach (char symbol in word)
					{
						if (char.IsDigit(symbol))
							digits += symbol;
						else break;
					}
					string result = "N" + digits;
					if (result.Length < word.Length)
					{
						result += Capitalize(word.Substring(digits.Length));
					}
					return result;
				}
				else
				{
					var r = word.Substring(1);
					return char.ToUpper(word[0]) + (restLower ? r.ToLower() : r);
				}
			}
			return char.ToUpper (word[0]).ToString ();
		}

		public static IEnumerable<string> GetMethodComments(CXXMethodDecl decl)
		{
			return ExtractTextComments(decl.DumpToString());
		}

		public static IEnumerable<string> GetTypeComments(EnumDecl decl)
		{
			return ExtractTextComments(decl.DumpToString()).Take(1);
		}

		public static IEnumerable<string> GetTypeComments(CXXRecordDecl decl)
		{
			return ExtractTextComments(decl.DumpToString()).Take(1);
		}

		static IEnumerable<string> ExtractTextComments(string dump)
		{
			//workaround since TextComment type is not surfaced in Clang.dll yet
			var dumpLines = dump.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Where(l => l.Contains("-TextComment "));
			foreach (var dumpLine in dumpLines)
			{
				int start = dumpLine.IndexOf("\"");
				int end = dumpLine.LastIndexOf("\"");
				if (start > 0 && end > 0)
					yield return dumpLine.Substring(start + 1, end - start - 1);
			}
		}
	}
}