using System;
using System.Linq;

namespace SharpieBinder
{
	public static class StringUtil {
		public static string DropPrefix (this string w)
		{
			var j = w.IndexOf ("_");
			var prefix = w.Substring (0, j+1);
			return w.DropPrefix (prefix);
		}

		public static string DropPrefix (this string w, string prefix)
		{
			if (w.StartsWith (prefix))
				return w.Substring (prefix.Length);
			Console.WriteLine ("Can not drop prefix {0} from {1}", prefix, w);
			return w;
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
			var elements = w.Split ('_');
			return string.Join ("", w.Split ('_').Select (x => Remap (Capitalize (x))));
		}

		public static string Capitalize (this string w)
		{
			if (w.Length == 0)
				return "";
			if (w.Length > 1)
				return Char.ToUpper (w [0]) + w.Substring (1).ToLower ();
			return Char.ToUpper (w[0]).ToString ();
		}
	}
}