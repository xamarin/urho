using System;
using Clang.Ast;
using System.IO;

namespace SharpieBinder
{
	
	class MainClass
	{
		const string Output = "../../bindings/generated";

		public static int Main (string[] args)
		{
			Directory.CreateDirectory (Output);
			Console.WriteLine(Environment.CurrentDirectory);

			if (System.Runtime.InteropServices.Marshal.SizeOf(typeof (IntPtr)) == 4) {
				Console.Error.WriteLine ("This needs a 64-bit Mono to run");
				return 1;
			}

			if (args.Length == 0) {
				args = new [] { "../../bindings/Urho.pch" };
			}

			var reader = new AstReader ();
			var binder = new CxxBinder(Output);
			var lookup = new ScanBaseTypes ();

			reader.TranslationUnitParsed += tu => { 
				tu.Accept(lookup);
				lookup.PrepareProperties();
				tu.Accept(binder);
				binder.GenerateProperties();
			};

			reader.Load (args [0]);
			binder.FixupOverrides();

			foreach (var st in binder.Generate()) {
				File.WriteAllText (Output + "/" + st.FileName, st.ToString ());
			}

			foreach (var a in binder.unhandledEnums)
				Console.WriteLine ("Missing special Enum processing for {0}", a);
			Console.WriteLine($"Dumped data into {Output}");
			binder.Close ();
			return 0;
		}
	}
}
