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

		/// <summary>
		/// Removes the "const" and "&" from a typename string definition 
		/// </summary>
		public static string DropConstAndReference(string tname)
		{
			if (tname.StartsWith("const"))
				tname = tname.Substring("const".Length);
			// strip the &
			tname = tname.Substring(0, tname.Length - 1);
			return tname.Trim();
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

		public static string PascalCase (this string w)
		{
			return string.Join ("", w.Split ('_').Select (x => Remap (Capitalize (x))));
		}

		public static string Capitalize (this string word)
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
					return char.ToUpper(word[0]) + word.Substring(1).ToLower();
				}
			}
			return char.ToUpper (word[0]).ToString ();
		}

		public static IEnumerable<string> GetMethodComments(CXXMethodDecl decl)
		{
			//NOTE: CLang.dll doesn't surface TextComment type so we have to parse them from Dump:
			var dumpLines = decl.DumpToString().Split(new [] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Where(l => l.Contains("-TextComment "));
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